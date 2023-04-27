using CollegeStatictics.DataTypes;
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
    public partial class EntitySelectorBox<T> : Control where T : class, ITable
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(T), typeof(EntitySelectorBox<T>));

        [RelayCommand]
        private async void OpenSelectorDialog()
        {
            SelectedItem = OpenSelectorItemDialog(MainVM.Pages[_itemContainerName]());
        }

        public T SelectedItem
        {
            get => (T)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        private string _itemContainerName;

        public EntitySelectorBox(string itemContainerName)
        {
            if (!MainVM.Pages.ContainsKey(itemContainerName))
                throw new ArgumentException($"No itemsContainer with name {itemContainerName}");

            _itemContainerName = itemContainerName;
        }

        public static T OpenSelectorItemDialog(ItemsContainer<T> itemsContainer)
        {
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

            return itemsContainer.SelectedItems?.Cast<T>().FirstOrDefault() ?? default(T);
        }
    }
}
