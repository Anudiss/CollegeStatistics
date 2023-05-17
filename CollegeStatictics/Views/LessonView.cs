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
using Microsoft.IdentityModel.Tokens;

using ModernWpf.Controls;

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

namespace CollegeStatictics.Views
{
    [MinWidth(800)]
    [MinHeight(800)]
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

        public LessonView(Lesson? item) : base(item)
        {
            DatabaseContext.Entities.HomeworkExecutionStatuses.Load();
            _formElements = GetFormElements();

            Time = Time == default ? GetLessonTimeNearestToCurrent() : Time;
        }

        //private bool TimetableRecordsFilter(TimetableRecord record) => (record.Timetable?.Group == Group || Group == null) &&
        //                                                               (record.Timetable?.Teacher == Teacher || Teacher == null) &&
        //                                                               (record.Timetable?.Subject == Subject || Subject == null);

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
            #region [ CommandLineContainer ]
            var commandButtonContainer = new SimpleStackPanel
            {
                Orientation = Orientation.Horizontal,
                Spacing = 12
            };

            commandButtonContainer.Children.Add(new Button()
            {
                Content = "Записать",
                Command = SaveCommand,
            });

            yield return commandButtonContainer;
            #endregion

            var header = new TextBlock { Text = "Пара" };

            #region [ Date time container ]
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

            yield return headerGrid;
            #endregion

            #region [ StudyPlanRecord ]
            var studyPlanRecordElement = CreateViewElementFor(nameof(StudyPlanRecord)); studyPlanRecordElement.Margin = new(0, 0, 10, 0);

            yield return studyPlanRecordElement;
            #endregion

            #region [ Homework ]
            var homeworkButton = new Button()
            {
                DataContext = Item,
            };

            homeworkButton.SetBinding(ContentControl.ContentProperty, new Binding($"{nameof(Lesson.LessonHomework)}.{nameof(LessonHomework.Homework)}")
            {
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                TargetNullValue = "Назначить домашнюю работу"
            });

            homeworkButton.Click += (_, _) =>
            {
                OpenDialog(new LessonHomeworkView(Item.LessonHomework ?? new LessonHomework()
                {
                    IssueDate = DateTime.Now,
                    Deadline = DateTime.Now.AddDays(1),
                    Lesson = Item
                }), l => l.Homework);

                OnPropertyChanged(nameof(HomeworkStudents));
            };

            yield return homeworkButton;
            #endregion

            #region [ Group selector ]
            yield return CreateViewElementFor(nameof(Group));
            #endregion

            #region [ Tabs ]
            var tabControl = new TabControl()
            {
                MinHeight = 500,
            };

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

            #region [ EmergencySituation tab ]

            var emergencySituationTabItem = new TabItem()
            {
                Header = "Нештатная ситуация",
            };

            tabControl.Items.Add(emergencySituationTabItem);

            tabControl.Items.CurrentChanging += (sender, e) =>
            {
                var item = ((ICollectionView)sender).CurrentItem;
                if (item == emergencySituationTabItem)
                {
                    if (EmergencySituation is null)
                    {
                        var dialogWindow = new DialogWindow()
                        {
                            Content = "Нештатная ситуация ещё не создана, создать?",

                            PrimaryButtonText = "Да",

                            SecondaryButtonText = "Нет"
                        };

                        dialogWindow.Show();

                        if (dialogWindow.Result != DialogResult.Primary)
                        {
                            e.Cancel = true;
                            tabControl.SelectedItem = item;
                            return;
                        }
                        else
                            EmergencySituation = new()
                            {
                                Lesson = Item
                            };
                    }

                    var container = new StackPanel();

                    var removeButton = new Button()
                    {
                        Content = "Удалить",
                        Margin = new(0, 0, 0, 10)
                    };

                    removeButton.Click += (_, _) => DatabaseContext.Entities.Remove(EmergencySituation);

                    container.Children.Add(removeButton);

                    var textBox = new TextBox()
                    {
                        Foreground = Brushes.Black,
                        FontSize = 14,
                        TextWrapping = TextWrapping.Wrap,
                        AcceptsReturn = true,
                        MinHeight = 400,
                        MaxHeight = 400,
                        MinWidth = 600,
                        DataContext = EmergencySituation
                    };
                    textBox.SetBinding(TextBox.TextProperty, new Binding(nameof(EmergencySituation.Description))
                    {
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });

                    container.Children.Add(textBox);

                    emergencySituationTabItem.Content = container;
                }
            };

            #endregion

            yield return tabControl;
            #endregion
        }

        private void RecreateHomeworkStudents()
        {
            Item.HomeworkStudents.Clear();

            var homeworkStudents = CreateHomeworkStudents();

            DatabaseContext.Entities.AddRange(homeworkStudents);
        }

        private IEnumerable<HomeworkStudent> CreateHomeworkStudents()
        {
            if (Group == null)
                return Enumerable.Empty<HomeworkStudent>();

            return from student in Group.Students
                select new HomeworkStudent()
                {
                    Student = student,
                    Lesson = Item,
                    HomeworkExecutionStatus = DatabaseContext.Entities.HomeworkExecutionStatuses.Local.First(),
                    Mark = null
                };
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
                VerticalAlignment = VerticalAlignment.Bottom
            };

            checkBox.SetBinding(ToggleButton.IsCheckedProperty, new Binding(nameof(IsRestoring))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            return checkBox;
        }
    }
}
