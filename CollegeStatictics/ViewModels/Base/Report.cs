﻿using CollegeStatictics.Database;
using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.DataTypes.Interfaces;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CollegeStatictics.ViewModels.Base;

public partial class Report<T> : ObservableValidator, IReport where T : class, new()
{
    /*
     * ◄——————Пример, отчёт "Остаток часов"——————►
     * 
     * T: StudyPlanRecord
     * 
     * Report<T>
     * ├─┬Entity selector boxes                ┌──────────────────────┐
     * │ ├───EntitySelectorBox<StudyPlan>───┐  │ EntitySelectorBox<T> │
     * │ │                                  │  ├──────────────────────┤
     * │ ├────EntitySelectorBox<Teacher>────┼─ᐅ│  T SelectedItem      │
     * │ │                                  │  │  SelectedItemChanged │
     * │ └─────EntitySelectorBox<Group>─────┘  └──────────────────────┘
     * │
     * ├─┬Columns
     * │ ├─[ DataGridTextColumn ] ─→ [  Выделено ]
     * │ └─[ DataGridTextColumn ] ─→ [ Проведено ]
     * │
     * ├──Generate()───────────────┐
     * │          ┌────────────────┴────────────────┐
     * │          │ Получить отфильтрованные данные │
     * │          └────────────────┬────────────────┘
     * │                           ↓     
     * │              ┌────────────┴────────────┐
     * │              │ Привести их к ReportRow │
     * │              └────────────┬────────────┘
     * │                           ↓     
     * │           ┌───────────────┴──────────────┐
     * │           │ Создать DataGrid с колонками │
     * │           └───────────────┬──────────────┘
     * │
     * │
     * │
     * │
     * │
     * │
     * │
     * │
     * ├─┬Selections
     * │ ├─[     StudyPlan     ] = PropertyAccessor<T, StudyPlan>(Expression: record => record.StudyPlan)                  ┌─> (Teacher, Group, Timetable) => StudyPlan[Teacher, Group]
     * │ └─[   Teacher,Group   ] = PropertyAccessor<T, Timetable>(Expression: record => record.StudyPlan[Teacher, Group])──┤
     * │                          │               ┌                                                     ┐                │ └─> (Teacher, Group, Timetable) => StudyPlan[Teacher, Group] = Timetable
     * │                          ├───────────────┤     Cannot do something with indexer parameters     ├────────────────┤
     * │                          └───────────────┤ It's need to create dynamic method with parameters. ├────────────────┘
     * │                                          └                                                     ┘
     * │                     ┌───────────────────────────────────────────────────────────────────────────────────────────────────────────┐
     * │                     │                                          Альтернативные решения                                           │
     * │                     ├───────┬───────────────────────────────────────────────────────────────────────────────────────────────────┤
     * │                     │ Номер │ Описание                                                                                          │
     * │                     ├───────┼───────────────────────────────────────────────────────────────────────────────────────────────────┤
     * │                     │       │ Добавить в Report источник данных                                                                 │
     * │                     │   1   │ Например, Report<StudyPlanRecord>(timetable => timetable.StudyPlan.StudyPlanRecords)              │
     * │                     │       │ Report: ReportItemsSource<Timetable>                                                              │
     * │                     ├───────┼───────────────────────────────────────────────────────────────────────────────────────────────────┤
     * │                     │       │                                                                                                   │
     * │                   + │   2   │ Создать getter и setter методы для индексера                                                      │
     * │                     │       │                                                                                                   │
     * │                     └───────┴───────────────────────────────────────────────────────────────────────────────────────────────────┘
     * │                                                
     * │
     * └─┬Selection values ←─ Dictionary<Type, object>
     *   ├─[ Teacher   - Плаксин Н.А.     ]
     *   ├─[ StudyPlan - Учебный план №12 ]
     *   └─[ Group     - 323              ]
     * 
     * ┌────────────────────────────────────────────────────┐
     * │┌────────────[ Entity selector Boxes ]─────────────┐│
     * │├─────Teacher─────┐┌────Study Plan────┐┌───Group───┤│
     * ││  Плаксин Н. А.  ││ Учебный план №12 ││    323    ││
     * │└─────────────────┘└──────────────────┘└───────────┘│
     * ├────────────────────────────────────────────────────┤
     * │┌───────────────────────────┬──────────┬───────────┐│
     * ││ Тема                      │ Выделено │ Проведено ││
     * │├───────────────────────────┼──────────┼───────────┤│
     * ││ Введение                  │    40    │    30     ││
     * │├───────────────────────────┼──────────┼───────────┤│
     * ││ Практическая работа №1    │    20    │    20     ││
     * │├───────────────────────────┼──────────┼───────────┤│
     * ││ Практическая работа №2    │    20    │    10     ││
     * │├───────────────────────────┼──────────┼───────────┤│
     * ││ Итого                     │    80    │    60     ││
     * │└───────────────────────────┴──────────┴───────────┘│
     * └────────────────────────────────────────────────────┘
     */

