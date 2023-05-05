using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.Views
{
    public class StudyPlanRecordView : ItemDialog<StudyPlanRecord>
    {
        [DefaultValue("")]
        [Label("Описание")]
        [TextBoxFormElement(AcceptsReturn = true)]
        [MaxHeight(300)]
        public string Content
        {
            get => Item.Content;
            set
            {
                Item.Content = value;
                OnPropertyChanged();
            }
        }

        [Label("Длительность в парах")]
        [SpinBoxFormElement]
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

        [DefaultValue("")]
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

        public StudyPlanRecordView(StudyPlanRecord? item) : base(item)
        {
        }
    }
}
