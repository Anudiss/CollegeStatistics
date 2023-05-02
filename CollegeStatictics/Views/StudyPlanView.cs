using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.Views
{
    public class StudyPlanView : ItemDialog<StudyPlan>
    {
        [Label("Предмет")]
        [EntitySelectorFormElement("Предметы")]
        public Subject Subject
        {
            get => _item.Subject;
            set
            {
                _item.Subject = value;
                OnPropertyChanged();
            }
        }

        [Label("Специальность")]
        [EntitySelectorFormElement("Специальности")]
        public Speciality Speciality
        {
            get => _item.Speciality;
            set
            {
                _item.Speciality = value;
                OnPropertyChanged();
            }
        }

        [Label("Дата начала действия")]
        [DatePickerFormElement]
        public DateTime StartDate
        {
            get => _item.StartDate;
            set
            {
                _item.StartDate = value;
                OnPropertyChanged();
            }
        }

        [Label("Номер курса")]
        [SpinBoxFormElement]
        [Range(1, 4)]
        public string Course
        {
            get => $"{_item.Course}";
            set
            {
                _item.Course = (byte)int.Parse(value);
                OnPropertyChanged();
            }
        }

        public StudyPlanView(StudyPlan? item) : base(item)
        {
        }
    }
}
