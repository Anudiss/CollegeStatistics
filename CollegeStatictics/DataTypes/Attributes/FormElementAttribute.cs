using System;

namespace CollegeStatictics.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormElementAttribute : Attribute
    {
        public string DefaultValue { get; set; } = "";
        public bool IsReadOnly { get; set; } = false;
        public ElementType ElementType { get; protected set; } = ElementType.TextBox;
    }

    public enum ElementType
    {
        TextBox,
        NumberBox,
        SpinBox,
        EntitySelectorBox,
        Subtable,
        Timetable,
        RadioButton,
        DatePicker
    }
}
