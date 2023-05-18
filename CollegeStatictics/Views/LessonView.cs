using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.DataTypes.Records;
using CollegeStatictics.ViewModels;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Windows;
using Microsoft.EntityFrameworkCore;

namespace CollegeStatictics.Views
{
    [MinWidth(800)]
    [MinHeight(800)]
    [ViewTitle("Занятие")]
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

        [Label("Тема")]
        [TextBoxFormElement(IsReadOnly = true)]
        public StudyPlanRecord StudyPlanRecord => Item.StudyPlanRecord;

        [Label("Предмет")]
        [TextBoxFormElement(IsReadOnly = true)]
        public Subject Subject => Item.StudyPlanRecord.StudyPlan.Subject;

        [Label("Преподаватель")]
        [TextBoxFormElement(IsReadOnly = true)]
        public Teacher Teacher => Item.StudyPlanRecord.StudyPlan.Timetables.SingleOrDefault(t => t.Group == Group)?.Teacher;

        [Required]
        [Label("Группа")]
        [EntitySelectorFormElement("Группы", FilterPropertyName = nameof(GroupFilter))]
        public Group Group
        {
            get => Item.Group;
            set
            {
                Item.Group = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Attendances));

                RecreateHomeworkStudents();

                OnPropertyChanged(nameof(HomeworkStudents));
                OnPropertyChanged(nameof(Teacher));

