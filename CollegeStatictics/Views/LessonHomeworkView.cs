using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;

using System;

namespace CollegeStatictics.Views;

public class LessonHomeworkView : ItemDialog<LessonHomework>
{
    [Label("Домашняя работа")]
    [EntitySelectorFormElement("Домашняя работа")]
    public Homework Homework
    {
        get => Item.Homework;
        set
        {
            Item.Homework = value;
            OnPropertyChanged();
        }
    }

    [Label("Время окончания")]
    [DatePickerFormElement]
    public DateTime Deadline
    {
        get => Item.Deadline;
        set
        {
            Item.Deadline = value;
            OnPropertyChanged();
        }
    }

    [Label("Время начала")]
    [DatePickerFormElement]
    public DateTime IssueDate
    {
        get => Item.IssueDate;
        set
        {
            Item.IssueDate = value;
            OnPropertyChanged();
        }
    }

    public LessonHomeworkView(LessonHomework? item) : base(item)
    {
    }
}
