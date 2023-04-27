using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;

namespace CollegeStatictics.ViewModels
{
    public class SpecialityView : ItemDialog<Speciality>
    {
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

        public SpecialityView(Speciality? item) : base(item)
        {
        }
    }
}
