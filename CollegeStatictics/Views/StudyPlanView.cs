using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CollegeStatictics.Views
{
    public class StudyPlanView : ItemDialog<StudyPlan>
    {
        [Label("Записи")]
        [SubtableFormElement(typeof(StudyPlanRecordView))]
        [Column("Topic", "Тема")]
        [Column("LessonType", "Тип пары")]
        [Column("DurationInLessons", "Длительность в парах")]
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

        [Label("Номер курса")]
        [Required(ErrorMessage = "Обязательное поле")]
        [SpinBoxFormElement]
        [Range(1, 4)]
        public string Course
        {
            get => $"{Item.Course}";
            set
            {
                Item.Course = (byte)int.Parse(value);
                OnPropertyChanged();
            }
        }

        public StudyPlanView(StudyPlan? item) : base(item)
        {
            Item.Course = 1;
        }
    }
}
