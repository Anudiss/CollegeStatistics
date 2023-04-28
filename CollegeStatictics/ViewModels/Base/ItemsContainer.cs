using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.Utilities;
using CollegeStatictics.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ITable = CollegeStatictics.DataTypes.ITable;

namespace CollegeStatictics.ViewModels.Base
{
    public partial class ItemsContainer<T> : ObservableValidator, IItemSelector<T> where T : class, ITable
    {
        #region [ Properties ]

        [ObservableProperty]
        private FilteredObservableCollection<T> items;

        [ObservableProperty]
        private IList selectedItems;

        [ObservableProperty]
        private DataGridSelectionMode selectionMode = DataGridSelectionMode.Single;

        [ObservableProperty]
        public ObservableCollection<DataGridColumn> columns;

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

                CanClose = () => !DatabaseContext.Entities.ChangeTracker.HasChanges()
            };

            contentDialog.Closing += ContentDialogClosingHandler;
            contentDialog.Show();
            contentDialog.Closing -= ContentDialogClosingHandler;

            Items.Refresh();
        }

        [RelayCommand]
        private void CreateDialog()
        {
            ItemDialog<T> itemDialog = (ItemDialog<T>)Activator.CreateInstance(_itemDialogType, new object[] { null })!;
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
        }

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

    public class ItemsContainerBuilder<T, R> where T : class, ITable
                                             where R : ItemDialog<T>
    {
        private readonly ObservableCollection<T> _sourceCollection;
        private readonly List<IFilter<T>> _filters;
        private readonly List<Searching<T>> _searchings;
        private readonly List<Grouping<T>> _groupings;
        private readonly ObservableCollection<DataGridColumn> _columns;

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

        public ItemsContainerBuilder<T, R> AddFilter(IFilter<T> filter)
        {
            _filters.Add(filter);
            return this;
        }

        public ItemsContainerBuilder<T, R> AddSearching(Searching<T> searching)
        {
            _searchings.Add(searching);
            return this;
        }

        public ItemsContainerBuilder<T, R> AddColumn(string propertyPath, string header)
        {
            _columns.Add(new DataGridTextColumn()
            {
                Header = header,
                Binding = new Binding(propertyPath)
                {
                    Mode = BindingMode.OneWay
                }
            });

            return this;
        }

        public ItemsContainerBuilder<T, R> AddGrouping(Grouping<T> grouping)
        {
            _groupings.Add(grouping);
            return this;
        }

        public ItemsContainer<T> Build() =>
            new(new(_sourceCollection, _filters, _searchings, _groupings), _columns, typeof(R));
    }

    #endregion
}
