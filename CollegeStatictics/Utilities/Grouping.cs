using CollegeStatictics.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace CollegeStatictics.Utilities
{
    public class Grouping<T>
    {
        public string PropertyPath { get; }

        public Grouping(string propertyPath)
        {
            MethodInfo method = typeof(DbContext).GetMethod("Set", Type.EmptyTypes)!;
            MethodInfo genericMethod = method.MakeGenericMethod(typeof(T));

            dynamic values = genericMethod.Invoke(DatabaseContext.Entities, Array.Empty<object>())!;
            EntityFrameworkQueryableExtensions.Load(values);

            PropertyPath = propertyPath;
        }
    }
}
