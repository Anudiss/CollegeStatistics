using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.DataTypes.Records;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace CollegeStatictics.Views
{
    public partial class LessonView : ItemDialog<Lesson>
    {
        #region [ Properties ]

        [Label("Дата")]
        [DatePickerFormElement]
        public DateTime Date
        {
            get => Item.Date;
            set
            {
                Item.Date = value;
                OnPropertyChanged();
            }
        }

        [Label("Время")]
        [TimeBoxFormElement]
        public TimeSpan Time
        {
            get => Item.Time;
            set
            {
                Item.Time = value;
                OnPropertyChanged();
            }
        }

        [ObservableProperty]
        private bool _isShortenedDay;

        [Label("Восстановление")]
        [CheckBoxFormElement]
        public bool IsRestoring
        {
            get => Item.IsRestoring;
            set
            {
                Item.IsRestoring = value;
                OnPropertyChanged();
            }
        }

        [Label("Запись из расписания")]
        public TimetableRecord TimetableRecord => Item.TimetableRecord;

        [Label("Предмет")]
        [TextBoxFormElement(IsReadOnly = true)]
        public Subject? Subject => Item?.TimetableRecord?.Timetable?.Subject;

        [Label("Преподаватель")]
        [TextBoxFormElement(IsReadOnly = true)]
        public Teacher? Teacher => Item?.TimetableRecord?.Timetable?.Teacher;

        [Label("Группа")]
        [TextBoxFormElement(IsReadOnly = true)]
        public Group? Group => Item?.TimetableRecord?.Timetable?.Group;

        public FilteredObservableCollection<TimetableRecord> TimetableRecords { get; }

        public IEnumerable<Subject> Subjects => TimetableRecords.View.Cast<TimetableRecord>()
                                                                     .Select(r => r.Timetable.Subject)
                                                                     .Distinct();

        public IEnumerable<Teacher> Teachers => TimetableRecords.View.Cast<TimetableRecord>()
                                                                     .Select(r => r.Timetable.Teacher)
                                                                     .Distinct();

        public IEnumerable<Group> Groups => TimetableRecords.View.Cast<TimetableRecord>()
                                                                 .Select(r => r.Timetable.Group)
                                                                 .Distinct();

        #endregion

        #region [ Fields ]

        private IEnumerable<FormElement> _formElements;

        #endregion

        public LessonView(Lesson? item) : base(item)
        {
            DatabaseContext.Entities.Teachers.Load();
            DatabaseContext.Entities.Groups.Load();
            DatabaseContext.Entities.Subjects.Load();
            DatabaseContext.Entities.Timetables.Load();

            DatabaseContext.Entities.TimetableRecords.Load();
            TimetableRecords = new FilteredObservableCollectionBuilder<TimetableRecord>(DatabaseContext.Entities.TimetableRecords.Local.ToObservableCollection())

                                   .AddFilter(new Selection<TimetableRecord>(record => record.Timetable.IsDeleted == false))
                                   .AddFilter(new Selection<TimetableRecord>(TimetableRecordsFilter))

                                   .Build();

            _formElements = GetFormElements();

            Time = GetLessonTimeNearestToCurrent();
        }

        private bool TimetableRecordsFilter(TimetableRecord record) => (record.Timetable?.Group == Group || Group == null) &&
                                                                       (record.Timetable?.Teacher == Teacher || Teacher == null) &&
                                                                       (record.Timetable?.Subject == Subject || Subject == null);

        private static TimeSpan GetLessonTimeNearestToCurrent()
        {
            var currentTime = DateTime.Now.TimeOfDay;

            var availableTimes = Constants.LessonStartTimes[DateTime.Now.DayOfWeek];

            var prevTime = TimeSpan.Zero;
            foreach (var availableTime in availableTimes)
            {
                if (currentTime <= availableTime)
                    return availableTime;
                
                else if (currentTime > availableTime)
                {
                    TimeSpan prevTimeDiff = currentTime - prevTime,
                             availableTimeDiff = currentTime - availableTime;

                    if (prevTimeDiff < availableTimeDiff)
                        return prevTime;
                }

                prevTime = availableTime;
            }

            return prevTime;
        }

        protected override IEnumerable<FrameworkElement> CreateViewElements()
        {
            var header = new TextBlock { Text = "Пара" };

            var headerGrid = new Grid
            {
            };
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var datePicker = CreateViewElementFor(nameof(Date)); datePicker.SetValue(Grid.ColumnProperty, 0);
            var timeBox = CreateViewElementFor(nameof(Time)); timeBox.SetValue(Grid.ColumnProperty, 1);
            var defaultLessonTimesBox = CreateDefaultLessonTimesComboBox(); defaultLessonTimesBox.SetValue(Grid.ColumnProperty, 2);
            var isRestoringCheckBox = CreateViewElementFor(nameof(IsRestoring)); isRestoringCheckBox.SetValue(Grid.ColumnProperty, 3);

            headerGrid.Children.Add(datePicker);
            headerGrid.Children.Add(timeBox);
            headerGrid.Children.Add(defaultLessonTimesBox);
            headerGrid.Children.Add(isRestoringCheckBox);
            
            yield return headerGrid;

            var uniformGrid = new UniformGrid
            {
                Rows = 2,
                Columns = 3
            };

            var groupSelector = CreateViewElementFor(nameof(Group)); groupSelector.Margin = new(0, 0, 10, 0);
            var teacherSelector = CreateViewElementFor(nameof(Teacher)); teacherSelector.Margin = new(0, 0, 10, 0);
            var subjectSelector = CreateViewElementFor(nameof(Subject)); subjectSelector.Margin = new(0, 0, 10, 0);

            uniformGrid.Children.Add(groupSelector);
            uniformGrid.Children.Add(teacherSelector);
            uniformGrid.Children.Add(subjectSelector);

            yield return uniformGrid;
        }

        private FrameworkElement CreateViewElementFor(string propName)
            => CreateViewElement(_formElements.First(fe => fe.Property.Name == propName));

        private FrameworkElement CreateComboBox(string itemPath, string itemsSourcePath)
        {
            var stackPanel = new StackPanel();

            var label = GetType().GetProperty(itemPath)!.GetCustomAttribute<LabelAttribute>()?.Label;

            var comboBox = new ComboBox();

            comboBox.SetBinding(ItemsControl.ItemsSourceProperty, new Binding(itemsSourcePath)
            {
                Mode = BindingMode.OneWay
            });
            comboBox.SetBinding(Selector.SelectedItemProperty, new Binding(itemPath)
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            stackPanel.Children.Add(new Label() { Content = label });
            stackPanel.Children.Add(comboBox);

            return stackPanel;
        }

        private FrameworkElement CreateDefaultLessonTimesComboBox()
        {
            var stackPanel = new StackPanel();

            var availableTimesBox = new ComboBox
            {
                ItemsSource = Constants.LessonStartTimes[DateTime.Now.DayOfWeek]
            };

            stackPanel.Children.Add(new Label { Content = "Время по умолчанию", Target = availableTimesBox });

            availableTimesBox.SetBinding(Selector.SelectedItemProperty, new Binding(nameof(Time))
            {
                Mode = BindingMode.TwoWay,
                ValidatesOnNotifyDataErrors = true,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            stackPanel.Children.Add(availableTimesBox);

            return stackPanel;
        }
    }
}