    public string? Title { get; }

    public bool HasFinalRow { get; } = true;

    public bool HasFinalColumn { get; } = false;

    public IEnumerable<ISelection<T>> Selections { get; }
    public IEnumerable<FrameworkElement> Elements { get; }
    public IEnumerable<IEntitySelectorBox> SelectionElements { get; }
    public IEnumerable<FrameworkElement> SelectionContainers { get; }
    public DatePicker[]? DatePickers { get; }

    public IPropertyAccessor<T>[] PropertyAccessors { get; }

    public IEnumerable<Column> Columns { get; }

    public Func<IEnumerable<double>, double> FinalFunction { get; }
    public Func<T, object>? ColumnGetter { get; }
    public Func<T, object?>? ValueGetter { get; }
    public Func<T, object>? Grouping { get; }
    public Func<T, DateTime>? DateGetter { get; }

    public FrameworkElement View { get; private set; }

    public DataTemplate ContentTemplate => (DataTemplate)Application.Current.FindResource("ReportTemplate");

    public Report( string? title, IEnumerable<Column> columns, List<IPropertyAccessor<T>> propertyAccessors, Dictionary<Type, string> labels,  IEnumerable<ISelection<T>> selections, bool hasFinalRow, bool hasFinalColumn, Func<IEnumerable<double>, double> finalFunction, Func<T, object>? columnHeaderGetter, Func<T, object?>? valueGetter, Func<T, object>? grouping, Func<T, DateTime>? dateGetter )
    {
        Title = title ?? $"Отчёт по {typeof(T).Name}";
        HasFinalRow = hasFinalRow;
        HasFinalColumn = hasFinalColumn;
        FinalFunction = finalFunction;
        ColumnGetter = columnHeaderGetter;
        ValueGetter = valueGetter;
        Grouping = grouping;

        PropertyAccessors = propertyAccessors.ToArray();

        Selections = selections;
        SelectionElements = CreateEntitySelectorBoxes().ToArray();
        SelectionContainers = SelectionElements.Select(entitySelectorBox =>
        {
            var stackPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical
            };

            var contentControl = new ContentControl()
            {
                Content = entitySelectorBox,
                ContentTemplate = (DataTemplate)Application.Current.FindResource("EntitySelectorBoxTemplate"),
                MinWidth = 200
            };

            if (labels.TryGetValue(entitySelectorBox.GetType().GetGenericArguments()[0], out string? label))
                stackPanel.Children.Add(new Label()
                {
                    Content = label,
                    FontSize = 12,
                    FontWeight = FontWeights.Medium,
                    Margin = new Thickness(0, 0, 0, 5),
                    Target = contentControl,
                });

            stackPanel.Children.Add(contentControl);

            return stackPanel;
        });

