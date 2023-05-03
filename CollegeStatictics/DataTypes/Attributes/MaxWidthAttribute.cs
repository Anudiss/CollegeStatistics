using System;

namespace CollegeStatictics.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class MaxWidthAttribute : Attribute
    {
        public double Width { get; }

        public MaxWidthAttribute(double width) => Width = width;
    }
}
