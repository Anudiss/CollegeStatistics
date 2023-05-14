using CollegeStatictics.ViewModels.Attributes;

using System;

namespace CollegeStatictics.DataTypes.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class EditableSubtableFormElementAttribute : FormElementAttribute
{
    public EditableSubtableFormElementAttribute()
    {
        ElementType = ElementType.EditableSubtable;
    }
}
