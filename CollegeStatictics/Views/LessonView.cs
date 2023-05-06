using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace CollegeStatictics.Views
{
    public class LessonView : ItemDialog<Lesson>
    {
        #region [ Properties ]

        [Label("Дата и время")]
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

        [Label("Восстановление")]
        [CheckBoxFormElement]
        public bool IsRestoring
        {
            get => Item.IsRestoring;
            set
            {
                Item.IsRestoring = value;
                OnPropertyChanged();
            }
        }

        [Label("Запись из расписания")]
        public TimetableRecord TimetableRecord
        {
            get => Item.TimetableRecord;
            set
            {
                Item.TimetableRecord = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Subject));
                OnPropertyChanged(nameof(Teacher));
                OnPropertyChanged(nameof(Group));
            }
        }

        [Label("Предмет")]
        [TextBoxFormElement(IsReadOnly = true)]
        public Subject Subject => Item.TimetableRecord.Timetable.Subject;

        [Label("Преподаватель")]
        [TextBoxFormElement(IsReadOnly = true)]
        public Teacher Teacher => Item.TimetableRecord.Timetable.Teacher;

        [Label("Группа")]
        [TextBoxFormElement(IsReadOnly = true)]
        public Group Group => Item.TimetableRecord.Timetable.Group;


        #endregion

        public LessonView(Lesson? item) : base(item)
        {

        }

        protected override IEnumerable<FrameworkElement> CreateViewElements()
        {
            var viewElements = base.CreateViewElements();

            return null;
        }
    }
}
