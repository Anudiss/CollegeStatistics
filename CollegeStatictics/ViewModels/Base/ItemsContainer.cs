using CollegeStatictics.Database;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ITable = CollegeStatictics.DataTypes.ITable;

namespace CollegeStatictics.ViewModels.Base
{
    public partial class ItemsContainer<T> : ObservableObject, IItemSelector<T> where T : class, ITable
    {
        [ObservableProperty]
        private FilteredObservableCollection<T> items;

        [ObservableProperty]
        private IList selectedItems;

        [ObservableProperty]
        public ObservableCollection<DataGridColumn> columns;

        [RelayCommand]
        private async void OpenDialog(T? item)
        {
            var contentDialog = new RichContentDialog()
            {
                Content = Activator.CreateInstance(_itemDialogType, new[] { item }),
                ContentTemplate = (DataTemplate)Application.Current.FindResource("ItemDialogTemplate"),

                DefaultButton = ContentDialogButton.Primary,

                IsPrimaryButtonEnabled = false,
                PrimaryButtonText = "Сохранить",

                IsSecondaryButtonEnabled = true,
                SecondaryButtonText = "Отмена"
            };

            await ContentDialogMaker.CreateContentDialogAsync(contentDialog, false);

            Items.Refresh();
        }

        private Type _itemDialogType;

        public ItemsContainer(FilteredObservableCollection<T> items, ObservableCollection<DataGridColumn> columns, Type itemDialogType)
        {
            Items = items;
            Columns = columns;
            _itemDialogType = itemDialogType;
        }
    }

    public class ItemsContainerBuilder<T, R> where T : class, ITable
                                             where R : ItemDialog<T>
    {
        private readonly ObservableCollection<T> _sourceCollection;
        private readonly List<IFilter<T>> _filters;
        private readonly List<Searching<T>> _searchings;
        private readonly ObservableCollection<DataGridColumn> _columns;

        public ItemsContainerBuilder(ObservableCollection<T> sourceCollection)
        {
            _sourceCollection = sourceCollection;

            _filters = new();
            _searchings = new();
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

        public ItemsContainer<T> Build() =>
            new(new(_sourceCollection, _filters, _searchings), _columns, typeof(R));
    }
}
