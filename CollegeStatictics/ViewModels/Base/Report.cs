using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.DataTypes.Interfaces;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;

using ModernWpf.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace CollegeStatictics.ViewModels.Base;

public partial class Report<T> : ObservableValidator, IReport where T : class, new()
{
    /*
     * |=====Пример, отчёт "Остаток часов"=====|
     * 
     * T: StudyPlanRecord
     * 
     * Report<T>
     * ├─[ Selections ]
     * │ ├─[     StudyPlan     ] = PropertyAccessor<T, StudyPlan>(Expression: record => record.StudyPlan)
     * │ └─[   Teacher,Group   ] = PropertyAccessor<T, Timetable>(Expression: record => record.StudyPlan[Teacher, Group])
     * │                          └─────────────────────[ Cannot do something with indexer parameters.        ]─────────────────────┘
     * │                                                [ It's need to create dynamic method with parameters. ]
     * │
     * │                     ┌───────────────────────────────────────────────────────────────────────────────────────────────────────────┐
     * │                     │                                          Альтернативные решения                                           │
     * │                     ├───────┬───────────────────────────────────────────────────────────────────────────────────────────────────┤
     * │                     │ Номер │ Описание                                                                                          │
     * │                     ├───────┼───────────────────────────────────────────────────────────────────────────────────────────────────┤
     * │                     │   1   │ Добавить в Report источник данных                                                                 │
     * │                     │       │ Например, Report<Timetable>(itemsSource: timetable => timetable.StudyPlan.StudyPlanRecords);      │
     * │                     │       │ Теперь в Report будет:                                                                            │
     * │                     │       │     1) PropertyAccessor<Timetable, IEnumerable<StudyPlanRecord>>                                  │
     * │                     │       │     2)                                   │
     * │                     ├───────┼───────────────────────────────────────────────────────────────────────────────────────────────────┤
     * │                                                
     * │
     * ├─[ Selection values ]
     * │ ├─[ Teacher   - Плаксин Н.А.     ]
     * │ ├─[ StudyPlan - Учебный план №12 ]
     * │ └─[ Group     - 323              ]
     * 
     * ┌───────────────────────────────────────────────────┐
     * │┌────────────[ Entity selector Boxes ]────────────┐│
     * │┌─[ Teacher ]────┐┌─[ StudyPlan ]────┐┌─[ Group ]─┐│
     * ││  Плаксин Н.А.  ││ Учебный план №12 ││    323    ││
     * │└────────────────┘└──────────────────┘└───────────┘│
     * ├───────────────────────────────────────────────────┤
     * │┌──────────────────────────┬──────────┬───────────┐│
     * ││ Тема                     │ Выделено │ Проведено ││
     * │├──────────────────────────┼──────────┼───────────┤│
     * ││ Введение                 │    40    │    30     ││
     * │├──────────────────────────┼──────────┼───────────┤│
     * ││ Практическая работа №1   │    20    │    20     ││
     * │├──────────────────────────┼──────────┼───────────┤│
     * ││ Практическая работа №2   │    20    │    10     ││
     * │├──────────────────────────┼──────────┼───────────┤│
     * ││ Итого                    │    80    │    60     ││
     * │└──────────────────────────┴──────────┴───────────┘│
     * └───────────────────────────────────────────────────┘
     */

    public string? Title { get; }

    public bool HasFinalRow { get; } = true;

    public bool HasFinalColumn { get; } = false;

    public FilteredObservableCollection<T> SourceCollection { get; }

    public IEnumerable<IPropertyAccessor<T>> Selections { get; }

    public IEnumerable<ReportColumn<T>> Columns { get; }

    public FrameworkElement View => Generate();

    public DataTemplate ContentTemplate => (DataTemplate)Application.Current.FindResource("ReportTemplate");

    private readonly object[] _selectionValues;

    public Report( string? title, IEnumerable<T> sourceColletion, IEnumerable<ReportColumn<T>> columns, IEnumerable<IPropertyAccessor<T>> propertyAccessors, bool hasFinalRow, bool hasFinalColumn )
    {
        Title = title ?? $"Отчёт по {typeof(T).Name}";
        HasFinalRow = hasFinalRow;
        HasFinalColumn = hasFinalColumn;

        SourceCollection = new(sourceColletion.ToList(), Enumerable.Empty<ISelection<T>>(), Enumerable.Empty<Searching<T>>(), Enumerable.Empty<Grouping<T>>());
        Columns = columns;
        Selections = propertyAccessors;

        DatabaseContext.Entities.Set<T>().Load();
    }

    [RelayCommand]
    public void Refresh() => OnPropertyChanged(nameof(IReport.View));

