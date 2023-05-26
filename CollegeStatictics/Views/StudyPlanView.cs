using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.Utils;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Windows;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
            if (lessons.Any() && lessons.GroupBy(l => l.Group).All(g => g.Count() >= item.DurationInLessons))
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

        [RelayCommand]
        private void ImportCtp()
        {
            var openFileDialog = new OpenFileDialog
            {
                FileName = "Шаблон КТП",
                DefaultExt = ".xlsx",
                Filter = "Книга excel (*.xlsx)|*.xlsx",
                CheckFileExists = true,
                Title = "Выберите файл для импорта данных"
            };

            if (openFileDialog.ShowDialog() != true)
                return;

            var filePath = openFileDialog.FileName;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //open file and returns as Stream
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            // Init reader
            reader.Read();

            int lessonTypeCol = 0,
                durationInLessonsCol = 1,
                topicCol = 2,
                contentCol = 3;

            var newLessonTypes = new List<LessonType>();
            var newStudyPlanRecords = new List<StudyPlanRecord>();

            while (reader.Read())
            {
                // End of data stream
                if (string.IsNullOrWhiteSpace(reader.GetString(0)))
                    break;

                var lessonTypeName = reader.GetString(lessonTypeCol).Trim().ToLower();
                var lessonType =
                    DatabaseContext.Entities.LessonTypes.Local
                        .FirstOrDefault(lt => lt.Name.ToLower() == lessonTypeName);

                if (lessonType is null)
                {
                    lessonType = new LessonType { Name = lessonTypeName.ToUpperFirstLetter() };
                    newLessonTypes.Add(lessonType);
                }

                var durationInLessons = (int)reader.GetDouble(durationInLessonsCol);

                string topic = reader.GetString(topicCol).Trim().ToLower(),
                       content = reader.GetString(contentCol).Trim();

                var isStudyPlanRecordExist = DatabaseContext.Entities.StudyPlanRecords.Local
                    .Any(spr => spr.StudyPlan == Item
                        && spr.LessonType == lessonType
                        && spr.DurationInLessons == durationInLessons
                        && spr.Topic.ToLower() == topic);

                if (isStudyPlanRecordExist == false)
                {
                    newStudyPlanRecords.Add(new StudyPlanRecord
                    {
                        StudyPlan = Item,
                        LessonType = lessonType,
                        DurationInLessons = durationInLessons,
                        Topic = topic.ToUpperFirstLetter(),
                        Content = content
                    });
                }
            }

            newLessonTypes.ForEach(DatabaseContext.Entities.LessonTypes.Local.Add);
            newStudyPlanRecords.ForEach(Item.StudyPlanRecords.Add);
            DatabaseContext.Entities.SaveChanges();

            RefreshSubtable();
        }

        [RelayCommand]
        private void DownloadCtpTemplate()
        {
            var dlg = new SaveFileDialog
            {
                FileName = "Шаблон КТП",
                DefaultExt = ".xlsx",
                Filter = "Книга excel (*.xlsx)|*.xlsx",
                Title = "Выберите место для сохранения"
            };

            var result = dlg.ShowDialog();
            if (result == true)
            {
                var filePathToSave = dlg.FileName;

                var ctpTemplate = new Uri("Resources/Шаблон КТП.csv", UriKind.Relative);
                var ctpTemplateFileStream = Application.GetResourceStream(ctpTemplate).Stream;
                ctpTemplateFileStream.CopyToAsync(new FileStream(filePathToSave, FileMode.OpenOrCreate));
            }
        }

        protected override void DeleteSubtableItems(IList items)
        {
            var studyPlanRecords = DatabaseContext.Entities.StudyPlanRecords.Local;
            var areThereLinkedStudyPlanRecords =
                studyPlanRecords.Any(sp => sp.Lessons.Any());
            if (areThereLinkedStudyPlanRecords)
            {
                DialogWindow.ShowMessage("Нельзя удалить запись, с которой связаны проведенные пары");
                return;
            }

            var dialogWindow = new DialogWindow
            {
                Content = new Label { Content = "Действительно удалить выбранные элементы?" },
                PrimaryButtonText = "Да",
                SecondaryButtonText = "Нет"
            };
            dialogWindow.Show();

            if (dialogWindow.Result != DialogResult.Primary)
                return;

            foreach (var selectedItem in items)
                DatabaseContext.Entities.Remove(selectedItem);
        }

        [Label("Записи")]
        [SubtableButtonFormElement("Скачать Шаблон КТП", nameof(DownloadCtpTemplate))]
        [SubtableButtonFormElement("Импорт КТП", nameof(ImportCtp))]
        [SubtableButtonFormElement("Провести пару", nameof(ConductLession))]
        [SubtableFormElement(typeof(StudyPlanRecordView))]
        [TextColumn("Topic", "Тема")]
        [TextColumn("LessonType", "Тип пары")]
        [TextColumn("DurationInLessons", "Длительность в парах")]
        public IEnumerable<StudyPlanRecord> Records =>
            DatabaseContext.Entities.StudyPlanRecords.Local.Where(r => r.StudyPlan == Item);

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
