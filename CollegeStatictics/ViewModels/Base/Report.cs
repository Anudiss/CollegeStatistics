using CollegeStatictics.Database;
using CollegeStatictics.DataTypes.Interfaces;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;

using ModernWpf.Controls.Primitives;

using System;
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

    public IEntitySelectorBox[] Selections { get; }
    public IEnumerable<ContentControl> SelectionContainers { get; }

    public IPropertyAccessor<T>[] PropertyAccessors { get; }

    public IEnumerable<Column> Columns { get; }

    public Func<IEnumerable<T>, Func<T, double>, double> FinalFunction { get; }

    public FrameworkElement View => Generate();

    public DataTemplate ContentTemplate => (DataTemplate)Application.Current.FindResource("ReportTemplate");

    public Report( string? title, IEnumerable<Column> columns, IEnumerable<IPropertyAccessor<T>> propertyAccessors, bool hasFinalRow, bool hasFinalColumn, Func<IEnumerable<T>, Func<T, double>, double>? finalFunction )
    {
        Title = title ?? $"Отчёт по {typeof(T).Name}";
        HasFinalRow = hasFinalRow;
        HasFinalColumn = hasFinalColumn;
        FinalFunction = finalFunction;

        PropertyAccessors = propertyAccessors.ToArray();

        Selections = CreateEntitySelectorBoxes().ToArray();
        SelectionContainers = Selections.Select(entitySelectorBox => new ContentControl()
        {
            Content = entitySelectorBox,
            ContentTemplate = (DataTemplate)Application.Current.FindResource("EntitySelectorBoxTemplate"),
            MinWidth = 200
        });
        Columns = columns;
    }

    private IEnumerable<IEntitySelectorBox> CreateEntitySelectorBoxes()
    {
        foreach (Type type in PropertyAccessors.SelectMany(pair => pair.Parameters.Any() ? pair.Parameters : new[] { pair.GetType().GetGenericArguments()[1] }))
            yield return CreateEntitySelectorBoxFromType(type);
    }

    private static IEntitySelectorBox CreateEntitySelectorBoxFromType( Type type )
    {
        var entitySelectorBoxType = typeof(FilteredEntitySelectorBox<>).MakeGenericType(type);
        IEntitySelectorBox entitySelectorBoxInstance = (IEntitySelectorBox)Activator.CreateInstance(entitySelectorBoxType, new object?[] { MainVM.GetViewByType(type), null })!;
        ((Control)entitySelectorBoxInstance).SetValue(TextBoxHelper.IsDeleteButtonProperty, true);

        return entitySelectorBoxInstance;
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

            GridLinesVisibility = DataGridGridLinesVisibility.All,

            IsReadOnly = true
        };

        // Getting values
        DatabaseContext.Entities.Set<T>().Load();
        var values = DatabaseContext.Entities.Set<T>().Local;

        // Filtering it
        IEnumerable<T> filteredValues = values.Where(FilterReportValue);

        // Casting to row
        List<Row> rows = filteredValues.Select(value => Row.CreateRowFromColumns(value, Columns)).ToList();

        // Generating dataGrid
        foreach (var column in Columns)
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = column.Header,
                Binding = new Binding($"[{column.Header}]")
                {
                    Mode = BindingMode.OneWay
                }
            });

        if (HasFinalRow)
        {
            Dictionary<string, object?> finalValues = Columns.Select(column => new { column.Header, Value = FinalFunction(rows.Select(row => row.Item).Cast<T>(), item => (double)column.ValueGetter(item)) })
                                                             .ToDictionary(value => value.Header, value => (object?)value.Value);
            rows.Add(new Row("Итого", finalValues));
        }

        foreach (var row in rows)
            dataGrid.Items.Add(new DataGridRow()
            {
                Header = row.Item,
                Item = row
            });

        return dataGrid;
    }

    private bool FilterReportValue( T value ) =>
        PropertyAccessors.All(accessor =>
        {
            if (accessor.Parameters.Any())
                return (bool)accessor.Get(value, accessor.Parameters.Select(p => FindSelectorBoxByReturnType(p)!.SelectedItem).ToArray())!;

            object? selectedItem = FindSelectorBoxByReturnType(accessor.GetType().GetGenericArguments()[1])!.SelectedItem;
            return selectedItem == null || accessor.Get(value, Array.Empty<object>()) == selectedItem;
        });

    private IEntitySelectorBox? FindSelectorBoxByReturnType( Type type ) =>
        Selections.FirstOrDefault(s => s.GetType().GetGenericArguments()[0] == type);
}

public class ReportBuilder<T> where T : class, new()
{
    public class ReportPropertyAccessorBuilder<TProperty>
    {
        private readonly ReportBuilder<T> _reportBuilder;
        private readonly Func<T, object?[], TProperty?> _getter;
        private readonly List<Type> _parameters;

        public ReportPropertyAccessorBuilder( Func<T, object?[], TProperty?> getter, ReportBuilder<T> reportBuilder )
        {
            _getter = getter;
            _parameters = new();
            _reportBuilder = reportBuilder;
        }

        public ReportPropertyAccessorBuilder<TProperty> Bind( Type type )
        {
            _parameters.Add(type);
            return this;
        }

        public ReportBuilder<T> Build()
        {
            _reportBuilder._propertyAccessors.Add(new PropertyAccessor<T, TProperty>(_getter, _parameters));
            return _reportBuilder;
        }
    }

    private readonly List<Column>                                    _columns;
    private readonly List<IPropertyAccessor<T>>                      _propertyAccessors;
    private          Func<IEnumerable<T>, Func<T, double>, double>?  _finalFunction = null;
    private          string?                                         _title = null;
    private          bool                                            _hasFinalRow, _hasFinalColumn;

    public ReportBuilder()
    {
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
            FinalFunction.Sum => Enumerable.Sum<T>,
            FinalFunction.Average => Enumerable.Average<T>,
            FinalFunction.Min => Enumerable.Min<T>,
            FinalFunction.Max => Enumerable.Max<T>,
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
            hasFinalRow: _hasFinalRow,
            hasFinalColumn: _hasFinalColumn,
            finalFunction: _finalFunction);
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

public class Row
{
    public object Item { get; }

    private Dictionary<string, object?> _values { get; } = new();

    [IndexerName("Value")]
    public object? this[string header] => _values[header];

    public Row( object item, IEnumerable<Column> columns )
    {
        Item = item;

        columns.ForEach(column => _values.Add(column.Header, column.ValueGetter(item)));
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