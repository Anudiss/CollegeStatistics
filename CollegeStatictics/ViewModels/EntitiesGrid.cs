using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.DataTypes.Interfaces;
using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Windows;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CollegeStatictics.ViewModels
{
    public partial class EntitiesGrid<T> : Control where T : class, ITable, IDeletable, new()
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(nameof(SelectedItems), typeof(ICollection<T>), typeof(EntitiesGrid<T>));

        [RelayCommand]
        public void OpenSelectorDialog()
        {
            SelectedItems = OpenSelectorItemDialog((ItemsContainer<T>)MainVM.PageBuilders[_itemContainerName]());
        }

        public ICollection<T> SelectedItems
        {
            get => (ICollection<T>)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        private string _itemContainerName;

        private ISelection<T>? _filter;

        public EntitiesGrid(string itemContainerName, ISelection<T>? filter = null)
        {
            if (!MainVM.PageBuilders.ContainsKey(itemContainerName))
                throw new ArgumentException($"No itemsContainer with name {itemContainerName}");

            _itemContainerName = itemContainerName;
            _filter = filter;
        }

        public ICollection<T> OpenSelectorItemDialog(ItemsContainer<T> itemsContainer)
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

            itemsContainer.SelectionMode = DataGridSelectionMode.Extended;

            contentDialog.Show();

            if (contentDialog.Result == DialogResult.Secondary)
                return SelectedItems;

            return itemsContainer.SelectedItems?.Cast<T>().ToList() ?? SelectedItems;
        }
    }
}
