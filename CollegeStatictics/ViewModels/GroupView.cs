using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;

namespace CollegeStatictics.ViewModels
{
    public class GroupView : ItemDialog<Group>
    {
        [Label("Номер")]
        [FormElement]
        public int Number
        {
            get => _item.Number;
            set
            {
                _item.Number = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [Label("Форма обучения")]
        [RadioButtonFormElement]
        public EducationForm EducationForm
        {
            get => _item.EducationForm;
            set
            {
                _item.EducationForm = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        public GroupView(Group? item) : base(item) { }
    }
}
