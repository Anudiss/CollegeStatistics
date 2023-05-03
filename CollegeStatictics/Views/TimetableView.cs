using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.Collections.Generic;

namespace CollegeStatictics.ViewModels
{
    [MinHeight(700)]
    [MinWidth(500)]
    public class TimetableView : ItemDialog<Timetable>
    {
        [TimetableFormElement]
        [Label("Расписание")]
        public ICollection<TimetableRecord> Records
        {
            get => Item.TimetableRecords;
            set
            {
                Item.TimetableRecords = value;
                OnPropertyChanged();
            }
        }

        [EntitySelectorFormElement("Предметы")]
        [Label("Предмет")]
        public Subject Subject
        {
            get => Item.Subject;
            set
            {
                Item.Subject = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [EntitySelectorFormElement("Преподаватели")]
        [Label("Преподаватель")]
        public Teacher Teacher
        {
            get => Item.Teacher;
            set
            {
                Item.Teacher = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [EntitySelectorFormElement("Группы")]
        [Label("Группа")]
        public Group Group
        {
            get => Item.Group;
            set
            {
                Item.Group = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        public TimetableView(Timetable? item) : base(item)
        {
        }
    }
}
