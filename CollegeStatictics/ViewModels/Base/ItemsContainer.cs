using CollegeStatictics.Database;
using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.DataTypes.Interfaces;
using CollegeStatictics.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ITable = CollegeStatictics.DataTypes.ITable;

namespace CollegeStatictics.ViewModels.Base
{
    public partial class ItemsContainer<T> : ObservableValidator, IItemSelector<T> where T : class, ITable, IDeletable, new()
    {
        #region [ Properties ]

        [ObservableProperty]
        private FilteredObservableCollection<T> items = default!;

        [ObservableProperty]
        private IList selectedItems = default!;

        [ObservableProperty]
        private DataGridSelectionMode selectionMode = DataGridSelectionMode.Extended;

        [ObservableProperty]
        private bool canCreate = false;

        [ObservableProperty]
        public ObservableCollection<DataGridColumn> columns = default!;

        partial void OnSelectedItemsChanged(IList value) =>
            RemoveItemsCommand.NotifyCanExecuteChanged();

        #endregion

        #region [ Commands ]

        [RelayCommand]
        private void OpenDialog(T item)
        {
            if (item == null)
                return;

            ItemDialog<T> itemDialog = (ItemDialog<T>)Activator.CreateInstance(_itemDialogType, new[] { item })!;
            var contentDialog = new DialogWindow
            {
                Content = itemDialog,
                ContentTemplate = (DataTemplate)Application.Current.FindResource("ItemDialogTemplate"),

                //DefaultButton = ContentDialogButton.Primary,

                //IsPrimaryButtonEnabled = false,
                PrimaryButtonText = "Сохранить",
                PrimaryButtonCommand = itemDialog.SaveCommand,

                //IsSecondaryButtonEnabled = true,
                SecondaryButtonText = "Отмена",
                SecondaryButtonCommand = itemDialog.CancelCommand,

                CanClose = () => !DatabaseContext.HasChanges(itemDialog.Item)
            };

            contentDialog.Closing += ContentDialogClosingHandler;
            contentDialog.Show();
            contentDialog.Closing -= ContentDialogClosingHandler;

            Items.Refresh();
            Items.UpdateFilters();
        }

        [RelayCommand]
        private void CreateDialog()
        {
            ItemDialog<T> itemDialog = (ItemDialog<T>)Activator.CreateInstance(_itemDialogType, new object[] { null! })!;
            var contentDialog = new DialogWindow
            {
                Content = itemDialog,
                ContentTemplate = (DataTemplate)Application.Current.FindResource("ItemDialogTemplate"),

                //DefaultButton = ContentDialogButton.Primary,

                //IsPrimaryButtonEnabled = false,
                PrimaryButtonText = "Сохранить",
                PrimaryButtonCommand = itemDialog.SaveCommand,

                //IsSecondaryButtonEnabled = true,
                SecondaryButtonText = "Отмена",
                SecondaryButtonCommand = itemDialog.CancelCommand,

                CanClose = () => !DatabaseContext.Entities.ChangeTracker.HasChanges()
            };

            contentDialog.Closing += ContentDialogClosingHandler;
            contentDialog.Show();
            contentDialog.Closing -= ContentDialogClosingHandler;

            Items.Refresh();
            Items.UpdateFilters();
        }

        [RelayCommand(CanExecute = nameof(CanRemoveItems))]
        private void RemoveItems()
        {
            if (!CanRemoveItems())
            {
                RemoveItemsCommand.NotifyCanExecuteChanged();
                return;
            }

            var acceptDialog = new DialogWindow
            {
                Content = new TextBlock()
                {
                    Text = $"Хотите удалить {SelectedItems.Count} элемент{SelectedItems.Count switch
                    {
                        1 => "",
                        < 5 => "а",
                        _ => "ов"
                    }}?",
                    FontSize = 16
                },
                PrimaryButtonText = "Да",
                SecondaryButtonText = "Нет"
            };

            acceptDialog.Show();
            if (acceptDialog.Result == DialogResult.Secondary)
                return;

            var itemsToDelete = SelectedItems.Cast<T>();
            foreach (var item in itemsToDelete)
                item.MarkToDelete();

            DatabaseContext.Entities.SaveChanges();

            Items.Refresh();
            Items.UpdateFilters();
        }

