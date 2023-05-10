using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.DataTypes.Records;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        public TimetableRecord TimetableRecord
        {
            get => Item.TimetableRecord;
            set
            {
                Item.TimetableRecord = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Subject));
                OnPropertyChanged(nameof(Teacher));
                OnPropertyChanged(nameof(Group));
            }
        }

        [Label("Предмет")]
        [TextBoxFormElement(IsReadOnly = true)]
        public Subject? Subject => Item?.TimetableRecord?.Timetable?.Subject;

        [Label("Преподаватель")]
        [TextBoxFormElement(IsReadOnly = true)]
        public Teacher? Teacher => Item?.TimetableRecord?.Timetable?.Teacher;

        [Label("Группа")]
        [TextBoxFormElement(IsReadOnly = true)]
        public Group? Group => Item?.TimetableRecord?.Timetable?.Group;

        #endregion

        #region [ Fields ]

        private IEnumerable<FormElement> _formElements;

        #endregion

        public LessonView(Lesson? item) : base(item)
        {
            _formElements = GetFormElements();

            Time = GetLessonTimeNearestToCurrent();
        }

        private TimeSpan GetLessonTimeNearestToCurrent()
        {
            var currentTime = DateTime.Now.TimeOfDay;

            var availableTimes = Constants.LessonStartTimes[DateTime.Now.DayOfWeek];

            var prevTime = TimeSpan.Zero;
            foreach (var availableTime in availableTimes)
            {
                // If 7 -> return 8
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
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            var datePicker = CreateViewElementFor(nameof(Date)); datePicker.Margin = new(0, 0, 10, 0);
            var timeBox = CreateViewElementFor(nameof(Time)); timeBox.Margin = new(0, 0, 10, 0);
            var defaultLessonTimesBox = CreateDefaultLessonTimesComboBox();

            stackPanel.Children.Add(datePicker);
            stackPanel.Children.Add(timeBox);
            stackPanel.Children.Add(defaultLessonTimesBox);

            return new FrameworkElement[] { stackPanel };
        }

        private FrameworkElement CreateViewElementFor(string propName)
            => CreateViewElement(_formElements.First(fe => fe.Property.Name == propName));

        private FrameworkElement CreateDefaultLessonTimesComboBox()
        {
            var stackPanel = new StackPanel();

            var availableTimesBox = new ComboBox
            {
                ItemsSource = Constants.LessonStartTimes[DateTime.Now.DayOfWeek]
            };

            stackPanel.Children.Add(new Label { Content = "Время по умолчанию", Target = availableTimesBox });

            availableTimesBox.SetBinding(ComboBox.SelectedItemProperty, new Binding(nameof(Time))
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
