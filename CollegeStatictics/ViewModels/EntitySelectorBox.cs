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
    public partial class FilteredEntitySelectorBox<T> : Control, IEntitySelectorBox where T : class, ITable, IDeletable, new()
    {
        #region [ Properties ]

        public object? SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(T), typeof(FilteredEntitySelectorBox<T>));

        public bool IsClearable
        {
            get { return (bool)GetValue(IsClearableProperty); }
            set { SetValue(IsClearableProperty, value); }
        }

        public static readonly DependencyProperty IsClearableProperty =
            DependencyProperty.Register("IsClearable", typeof(bool), typeof(FilteredEntitySelectorBox<T>));

        #endregion

        #region [ Commands ]

        [RelayCommand]
        private void OpenSelectorDialog()
        {
            SelectedItem = OpenSelectorItemDialog();
        }

        [RelayCommand]
        private void ClearSelectedItem()
        {
            SelectedItem = default!;
        }

        #endregion

        #region [ Fields ]

        private readonly ISelection<T>? _filter;

        private readonly string _itemContainerName;

        #endregion

        public FilteredEntitySelectorBox( string itemContainerName, ISelection<T> filter )
        {
            if (!MainVM.PageBuilders.ContainsKey(itemContainerName))
                throw new ArgumentException($"No itemsContainer with name {itemContainerName}");

            _itemContainerName = itemContainerName;
            _filter = filter;
        }

        public FilteredEntitySelectorBox( string itemContainerName ) : this(itemContainerName, null)
        { }

        public object? OpenSelectorItemDialog()
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
                return SelectedItem;

            return itemsContainer.SelectedItems?.Cast<T>().FirstOrDefault() ?? SelectedItem;
        }

        object? IEntitySelectorBox.OpenSelectorItemDialog() => throw new NotImplementedException();
    }
}
