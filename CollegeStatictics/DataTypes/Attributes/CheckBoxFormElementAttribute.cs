using CollegeStatictics.ViewModels.Attributes;

namespace CollegeStatictics.DataTypes.Attributes
{
    public class CheckBoxFormElementAttribute : FormElementAttribute
    {
        public CheckBoxFormElementAttribute() => ElementType = ElementType.CheckBox;
    }
}
