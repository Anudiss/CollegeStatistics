using CollegeStatictics.ViewModels.Attributes;

namespace CollegeStatictics.DataTypes.Attributes
{
    public class DatePickerFormElementAttribute : FormElementAttribute
    {
        public DatePickerFormElementAttribute()
        {
            ElementType = ElementType.DatePicker;
        }
    }
}