                ValidateProperty(value);
            }
        }

        [EditableSubtableFormElement]
        [TextColumn(nameof(Attendance.Student), "Студент", IsReadOnly = true)]
        [CheckBoxColumn(nameof(AttendanceElement.IsAttended), "Присутствие", IsReadOnly = false)]
        public IEnumerable<AttendanceElement> Attendances => AttendanceElement.GetFromLesson(Item);

        [EditableSubtableFormElement]
        [TextColumn(nameof(HomeworkStudent.Student), "Студент", IsReadOnly = true)]
        [TextColumn($"{nameof(HomeworkStudent.Lesson)}.{nameof(Lesson.LessonHomework)}.{nameof(LessonHomework.Deadline)}", "Дата окончания", IsReadOnly = true)]
        [ComboBoxColumn(nameof(HomeworkStudent.HomeworkExecutionStatus), "Статус", nameof(HomeworkStudent.ExecutionStatuses))]
        [NumberBoxColumn(nameof(HomeworkStudent.Mark), "Оценка", 2, 5, IsReadOnly = false)]
        public IEnumerable<HomeworkStudent> HomeworkStudents => Item.HomeworkStudents;

        public LessonHomework Homework
        {
            get => Item.LessonHomework;
            set
            {
                Item.LessonHomework = value;
                OnPropertyChanged();
                RecreateHomeworkStudents();

                OnPropertyChanged(nameof(HomeworkStudents));
            }
        }

        public EmergencySituation? EmergencySituation
        {
            get => Item.EmergencySituation;
            set
            {
                Item.EmergencySituation = value;
                OnPropertyChanged();
            }
        }

        public Selection<Group> GroupFilter => new Selection<Group>(group => group.Speciality == StudyPlanRecord.StudyPlan.Speciality);

        #endregion

        #region [ Fields ]

        private IEnumerable<FormElement> _formElements;

        #endregion

        #region [ Setup ]

        public LessonView(Lesson? item) : base(item)
        {
            DatabaseContext.Entities.HomeworkExecutionStatuses.Load();
            _formElements = GetFormElements();

            Time = Time == default ? GetLessonTimeNearestToCurrent() : Time;
        }

        #endregion

        //private bool TimetableRecordsFilter(TimetableRecord record) => (record.Timetable?.Group == Group || Group == null) &&
        //                                                               (record.Timetable?.Teacher == Teacher || Teacher == null) &&
        //                                                               (record.Timetable?.Subject == Subject || Subject == null);

        #region [ Private methods ]


        #region [ View elements creation ]

        protected override IEnumerable<FrameworkElement> CreateViewElements()
        {
            yield return CreateHeaderPanel();
            yield return CreateDateTimesPanel();
            yield return CreateSubjectAndTeacherPanel();
            yield return CreateTopicAndGroupPanel();

            #region [ Tabs ]
            var panel = new Border
            {
                MinHeight = 500,
                Padding = new(10),
                CornerRadius = new(2.5),
            };

            var tabControl = new TabControl();
            panel.Child = tabControl;

            #region [ Attendance table ]
            tabControl.Items.Add(new TabItem()
            {
                Header = "Посещаемость",
                Content = CreateViewElementFor(nameof(Attendances))
            });
            #endregion

            #region [ Homework table ]
            tabControl.Items.Add(new TabItem()
            {
                Header = "Домашняя работа",
                Content = CreateViewElementFor(nameof(HomeworkStudents))
            });
            #endregion

            yield return panel;
            #endregion
        }

        private FrameworkElement CreateHeaderPanel()
        {
            var headerPanel = new DockPanel();

            headerPanel.Children.Add(new TextBlock
            {
                Text = "Занятие",
                FontSize = 24,
                VerticalAlignment = VerticalAlignment.Center
            });

            #region [ Write changes button ]
            var writeChangesButton = new Button
            {
                Content = "Записать",
                Command = SaveCommand,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Background = (Brush)new BrushConverter().ConvertFromString("#073B4C")!,
                Foreground = Brushes.White,
            };
            writeChangesButton.SetValue(DockPanel.DockProperty, Dock.Right);

            headerPanel.Children.Add(writeChangesButton);
            #endregion

            #region [ Homework button ]
            var homeworkButton = new Button
            {
                DataContext = Item,
                Margin = new(0, 0, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Right,
                Content = "Домашня работа"
            };
            homeworkButton.SetValue(DockPanel.DockProperty, Dock.Right);

            homeworkButton.Click += (_, _) =>
            {
                OpenDialog(new LessonHomeworkView(Item.LessonHomework ?? new LessonHomework()
                {
                    IssueDate = DateTime.Now,
                    Deadline = DateTime.Now.AddDays(1),
                    Lesson = Item,
                }), l => l.Homework);

                OnPropertyChanged(nameof(HomeworkStudents));
            };

            headerPanel.Children.Add(homeworkButton);
            #endregion

            #region [ Emeergency situtation button ]
            var attachEmergencySitutationButton = new Button
            {
                DataContext = Item,
                Margin = new(0, 0, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Right,
                Content = "Нештатная ситуация"
            };
            attachEmergencySitutationButton.SetValue(DockPanel.DockProperty, Dock.Right);

            attachEmergencySitutationButton.Click += (_, _) =>
            {
                OpenDialog(new EmergencySituationView(Item.EmergencySituation ?? new EmergencySituation()
                {
                    Lesson = Item,
                }), l => l.EmergencySituation);

                OnPropertyChanged(nameof(EmergencySituation));
            };

            headerPanel.Children.Add(attachEmergencySitutationButton);
            #endregion

            return headerPanel;
        }

        private FrameworkElement CreateDateTimesPanel()
        {
            var headerGrid = new UniformGrid
            {
                Columns = 4
            };

            var datePicker = CreateViewElementFor(nameof(Date));
            datePicker.Margin = new(0, 0, 10, 0);
            var timeBox = CreateViewElementFor(nameof(Time));
            timeBox.Margin = new(0, 0, 10, 0);
            var defaultLessonTimesBox = CreateDefaultLessonTimesComboBox();
            defaultLessonTimesBox.Margin = new(0, 0, 10, 0);
            var isRestoringCheckBox = CreateIsRestoringCheckBox();

            headerGrid.Children.Add(timeBox);
            headerGrid.Children.Add(defaultLessonTimesBox);

            headerGrid.Children.Add(datePicker);
            headerGrid.Children.Add(isRestoringCheckBox);

            return headerGrid;
        }

        private FrameworkElement CreateSubjectAndTeacherPanel()
        {
            var panel = new UniformGrid
            {
                Columns = 2
            };

            var subjectTextBox = CreateViewElementFor(nameof(Subject));
            panel.Children.Add(subjectTextBox);
            subjectTextBox.Margin = new(0, 0, 10, 0);

            var teacherTextBox = CreateViewElementFor(nameof(Teacher));
            panel.Children.Add(teacherTextBox);

            return panel;
        }

        private FrameworkElement CreateTopicAndGroupPanel()
        {
            var panel = new UniformGrid
            {
                Columns = 2
            };

            var topicTextBox = CreateViewElementFor(nameof(StudyPlanRecord));
            panel.Children.Add(topicTextBox);
            topicTextBox.Margin = new(0, 0, 10, 0);

            var groupSelectorBox = CreateViewElementFor(nameof(Group));
            panel.Children.Add(groupSelectorBox);

            return panel;
        }


        #region ARRANGE_THEM

        private IEnumerable<HomeworkStudent> CreateHomeworkStudents()
        {
            if (Group == null)
                return Enumerable.Empty<HomeworkStudent>();

            return from student in Group.Students
                select new HomeworkStudent()
                {
                    Student = student,
                    HomeworkExecutionStatus = DatabaseContext.Entities.HomeworkExecutionStatuses.Local.First(),
                    Mark = null
                };
        }

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

            var dayOfWeek = DateTime.Now.DayOfWeek;
            var availableTimesBox = new ComboBox
            {
                ItemsSource = Constants.LessonStartTimes[dayOfWeek],
                HorizontalAlignment = HorizontalAlignment.Stretch
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

        private FrameworkElement CreateIsRestoringCheckBox()
        {
            var checkBox = new CheckBox
            {
                Content = "Восстановление",
                VerticalAlignment = VerticalAlignment.Bottom,
                VerticalContentAlignment = VerticalAlignment.Bottom
            };

            checkBox.SetBinding(ToggleButton.IsCheckedProperty, new Binding(nameof(IsRestoring))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            return checkBox;
        }

        private FrameworkElement CreateViewElementFor(string propName)
            => CreateViewElement(_formElements.First(fe => fe.Property.Name == propName));

        #endregion

        #endregion

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

        private void RecreateHomeworkStudents()
        {
            Item.HomeworkStudents.Clear();

            var homeworkStudents = CreateHomeworkStudents();

            homeworkStudents.ForEach(hs => Item.HomeworkStudents.Add(hs));
            //DatabaseContext.Entities.AddRange(homeworkStudents);
        }

        private void OpenDialog<T>(ItemDialog<T> view, Expression<Func<LessonView, object?>> property) where T : class, ITable, new()
        {
            var dialogWindow = new DialogWindow()
            {
                Content = view,
                ContentTemplate = (DataTemplate)Application.Current.FindResource("ItemDialogTemplate"),

                PrimaryButtonText = "Сохранить",

                SecondaryButtonText = "Отменить"
            };

            dialogWindow.Show();

            if (dialogWindow.Result == DialogResult.Primary)
                ((PropertyInfo)((MemberExpression)property.Body).Member).SetValue(this, view.Item);
        }

        #endregion
    }
}
