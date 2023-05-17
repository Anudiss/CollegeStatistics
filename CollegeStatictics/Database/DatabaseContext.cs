using System.Collections.Generic;
using CollegeStatictics.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeStatictics.Database;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<DayOfWeek> DayOfWeeks { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EducationForm> EducationForms { get; set; }

    public virtual DbSet<EmergencySituation> EmergencySituations { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Homework> Homeworks { get; set; }

    public virtual DbSet<HomeworkExecutionStatus> HomeworkExecutionStatuses { get; set; }

    public virtual DbSet<HomeworkStudent> HomeworkStudents { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<LessonHomework> LessonHomeworks { get; set; }

    public virtual DbSet<LessonType> LessonTypes { get; set; }

    public virtual DbSet<NoteToLesson> NoteToLessons { get; set; }

    public virtual DbSet<NoteToStudent> NoteToStudents { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudyPlan> StudyPlans { get; set; }

    public virtual DbSet<StudyPlanRecord> StudyPlanRecords { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Timetable> Timetables { get; set; }

    public virtual DbSet<TimetableRecord> TimetableRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CollegeStatisticsNew;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => new { e.LessonId, e.StudentId }).HasName("PK_Attendance_1");

            entity.ToTable("Attendance");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_Lesson1");

            entity.HasOne(d => d.Student).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_Student");
        });

        modelBuilder.Entity<DayOfWeek>(entity =>
        {
            entity.ToTable("DayOfWeek");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Reduction).HasMaxLength(2);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<EducationForm>(entity =>
        {
            entity.ToTable("EducationForm");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<EmergencySituation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EmergencySituation_1");

            entity.ToTable("EmergencySituation");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Lesson).WithOne(p => p.EmergencySituation)
                .HasForeignKey<EmergencySituation>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmergencySituation_Lesson");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Group");

            entity.HasOne(d => d.EducationForm).WithMany(p => p.Groups)
                .HasForeignKey(d => d.EducationFormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Group_EducationForm");

            entity.HasOne(d => d.GroupLeader).WithMany(p => p.Groups)
                .HasForeignKey(d => d.GroupLeaderId)
                .HasConstraintName("FK_Group_Student");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Groups)
                .HasForeignKey(d => d.SpecialityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Group_Speciality");

            entity.HasOne(d => d.Curator).WithMany(p => p.Groups)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Group_Teacher");
        });

        modelBuilder.Entity<Homework>(entity =>
        {
            entity.ToTable("Homework");

            entity.Property(e => e.Topic).HasMaxLength(100);
        });

        modelBuilder.Entity<HomeworkExecutionStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ExecutionStatus");

            entity.ToTable("HomeworkExecutionStatus");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<HomeworkStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HomeworkStudent_1");

            entity.ToTable("HomeworkStudent");

            entity.HasOne(d => d.HomeworkExecutionStatus).WithMany(p => p.HomeworkStudents)
                .HasForeignKey(d => d.HomeworkExecutionStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonHomework_HomeworkExecutionStatus");

            entity.HasOne(d => d.Lesson).WithMany(p => p.HomeworkStudents)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HomeworkStudent_Lesson");

            entity.HasOne(d => d.Student).WithMany(p => p.HomeworkStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HomeworkStudent_Student");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Lesson_1");

            entity.ToTable("Lesson");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Time).HasPrecision(0);

            entity.HasOne(d => d.Group).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lesson_Group");

            entity.HasOne(d => d.StudyPlanRecord).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.StudyPlanRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lesson_StudyPlanRecord");
        });

        modelBuilder.Entity<LessonHomework>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK_LessonHomework_1");

            entity.ToTable("LessonHomework");

            entity.Property(e => e.LessonId).ValueGeneratedNever();
            entity.Property(e => e.Deadline).HasColumnType("smalldatetime");
            entity.Property(e => e.IssueDate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.Homework).WithMany(p => p.LessonHomeworks)
                .HasForeignKey(d => d.HomeworkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonHomework_Homework1");

            entity.HasOne(d => d.Lesson).WithOne(p => p.LessonHomework)
                .HasForeignKey<LessonHomework>(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonHomework_Lesson");
        });

        modelBuilder.Entity<LessonType>(entity =>
        {
            entity.ToTable("LessonType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<NoteToLesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NoteToLesson_1");

            entity.ToTable("NoteToLesson");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Lesson).WithOne(p => p.NoteToLesson)
                .HasForeignKey<NoteToLesson>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NoteToLesson_Lesson");
        });

        modelBuilder.Entity<NoteToStudent>(entity =>
        {
            entity.ToTable("NoteToStudent");

            entity.HasOne(d => d.Lesson).WithMany(p => p.NoteToStudents)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NoteToStudent_Lesson");

            entity.HasOne(d => d.Student).WithMany(p => p.NoteToStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NoteToStudent_Student");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.ToTable("Speciality");

            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Department).WithMany(p => p.Specialities)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Speciality_Department");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Student_Group");
        });

        modelBuilder.Entity<StudyPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_StudyPlan_1");

            entity.ToTable("StudyPlan");

            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Speciality).WithMany(p => p.StudyPlans)
                .HasForeignKey(d => d.SpecialityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudyPlan_Speciality");

            entity.HasOne(d => d.Subject).WithMany(p => p.StudyPlans)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudyPlan_Subject");
        });

        modelBuilder.Entity<StudyPlanRecord>(entity =>
        {
            entity.ToTable("StudyPlanRecord");

            entity.HasOne(d => d.LessonType).WithMany(p => p.StudyPlanRecords)
                .HasForeignKey(d => d.LessonTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudyPlanRecord_LessonType");

            entity.HasOne(d => d.StudyPlan).WithMany(p => p.StudyPlanRecords)
                .HasForeignKey(d => d.StudyPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudyPlanRecord_StudyPlan");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.ToTable("Subject");

            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.ToTable("Teacher");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<Timetable>(entity =>
        {
            entity.ToTable("Timetable");

            entity.HasOne(d => d.Group).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_Group");

            entity.HasOne(d => d.StudyPlan).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.StudyPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_StudyPlan");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_Teacher");
        });

        modelBuilder.Entity<TimetableRecord>(entity =>
        {
            entity.ToTable("TimetableRecord");

            entity.HasOne(d => d.DayOfWeek).WithMany(p => p.TimetableRecords)
                .HasForeignKey(d => d.DayOfWeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TimetableRecord_DayOfWeek");

            entity.HasOne(d => d.Timetable).WithMany(p => p.TimetableRecords)
                .HasForeignKey(d => d.TimetableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TimetableRecord_Timetable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
