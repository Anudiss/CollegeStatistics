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


    [Label("Время начала")]
    [TextBoxFormElement]
    public DateTime IssueDate
    {
        get => Item.IssueDate;
        set
        {
            Item.IssueDate = value;
            OnPropertyChanged();
        }
    }

    [Label("Время окончания")]
    [TextBoxFormElement]
    public DateTime Deadline
    {
        get => Item.Deadline;
        set
        {
            Item.Deadline = value;
            OnPropertyChanged();
        }
    }

    public LessonHomeworkView(LessonHomework? item) : base(item)
    {
    }
}
