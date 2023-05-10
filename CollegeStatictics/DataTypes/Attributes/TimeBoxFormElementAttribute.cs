using CollegeStatictics.ViewModels.Attributes;

namespace CollegeStatictics.DataTypes.Attributes
{
    public class TimeBoxFormElementAttribute : FormElementAttribute
    {
        public TimeBoxFormElementAttribute() => ElementType = ElementType.TimeBox;
    }
}