        private bool CanRemoveItems() => !(SelectedItems == null || SelectedItems.Count == 0);

        #endregion

        #region [ Fields ]

        private Type _itemDialogType;

        #endregion

        #region [ Initializing ]

        public ItemsContainer(FilteredObservableCollection<T> items, ObservableCollection<DataGridColumn> columns, Type itemDialogType)
        {
            Items = items;
            Columns = columns;
            _itemDialogType = itemDialogType;
        }

        #endregion

        #region [ Private methods ]

        private void ContentDialogClosingHandler(object? sender, CancelEventArgs e)
        {
            if (DatabaseContext.Entities.ChangeTracker.HasChanges() && (sender as DialogWindow)!.Result == DialogResult.None)
            {
                var acceptDialog = new DialogWindow
                {
                    Content = "Сохранить изменения?",
                    PrimaryButtonText = "Да",
                    SecondaryButtonText = "Нет",
                    TertiaryButtonText = "Отмена",
                };

                e.Cancel = false;
                acceptDialog.Show();

                if (acceptDialog.Result == DialogResult.Primary)
                    DatabaseContext.Entities.SaveChanges();
                else if (acceptDialog.Result == DialogResult.Secondary)
                    DatabaseContext.CancelChanges();
                else
                    e.Cancel = true;
            }
        }

        #endregion
    }

    #region [ Data types ]

    public class ItemsContainerBuilder<T, R> where T : class, ITable, IDeletable, new()
                                             where R : ItemDialog<T>
    {
        private readonly ObservableCollection<T> _sourceCollection;
        private readonly List<ISelection<T>> _filters;
        private readonly List<Searching<T>> _searchings;
        private readonly List<Grouping<T>> _groupings;
        private readonly ObservableCollection<DataGridColumn> _columns;
        private bool canCreate = true;

        public ItemsContainerBuilder(ObservableCollection<T> sourceCollection)
        {
            _sourceCollection = sourceCollection;

            _filters = new();
            _searchings = new();
            _groupings = new();
            _columns = new();
        }

        public ItemsContainerBuilder() : this(LoadItems())
        { }

        private static ObservableCollection<T> LoadItems()
        {
            var values = DatabaseContext.Entities.Set<T>();
            values.Load();

            return values.Local.ToObservableCollection();
        }

        public ItemsContainerBuilder<T, R> AddFilter(ISelection<T> filter)
        {
            _filters.Add(filter);
            return this;
        }

        public ItemsContainerBuilder<T, R> AddSearching(Searching<T> searching)
        {
            _searchings.Add(searching);
            return this;
        }

        public ItemsContainerBuilder<T, R> AddTextBoxColumn(string propertyPath, string header, string stringFormat)
        {
            _columns.Add(new DataGridTextColumn
            {
                Header = header,
                Binding = new Binding(propertyPath)
                {
                    Mode = BindingMode.OneWay,
                    StringFormat = stringFormat
                }
            });

            return this;
        }

        public ItemsContainerBuilder<T, R> AddTextBoxColumn(string propertyPath, string header)
            => AddTextBoxColumn(propertyPath, header, "{0}");

        public ItemsContainerBuilder<T, R> AddCheckBoxColumn(string propertyPath, string header)
        {
            _columns.Add(new DataGridCheckBoxColumn
            {
                Header = header,
                IsReadOnly = true,
                Binding = new Binding(propertyPath)
                {
                    Mode = BindingMode.OneWay
                }
            });

            return this;
        }

        public ItemsContainerBuilder<T, R> AddColumn(string propertyPath, string header, string stringFormat)
        {
            _columns.Add(new DataGridTextColumn()
            {
                Header = header,
                Binding = new Binding(propertyPath)
                {
                    Mode = BindingMode.OneWay,
                    StringFormat = stringFormat
                }
            });

            return this;
        }

        public ItemsContainerBuilder<T, R> BanCreate()
        {
            canCreate = false;
            return this;
        }

        public ItemsContainerBuilder<T, R> AddGrouping(Grouping<T> grouping)
        {
            _groupings.Add(grouping);
            return this;
        }

        public ItemsContainer<T> Build() =>
            new(new(_sourceCollection, _filters, _searchings, _groupings), _columns, typeof(R))
            {
                CanCreate = canCreate
            };
    }

    #endregion
}
