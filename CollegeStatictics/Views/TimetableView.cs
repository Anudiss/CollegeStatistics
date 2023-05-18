using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Views;
using CollegeStatictics.Windows;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CollegeStatictics.ViewModels
{
    [MinHeight(700)]
    [MinWidth(500)]
    [ViewTitle("Расписание")]
    public partial class TimetableView : ItemDialog<Timetable>
    {
        //[ButtonElement("Провести занятие")]
        //[RelayCommand]
        //private void ConductLesson()
        //{
        //    var dataGrid = new DataGrid()
        //    {
        //        AutoGenerateColumns = false,
        //        CanUserAddRows = false,
        //        CanUserDeleteRows = false,
        //        CanUserResizeColumns = false,
        //        IsReadOnly = true,
        //        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
        //        Height = 200
        //    };

        //    dataGrid.GroupStyle.Add((GroupStyle)Application.Current.FindResource("DataGridGroupStyle")!);

        //    dataGrid.Columns.Add(new DataGridTextColumn
        //    {
        //        Header = "Номер пары",
        //        Binding = new Binding("Couple")
        //    });

        //    var timetableRecordsView = CollectionViewSource.GetDefaultView(Item.TimetableRecords);
        //    timetableRecordsView.GroupDescriptions.Add(new PropertyGroupDescription
        //    {
        //        PropertyName = "DayOfWeek"
        //    });
        //    timetableRecordsView.SortDescriptions.Add(new SortDescription
        //    {
        //        PropertyName = "DayOfWeekId"
        //    });

        //    dataGrid.ItemsSource = Item.TimetableRecords;

        //    var dialogWindow = new DialogWindow
        //    {
        //        Content = dataGrid,
        //        PrimaryButtonText = "Закрыть"
        //    };

        //    dataGrid.LoadingRow += (sender, e) =>
        //    {
        //        e.Row.InputBindings.Add(new MouseBinding
        //        {
        //            Gesture = new MouseGesture(MouseAction.LeftDoubleClick),
        //            Command = CreateLessonCommand,
        //            CommandParameter = ((StudyPlanRecord)e.Row.Item, dialogWindow)
        //        });
        //    };

        //    dialogWindow.Show();

        //    timetableRecordsView.GroupDescriptions.Clear();
        //    timetableRecordsView.SortDescriptions.Clear();
        //}

        //[RelayCommand]
        //private void CreateLesson(object pair)
        //{
        //    (StudyPlanRecord studyPlanRecord, DialogWindow parentDialogWindow) = ((StudyPlanRecord, DialogWindow))pair;

        //    parentDialogWindow.Close();

        //    var lesson = new Lesson
        //    {
        //        StudyPlanRecord = studyPlanRecord
        //    };

        //    var dialogWindow = new DialogWindow
        //    {
        //        Content = new LessonView(lesson),
        //        ContentTemplate = (DataTemplate)Application.Current.FindResource("ItemDialogTemplate"),

        //        PrimaryButtonText = "Сохранить",
        //        SecondaryButtonText = "Отмена",

        //        Width = 800
        //    };

        //    void WindowDialogClosingHandler(object? sender, CancelEventArgs e)
        //    {
        //        var dialogResult = (sender as DialogWindow)!.Result;
        //        bool areThereUnsavedChanges = DatabaseContext.Entities.ChangeTracker.HasChanges();

        //        if (areThereUnsavedChanges == true && dialogResult == DialogResult.None)
        //        {
        //            var acceptDialog = new DialogWindow
        //            {
        //                Content = "Сохранить изменения?",
        //                PrimaryButtonText = "Да",
        //                SecondaryButtonText = "Нет",
        //                TertiaryButtonText = "Отмена",
        //            };

        //            acceptDialog.Show();

        //            if (acceptDialog.Result == DialogResult.Primary)
        //            {
        //                if (lesson.Id == 0)
        //                    DatabaseContext.Entities.Add(lesson);
        //            }
        //            else if (acceptDialog.Result == DialogResult.Secondary)
        //                DatabaseContext.CancelChanges(lesson);
        //            else
        //                e.Cancel = true;
        //        }
        //    }

        //    dialogWindow.Closing += WindowDialogClosingHandler;
        //    dialogWindow.Show();
        //    dialogWindow.Closing -= WindowDialogClosingHandler;

        //    if (dialogWindow.Result == DialogResult.Primary)
        //    {
        //        if (lesson.Id == 0)
        //            DatabaseContext.Entities.Add(lesson);
        //    }
        //    else
        //        DatabaseContext.CancelChanges(lesson);
        //}

        [TimetableFormElement]
        [Label("Расписание")]
        public ICollection<TimetableRecord> Records
        {
            get => Item.TimetableRecords;
            set
            {
                Item.TimetableRecords = value;
                OnPropertyChanged();
            }
        }

        [EntitySelectorFormElement("Учебный план")]
        [Label("Учебный план")]
        public StudyPlan StudyPlan
        {
            get => Item.StudyPlan;
            set
            {
                Item.StudyPlan = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [EntitySelectorFormElement("Преподаватели")]
        [Label("Преподаватель")]
        public Teacher Teacher
        {
            get => Item.Teacher;
            set
            {
                Item.Teacher = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [EntitySelectorFormElement("Группы")]
        [Label("Группа")]
        public Group Group
        {
            get => Item.Group;
            set
            {
                Item.Group = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        public TimetableView(Timetable? item) : base(item)
        {
        }
    }
}
