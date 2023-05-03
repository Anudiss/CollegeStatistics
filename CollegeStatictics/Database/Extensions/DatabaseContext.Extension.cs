using CollegeStatictics.DataTypes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CollegeStatictics.Database
{
    public partial class DatabaseContext
    {
        private static DatabaseContext _entities = null!;
        public static DatabaseContext Entities => _entities ??= new();

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

        public static bool HasChanges<T>(T item) where T : class
        {
            var entry = Entities.Entry(item);
            return new[] { EntityState.Modified, EntityState.Deleted, EntityState.Added }.Contains(entry.State);
        }
    }
}
