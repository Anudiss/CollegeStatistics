using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.ViewModels
{
    public class GroupView : ItemDialog<Group>
    {
        public Selection<Student> StudentGroupFilter => new(student => student.Group != Item);

        [SelectableSubtableFormElement("Студенты", FilterPropertyName = nameof(StudentGroupFilter))]
        [TextColumn("Id", "Id")]
        [TextColumn("Surname", "Фамилия")]
        [TextColumn("Name", "Имя")]
        [TextColumn("Patronymic", "Отчество")]
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

        [Label("Староста")]
        [EntitySelectorFormElement("Студенты", FilterPropertyName = nameof(GroupLeaderFilter), IsClearable = true)]
        public Student? GroupLeader
        {
            get => Item.GroupLeader;
            set
            {
                Item.GroupLeader = value;
                OnPropertyChanged();
            }
        }

        public Selection<Student> GroupLeaderFilter => new(student => student.Group == Item);

        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Куратор")]
        [EntitySelectorFormElement("Преподаватели")]
        public Teacher Curator
        {
            get => Item.Curator;
            set
            {
                ValidateProperty(value);

                Item.Curator = value;
                OnPropertyChanged();
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
                ValidateProperty(value);

                Item.Speciality = value;
                OnPropertyChanged();
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
                ValidateProperty(value);

                Item.EducationForm = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Номер")]
        [NumberBoxFormElement]
        [MaxLength(3)]
        [Unique(nameof(Group.Number), ErrorMessage = "Группа с таким номером уже существует")]
        public string Number
        {
            get => $"{Item.Number}";
            set
            {
                ValidateProperty(value);

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
