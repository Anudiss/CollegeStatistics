using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.Utilities;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;

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

            if (_item.Id == 0)
                DatabaseContext.Entities.Set<T>().Local.Add(_item);

            DatabaseContext.Entities.SaveChanges();
        }
        
        private bool CanSave() => !HasErrors;

        [RelayCommand]
        private void Cancel() => DatabaseContext.CancelChanges();

        #endregion

        #region [ Properties ]

        protected readonly T _item;

        public IEnumerable<FrameworkElement> ViewElements => CreateViewElements();

        #endregion

        #region [ Initializing ]

        public ItemDialog(T? item)
        {
            _item = item ?? CreateDefaultItem();
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
                yield return formElement.attribute.ElementType switch
                {
                    ElementType.TextBox => CreateTextBox(formElement),
                    ElementType.EntitySelectorBox => CreateEntitySelectorBox(formElement),
                    ElementType.RadioButton => CreateRadioButtonList(formElement),
                    ElementType.Subtable => CreateSubtableElement(formElement),
                    ElementType.Timetable => CreateTimetableElement(formElement),
                    _ => throw new NotSupportedException("Invalid element type")
                };
            }
        }

        private FrameworkElement CreateTimetableElement((PropertyInfo property, FormElementAttribute attribute) formElement)
        {
            var dataGrid = new DataGrid()
            {
                Style = (Style)Application.Current.FindResource("TimetableDataGridStyle"),
                
                ItemsSource = TimetableRecordElement.GetRecordElements(_item as Timetable)
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

            return new Border()
            {
                Child = dataGrid
            };
        }

        private FrameworkElement CreateSubtableElement((PropertyInfo property, FormElementAttribute attribute) formElement)
        {
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

            dataGrid.ItemsSource = (dynamic)formElement.property.GetValue(this);

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

            EntitiesGridFormElementAttribute attribute = (EntitiesGridFormElementAttribute)formElement.attribute;

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
                dataGrid.ItemsSource = (dynamic)formElement.property.GetValue(this);
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
                dataGrid.ItemsSource = (dynamic)formElement.property.GetValue(this);
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
                IsReadOnly = formElement.attribute.IsReadOnly
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

        private static FrameworkElement CreateEntitySelectorBox((PropertyInfo property, FormElementAttribute attribute) formElement)
        {
            EntitySelectorFormElementAttribute attribute = (EntitySelectorFormElementAttribute)formElement.attribute;

            var entitySelectorBoxType = Type.GetType("CollegeStatictics.ViewModels.EntitySelectorBox`1")!
                                            .MakeGenericType(formElement.property.PropertyType);

            var entitySelectorBox = Activator.CreateInstance(entitySelectorBoxType, new[] { attribute.ItemContainerName });
            var dp = (DependencyProperty)entitySelectorBoxType.GetField("SelectedItemProperty").GetValue(entitySelectorBox);

            ((Control)entitySelectorBox).SetBinding(dp, new Binding(formElement.property.Name)
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

            foreach (var property in properties.Where(p => !string.IsNullOrEmpty(p.GetCustomAttribute<FormElementAttribute>()!.DefaultValue?.Trim())))
            {
                PropertyInfo? itemProperty = itemType.GetProperty(property.Name);
                var defaultValuePath = property.GetCustomAttribute<FormElementAttribute>()!.DefaultValue;
                if (itemProperty == null)
                    throw new NotSupportedException($"Type {typeof(T).Name} must contains property '{property.Name}'");
                itemProperty.SetValue(itemInstance, GetType()!.GetProperty(defaultValuePath)?.GetValue(this));
            }

            return itemInstance;
        }

        #endregion
    }
}
