using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;

using System.ComponentModel.DataAnnotations;

namespace CollegeStatictics.Views;

public class HomeworkView : ItemDialog<Homework>
{
    [TextBoxFormElement(AcceptsReturn = true)]
    [MinHeight(400)]
    [Label("Описание")]
    public string Description
    {
        get => Item.Text;
        set
        {
            Item.Text = value;
            OnPropertyChanged();
        }
    }

    [Required(ErrorMessage = "Обязательное поле")]
    [TextBoxFormElement]
    [Label("Тема")]
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

    public HomeworkView(Homework? item) : base(item)
    {
    }
}