        Columns = columns;
        DateGetter = dateGetter;
        if (dateGetter is not null)
        {
            DatePickers = new DatePicker[2];

            var beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DatePickers[0] = new DatePicker()
            {
                SelectedDate = beginDate
            };

            DatePickers[1] = new DatePicker()
            {
                SelectedDate = beginDate.AddMonths(1)
            };
        }
    }

    private IEnumerable<IEntitySelectorBox> CreateEntitySelectorBoxes()
    {
        foreach (Type type in PropertyAccessors.SelectMany(pair => pair.Parameters.Any() ? pair.Parameters : new[] { pair.GetType().GetGenericArguments()[1] }).Except(new[] { typeof(DateTime) }))
            yield return CreateEntitySelectorBoxFromType(type);
    }

    private static IEntitySelectorBox CreateEntitySelectorBoxFromType( Type type )
    {
        var entitySelectorBoxType = typeof(FilteredEntitySelectorBox<>).MakeGenericType(type);
        IEntitySelectorBox entitySelectorBoxInstance = (IEntitySelectorBox)Activator.CreateInstance(entitySelectorBoxType, new object?[] { MainVM.GetViewByType(type), null })!;
        ( (dynamic)entitySelectorBoxInstance ).IsClearable = true;

        return entitySelectorBoxInstance;
    }

    [RelayCommand]
    public void Refresh()
    {
        View = Generate();
        OnPropertyChanged(nameof(View));
    }

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

            ColumnHeaderStyle = (Style)Application.Current.FindResource("DataGridColumnHeaderStyle"),

            GridLinesVisibility = DataGridGridLinesVisibility.All,

            IsReadOnly = true
        };

        // Getting values
        DatabaseContext.Entities.Set<T>().Load();
        var values = DatabaseContext.Entities.Set<T>().Local;

        // Filtering it
        List<T> filteredValues = values.Where(value => Selections.All(s => s.IsAccepted(value)))
                                       .Where(FilterReportValue)
                                       .ToList();


        var columns = Columns.ToList();

        if (ColumnGetter != null)
        {
            var headers = filteredValues.Select(value => ColumnGetter(value)).Distinct();

            headers.ForEach(header => columns.Add(new BindedColumn(header.ToString()!, item => ValueGetter((T)item), item => ColumnGetter((T)item) == header)));
        }

        // Generating dataGrid
        foreach (var column in columns.OrderBy(c => c.Header))
        {
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = column.Header,
                Binding = new Binding($"[{column.Header}]")
                {
                    Mode = BindingMode.OneWay,
                    StringFormat = "{0:0.##}",
                    TargetNullValue = "-"
                },
            });
        }

        if (HasFinalColumn)
        {
            var columnsCopy = columns.ToArray();
            var finalColumn = new Column("Итого", item => FinalFunction(columnsCopy.Select(c => c.ValueGetter(item)).OfType<double>()));
            columns.Add(finalColumn);

            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = finalColumn.Header,
                Binding = new Binding($"[{finalColumn.Header}]")
                {
                    Mode = BindingMode.OneWay,
                    StringFormat = "{0:0.##}",
                    TargetNullValue = "-"
                }
            });
        }

        // Casting to row
        List<Row> rows;
        if (Grouping == null)
            rows = filteredValues.Select(value => Row.CreateRowFromColumns(value, columns)).ToList();
        else
        {
            rows = filteredValues.GroupBy(Grouping)
                                 .Select(group => {
                                     var values = columns.ToDictionary(c => c.Header, c =>
                                     {
                                         double v = FinalFunction(group.Select(g => (double?)c.ValueGetter(g)).Where(e => e != 0 && e is not null).Cast<double>().DefaultIfEmpty());
                                         return (object?)(v == 0 ? null : v);
                                     });
                                     if (HasFinalColumn)
                                         values["Итого"] = FinalFunction(values.Select(pair => pair.Value).OfType<double>());
                                     return new Row(group.Key, values);
                                 })
                                 .ToList();
        }

        if (!rows.Any())
            return dataGrid;


        foreach (var row in rows.OrderBy(r => r.Item.ToString()))
            dataGrid.Items.Add(new DataGridRow()
            {
                Header = row.Item,
                Item = row
            });

        if (HasFinalRow)
        {
            Dictionary<string, object?> finalValues = columns.Select(column => new { column.Header, Value = FinalFunction(rows.Select(row => row[column.Header]).OfType<double>()) })
                                                             .ToDictionary(value => value.Header, value => (object?)value.Value);

            var finalRow = new Row("Итого", finalValues);
            dataGrid.Items.Add(new DataGridRow()
            {
                Header = finalRow.Item,
                Item = finalRow,
                Style = (Style)Application.Current.FindResource("FinalRowStyle")
            });
        }

        return dataGrid;
    }

    private bool FilterReportValue( T value ) =>
        PropertyAccessors.All(accessor =>
        {
            if (accessor.Parameters.Any())
                return (bool)accessor.Get(value, accessor.Parameters.Select(p => FindSelectorBoxByReturnType(p)!.SelectedItem).ToArray())!;

            if (DateGetter is not null)
            {
                var date = DateGetter(value);
                if (date < DatePickers[0].SelectedDate || date > DatePickers[1].SelectedDate)
                    return false;
            }

            object? selectedItem = FindSelectorBoxByReturnType(accessor.GetType().GetGenericArguments()[1])?.SelectedItem;
            return selectedItem == null || accessor.Get(value, Array.Empty<object>()) == selectedItem;
        });

    private IEntitySelectorBox? FindSelectorBoxByReturnType( Type type ) =>
        SelectionElements.FirstOrDefault(s => s.GetType().GetGenericArguments()[0] == type);
}

