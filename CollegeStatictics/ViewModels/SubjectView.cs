using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;

namespace CollegeStatictics.ViewModels
{
    public class SubjectView : ItemDialog<Subject>
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

        public SubjectView(Subject? item) : base(item)
        {
        }
    }
}
