using System;

namespace CollegeStatictics.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LabelAttribute : Attribute
    {
        public string Label { get; } = null!;

        public LabelAttribute(string label)
            => Label = label;
    }
}
