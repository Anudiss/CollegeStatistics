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
using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.DataTypes.Records;
using CollegeStatictics.Utils;
using CollegeStatictics.ViewModels;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Windows;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public Teacher? Teacher => Item.StudyPlanRecord.StudyPlan.Timetables.SingleOrDefault(t => t.Group == Group)?.Teacher;

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

                RecreateHomeworkStudents();
                RecreateAttendances();

                RefreshSubtables();
                //OnPropertyChanged(nameof(Attendances));
                //OnPropertyChanged(nameof(HomeworkStudents));
                //OnPropertyChanged(nameof(Teacher));

                ValidateProperty(value);

                CastSpellToRefreshAttendances();
            }
        }

        [EditableSubtableFormElement]
        [TextColumn(nameof(Attendance.Student), "Студент", IsReadOnly = true)]
        [CheckBoxColumn(nameof(Attendance.IsAttented), "Присутствие", IsReadOnly = false)]
        [NumberBoxColumn(nameof(Attendance.Mark), "Оценка", 2, 5, IsReadOnly = false)]
        [ButtonColumn(nameof(Attendance.Student), "Заметка", nameof(OpenNoteToStudentCommand))]
        public IEnumerable<Attendance> Attendances => Item.Attendances;

        [EditableSubtableFormElement]
        [TextColumn(nameof(HomeworkStudent.Student), "Студент", IsReadOnly = true)]
        [TextColumn($"{nameof(HomeworkStudent.Lesson)}.{nameof(Lesson.LessonHomework)}.{nameof(LessonHomework.Deadline)}", "Дата окончания", IsReadOnly = true, StringFormat = "{0:dd.MM.yyyy}")]
        [ComboBoxColumn(nameof(HomeworkStudent.HomeworkExecutionStatus), "Статус", nameof(HomeworkStudent.ExecutionStatuses))]
        [NumberBoxColumn(nameof(HomeworkStudent.Mark), "Оценка", 2, 5, IsReadOnly = false)]
        public IEnumerable<HomeworkStudent> HomeworkStudents => Item.HomeworkStudents;

        public bool IsConducted => Item.IsConducted;

        public LessonHomework? Homework
        {
            get => Item.LessonHomework;
            set
            {
                Item.LessonHomework = value;
                OnPropertyChanged();
                RecreateHomeworkStudents();

                OnPropertyChanged(nameof(HomeworkStudents));
                CastSpellToRefreshHomeworkStudents();
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

        public NoteToLesson? NoteToLesson
        {
            get => Item.NoteToLesson;
            set
            {
                Item.NoteToLesson = value;
                OnPropertyChanged();
            }
        }

        public Selection<Group> GroupFilter => new Selection<Group>(group => group.Speciality == StudyPlanRecord.StudyPlan.Speciality && StudyPlanRecord.DurationInLessons > StudyPlanRecord.Lessons.Where(g => g.IsConducted && g.Group == group).Count());
        #endregion

        [RelayCommand]
        private void OpenNoteToStudent(Student student)
        {
            var noteToStudent = DatabaseContext.Entities.NoteToStudents.Local
                .FirstOrDefault(n => n.Lesson == Item && n.Student == student)
                ?? new NoteToStudent()
                    {
                        Lesson = Item,
                        Student = student
                    };

            var dialogWindow = new DialogWindow()
            {
                Content = new NoteToStudentView(noteToStudent),
                ContentTemplate = (DataTemplate)Application.Current.FindResource("ItemDialogTemplate"),
                PrimaryButtonText = "Сохранить",
                SecondaryButtonText = "Отменить"
            };
            dialogWindow.Show();

            if (dialogWindow.Result == DialogResult.Primary && student.NoteToStudents.Any(n => n == noteToStudent) == false)
                student.NoteToStudents.Add(noteToStudent);
        }

        private readonly IEnumerable<FormElement> _formElements;

        public LessonView(Lesson? item) : base(item)
        {
            DatabaseContext.Entities.HomeworkExecutionStatuses.Load();
            _formElements = GetFormElements();

            Time = Time == default ? LessonTimeUtils.GetLessonTimeNearestTo(DateTime.Now.TimeOfDay) : Time;
            RecreateAttendances();
        }

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

            _tabControl = tabControl;

            yield return panel;
            #endregion
        }

        private TabControl _tabControl;
        private void CastSpellToRefreshHomeworkStudents()
        {
            _tabControl.SelectedIndex = 0;
            _tabControl.SelectedIndex = 1;
        }

        private void CastSpellToRefreshAttendances()
        {
            _tabControl.SelectedIndex = 1;
            _tabControl.SelectedIndex = 0;
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

            #region [ Emergency situtation button ]
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

            #region [ Note to lesson button ]
            var attachNoteToLessonButton = new Button
            {
                DataContext = Item,
                Margin = new(0, 0, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Right,
                Content = "Заметка к занятию"
            };
            attachNoteToLessonButton.SetValue(DockPanel.DockProperty, Dock.Right);
            attachNoteToLessonButton.Click += (_, _) =>
            {
                OpenDialog(new NoteToLessonView(Item.NoteToLesson ?? new NoteToLesson()
                {
                    Lesson = Item,
                }), l => l.NoteToLesson);

                OnPropertyChanged(nameof(EmergencySituation));
            };

            headerPanel.Children.Add(attachNoteToLessonButton);
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

            var groupSelectorBoxContainer = CreateViewElementFor(nameof(Group));
            panel.Children.Add(groupSelectorBoxContainer);

            var entitySelectorBox =
                (FilteredEntitySelectorBox<Group>)
                    ((((StackPanel)groupSelectorBoxContainer).Children)
                        .OfType<ContentControl>().Last().Content);

            entitySelectorBox.OpenDialogAcceptor += () =>
            {
                var dialogWindow = new DialogWindow
                {
                    Title = "",
                    Content = "При изменении группы текущие данные посещаемости будут потеряны. Продолжить?",
                    PrimaryButtonText = "Да",
                    SecondaryButtonText = "Нет"
                };
                dialogWindow.Show();

                return dialogWindow.Result == DialogResult.Primary;
            };

            return panel;
        }


        #region ARRANGE_THEM

        private IEnumerable<HomeworkStudent> CreateHomeworkStudents()
        {
            if (Group == null || Homework == null)
                return Enumerable.Empty<HomeworkStudent>();

            return from student in Group.Students
                   where !Item.HomeworkStudents.Any(h => h.Student == student)
                   select new HomeworkStudent()
                   {
                       Lesson = Item,
                       Student = student,
                       HomeworkExecutionStatus = DatabaseContext.Entities.HomeworkExecutionStatuses.Local.First(),
                       Mark = null
                   };
        }

        private IEnumerable<Attendance> CreateAttendances()
        {
            if (Group == null)
                return Enumerable.Empty<Attendance>();

            return from student in Group.Students
                   where !Item.Attendances.Any(a => a.Student == student)
                   select new Attendance()
                   {
                       Lesson = Item,
                       Student = student,
                       Mark = null,
                       IsAttented = true,
                   };
        }

        private FrameworkElement CreateDefaultLessonTimesComboBox()
        {
            var stackPanel = new StackPanel();

            var dayOfWeek = DateTime.Now.DayOfWeek;
            var availableTimesBox = new ComboBox
            {
                ItemsSource = LessonTimeUtils.LessonStartTimes[dayOfWeek],
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

        private void RecreateHomeworkStudents()
        {
            var shouldRecreate = Item.HomeworkStudents.Where(h => h.Student.Group != Group).ToList();

            shouldRecreate.ForEach(a => Item.HomeworkStudents.Remove(a));

            var homeworkStudents = CreateHomeworkStudents();
            homeworkStudents.ForEach(Item.HomeworkStudents.Add);
            //DatabaseContext.Entities.AddRange(homeworkStudents);
        }

        private void RecreateAttendances()
        {
            var shouldRecreate = Item.Attendances.Where(a => a.Student.Group != Group).ToList();
            
            shouldRecreate.ForEach(a => Item.Attendances.Remove(a));

            var attendances = CreateAttendances();
            attendances.ForEach(Item.Attendances.Add);
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
