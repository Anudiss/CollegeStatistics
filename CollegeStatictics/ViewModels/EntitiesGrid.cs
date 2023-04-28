using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Windows;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Windows;
using System;
using CollegeStatictics.DataTypes;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CollegeStatictics.ViewModels
{
    public partial class EntitiesGrid<T> : Control where T : class, ITable
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(nameof(SelectedItems), typeof(ICollection<T>), typeof(EntitiesGrid<T>));

        [RelayCommand]
        private async void OpenSelectorDialog()
        {
            SelectedItems = OpenSelectorItemDialog(MainVM.PageBuilders[_itemContainerName]());
        }

        public ICollection<T> SelectedItems
        {
            get => (ICollection<T>)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        private string _itemContainerName;

        public EntitiesGrid(string itemContainerName)
        {
            if (!MainVM.PageBuilders.ContainsKey(itemContainerName))
                throw new ArgumentException($"No itemsContainer with name {itemContainerName}");

            _itemContainerName = itemContainerName;
        }

        public ICollection<T> OpenSelectorItemDialog(ItemsContainer<T> itemsContainer)
        {
            var contentDialog = new DialogWindow()
            {
                Content = itemsContainer,
                ContentTemplate = (DataTemplate)Application.Current.FindResource("ItemsContainerTemplate"),

                PrimaryButtonText = "Выбрать",
                //DefaultButton = ContentDialogButton.Primary,

                SecondaryButtonText = "Отмена"
            };

            itemsContainer.SelectionMode = DataGridSelectionMode.Extended;

            contentDialog.Show();

            if (contentDialog.Result == DialogResult.Secondary)
                return SelectedItems;

            return itemsContainer.SelectedItems?.Cast<T>().ToList() ?? new List<T>();
        }
    }
}
