using System;
using System.Collections;
using System.Collections.Generic;

namespace CollegeStatictics.DataTypes.Interfaces;

public interface IPropertyAccessor<T>
{
    public IEnumerable<Type> Parameters { get; }

    object? Get( T instance, object?[] index );
}