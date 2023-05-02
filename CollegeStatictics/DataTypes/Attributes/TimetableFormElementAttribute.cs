using CollegeStatictics.ViewModels.Attributes;

namespace CollegeStatictics.DataTypes.Attributes
{
    public class TimetableFormElementAttribute : FormElementAttribute
    {
        public TimetableFormElementAttribute() =>
            ElementType = ElementType.Timetable;
        }
    }
}
