using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;

namespace CollegeStatictics.Views;

[MaxHeight(600)]
public class EmergencySituationView : ItemDialog<EmergencySituation>
{
    [MinHeight(400)]
    [Label("Описание")]
    [TextBoxFormElement(AcceptsReturn = true)]
    public string Description
    {
        get => Item.Description;
        set
        {
            Item.Description = value;
            OnPropertyChanged();
        }
    }

    public EmergencySituationView(EmergencySituation? item) : base(item)
    {
    }
}
