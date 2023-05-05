using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CollegeStatictics.Views
{
    [MinWidth(800)]
    [MinHeight(800)]
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

        // TODO: Fix default value in spin box
        [DefaultValue(2)]
        [Label("Номер курса")]
        [Required(ErrorMessage = "Обязательное поле")]
        [SpinBoxFormElement]
        [Range(1, 4, ErrorMessage = "лщфьыщвлфцв")]
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
            DatabaseContext.Entities.StudyPlanRecords.Load();
        }
    }
}
