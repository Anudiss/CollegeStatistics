using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.Collections.Generic;

namespace CollegeStatictics.ViewModels
{
    [MinHeight(800)]
    public class TimetableView : ItemDialog<Timetable>
    {
        [FormElement(ElementType = ElementType.Timetable)]
        [Label("Расписание")]
        public ICollection<TimetableRecord> Records
        {
            get => _item.TimetableRecords;
            set
            {
                _item.TimetableRecords = value;
                OnPropertyChanged();
            }
        }

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
