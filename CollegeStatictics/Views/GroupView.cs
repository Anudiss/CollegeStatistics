using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.ViewModels
{
    public class GroupView : ItemDialog<Group>
    {
        [SelectableSubtableFormElement("Студенты")]
        [Column("Id", "Id")]
        [Column("Surname", "Фамилия")]
        [Column("Name", "Имя")]
        [Column("Patronymic", "Отчество")]
        [Label("Студенты")]
        public ICollection<Student> Students
        {
            get => Item.Students;
            set
            {
                Item.Students = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Куратор")]
        [EntitySelectorFormElement("Преподаватели")]
        public Teacher Curator
        {
            get => Item.Curator;
            set
            {
                Item.Curator = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Специальность")]
        [EntitySelectorFormElement("Специальности")]
        public Speciality Speciality
        {
            get => Item.Speciality;
            set
            {
                Item.Speciality = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Форма обучения")]
        [RadioButtonFormElement]
        public EducationForm EducationForm
        {
            get => Item.EducationForm;
            set
            {
                Item.EducationForm = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Номер")]
        [NumberBoxFormElement]
        [MaxLength(3)]
        [Unique(nameof(Group.Number))]
        public string Number
        {
            get => $"{Item.Number}";
            set
            {
                if (value.Length == 0)
                    Item.Number = 0;
                else
                    Item.Number = int.Parse(value);
                    
                OnPropertyChanged();
            }
        }

        public GroupView(Group? item) : base(item) { }
    }
}
