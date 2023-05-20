using CollegeStatictics.DataTypes.Interfaces;

using ModernWpf.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CollegeStatictics.ViewModels.Base;

public class PropertyAccessor<TSource, TProperty> : IPropertyAccessor<TSource> where TSource : class, new()
{
    public IEnumerable<PropertyInfo> PropertyChain { get; }

    public PropertyAccessor( IEnumerable<PropertyInfo> propertyChain ) =>
        PropertyChain = propertyChain;

    public PropertyAccessor( Expression<Func<TSource, TProperty>> property ) : this(GetPropertyChain(property).Reverse())
    { }

    public object GetValue( TSource source )
    {
        object tempSource = source;
        foreach (var propertyInfo in PropertyChain)
            tempSource = propertyInfo.GetValue(tempSource)!;

        return tempSource;
    }

    public void SetValue( TSource source, object value )
    {
        object tempSource = source;
        foreach (var propertyInfo in PropertyChain.Take(PropertyChain.Count() - 1))
            tempSource = propertyInfo.GetValue(tempSource)!;

        PropertyChain.Last().SetValue(tempSource, value);
    }

    public static PropertyAccessor<TSource, TProperty> GetPropertyAccessor( Expression<Func<TSource, TProperty>> expr )
    {
        List<PropertyInfo> propertyInfos = new();

        var me = (object)expr.Body.NodeType switch
        {
            ExpressionType.Convert or ExpressionType.ConvertChecked => ( ( expr.Body is UnaryExpression ue ) ? ue.Operand : null )
                                                                       as MemberExpression,
            _ => expr.Body as MemberExpression,
        };

        while (me != null)
        {
            propertyInfos.Add((PropertyInfo)me.Member);
            me = me.Expression as MemberExpression;
        }

        propertyInfos.Reverse();

        return new(propertyInfos);
    }

    public static IEnumerable<PropertyInfo> GetPropertyChain( Expression<Func<TSource, TProperty>> expr )
    {
        var me = (object)expr.Body.NodeType switch
        {
            ExpressionType.Convert or ExpressionType.ConvertChecked => ( ( expr.Body is UnaryExpression ue ) ? ue.Operand : null )
                                                                       as MemberExpression,
            _ => expr.Body as MemberExpression,
        };

        while (me != null)
        {
            yield return (PropertyInfo)me.Member;
            me = me.Expression as MemberExpression;
        }
    }
}
