﻿using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace CollegeStatictics.Views
{
    public class StudyPlanRecordView : ItemDialog<StudyPlanRecord>
    {
        [DefaultValue("")]
        [Label("Описание")]
        [TextBoxFormElement(AcceptsReturn = true)]
        //[MinHeight(300)]
        //[MaxHeight(300)]
        public string Content
        {
            get => Item.Content;
            set
            {
                Item.Content = value;
                OnPropertyChanged();
            }
        }

        // TODO: Fix spin box default value
        [DefaultValue(2)]
        [Label("Длительность в парах")]
        [SpinBoxFormElement]
        [Range(1, int.MaxValue)]
        public int DurationInLessons
        {
            get => Item.DurationInLessons;
            set
            {
                /*if (int.TryParse(value?.Trim(), out var duration))
                {
                    Item.DurationInLessons = duration;
                    OnPropertyChanged();
                    ValidateProperty(value);
                }*/

                Item.DurationInLessons = value;
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

        [DefaultValue("")]
        [Label("Тема")]
        [TextBoxFormElement]
        public string Topic
        {
            get => Item.Topic;
            set
            {
                Item.Topic = value;
                OnPropertyChanged();
            }
        }

        public StudyPlanRecordView(StudyPlanRecord? item) : base(item)
        {
        }
    }
}