    public FrameworkElement Generate()
    {
        var dataGrid = new DataGrid
        {
            AutoGenerateColumns = false,
            CanUserAddRows = false,
            CanUserDeleteRows = false,
            CanUserReorderColumns = false,
            CanUserResizeRows = false,
            CanUserResizeColumns = false,
            CanUserSortColumns = false,

            IsReadOnly = true
        };

        dataGrid.Columns.Clear();

        foreach ((DataGridColumn column, Func<T, object> getter) in Columns)
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = column.Header,
                Binding = ( (DataGridBoundColumn)column ).Binding
            });

        var reportRows = SourceCollection.View.Cast<T>().Select(item => new ReportRow<T>(item, Columns.Select(column => column.getter).ToArray()));
        var rows = reportRows.Cast<object>().Append(new ReportRow<string>("Итого", Columns.Select(
            (Func<ReportColumn<T>, int, Func<string, object>>)( (column, i) => { return (string _) => reportRows.Sum(row => (int)row[i]); } )).ToArray()));

        foreach (dynamic row in rows.Take(rows.Count() - 1))
            dataGrid.Items.Add(new DataGridRow()
            {
                Header = row.RowItem,
                Item = row
            });

        dynamic lastRow = rows.Last();
        dataGrid.Items.Add(new DataGridRow()
        {
            Header = lastRow.RowItem,
            Item = lastRow,
            Background = Brushes.LightBlue
        });

        return dataGrid;
    }
}

public class ReportBuilder<T> where T : class, new()
{
    private readonly IEnumerable<T>             _sourceCollection;
    private readonly List<ReportColumn<T>>      _columns;
    private readonly List<IPropertyAccessor<T>> _propertyAccessors;
    private readonly Func<IEnumerable<T>, int>  _finalFunction;
    private          string?                    _title = null;
    private          bool                       _hasFinalRow, _hasFinalColumn;

    public ReportBuilder( IEnumerable<T> sourceCollection )
    {
        _sourceCollection = sourceCollection;
        _propertyAccessors = new();
        _columns = new();
    }

    public ReportBuilder<T> SetTitle( string title )
    {
        _title = title;
        return this;
    }

    public ReportBuilder<T> HasFinalRow()
    {
        _hasFinalRow = true;
        return this;
    }
    public ReportBuilder<T> HasFinalColumn()
    {
        _hasFinalColumn = true;
        return this;
    }

    public ReportBuilder<T> AddSelection<TProperty>( Expression<Func<T, TProperty>> propertyExpression )
    {
        /*
         * Selection for StudyPlanRecord -> StudyPlan -> Subject -> Name
         * 
         * T:           StudyPlanRecord
         * TProperty:   string
         * 
         * Expression:  record =>     record    .  StudyPlan   .   Subject    .     Name
         *                       └─[  Member  ]─┴─[  Member  ]─┴─[  Member  ]─┴─[  Member  ]─┘
         *                                                                    └───[ Body ]────┘
         *                                                     └───────────[ Body ]───────────┘
         *                                      └──────────────────[ Body ]───────────────────┘
         *                      └──────────────────────────[ Body ]───────────────────────────┘
         *
         * Return:      PropertyAccessor<T, TProperty>
         *              │
         *              ├─PropertyChain: Property of type IEnumerable<PropertyInfo>, that contains Properties used in expression
         *              │ ├─[  StudyPlan  ]
         *              │ ├─[   Subject   ]
         *              │ └─[    Name     ]
         *              │
         *              ├─GetValue(T record): Method that get value of type TProperty from T by PropertyChain
         *              │ ├─source   = record    -> StudyPlan // Getting value of [ StudyPlan ] property from [  record   ] object
         *              │ ├─source   = StudyPlan -> Subject   // Getting value of [  Subject  ] property from [ StudyPlan ] object
         *              │ ├─source   = Subject   -> Name      // Getting value of [   Name    ] property from [  Subject  ] object
         *              │ └─return     source
         *              │
         *              └─SetValue(T record, TProperty value): Method that set value of type TProperty in T by PropertyChain
         *                ├─source   = record    -> StudyPlan // Getting value of [ StudyPlan ] property from [  record   ] object
         *                ├─source   = StudyPlan -> Subject   // Getting value of [  Subject  ] property from [ StudyPlan ] object
         *                │
         *                └─source -> Name = value            // Setting value in [   Name    ] property from [  Subject  ] object
         */

        _propertyAccessors.Add(new PropertyAccessor<T, TProperty>(propertyExpression));
        return this;
    }

    public ReportBuilder<T> AddColumn( string header, Func<T, object> getter )
    {
        _columns.Add((new DataGridTextColumn()
        {
            Header = header,
            Binding = new Binding($"[{_columns.Count}]")
            {
                Mode = BindingMode.OneWay
            },

            IsReadOnly = true
        }, getter));
        return this;
    }

    public Report<T> Build() =>
        new(
            _title,
            _sourceCollection,
            _columns,
            _propertyAccessors,
            _hasFinalRow,
            _hasFinalColumn);
}

public class ReportRow<T>
{
    private readonly Func<T, object>[] _getters;
    public T RowItem { get; }

    public object this[int index] => _getters[index](RowItem);

    public ReportRow( T item, params Func<T, object>[] getters )
    {
        RowItem = item;
        _getters = getters;
    }
}

public enum FinalFunction
{
    None = 0,
    Sum,
    Average,
    Min,
    Max,
}

public record struct ReportColumn<T>( DataGridColumn column, Func<T, object> getter )
{
    public static implicit operator (DataGridColumn column, Func<T, object> getter)( ReportColumn<T> value ) => (value.column, value.getter);
    public static implicit operator ReportColumn<T>( (DataGridColumn column, Func<T, object> getter) value ) => new ReportColumn<T>(value.column, value.getter);
}