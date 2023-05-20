using System.Collections.Generic;
using System.Reflection;

namespace CollegeStatictics.DataTypes.Interfaces;

public interface IPropertyAccessor<TSource> where TSource : class, new()
{
    public IEnumerable<PropertyInfo> PropertyChain { get; }

    public object GetValue(TSource source);

    public void SetValue(TSource source, object value);
}
