using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;

namespace CollegeStatictics.ViewModels
{
    public class TeacherView : ItemDialog<Teacher>
    {
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
