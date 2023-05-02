using System;

namespace CollegeStatictics.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MinWidthAttribute : Attribute
    {
        public double Width { get; }

        public MinWidthAttribute(double width) =>
            Width = width;
    }
}
