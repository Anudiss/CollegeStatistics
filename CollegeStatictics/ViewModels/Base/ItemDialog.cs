using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.ViewModels.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using System;
using System.Collections.Generic;
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

        /*
         * [FormElement(DefaultValue = "Имя")]
         * public string Name
         * {
         *     get => _item.Name;
         *     set
         *     {
         *         _item.Name = value;
         *         OnPropertyChanged();
         *         ValidateProperty(value);
         *     }
         * }
         */

        public IEnumerable<FrameworkElement> ViewElements => CreateViewElements();

        [Label("Уникальный идентификатор")]
        [FormElement(IsReadOnly = true)]
        public int Id
        {
            get
            {
                return _item.Id != 0 ? _item.Id : ((DatabaseContext.Entities.Set<T>().Local.LastOrDefault()?.Id + 1) ?? 1);
            }
        }

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
                    _ => throw new NotSupportedException("Invalid element type")
                };
            }
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
                    Margin = new Thickness(0, 0, 0, 10)
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
                    Margin = new Thickness(0, 0, 0, 10)
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
            values!.Load();

            var groupBox = new GroupBox();
            
            var labelAttribute = formElement.property.GetCustomAttribute<LabelAttribute>();
            if (labelAttribute != null)
                groupBox.Header = labelAttribute.Label;

            var stackPanel = new StackPanel();
            groupBox.Content = stackPanel;

            if (formElement.property.GetValue(this) == null)
                formElement.property.SetValue(this, values.Local.FirstOrDefault());

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
