using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.ViewModels
{
    [ViewTitle("Специальность")]
    public class SpecialityView : ItemDialog<Speciality>
    {
        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Подразделение")]
        [EntitySelectorFormElement("Отделения")]
        public Department Department
        {
            get => Item.Department;
            set
            {
                Item.Department = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [MaxLength(150)]
        [Required(ErrorMessage = "Поле обязательно")]
        [Label("Название")]
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

        public SpecialityView(Speciality? item) : base(item)
        {
        }
    }
}
