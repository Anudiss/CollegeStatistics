using CollegeStatictics.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;

namespace CollegeStatictics.DataTypes.Interfaces
{
    public interface IDeletable
    {
        public bool IsDeleted { get; set; }

        public void MarkToDelete()
        {
            IsDeleted = true;
        }

        public void Delete()
        {
            RemoveLinkedData();

            MethodInfo method = typeof(DbContext).GetMethod("Set", Type.EmptyTypes)!;
            MethodInfo genericMethod = method.MakeGenericMethod(GetType());

            dynamic values = genericMethod.Invoke(DatabaseContext.Entities, Array.Empty<object>())!;
            EntityFrameworkQueryableExtensions.Load(values);

            values.Remove(this);

            DatabaseContext.Entities.SaveChanges();
        }

        public void RemoveLinkedData() { }
    }
}
