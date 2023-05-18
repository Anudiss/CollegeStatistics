using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Windows;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CollegeStatictics.Views
{
    [MinWidth(800)]
    [MinHeight(800)]
    [ViewTitle("Учебный план")]
    public partial class StudyPlanView : ItemDialog<StudyPlan>
    {
        [RelayCommand]
        private void ConductLession(IList selectedItems)
        {
            var item = selectedItems?.Cast<StudyPlanRecord>().FirstOrDefault();
            if (item == null) return;

            if (item.StudyPlan.Timetables.Any() == false)
            {
                DialogWindow.ShowMessage("Для этого учебного плана нет расписания");

                return;
            }

            var lessons = item.Lessons.Where(lesson => lesson.IsConducted);
            if (lessons.Count() >= item.DurationInLessons)
            {
                DialogWindow.ShowMessage("Превышено количество пар, выделенных на тему");

                return;
            }

            var itemDialog = new LessonView(new Lesson()
            {
                StudyPlanRecord = item
            });

            var dialogWindow = new DialogWindow
            {
                Content = itemDialog,
                ContentTemplate = (DataTemplate)Application.Current.FindResource("ItemDialogTemplate"),

                PrimaryButtonText = "Сохранить",
                PrimaryButtonCommand = itemDialog.SaveCommand,
                SecondaryButtonText = "Отмена",
                SecondaryButtonCommand = itemDialog.CancelCommand,

                Width = 800
            };

            void WindowDialogClosingHandler(object? sender, CancelEventArgs e)
            {
                var dialogResult = (sender as DialogWindow)!.Result;
                bool areThereUnsavedChanges = DatabaseContext.Entities.ChangeTracker.HasChanges();

                if (areThereUnsavedChanges == true && dialogResult == DialogResult.None)
                {
                    var acceptDialog = new DialogWindow
                    {
                        Content = "Сохранить изменения?",
                        PrimaryButtonText = "Да",
                        SecondaryButtonText = "Нет",
                        TertiaryButtonText = "Отмена",
                    };

                    acceptDialog.Show();

                    if (acceptDialog.Result == DialogResult.Primary)
                    {
                        if (DatabaseContext.Entities.Entry(itemDialog.Item).State == EntityState.Detached)
                            DatabaseContext.Entities.Add(itemDialog.Item);
                        DatabaseContext.Entities.SaveChanges();
                    }
                    else if (acceptDialog.Result == DialogResult.Secondary)
                        DatabaseContext.CancelChanges(itemDialog.Item);
                    else
                        e.Cancel = true;
                }
            }

            dialogWindow.Closing += WindowDialogClosingHandler;
            dialogWindow.Show();
            dialogWindow.Closing -= WindowDialogClosingHandler;
        }

        [Label("Записи")]
        [SubtableButtonFormElement("Провести пару", nameof(ConductLession))]
        [SubtableFormElement(typeof(StudyPlanRecordView))]
        [TextColumn("Topic", "Тема")]
        [TextColumn("LessonType", "Тип пары")]
        [TextColumn("DurationInLessons", "Длительность в парах")]
        public IEnumerable<StudyPlanRecord> Records => DatabaseContext.Entities.StudyPlanRecords.Local.Where(r => r.StudyPlan == Item);

        [Label("Предмет")]
        [EntitySelectorFormElement("Предметы")]
        [Required(ErrorMessage = "Обязательное поле")]
        public Subject Subject
        {
            get => Item.Subject;
            set
            {
                Item.Subject = value;
                OnPropertyChanged();
            }
        }

        [Label("Специальность")]
        [EntitySelectorFormElement("Специальности")]
        [Required(ErrorMessage = "Обязательное поле")]
        public Speciality Speciality
        {
            get => Item.Speciality;
            set
            {
                Item.Speciality = value;
                OnPropertyChanged();
            }
        }

        [Label("Дата начала действия")]
        [DatePickerFormElement]
        public DateTime StartDate
        {
            get => Item.StartDate;
            set
            {
                Item.StartDate = value;
                OnPropertyChanged();
            }
        }

        [DefaultValue((byte)1)]
        [Label("Номер курса")]
        [Required(ErrorMessage = "Обязательное поле")]
        [SpinBoxFormElement]
        [Range(1, 4)]
        public byte Course
        {
            get => Item.Course;
            set
            {
                Item.Course = value;
                OnPropertyChanged();
            }
        }

        public StudyPlanView(StudyPlan? item) : base(item)
        {
            DatabaseContext.Entities.Lessons.Load();
        }
    }
}
