using CollegeStatictics.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;

namespace CollegeStatictics.DataTypes.Interfaces
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }

        void MarkToDelete() => IsDeleted = true;

        void UnmarkToDelete() => IsDeleted = false;
        
        void Delete()
        {
            RemoveLinkedData();

            var entities = DatabaseContext.LoadEntities(GetType());

            entities.Remove(this);

            DatabaseContext.Entities.SaveChanges();
        }

        void RemoveLinkedData() { }
    }
}
