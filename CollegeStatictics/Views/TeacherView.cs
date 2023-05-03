using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.ViewModels
{
    public class TeacherView : ItemDialog<Teacher>
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Отчество")]
        [TextBoxFormElement]
        public string Patronymic
        {
            get => Item.Patronymic;
            set
            {
                Item.Patronymic = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [MaxLength(50)]
        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Имя")]
        [TextBoxFormElement]
        public string Name
        {
            get => Item.Name;
            set
            {
                Item.Name = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [MaxLength(50)]
        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Фамилия")]
        [TextBoxFormElement]
        public string Surname
        {
            get => Item.Surname;
            set
            {
                Item.Surname = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        public TeacherView(Teacher? item) : base(item)
        {
        }
    }
}
