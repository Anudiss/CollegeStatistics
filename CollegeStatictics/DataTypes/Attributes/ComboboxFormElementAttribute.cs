using CollegeStatictics.ViewModels.Attributes;

namespace CollegeStatictics.DataTypes.Attributes
{
    public class ComboBoxFormElementAttribute : FormElementAttribute
    {
        ComboBoxFormElementAttribute() => ElementType = ElementType.ComboBox;
    }
}