public class ReportBuilder<T> where T : class, new()
{
    public class ReportPropertyAccessorBuilder<TProperty>
    {
        private readonly Dictionary<Type, string> _labels;
        private readonly ReportBuilder<T> _reportBuilder;
        private readonly Func<T, object?[], TProperty?> _getter;
        private readonly List<Type> _parameters;

        public ReportPropertyAccessorBuilder( Func<T, object?[], TProperty?> getter, ReportBuilder<T> reportBuilder )
        {
            _getter = getter;
            _labels = new();
            _parameters = new();
            _reportBuilder = reportBuilder;
        }

        public ReportPropertyAccessorBuilder<TProperty> Bind( Type type )
        {
            _parameters.Add(type);
            return this;
        }

        public ReportPropertyAccessorBuilder<TProperty> SetLabel(string label)
        {
            if (_parameters.Any())
                _reportBuilder._labels.Add(_parameters.Last(), label);
            else
                _reportBuilder._labels.Add(typeof(TProperty), label);
            return this;
        }

        public ReportBuilder<T> Build()
        {
            _reportBuilder._propertyAccessors.Add(new PropertyAccessor<T, TProperty>(_getter, _parameters));
            return _reportBuilder;
        }
    }

    private readonly List<Column>                        _columns;
    private readonly List<IPropertyAccessor<T>>          _propertyAccessors;
    private readonly Dictionary<Type, string>            _labels;
    private readonly List<ISelection<T>>                 _selections;
    private          Func<IEnumerable<double>, double>?  _finalFunction = null;
    private          Func<T, object>                     _headerGetter;
    private          Func<T, DateTime>                   _dateGetter;
    private          Func<T, object>                     _valueGetter;
    private          string?                             _title = null;
    private          Func<T, object>?                    _grouping = null;
    private          bool                                _hasFinalRow, _hasFinalColumn;

    public ReportBuilder()
    {
        _propertyAccessors = new();
        _labels = new();
        _columns = new();
        _selections = new();
    }

    public ReportPropertyAccessorBuilder<TProperty> BindColumnHeader<TProperty>( Func<T, object> columnGetter, Func<T, object> valueGetter )
    {
        _headerGetter = columnGetter;
        _valueGetter = valueGetter;
        return new ReportBuilder<T>.ReportPropertyAccessorBuilder<TProperty>(( item, parameters ) => (TProperty)columnGetter(item), this);
    }

    public ReportBuilder<T> AddDateSelection( Func<T, DateTime> dateGetter )
    {
        _dateGetter = dateGetter;
        return this;
    }

    public ReportBuilder<T> AddSelection( ISelection<T> selection )
    {
        _selections.Add(selection);
        return this;
    }

