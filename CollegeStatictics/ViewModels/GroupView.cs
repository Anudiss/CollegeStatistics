using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.ViewModels
{
    public class GroupView : ItemDialog<Group>
    {
        [EntitiesGridFormElement("Студенты")]
        public ICollection<Student> Students
        {
            get => _item.Students;
            set
            {
                _item.Students = new List<Student>(value);
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Куратор")]
        [EntitySelectorFormElement("Преподаватели")]
        public Teacher Cuarator
        {
            get => _item.Curator;
            set
            {
                _item.Curator = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Специальность")]
        [EntitySelectorFormElement("Специальности")]
        public Speciality Speciality
        {
            get => _item.Speciality;
            set
            {
                _item.Speciality = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Форма обучения")]
        [RadioButtonFormElement]
        public EducationForm EducationForm
        {
            get => _item.EducationForm;
            set
            {
                _item.EducationForm = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Номер")]
        [FormElement]
        public int Number
        {
            get => _item.Number;
            set
            {
                _item.Number = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        public GroupView(Group? item) : base(item) { }
    }
}
