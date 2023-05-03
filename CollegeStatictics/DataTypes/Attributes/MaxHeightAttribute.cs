using System;

namespace CollegeStatictics.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class  MaxHeightAttribute : Attribute
    {
        public double Height { get; }

        public MaxHeightAttribute(double height) => Height = height;
    }
}
