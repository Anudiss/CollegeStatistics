using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.DataTypes.Interfaces;
using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CollegeStatictics.ViewModels
{
    [ObservableObject]
    public partial class EntitySelectorBox<T> : Control where T : class, ITable, IDeletable, new()
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(T), typeof(EntitySelectorBox<T>));

        [RelayCommand]
        private void OpenSelectorDialog()
        {
            SelectedItem = OpenSelectorItemDialog((ItemsContainer<T>)MainVM.PageBuilders[_itemContainerName]());
        }

        public T SelectedItem
        {
            get => (T)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        private readonly ISelection<T>? _filter;

        private readonly string _itemContainerName;

        public EntitySelectorBox(string itemContainerName, ISelection<T> filter)
        {
            if (!MainVM.PageBuilders.ContainsKey(itemContainerName))
                throw new ArgumentException($"No itemsContainer with name {itemContainerName}");

            _itemContainerName = itemContainerName;
            _filter = filter;
        }

        public EntitySelectorBox(string itemContainerName) : this(itemContainerName, null)
        { }

        public T OpenSelectorItemDialog(ItemsContainer<T> itemsContainer)
        {
            if (_filter is not null)
            {
                itemsContainer.Items.Selections.Add(_filter);
                itemsContainer.Items.Refresh();
            }

            var contentDialog = new DialogWindow()
            {
                Content = itemsContainer,
                ContentTemplate = (DataTemplate)Application.Current.FindResource("ItemsContainerTemplate"),

                PrimaryButtonText = "Выбрать",
                //DefaultButton = ContentDialogButton.Primary,

                SecondaryButtonText = "Отмена"
            };

            itemsContainer.SelectionMode = DataGridSelectionMode.Single;

            contentDialog.Show();

            if (contentDialog.Result == DialogResult.Secondary)
                return SelectedItem;

            return itemsContainer.SelectedItems?.Cast<T>().FirstOrDefault() ?? SelectedItem;
        }
    }
}
