using Microsoft.EntityFrameworkCore;

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
    }
}
