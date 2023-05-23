using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.DataTypes.Interfaces;
using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Windows;

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CollegeStatictics.ViewModels
{
    public class FilteredEntitySelectorBox<T> : EntitySelectorBox<T> where T : class, ITable, IDeletable, new()
    {
        private readonly ISelection<T>? _filter;

        private readonly string _itemContainerName;

        public FilteredEntitySelectorBox( string itemContainerName, ISelection<T>? filter )
        {
            if (!MainVM.PageBuilders.ContainsKey(itemContainerName))
                throw new ArgumentException($"No itemsContainer with name {itemContainerName}");

            _itemContainerName = itemContainerName;
            _filter = filter;
        }

        public FilteredEntitySelectorBox( string itemContainerName ) : this(itemContainerName, null)
        { }

        public override T OpenSelectorItemDialog()
        {
            ItemsContainer<T> itemsContainer = (ItemsContainer<T>)MainVM.PageBuilders[_itemContainerName]();

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
                return (T)SelectedItem;

            return itemsContainer.SelectedItems?.Cast<T>().FirstOrDefault() ?? (T)SelectedItem;
        }
    }
}
