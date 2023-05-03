using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CollegeStatictics.ViewModels.Base
{
    public abstract partial class ItemDialog<T> : ObservableValidator where T : class, ITable
    {
        #region [ Commands ]

        [RelayCommand(CanExecute = nameof(CanSave))]
        public void Save()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            if (Item.Id == 0)
                DatabaseContext.Entities.Set<T>().Local.Add(Item);

            DatabaseContext.Entities.SaveChanges();
        }
        
        private bool CanSave() => !HasErrors;

        [RelayCommand]
        private void Cancel() => DatabaseContext.CancelChanges(Item);

        #endregion

        #region [ Properties ]

        public T Item { get; }

        public IEnumerable<FrameworkElement> ViewElements => CreateViewElements();

        #endregion

        #region [ Initializing ]

        public ItemDialog(T? item)
        {
            Item = item ?? CreateDefaultItem();
        }

        #endregion

        #region [ Private methods ]

        private IEnumerable<FrameworkElement> CreateViewElements()
        {
            var formElements = from property in GetType().GetProperties().Reverse()
                               let attribute = property.GetCustomAttribute<FormElementAttribute>()
                               where attribute != null
                               select (property, attribute);

            foreach (var formElement in formElements)
            {
                FrameworkElement frameworkElement = formElement.attribute.ElementType switch
                {
                    ElementType.TextBox => CreateTextBox(formElement),
                    ElementType.EntitySelectorBox => CreateEntitySelectorBox(formElement),
                    ElementType.RadioButton => CreateRadioButtonList(formElement),
                    ElementType.SelectableSubtable => CreateSelectableSubtableElement(formElement),
                    ElementType.Subtable => CreateSubtableElement(formElement),
                    ElementType.Timetable => CreateTimetableElement(formElement),
                    ElementType.NumberBox => CreateNumberBox(formElement),
                    ElementType.SpinBox => CreateSpinBox(formElement),
                    ElementType.DatePicker => CreateDatePicker(formElement),
                    _ => throw new NotSupportedException("Invalid element type")
                };

                frameworkElement.MinWidth = formElement.property.GetCustomAttribute<MinWidthAttribute>()?.Width ?? 0;
                frameworkElement.MinHeight = formElement.property.GetCustomAttribute<MinHeightAttribute>()?.Height ?? 0;

                frameworkElement.MaxWidth = formElement.property.GetCustomAttribute<MaxWidthAttribute>()?.Width ?? double.MaxValue;
                frameworkElement.MaxHeight = formElement.property.GetCustomAttribute<MaxHeightAttribute>()?.Height ?? double.MaxValue;

                yield return frameworkElement;
            }
        }

        private FrameworkElement CreateTimetableElement((PropertyInfo property, FormElementAttribute attribute) formElement)
        {
            var dataGrid = new DataGrid()
            {
                Style = (Style)Application.Current.FindResource("TimetableDataGridStyle"),
                //ItemsSource = TimetableRecordElement.GetRecordElements(_item as Timetable)
            };

            DatabaseContext.Entities.DayOfWeeks.Load();
            var systemDayOfWeeks = Enum.GetValues<System.DayOfWeek>();
            foreach (var dayOfWeek in DatabaseContext.Entities.DayOfWeeks.Local.Skip(1))
            {
                dataGrid.Columns.Add(new DataGridCheckBoxColumn()
                {
                    Width = DataGridLength.SizeToCells,
                    Header = dayOfWeek.Reduction,
                    Binding = new Binding($"Is{systemDayOfWeeks[dayOfWeek.Id]}Checked")
                    {
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    }
                });
            }

            foreach (var record in TimetableRecordElement.GetRecordElements(Item as Timetable))
            {
                dataGrid.Items.Add(new DataGridRow()
                {
                    Item = record,
                    Header = $"{record.Couple} пара"
                });
            }

            return new Border()
            {
                Child = dataGrid
            };
        }

        private FrameworkElement CreateSubtableElement((PropertyInfo property, FormElementAttribute attribute) formElement)
        {
            if (formElement.property.PropertyType.GetInterface("IEnumerable") == null)
                throw new ArgumentException("Subtable form element must be on property that has IEnumerable type");

            MethodInfo method = typeof(DbContext).GetMethod("Set", Type.EmptyTypes)!;
            MethodInfo genericMethod = method.MakeGenericMethod(formElement.property.PropertyType.GetGenericArguments()[0]);

            dynamic values = genericMethod.Invoke(DatabaseContext.Entities, Array.Empty<object>())!;
            EntityFrameworkQueryableExtensions.Load(values);

            var groupBox = new GroupBox
            {
                Header = formElement.property.GetCustomAttribute<LabelAttribute>()?.Label,
            };

            var grid = new StackPanel();
            /*
                        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                        grid.RowDefinitions.Add(new RowDefinition() { Height = new(1, GridUnitType.Star) });*/

            #region Datagrid initialization
            var columnAttributes = formElement.property.GetCustomAttributes<ColumnAttribute>();

            var dataGrid = new DataGrid()
            {
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                CanUserDeleteRows = false,
                CanUserResizeColumns = false,
                IsReadOnly = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Height = 160
            };

            var dataGridBorder = new Border
            {
                Child = dataGrid
            };

            dataGrid.SetValue(ScrollViewer.CanContentScrollProperty, false);
            dataGrid.SetValue(Grid.ColumnProperty, 1);

            dataGrid.ItemsSource = (IEnumerable)formElement.property.GetValue(this)!;

            foreach (var column in columnAttributes)
                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = column.Header,
                    Binding = new Binding(column.Path)
                    {
                        Mode = BindingMode.OneWay
                    }
                });

            #endregion

            var buttonsContainer = new SimpleStackPanel
            {
                Orientation = Orientation.Horizontal,
                Spacing = 10,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new(0, 0, 0, 10)
            };

            buttonsContainer.SetValue(Grid.ColumnProperty, 0);

            var addButton = new Button
            {
                Content = "Добавить"
            };

            var removeButton = new Button
            {
                Content = "Удалить"
            };

            SubtableFormElementAttribute attribute = (SubtableFormElementAttribute)formElement.attribute;

            var entitySelectorBoxType = Type.GetType("CollegeStatictics.ViewModels.Base.ItemDialog`1")!
                                            .MakeGenericType(formElement.property.PropertyType.GetGenericArguments()[0]);

            addButton.Click += delegate
            {
                dynamic itemDialog = attribute.Create(null);

                var linkedType = Item.GetType();

                Type type = itemDialog.Item.GetType();
                PropertyInfo itemProperty = type.GetProperties().First(property => property.PropertyType == linkedType);
                itemProperty.SetValue(itemDialog.Item, Item);

                var contentDialog = new DialogWindow
                {
                    Content = itemDialog,
                    ContentTemplate = (DataTemplate)Application.Current.FindResource("ItemDialogTemplate"),

                    PrimaryButtonText = "Сохранить",

                    SecondaryButtonText = "Отмена",
                    SecondaryButtonCommand = itemDialog.CancelCommand,

                    CanClose = () => !DatabaseContext.HasChanges(itemDialog.Item)
                };

                void ContentDialogClosingHandler(object? sender, CancelEventArgs e)
                {
                    if (DatabaseContext.Entities.ChangeTracker.HasChanges() && (sender as DialogWindow)!.Result == DialogResult.None)
                    {
                        var acceptDialog = new DialogWindow
                        {
                            Content = "Сохранить изменения?",
                            PrimaryButtonText = "Да",
                            SecondaryButtonText = "Нет",
                            TertiaryButtonText = "Отмена",
                        };

                        e.Cancel = false;
                        acceptDialog.Show();

                        if (acceptDialog.Result == DialogResult.Primary)
                            DatabaseContext.Entities.SaveChanges(itemDialog.Item);
                        else if (acceptDialog.Result == DialogResult.Secondary)
                            DatabaseContext.CancelChanges(itemDialog.Item);
                        else
                            e.Cancel = true;
                    }
                }

                contentDialog.Closing += ContentDialogClosingHandler;
                contentDialog.Show();
                contentDialog.Closing -= ContentDialogClosingHandler;


                if (contentDialog.Result != DialogResult.Primary)
                {
                    DatabaseContext.CancelChanges(itemDialog.Item);
                    return;
                }

                var addMethod = formElement.property.PropertyType.GetMethod("Add");

                addMethod?.Invoke(formElement.property.GetValue(this), new object[] { itemDialog.Item });

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = (dynamic)formElement.property.GetValue(this)!;
            };

            removeButton.Click += delegate
            {
                if (dataGrid.SelectedItems?.Count == 0)
                    return;

                var dialogWindow = new DialogWindow
                {
                    Content = new Label { Content = "Действительно удалить выбранные элементы?" },
                    PrimaryButtonText = "Да",
                    SecondaryButtonText = "Нет"
                };
                dialogWindow.Show();

                if (dialogWindow.Result != DialogResult.Primary)
                    return;

                var removeMethod = formElement.property.PropertyType.GetMethod("Remove");

                foreach (var selectedItem in dataGrid.SelectedItems!)
                    removeMethod?.Invoke(formElement.property.GetValue(this), new object[] { selectedItem });

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = (dynamic)formElement.property.GetValue(this)!;
            };

            buttonsContainer.Children.Add(removeButton);
            buttonsContainer.Children.Add(addButton);

            grid.Children.Add(buttonsContainer);
            grid.Children.Add(dataGridBorder);

            groupBox.Content = grid;

            return groupBox;
        }

        private FrameworkElement CreateSelectableSubtableElement((PropertyInfo property, FormElementAttribute attribute) formElement)
        {
            if (formElement.property.PropertyType.GetInterface("IEnumerable") == null)
                throw new ArgumentException("Subtable form element must be on property that has IEnumerable type");

            MethodInfo method = typeof(DbContext).GetMethod("Set", Type.EmptyTypes)!;
            MethodInfo genericMethod = method.MakeGenericMethod(formElement.property.PropertyType.GetGenericArguments()[0]);

            dynamic values = genericMethod.Invoke(DatabaseContext.Entities, Array.Empty<object>())!;
            EntityFrameworkQueryableExtensions.Load(values);

            var groupBox = new GroupBox
            {
                Header = formElement.property.GetCustomAttribute<LabelAttribute>()?.Label,
            };

            var grid = new StackPanel();
/*
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new(1, GridUnitType.Star) });*/

            #region Datagrid initialization
            var columnAttributes = formElement.property.GetCustomAttributes<ColumnAttribute>();

            var dataGrid = new DataGrid()
            {
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                CanUserDeleteRows = false,
                CanUserResizeColumns = false,
                IsReadOnly = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Height = 160
            };

            var dataGridBorder = new Border
            {
                Child = dataGrid
            };

            dataGrid.SetValue(ScrollViewer.CanContentScrollProperty, false);
            dataGrid.SetValue(Grid.ColumnProperty, 1);

            dataGrid.ItemsSource = (dynamic)formElement.property.GetValue(this)!;

            foreach (var column in columnAttributes)
                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = column.Header,
                    Binding = new Binding(column.Path)
                    {
                        Mode = BindingMode.OneWay
                    }
                });

            #endregion

            var buttonsContainer = new SimpleStackPanel
            {
                Orientation = Orientation.Horizontal,
                Spacing = 10,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new(0, 0, 0, 10)
            };

            buttonsContainer.SetValue(Grid.ColumnProperty, 0);

            var addButton = new Button
            {
                Content = "Добавить"
            };

            var removeButton = new Button
            {
                Content = "Удалить"
            };

            SelectableSubtableFormElementAttribute attribute = (SelectableSubtableFormElementAttribute)formElement.attribute;

            var entitySelectorBoxType = Type.GetType("CollegeStatictics.ViewModels.EntitiesGrid`1")!
                                            .MakeGenericType(formElement.property.PropertyType.GetGenericArguments()[0]);

            addButton.Click += delegate
            {
                dynamic entitySelectorBox = Activator.CreateInstance(entitySelectorBoxType, new[] { attribute.ItemContainerName })!;
                entitySelectorBox.OpenSelectorDialog();

                var addMethod = formElement.property.PropertyType.GetMethod("Add");

                if (entitySelectorBox.SelectedItems == null)
                    return;

                foreach (var selectedItem in entitySelectorBox.SelectedItems)
                    addMethod?.Invoke(formElement.property.GetValue(this), new object[] { selectedItem });

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = (dynamic)formElement.property.GetValue(this)!;
            };

            removeButton.Click += delegate
            {
                if (dataGrid.SelectedItems == null)
                    return;

                var dialogWindow = new DialogWindow
                {
                    Content = new Label { Content = "Действительно удалить выбранные элементы?" },
                    PrimaryButtonText = "Да",
                    SecondaryButtonText = "Нет"
                };
                dialogWindow.Show();

                if (dialogWindow.Result != DialogResult.Primary)
                    return;

                var removeMethod = formElement.property.PropertyType.GetMethod("Remove");

                foreach (var selectedItem in dataGrid.SelectedItems)
                    removeMethod?.Invoke(formElement.property.GetValue(this), new object[] { selectedItem });

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = (dynamic)formElement.property.GetValue(this)!;
            };

            buttonsContainer.Children.Add(addButton);
            buttonsContainer.Children.Add(removeButton);

            grid.Children.Add(buttonsContainer);
            grid.Children.Add(dataGridBorder);

            groupBox.Content = grid;

            return groupBox;
        }

        private static FrameworkElement CreateTextBox((PropertyInfo property, FormElementAttribute attribute) formElement)
        {
            var stackPanel = new StackPanel();

            var textBox = new TextBox
            {
                IsReadOnly = formElement.attribute.IsReadOnly,
                AcceptsReturn = ((TextBoxFormElementAttribute)formElement.attribute).AcceptsReturn,
                VerticalAlignment = VerticalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
            };

            var labelAttribute = formElement.property.GetCustomAttribute<LabelAttribute>();
            if (labelAttribute != null)
            {
                var label = new Label
                {
                    Content = labelAttribute.Label,
                    Target = textBox,
                };
                stackPanel.Children.Add(label);
            }

            stackPanel.Children.Add(textBox);

            textBox.SetBinding(TextBox.TextProperty, new Binding(formElement.property.Name)
            {
                Mode = formElement.attribute.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                ValidatesOnNotifyDataErrors = true,
            });

            return stackPanel;
        }

        private static FrameworkElement CreateNumberBox((PropertyInfo property, FormElementAttribute attribute) formElement)
        {
            var stackPanel = new StackPanel();

            var textBox = new TextBox
            {
                IsReadOnly = formElement.attribute.IsReadOnly,
                MaxLength = formElement.property.GetCustomAttribute<MaxLengthAttribute>()?.Length ?? int.MaxValue
            };

            textBox.PreviewTextInput += (s, e) =>
            {
                if (string.IsNullOrEmpty(e.Text?.Trim()))
                    return;

                else if (int.TryParse(e.Text, out var _) == false)
                    e.Handled = true;
            };

            var labelAttribute = formElement.property.GetCustomAttribute<LabelAttribute>();
            if (labelAttribute != null)
            {
                var label = new Label
                {
                    Content = labelAttribute.Label,
                    Target = textBox,
                };
                stackPanel.Children.Add(label);
            }

            stackPanel.Children.Add(textBox);

            textBox.SetBinding(TextBox.TextProperty, new Binding(formElement.property.Name)
            {
                Mode = formElement.attribute.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                ValidatesOnNotifyDataErrors = true,
            });

            return stackPanel;
        }

        private static FrameworkElement CreateSpinBox((PropertyInfo property, FormElementAttribute attribute) formElement)
        {
            var stackPanel = new StackPanel();
            
            var spinBox = new NumberBox
            {
                IsEnabled = formElement.attribute.IsReadOnly == false,
                SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
            };

            if (formElement.property.GetCustomAttribute<RangeAttribute>() is RangeAttribute rangeAttribute)
            {
                spinBox.Minimum = (int)rangeAttribute.Minimum;
                spinBox.Maximum = (int)rangeAttribute.Maximum;
            }

            var labelAttribute = formElement.property.GetCustomAttribute<LabelAttribute>();
            if (labelAttribute != null)
            {
                var label = new Label
                {
                    Content = labelAttribute.Label,
                    Target = spinBox,
                };
                stackPanel.Children.Add(label);
            }

            stackPanel.Children.Add(spinBox);

            spinBox.SetBinding(NumberBox.TextProperty, new Binding(formElement.property.Name)
            {
                Mode = formElement.attribute.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                ValidatesOnNotifyDataErrors = true,
            });

            return stackPanel;
        }

        private FrameworkElement CreateDatePicker((PropertyInfo property, FormElementAttribute attribute) formElement)
        {
            var stackPanel = new StackPanel();

            DateTime selectedDate = (DateTime)formElement.property.GetValue(this)!;
            if (selectedDate == DateTime.MinValue)
                formElement.property.SetValue(this, DateTime.Now);

            var datePicker = new DatePicker
            {
                IsEnabled = formElement.attribute.IsReadOnly == false
            };

            var labelAttribute = formElement.property.GetCustomAttribute<LabelAttribute>();
            if (labelAttribute != null)
            {
                var label = new Label
                {
                    Content = labelAttribute.Label,
                    Target = datePicker,
                };
                stackPanel.Children.Add(label);
            }

            stackPanel.Children.Add(datePicker);

            datePicker.SetBinding(DatePicker.SelectedDateProperty, new Binding(formElement.property.Name)
            {
                Mode = formElement.attribute.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                ValidatesOnNotifyDataErrors = true,
            });

            return stackPanel;
        }

        private static FrameworkElement CreateEntitySelectorBox((PropertyInfo property, FormElementAttribute attribute) formElement)
        {
            EntitySelectorFormElementAttribute attribute = (EntitySelectorFormElementAttribute)formElement.attribute;

            var entitySelectorBoxType = Type.GetType("CollegeStatictics.ViewModels.EntitySelectorBox`1")!
                                            .MakeGenericType(formElement.property.PropertyType);

            var entitySelectorBox = Activator.CreateInstance(entitySelectorBoxType, new[] { attribute.ItemContainerName });
            var dp = (DependencyProperty)entitySelectorBoxType.GetField("SelectedItemProperty")!.GetValue(entitySelectorBox)!;

            ((Control)entitySelectorBox!).SetBinding(dp, new Binding(formElement.property.Name)
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            var stackPanel = new StackPanel();

            var entitySelectorBoxContainer = new ContentControl
            {
                Content = entitySelectorBox,
                ContentTemplate = (DataTemplate)Application.Current.FindResource("EntitySelectorBoxTemplate")
            };

            var labelAttribute = formElement.property.GetCustomAttribute<LabelAttribute>();
            if (labelAttribute != null)
            {
                var label = new Label
                {
                    Content = labelAttribute.Label,
                    Target = entitySelectorBoxContainer,
                };
                stackPanel.Children.Add(label);
            }

            stackPanel.Children.Add(entitySelectorBoxContainer);

            return stackPanel;
        }

        private FrameworkElement CreateRadioButtonList((PropertyInfo property, FormElementAttribute attribute) formElement)
        {
            MethodInfo method = typeof(DbContext).GetMethod("Set", Type.EmptyTypes)!;
            MethodInfo genericMethod = method.MakeGenericMethod(formElement.property.PropertyType);

            dynamic values = genericMethod.Invoke(DatabaseContext.Entities, Array.Empty<object>())!;
            EntityFrameworkQueryableExtensions.Load(values);

            var groupBox = new GroupBox
            {
                Padding = new(0, 0, 0, 0)
            };

            var labelAttribute = formElement.property.GetCustomAttribute<LabelAttribute>();
            if (labelAttribute != null)
                groupBox.Header = labelAttribute.Label;

            var stackPanel = new StackPanel();

            var border = new Border
            {
                Child = stackPanel
            };

            groupBox.Content = border;

            if (formElement.property.GetValue(this) == null)
                formElement.property.SetValue(this, Enumerable.FirstOrDefault(values.Local));

            foreach (var value in values.Local)
            {
                var radioButton = new RadioButton
                {
                    Content = value,
                    IsChecked = formElement.property.GetValue(this) == value
                };
                radioButton.Click += delegate { formElement.property.SetValue(this, value); };
                stackPanel.Children.Add(radioButton);
            }

            return groupBox;
        }

        private T CreateDefaultItem()
        {
            var properties = GetType().GetProperties().Where(property => property.GetCustomAttribute<FormElementAttribute>() != null);

            var itemType = typeof(T);

            T itemInstance = (T)itemType.GetConstructor(Type.EmptyTypes)!.Invoke(Array.Empty<object>());

            return itemInstance;
        }

        #endregion
    }
}
