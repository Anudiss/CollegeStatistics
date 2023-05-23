using CollegeStatictics.ViewModels.Attributes;
using System;

namespace CollegeStatictics.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SelectableSubtableFormElementAttribute : FormElementAttribute
    {
        public string? ItemContainerName { get; } = null;

        public string? FilterPropertyName { get; set; } = null;

        public SelectableSubtableFormElementAttribute(string itemContainerName)
        {
            ItemContainerName = itemContainerName;
            ElementType = ElementType.SelectableSubtable;
        }
    }
}
