using System;

namespace CollegeStatictics.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormElementAttribute : Attribute
    {
        public string DefaultValue { get; set; } = "";
        public bool IsReadOnly { get; set; } = false;
        public ElementType ElementType { get; set; } = ElementType.TextBox;
    }

    public enum ElementType
    {
        TextBox,
        EntitySelectorBox,
        Subtable,
        Timetable,
        RadioButton
    }
}
