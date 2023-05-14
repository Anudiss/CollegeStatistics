using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.DataTypes.Records;
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace CollegeStatictics.ViewModels.Base
{
    public abstract partial class ItemDialog<T> : ObservableValidator where T : class, ITable, new()
    {
        #region [ Commands ]

        [RelayCommand(CanExecute = nameof(CanSave))]
        public void Save()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            if (DatabaseContext.Entities.Entry(Item).State == EntityState.Detached)
                DatabaseContext.Entities.Set<T>().Local.Add(Item);

            DatabaseContext.Entities.SaveChanges();
        }
        
        private bool CanSave() => !HasErrors;

        [RelayCommand]
        private void Cancel() => DatabaseContext.CancelChanges();

        #endregion

        #region [ Properties ]

        public T Item { get; }

        public IEnumerable<FrameworkElement> ViewElements => CreateViewElements();

        #endregion

        #region [ Initializing ]

        public ItemDialog(T? item)
        {
            Item = item ?? new T();
            InitializeItemDefaultValues();
        }

        #endregion

        #region [ Private methods ]

        #region [ Assemblying view elements ]

        protected virtual IEnumerable<FrameworkElement> CreateViewElements()
        {
            var formElements = GetFormElements();
            foreach (var formElement in formElements)
            {
                var viewElement = CreateViewElement(formElement);

                ApplyAttributesToViewElement(viewElement, formElement);

                yield return viewElement;
            }

            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;

            var methodAttributePairs = from method in GetType().GetMethods(bindingFlags).Reverse()
                                       let attribute = method.GetCustomAttribute<ButtonElementAttribute>()
                                       where attribute != null
                                       select (method, attribute);

            var simpleStackPanel = new SimpleStackPanel
            {
                Spacing = 6,
                HorizontalAlignment = HorizontalAlignment.Right,
            };

            foreach (var pair in methodAttributePairs)
            {
                var commandName = $"{pair.method.Name}Command";
                var command = (ICommand)GetType()
                                       .GetProperty(commandName)!
                                       .GetValue(this)!;

                var button = new Button
                {
                    Content = pair.attribute.Content,
                    Command = command,
                };
                simpleStackPanel.Children.Add(button);
            }

            yield return simpleStackPanel;
        }

        protected IEnumerable<FormElement> GetFormElements()
        {
            var propertyAttributePairs = from property in GetType().GetProperties().Reverse()
                                         let attribute = property.GetCustomAttribute<FormElementAttribute>()
                                         where attribute != null
                                         select (property, attribute);

            return propertyAttributePairs.Select(pair => new FormElement(pair.property, pair.attribute));
        }

        protected FrameworkElement CreateViewElement(FormElement formElement)
            => formElement.Attribute.ElementType switch
                {
                    ElementType.TextBox => CreateTextBox(formElement),
                    ElementType.NumberBox => CreateNumberBox(formElement),
                    ElementType.SpinBox => CreateSpinBox(formElement),
                    ElementType.CheckBox => CreateCheckBox(formElement),
                    ElementType.EntitySelectorBox => CreateEntitySelectorBox(formElement),
                    ElementType.RadioButton => CreateRadioButtonList(formElement),
                    ElementType.SelectableSubtable => CreateSelectableSubtableElement(formElement),
                    ElementType.EditableSubtable => CreateEditableSubtableElement(formElement),
                    ElementType.Subtable => CreateSubtableElement(formElement),
                    ElementType.Timetable => CreateTimetableElement(formElement),
                    ElementType.DatePicker => CreateDatePicker(formElement),
                    ElementType.TimeBox => CreateTimeBoxElement(formElement),
                    _ => throw new NotSupportedException("Invalid element type")
                };

        private static void ApplyAttributesToViewElement(FrameworkElement frameworkElement, FormElement formElement)
        {
            frameworkElement.MinWidth = formElement.Property.GetCustomAttribute<MinWidthAttribute>()?.Width ?? 0;
            frameworkElement.MinHeight = formElement.Property.GetCustomAttribute<MinHeightAttribute>()?.Height ?? 0;

            frameworkElement.MaxWidth = formElement.Property.GetCustomAttribute<MaxWidthAttribute>()?.Width ?? double.MaxValue;
            frameworkElement.MaxHeight = formElement.Property.GetCustomAttribute<MaxHeightAttribute>()?.Height ?? double.MaxValue;
        }

        #endregion

        #region [ Creation of view elements ]

        #region [ Primitive controls ]
        protected FrameworkElement CreateTextBox(FormElement formElement)
        {
            var stackPanel = new StackPanel();

            var textBox = new TextBox
            {
                TextWrapping = TextWrapping.Wrap,
                IsReadOnly = formElement.Attribute.IsReadOnly,
                AcceptsReturn = ((TextBoxFormElementAttribute)formElement.Attribute).AcceptsReturn
            };

            ApplyAttributesToViewElement(textBox, formElement);

            TryAttachLabel(stackPanel, textBox, formElement);

            SetBinding(textBox, TextBox.TextProperty, formElement);

            stackPanel.Children.Add(textBox);
            return stackPanel;
        }

        protected FrameworkElement CreateNumberBox(FormElement formElement)
        {
            var stackPanel = new StackPanel();

            var textBox = new TextBox
            {
                IsReadOnly = formElement.Attribute.IsReadOnly,
                MaxLength = formElement.Property.GetCustomAttribute<MaxLengthAttribute>()?.Length ?? int.MaxValue
            };

            textBox.PreviewTextInput += (_, e) =>
            {
                if (string.IsNullOrEmpty(e.Text?.Trim()))
                    return;

                else if (int.TryParse(e.Text, out var _) == false)
                    e.Handled = true;
            };


            TryAttachLabel(stackPanel, textBox, formElement);

            SetBinding(textBox, TextBox.TextProperty, formElement);

            stackPanel.Children.Add(textBox);
            return stackPanel;
        }

        protected FrameworkElement CreateSpinBox(FormElement formElement)
        {
            var stackPanel = new StackPanel();
            
            var spinBox = new NumberBox
            {
                IsEnabled = formElement.Attribute.IsReadOnly == false,
                SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
            };

            if (formElement.Property.GetCustomAttribute<RangeAttribute>() is RangeAttribute rangeAttribute)
            {
                spinBox.Minimum = (int)rangeAttribute.Minimum;
                spinBox.Maximum = (int)rangeAttribute.Maximum;
            }

            TryAttachLabel(stackPanel, spinBox, formElement);

            SetBinding(spinBox, NumberBox.TextProperty, formElement);

            stackPanel.Children.Add(spinBox);
            return stackPanel;
        }

        protected FrameworkElement CreateCheckBox(FormElement formElement)
        {
            var stackPanel = new StackPanel();

            var checkBox = new CheckBox();

            ApplyAttributesToViewElement(checkBox, formElement);

            TryAttachLabel(stackPanel, checkBox, formElement);

            SetBinding(checkBox, ToggleButton.IsCheckedProperty, formElement);

            stackPanel.Children.Add(checkBox);
            return stackPanel;
        }

        protected FrameworkElement CreateDatePicker(FormElement formElement)
        {
            var stackPanel = new StackPanel();

            var selectedDate = (DateTime)formElement.Property.GetValue(this)!;
            if (selectedDate == DateTime.MinValue)
                formElement.Property.SetValue(this, DateTime.Now);

            var datePicker = new DatePicker
            {
                IsEnabled = formElement.Attribute.IsReadOnly == false,
                SelectedDateFormat = DatePickerFormat.Long
            };

            TryAttachLabel(stackPanel, datePicker, formElement);

            SetBinding(datePicker, DatePicker.SelectedDateProperty, formElement);

            stackPanel.Children.Add(datePicker);
            return stackPanel;
        }

        protected FrameworkElement CreateTimeBoxElement(FormElement formElement)
        {
            var stackPanel = new StackPanel();

            var timeTextBox = new TextBox();
            TextBoxHelper.SetIsDeleteButtonVisible(timeTextBox, false);
            timeTextBox.PreviewTextInput += (_, e) =>
            {
                // 00:00


            };

            TryAttachLabel(stackPanel, timeTextBox, formElement);

            SetBinding(timeTextBox, DatePickerTextBox.TextProperty, formElement);

            stackPanel.Children.Add(timeTextBox);
            return stackPanel;
        }

        protected FrameworkElement CreateRadioButtonList(FormElement formElement)
        {
            var entityType = formElement.Property.PropertyType;
            var entities = DatabaseContext.LoadEntities(entityType);

            var groupBox = new GroupBox();

            var labelAttribute = formElement.Property.GetCustomAttribute<LabelAttribute>();
            if (labelAttribute != null)
                groupBox.Header = labelAttribute.Label;
        

            var stackPanel = new StackPanel();

            var border = new Border { Child = stackPanel };
            groupBox.Content = border;

            // If property value is null, then assign it default value
            if (formElement.Property.GetValue(this) == null)
                formElement.Property.SetValue(this, Enumerable.FirstOrDefault(entities.Local));

            foreach (var entity in entities.Local)
            {
                var radioButton = new RadioButton
                {
                    Content = entity,
                    IsChecked = entity == formElement.Property.GetValue(this)
                };
                radioButton.Click += (_ ,__) => formElement.Property.SetValue(this, entity);

                stackPanel.Children.Add(radioButton);
            }

            return groupBox;
        }

        #endregion

        #region [ Table controls ]

        private FrameworkElement CreateEditableSubtableElement(FormElement formElement)
        {
            var dataGrid = new DataGrid()
            {
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                CanUserDeleteRows = false,
                CanUserResizeColumns = false,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                MinHeight = 160
            };

            dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding(formElement.Property.Name)
            {
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            var columns = formElement.Property.GetCustomAttributes<TextColumnAttribute>();

            foreach (var column in columns)
                dataGrid.Columns.Add(column.ToDataGridColumn());

            return dataGrid;
        }


        private FrameworkElement CreateTimetableElement(FormElement formElement)
        {
            var dataGrid = new DataGrid()
            {
                Style = (Style)Application.Current.FindResource("TimetableDataGridStyle"),
            };

            DatabaseContext.Entities.DayOfWeeks.Load();
                
            var systemDayOfWeeks = Enum.GetValues<System.DayOfWeek>();

            foreach (var dayOfWeek in DatabaseContext.Entities.DayOfWeeks.Local.OrderBy(d => d.Id).Skip(1))
            {
                dataGrid.Columns.Add(new DataGridCheckBoxColumn()
                {
                    Width = DataGridLength.SizeToCells,
                    Header = dayOfWeek.Reduction,
                    IsReadOnly = formElement.Attribute.IsReadOnly,

                    Binding = new Binding($"Is{systemDayOfWeeks[dayOfWeek.Id]}Checked")
                    {
                        Mode = formElement.Attribute.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    }
                });
            }

            var timetable = (Item as Timetable)!;
            foreach (var timetableRecord in TimetableRecordElement.GetRecordElements(timetable))
            {
                dataGrid.Items.Add(new DataGridRow()
                {
                    Item = timetableRecord,
                    Header = $"{timetableRecord.Couple} пара"
                });
            }

            return new Border { Child = dataGrid };
        }

        private FrameworkElement CreateSubtableElement(FormElement formElement)
        {
            var isPropertyEnumerable = formElement.Property.PropertyType.GetInterface("IEnumerable") != null;
            if (isPropertyEnumerable == false)
                throw new ArgumentException("Subtable form element must be on property that has IEnumerable type");

            var entityType = formElement.Property.PropertyType.GetGenericArguments()[0];
            var entities = DatabaseContext.LoadEntities(entityType);

            var dockPanel = new DockPanel();

            var groupBox = new GroupBox
            {
                Header = formElement.Property.GetCustomAttribute<LabelAttribute>()?.Label,
                VerticalAlignment = VerticalAlignment.Center
            };
            dockPanel.Children.Add(groupBox);

            groupBox.SetValue(DockPanel.DockProperty, Dock.Left);

            var stackPanel = new StackPanel();

            #region Datagrid initialization
            var columnAttributes = formElement.Property.GetCustomAttributes<TextColumnAttribute>();

            var dataGrid = new DataGrid()
            {
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                CanUserDeleteRows = false,
                CanUserResizeColumns = false,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Height = 160
            };

            var openItemDialogCommand = new RelayCommand<object>(item =>
            {
                OpenItemDialog(formElement, item);

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = (dynamic)formElement.Property.GetValue(this)!;
            });

            dataGrid.LoadingRow += (sender, e) =>
            {
                dynamic itemsContainer = ((DataGrid)sender).DataContext;

                e.Row.InputBindings.Add(new MouseBinding
                {
                    Gesture = new MouseGesture(MouseAction.LeftDoubleClick),
                    Command = openItemDialogCommand,
                    CommandParameter = e.Row.Item
                });
            };

            var dataGridBorder = new Border
            {
                Child = dataGrid
            };

            dataGrid.SetValue(ScrollViewer.CanContentScrollProperty, false);
            dataGrid.SetValue(Grid.ColumnProperty, 1);

            dataGrid.ItemsSource = (IEnumerable)formElement.Property.GetValue(this)!;

            foreach (var column in columnAttributes)
                dataGrid.Columns.Add(column.ToDataGridColumn());

            #endregion

            var buttonAttributes = formElement.Property.GetCustomAttributes<SubtableButtonFormElementAttribute>();

            var buttonsContainer = new SimpleStackPanel
            {
                Orientation = Orientation.Horizontal,
                Spacing = 10,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new(0, 0, 0, 10)
            };

            dockPanel.Children.Add(buttonsContainer);
            buttonsContainer.SetValue(DockPanel.DockProperty, Dock.Right);

            foreach (var buttonAttribute in buttonAttributes)
                buttonsContainer.Children.Add(new Button()
                {
                    Content = buttonAttribute.Text,
                    Command = (ICommand)GetType().GetProperty(buttonAttribute.CommandName)!.GetValue(this)!,
                    CommandParameter = dataGrid.SelectedItems
                });

            var addButton = new Button
            {
                Content = "Добавить"
            };

            var removeButton = new Button
            {
                Content = "Удалить"
            };

            var entitySelectorBoxType = Type.GetType("CollegeStatictics.ViewModels.Base.ItemDialog`1")!
                                            .MakeGenericType(formElement.Property.PropertyType.GetGenericArguments()[0]);

            addButton.Click += delegate
            {
                OpenItemDialog(formElement, null);

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = (dynamic)formElement.Property.GetValue(this)!;
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

                foreach (var selectedItem in dataGrid.SelectedItems!)
                    DatabaseContext.Entities.Remove(selectedItem);

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = (dynamic)formElement.Property.GetValue(this)!;
            };

            buttonsContainer.Children.Add(removeButton);
            buttonsContainer.Children.Add(addButton);

            stackPanel.Children.Add(dockPanel);
            stackPanel.Children.Add(dataGridBorder);

            return new GroupBox
            {
                Content = stackPanel
            };
        }

        private FrameworkElement CreateSelectableSubtableElement(FormElement formElement)
        {
            var isPropertyEnumerable = formElement.Property.PropertyType.GetInterface("IEnumerable") != null;
            if (isPropertyEnumerable == false)
                throw new ArgumentException("Subtable form element must be on property that has IEnumerable type");

            var entityType = formElement.Property.PropertyType.GetGenericArguments()[0];
            var entities = DatabaseContext.LoadEntities(entityType);

            var groupBox = new GroupBox
            {
                Header = formElement.Property.GetCustomAttribute<LabelAttribute>()?.Label,
            };

            var grid = new StackPanel();

            #region Datagrid initialization
            var columnAttributes = formElement.Property.GetCustomAttributes<TextColumnAttribute>();

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

            dataGrid.ItemsSource = (dynamic)formElement.Property.GetValue(this)!;

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

            SelectableSubtableFormElementAttribute attribute = (SelectableSubtableFormElementAttribute)formElement.Attribute;

            var entitySelectorBoxType = Type.GetType("CollegeStatictics.ViewModels.EntitiesGrid`1")!
                                            .MakeGenericType(formElement.Property.PropertyType.GetGenericArguments()[0]);

            addButton.Click += delegate
            {
                dynamic entitySelectorBox = Activator.CreateInstance(entitySelectorBoxType, new[] { attribute.ItemContainerName })!;
                entitySelectorBox.OpenSelectorDialog();

                var addMethod = formElement.Property.PropertyType.GetMethod("Add");

                if (entitySelectorBox.SelectedItems == null)
                    return;

                foreach (var selectedItem in entitySelectorBox.SelectedItems)
                    addMethod?.Invoke(formElement.Property.GetValue(this), new object[] { selectedItem });

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = (dynamic)formElement.Property.GetValue(this)!;
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

                var removeMethod = formElement.Property.PropertyType.GetMethod("Remove");

                foreach (var selectedItem in dataGrid.SelectedItems)
                    removeMethod?.Invoke(formElement.Property.GetValue(this), new object[] { selectedItem });

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = (dynamic)formElement.Property.GetValue(this)!;
            };

            buttonsContainer.Children.Add(addButton);
            buttonsContainer.Children.Add(removeButton);

            grid.Children.Add(buttonsContainer);
            grid.Children.Add(dataGridBorder);

            groupBox.Content = grid;

            return groupBox;
        }

        private FrameworkElement CreateEntitySelectorBox(FormElement formElement)
        {
            var attribute = (EntitySelectorFormElementAttribute)formElement.Attribute;

            var entitySelectorBoxType = Type.GetType("CollegeStatictics.ViewModels.EntitySelectorBox`1")!
                                            .MakeGenericType(formElement.Property.PropertyType);

            var filter = attribute.FilterPropertyName is not null ? GetType().GetProperty(attribute.FilterPropertyName)!.GetValue(this) : null;

            var entitySelectorBox = Activator.CreateInstance(entitySelectorBoxType, new[] { attribute.ItemContainerName, filter });
            var dp = (DependencyProperty)entitySelectorBoxType.GetField("SelectedItemProperty")!.GetValue(entitySelectorBox)!;

            ((Control)entitySelectorBox!).SetBinding(dp, new Binding(formElement.Property.Name)
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

            TryAttachLabel(stackPanel, entitySelectorBoxContainer, formElement);

            stackPanel.Children.Add(entitySelectorBoxContainer);
            return stackPanel;
        }

        #endregion

        #endregion

        private void OpenItemDialog(FormElement formElement, object item)
        {
            var attribute = (SubtableFormElementAttribute)formElement.Attribute;

            dynamic itemDialog = attribute.Create(item);

            var linkedType = Item.GetType();

            Type type = itemDialog.Item.GetType();
            PropertyInfo itemProperty = type.GetProperties().First(property => property.PropertyType == linkedType);
            itemProperty.SetValue(itemDialog.Item, Item);

            var dialogWindow = new DialogWindow
            {
                Content = itemDialog,
                ContentTemplate = (DataTemplate)Application.Current.FindResource("ItemDialogTemplate"),

                PrimaryButtonText = "Сохранить",

                SecondaryButtonText = "Отмена",

                CanClose = () => true
            };

            void WindowDialogClosingHandler(object? sender, CancelEventArgs e)
            {
                var dialogResult = (sender as DialogWindow)!.Result;
                bool areThereUnsavedChanges = DatabaseContext.Entities.ChangeTracker.HasChanges();

                if (areThereUnsavedChanges == true && dialogResult == DialogResult.None)
                {
                    var acceptDialog = new DialogWindow
                    {
                        Content = "Сохранить изменения?",
                        PrimaryButtonText = "Да",
                        SecondaryButtonText = "Нет",
                        TertiaryButtonText = "Отмена",
                    };

                    acceptDialog.Show();

                    if (acceptDialog.Result == DialogResult.Primary)
                    {
                        if (itemDialog.Item.Id == 0)
                            DatabaseContext.Entities.Add(itemDialog.Item);
                    }
                    else if (acceptDialog.Result == DialogResult.Secondary)
                        DatabaseContext.CancelChanges(itemDialog.Item);
                    else
                        e.Cancel = true;
                }
            }

            dialogWindow.Closing += WindowDialogClosingHandler;
            dialogWindow.Show();
            dialogWindow.Closing -= WindowDialogClosingHandler;

            if (dialogWindow.Result == DialogResult.Primary)
            {
                if (DatabaseContext.Entities.Entry((object)itemDialog.Item).State == EntityState.Detached)
                    DatabaseContext.Entities.Add(itemDialog.Item);

                DatabaseContext.Entities.SaveChanges(itemDialog.Item);
            }
            else
            {
                var entityEntry = DatabaseContext.Entities.Entry((object)itemDialog.Item);
                entityEntry.CurrentValues.SetValues(entityEntry.OriginalValues);
            }
        }

        private void InitializeItemDefaultValues()
        {
            if (DatabaseContext.Entities.Entry(Item).State != EntityState.Detached)
                return;

            foreach (var property in GetType().GetProperties().Where(p => p.GetCustomAttribute<DefaultValueAttribute>() != null))
            {
                DefaultValueAttribute attribute = property.GetCustomAttribute<DefaultValueAttribute>()!;

                property.SetValue(this, attribute.Value);
            }
        }

        #region [ Static (helper) methods ]

        protected static void TryAttachLabel(Panel panel, UIElement target, FormElement formElement)
        {
            if (formElement.Property.GetCustomAttribute<LabelAttribute>() is LabelAttribute labelAttribute)
                panel.Children.Add(new Label
                {
                    Content = labelAttribute.Label,
                    Target = target,
                });
        }

        private static void SetBinding(FrameworkElement frameworkElement, DependencyProperty property, FormElement formElement)
        {
            frameworkElement.SetBinding(property, new Binding(formElement.Property.Name)
            {
                Mode = formElement.Attribute.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                ValidatesOnNotifyDataErrors = true
            });
        }

        #endregion

        #endregion
    }
}
