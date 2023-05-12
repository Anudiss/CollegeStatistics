using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        #region [ OnModelCreating ]

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EducationForm>(entity =>
            {
                entity.Property(d => d.Id).ValueGeneratedNever();

                entity.HasData(new() { Id = 0, Name = "Бюджет" }, new() { Id = 1, Name = "Коммерция" });
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Navigation(d => d.Group).AutoInclude();
            });

            modelBuilder.Entity<StudyPlan>(entity =>
            {
                entity.Navigation(d => d.Speciality).AutoInclude();

                entity.Navigation(d => d.StudyPlanRecords).AutoInclude();

                entity.Navigation(d => d.Subject).AutoInclude();
            });

            modelBuilder.Entity<StudyPlanRecord>(entity =>
            {
                entity.Navigation(d => d.LessonType).AutoInclude();
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Navigation(d => d.NoteToLessons).AutoInclude();

                entity.Navigation(d => d.EmergencySituations).AutoInclude();

                entity.Navigation(d => d.Attendances).AutoInclude();

                entity.Navigation(d => d.Homeworks).AutoInclude();

                entity.Navigation(d => d.NoteToStudents).AutoInclude();

                entity.Navigation(d => d.TimetableRecord).AutoInclude();
            });

            modelBuilder.Entity<NoteToStudent>(entity =>
            {
                entity.Navigation(d => d.Student).AutoInclude();
            });

            modelBuilder.Entity<Homework>(entity => 
            {
                entity.Navigation(d => d.ExecutionStatus).AutoInclude();
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Navigation(d => d.Students).AutoInclude();

                entity.Navigation(d => d.Speciality).AutoInclude();

                entity.Navigation(d => d.Curator).AutoInclude();

                entity.Navigation(d => d.EducationForm).AutoInclude();
            });

            modelBuilder.Entity<Timetable>(entity =>
            {
                entity.Navigation(d => d.Group).AutoInclude();

                entity.Navigation(d => d.Subject).AutoInclude();

                entity.Navigation(d => d.Teacher).AutoInclude();

                entity.Navigation(d => d.TimetableRecords).AutoInclude();
            });

            modelBuilder.Entity<TimetableRecord>(entity =>
            {
                entity.Navigation(d => d.DayOfWeek).AutoInclude();
            });

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.Navigation(d => d.Department).AutoInclude();
            });
        }

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

        public void SaveChanges<TEntity>(TEntity entity)
        {
            var entry = Entities.Entry(entity!);
            entry.State = EntityState.Unchanged;
            entry.GetInfrastructure().AcceptChanges();
        }

        public int SaveChanges<TEntity>() where TEntity : class
        {
            var original = ChangeTracker.Entries()
                        .Where(x => !typeof(TEntity).IsAssignableFrom(x.Entity.GetType()) && x.State != EntityState.Unchanged)
                        .GroupBy(x => x.State)
                        .ToList();

            foreach (var entry in ChangeTracker.Entries().Where(x => !typeof(TEntity).IsAssignableFrom(x.Entity.GetType())))
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
