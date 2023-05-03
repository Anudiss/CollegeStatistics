using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.Views
{
    public class StudyPlanRecordView : ItemDialog<StudyPlanRecord>
    {
        [Label("Тема")]
        [TextBoxFormElement]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Topic
        {
            get => Item.Topic;
            set
            {
                Item.Topic = value;
                OnPropertyChanged();
                ValidateProperty(value);
            }
        }

        [Label("Тип пары")]
        [RadioButtonFormElement]
        public LessonType LessonType
        {
            get => Item.LessonType;
            set
            {
                Item.LessonType = value;
                OnPropertyChanged();
            }
        }

        [Label("Длительность в парах")]
        [NumberBoxFormElement]
        [Range(1, int.MaxValue)]
        public string DurationInLessons
        {
            get => $"{Item.DurationInLessons}";
            set
            {
                if (int.TryParse(value?.Trim(), out var duration))
                {
                    Item.DurationInLessons = duration;
                    OnPropertyChanged();
                    ValidateProperty(value);
                }
            }
        }

        [Label("Описание")]
        [TextBoxFormElement(AcceptsReturn = true)]
        [MinHeight(300)]
        public string Content
        {
            get => Item.Content;
            set
            {
                Item.Content = value;
                OnPropertyChanged();
            }
        }

        public StudyPlanRecordView(StudyPlanRecord? item) : base(item)
        {
        }
    }
}
