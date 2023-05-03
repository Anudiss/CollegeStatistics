using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using System;

namespace CollegeStatictics.DataTypes.Attributes
{
    public class SubtableFormElementAttribute : FormElementAttribute
    {
        private Type _type;

        public SubtableFormElementAttribute(Type type)
        {
            ElementType = ElementType.Subtable;
            _type = type;
        }

        public ItemDialog<T> Create<T>(T value) where T : class, ITable =>
            (ItemDialog<T>)Activator.CreateInstance(_type, new[] { value });

        public object Create(object? value) =>
            Activator.CreateInstance(_type, new[] { value });
    }
}
