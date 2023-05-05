using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Base;
using System;

namespace CollegeStatictics.Views
{
    public class LessonView : ItemDialog<Lesson>
    {
        [DatePickerFormElement]
        public DateTime DateTime
        {
            get => Item.Datetime;
            set
            {
                Item.Datetime = value;
                OnPropertyChanged();
            }
        }

        public LessonView(Lesson? item) : base(item)
        {
        }
    }
}
