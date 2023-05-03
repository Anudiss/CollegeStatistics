using System;

namespace CollegeStatictics.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class MinHeightAttribute : Attribute
    {
        public double Height { get; }

        public MinHeightAttribute(double height) =>
            Height = height;
    }
}
