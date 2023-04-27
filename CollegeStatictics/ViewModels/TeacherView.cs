using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.ViewModels
{
    public class TeacherView : ItemDialog<Teacher>
    {
        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Фамилия")]
        [FormElement]
        public string Surname
        {
            get => _item.Surname;
            set
            {
                _item.Surname = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Имя")]
        [FormElement]
        public string Name
        {
            get => _item.Name;
            set
            {
                _item.Name = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Отчество")]
        [FormElement]
        public string Patronymic
        {
            get => _item.Patronymic;
            set
            {
                _item.Patronymic = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        public TeacherView(Teacher? item) : base(item)
        {
        }
    }
}