    public ReportBuilder<T> GroupBy( Func<T, object> grouping )
    {
        _grouping = grouping;
        return this;
    }

    public ReportBuilder<T> BindLabel(Type type, string label)
    {
        _labels.Add(type, label);
        return this;
    }

    public ReportBuilder<T> SetTitle( string title )
    {
        _title = title;
        return this;
    }

    public ReportBuilder<T> HasFinalRow()
    {
        _hasFinalRow = true;
        _finalFunction ??= Enumerable.Sum;
        return this;
    }
    public ReportBuilder<T> HasFinalColumn()
    {
        _hasFinalColumn = true;
        _finalFunction ??= Enumerable.Sum;
        return this;
    }

    public ReportBuilder<T> SetFinalFunction( FinalFunction finalFunction )
    {
        _finalFunction = finalFunction switch
        {
            FinalFunction.Sum => Enumerable.Sum,
            FinalFunction.Average => Enumerable.Average,
            FinalFunction.Min => Enumerable.Min,
            FinalFunction.Max => Enumerable.Max,
            _ => throw new ArgumentException($"Final function: {finalFunction} doesn't support")
        };
        return this;
    }

    #region [ PropertyAccessor ]
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
    #endregion

    public ReportPropertyAccessorBuilder<TProperty> AddPropertySelection<TProperty>( Func<T, object?[], TProperty> propertyExpression ) =>
        new(propertyExpression, this);

    public ReportBuilder<T> AddColumn( string header, Func<T, object> getter )
    {
        _columns.Add(new Column(header, item => getter((T)item)));
        return this;
    }

    public Report<T> Build() =>
        new(
            title: _title,
            columns: _columns,
            propertyAccessors: _propertyAccessors,
            labels: _labels,
            selections: _selections,
            hasFinalRow: _hasFinalRow,
            hasFinalColumn: _hasFinalColumn,
            finalFunction: _finalFunction,
            columnHeaderGetter: _headerGetter,
            valueGetter: _valueGetter,
            grouping: _grouping,
            _dateGetter);
}
public class Column
{
    public string Header { get; }

    public Func<object, object?> ValueGetter { get; }

    public Column( string header, Func<object, object?> valueGetter )
    {
        Header = header;
        ValueGetter = valueGetter;
    }
}

public class BindedColumn : Column
{
    public Func<object, bool> Filter { get; }

    public BindedColumn( string header, Func<object, object?> valueGetter, Func<object, bool> filter )
        : base(header, item => filter(item) ? valueGetter(item) : null)
    {
        Filter = filter;
    }
}

public class Row
{
    public object Item { get; }

    private Dictionary<string, object?> _values { get; } = new();

    [IndexerName("Value")]
    public object? this[string header] => _values[header];

    public Row( object item, IEnumerable<Column> columns )
    {
        Item = item;

        columns.ForEach(column =>
        {
            if (column is not BindedColumn bindedColumn)
            {
                _values.Add(column.Header, column.ValueGetter(item));
                return;
            }


        });
    }

    public Row( object item, Dictionary<string, object?> values )
    {
        Item = item;
        _values = values;
    }

    public static Row CreateRowFromColumns( object item, IEnumerable<Column> columns ) =>
        new(item, columns);
}

public delegate double FinalFunction<T>( IEnumerable<T> items, Func<T, double> valueGetter );

public class DataGridEntity<T> where T : class
{
    public IEnumerable<T> ItemsSource { get; }
    private List<Column> _columnCollection { get; } = new();

    public DataGridEntity() =>
        ItemsSource = DatabaseContext.Entities.Set<T>().Local;

    public void AddColumn( string header, Func<object, object?> valueGetter ) =>
        _columnCollection.Add(new(header, valueGetter));

    public IEnumerable<Row> GetRows() =>
        ItemsSource.Select(item => Row.CreateRowFromColumns(item!, _columnCollection));
}

public enum FinalFunction
{
    Sum,
    Average,
    Count,
    Max,
    Min,
}