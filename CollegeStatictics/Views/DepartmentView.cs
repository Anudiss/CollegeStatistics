using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.ViewModels
{
    public class DepartmentView : ItemDialog<Department>
    {
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

        public DepartmentView(Department? item) : base(item)
        {
        }
    }
}
