using CollegeStatictics.DataTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace CollegeStatictics.Database
{
    public partial class DatabaseContext
    {
        #region [ Properties ]

        private static DatabaseContext _entities = null!;
        public static DatabaseContext Entities => _entities ??= new();

        #endregion

        #region [ Public static methods ]

        public static void CancelChanges()
        {
            foreach (var entry in Entities.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public static void CancelChanges<T>(T item) where T : class
        {
            var entry = Entities.Entry(item);

            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;
            }
        }

        public int SaveChanges<TEntity>() where TEntity : class
        {
            var original = this.ChangeTracker.Entries()
                        .Where(x => !typeof(TEntity).IsAssignableFrom(x.Entity.GetType()) && x.State != EntityState.Unchanged)
                        .GroupBy(x => x.State)
                        .ToList();

            foreach (var entry in this.ChangeTracker.Entries().Where(x => !typeof(TEntity).IsAssignableFrom(x.Entity.GetType())))
            {
                entry.State = EntityState.Unchanged;
            }

            var rows = base.SaveChanges();

            foreach (var state in original)
            {
                foreach (var entry in state)
                {
                    entry.State = state.Key;
                }
            }

            return rows;
        }

        public static bool HasChanges<T>(T item) where T : class
        {
            var entry = Entities.Entry(item);
            return new[] { EntityState.Modified, EntityState.Deleted, EntityState.Added }.Contains(entry.State);
        }

        public static dynamic LoadEntities(Type entityType)
        {
            var setMethod = typeof(DbContext).GetMethod("Set", Type.EmptyTypes)!;
            var SetMethodForEntityOfType = setMethod.MakeGenericMethod(entityType);

            dynamic entitySet = SetMethodForEntityOfType.Invoke(Entities, Array.Empty<object>())!;
            EntityFrameworkQueryableExtensions.Load(entitySet);

            return entitySet;
        }

        #endregion
    }
}
