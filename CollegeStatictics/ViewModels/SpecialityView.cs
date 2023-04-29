using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.ViewModels
{
    public class SpecialityView : ItemDialog<Speciality>
    {
        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Подразделение")]
        [EntitySelectorFormElement("Подразделения")]
        public Department Department
        {
            get => _item.Department;
            set
            {
                _item.Department = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [MaxLength(150)]
        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Название")]
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

        public SpecialityView(Speciality? item) : base(item)
        {
        }
    }
}
