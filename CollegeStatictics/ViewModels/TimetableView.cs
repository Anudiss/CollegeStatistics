using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;

namespace CollegeStatictics.ViewModels
{
    public class TimetableView : ItemDialog<Timetable>
    {
        [EntitySelectorFormElement("Предметы")]
        [Label("Предмет")]
        public Subject Subject
        {
            get => _item.Subject;
            set
            {
                _item.Subject = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [EntitySelectorFormElement("Преподаватели")]
        [Label("Преподаватель")]
        public Teacher Teacher
        {
            get => _item.Teacher;
            set
            {
                _item.Teacher = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [EntitySelectorFormElement("Группы")]
        [Label("Группа")]
        public Group Group
        {
            get => _item.Group;
            set
            {
                _item.Group = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        public TimetableView(Timetable? item) : base(item)
        {
        }
    }
}
