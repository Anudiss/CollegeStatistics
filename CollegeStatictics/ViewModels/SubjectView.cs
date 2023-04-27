using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.ViewModels
{
    public class SubjectView : ItemDialog<Subject>
    {
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

        public SubjectView(Subject? item) : base(item)
        {
        }
    }
}
