using CollegeStatictics.DataTypes;
using CollegeStatictics.Utilities;
using CollegeStatictics.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CollegeStatictics.ViewModels
{
    public partial class EntitySelectorBox<T> : ObservableObject where T : class, ITable
    {
        [RelayCommand]
        private async void OpenSelectorDialog()
        {
            SelectedItems = OpenSelectorDialog(MainVM.Pages[_itemContainerName]());
        }

        [ObservableProperty]
        private IEnumerable<T> selectedItems;

        private string _itemContainerName;

        public EntitySelectorBox(string itemContainerName)
        {
            if (!MainVM.Pages.ContainsKey(itemContainerName))
                throw new ArgumentException($"No itemsContainer with name {itemContainerName}");

            _itemContainerName = itemContainerName;
        }

        public static IEnumerable<T> OpenSelectorDialog(ItemsContainer<T> itemsContainer)
        {
            var contentDialog = new RichContentDialog()
            {
                Content = itemsContainer,
                ContentTemplate = (DataTemplate)Application.Current.FindResource("ItemsContainerTemplate"),

                PrimaryButtonText = "Выбрать",
                DefaultButton = ContentDialogButton.Primary,

                SecondaryButtonText = "Отмена"
            };

            ContentDialogMaker.CreateContentDialog(contentDialog, false);

            return itemsContainer.SelectedItems?.Cast<T>() ?? Enumerable.Empty<T>();
        }
    }
}
