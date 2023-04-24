using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace CollegeStatictics.Controls.Extensions
{
    public class ExtendedDataGrid : DataGrid
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(nameof(SelectedItems), typeof(IList), typeof(ExtendedDataGrid), new(default(IList)));

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register(nameof(Columns), typeof(ObservableCollection<DataGridColumn>), typeof(ExtendedDataGrid), new(OnColumnsChanged));

        public new IList SelectedItems
        {
            get => (IList)GetValue(SelectedItemsProperty);
            set => throw new System.Exception("Selected items is readonly property. To bind it use Mode=OneWayToSource");
        }

        public new ObservableCollection<DataGridColumn> Columns
        {
            get => (ObservableCollection<DataGridColumn>)GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            SetValue(SelectedItemsProperty, base.SelectedItems);
        }

        public static void OnColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)d;
            if (e.NewValue is not ObservableCollection<DataGridColumn> columns)
                return;

            dataGrid.Columns.Clear();
            foreach (DataGridColumn column in columns)
                dataGrid.Columns.Add(column);
        }
    }
}
