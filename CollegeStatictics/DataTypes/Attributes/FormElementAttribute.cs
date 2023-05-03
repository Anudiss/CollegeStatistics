using System;

namespace CollegeStatictics.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormElementAttribute : Attribute
    {
        public bool IsReadOnly { get; set; } = false;
        public ElementType ElementType { get; protected set; } = ElementType.TextBox;
    }

    public enum ElementType
    {
        TextBox,
        NumberBox,
        SpinBox,
        EntitySelectorBox,
        SelectableSubtable,
        Subtable,
        Timetable,
        RadioButton,
        DatePicker
    }
}
