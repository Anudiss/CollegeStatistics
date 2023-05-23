using CollegeStatictics.DataTypes.Interfaces;

using System;
using System.Collections.Generic;

namespace CollegeStatictics.ViewModels.Base
{
    public class PropertyAccessor<T, TProperty> : IPropertyAccessor<T>
    {
        private readonly Func<T, object?[], TProperty?> _getter;
        public IEnumerable<Type> Parameters { get; }

        public static PropertyAccessor<T, TProperty> Create( Func<T, object?[]?, TProperty> expression, IEnumerable<Type> parameters ) =>
            new(expression!, parameters);

        public PropertyAccessor( Func<T, object?[], TProperty?> getter, IEnumerable<Type> parameters )
        {
            _getter = getter;
            Parameters = parameters;
        }

        public object? Get( T instance, object?[] index ) => _getter.Invoke(instance, index);
    }
}