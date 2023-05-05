using CollegeStatictics.ViewModels.Attributes;

namespace CollegeStatictics.DataTypes.Attributes
{
    public class DatePickerFormElementAttribute : FormElementAttribute
    {
        public string DateFormat { get; set; } = "dd.MM.yyyy";

        public DatePickerFormElementAttribute()
        {
            ElementType = ElementType.DatePicker;
        }
    }
}
