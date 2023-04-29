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

    public virtual DbSet<CommisionCurator> CommisionCurators { get; set; }

    public virtual DbSet<DayOfWeek> DayOfWeeks { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EducationForm> EducationForms { get; set; }

    public virtual DbSet<EmergencySituation> EmergencySituations { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Homework> Homeworks { get; set; }

    public virtual DbSet<HomeworkExecutionStatus> HomeworkExecutionStatuses { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<LessonType> LessonTypes { get; set; }

    public virtual DbSet<NoteToLesson> NoteToLessons { get; set; }

    public virtual DbSet<NoteToStudent> NoteToStudents { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudyPlan> StudyPlans { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Timetable> Timetables { get; set; }

    public virtual DbSet<TimetableRecord> TimetableRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StatiscticsSchool2;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.ToTable("Attendance");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_Lesson");

            entity.HasOne(d => d.Student).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_Student");
        });

        modelBuilder.Entity<CommisionCurator>(entity =>
        {
            entity.ToTable("CommisionCurator");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.Department).WithMany(p => p.CommisionCurators)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommisionCurator_Department");
        });

        modelBuilder.Entity<DayOfWeek>(entity =>
        {
            entity.ToTable("DayOfWeek");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<EducationForm>(entity =>
        {
            entity.ToTable("EducationForm");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<EmergencySituation>(entity =>
        {
            entity.ToTable("EmergencySituation");

            entity.HasOne(d => d.Lesson).WithMany(p => p.EmergencySituations)
                .HasForeignKey(d => d.LessonId)
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

            entity.Property(e => e.Deadline).HasColumnType("datetime");
            entity.Property(e => e.IssueDate).HasColumnType("datetime");

            entity.HasOne(d => d.ExecutionStatus).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.ExecutionStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Homework_HomeworkExecutionStatus");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Homework_Lesson");
        });

        modelBuilder.Entity<HomeworkExecutionStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ExecutionStatus");

            entity.ToTable("HomeworkExecutionStatus");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Lesson_1");

            entity.ToTable("Lesson");

            entity.Property(e => e.Datetime).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<LessonType>(entity =>
        {
            entity.ToTable("LessonType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<NoteToLesson>(entity =>
        {
            entity.ToTable("NoteToLesson");

            entity.HasOne(d => d.Lesson).WithMany(p => p.NoteToLessons)
                .HasForeignKey(d => d.LessonId)
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Group");
        });

        modelBuilder.Entity<StudyPlan>(entity =>
        {
            entity.HasKey(e => new { e.SpecialityId, e.SubjectId, e.Course, e.LessonTypeId });

            entity.ToTable("StudyPlan");

            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.LessonType).WithMany(p => p.StudyPlans)
                .HasForeignKey(d => d.LessonTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudyPlan_LessonType");

            entity.HasOne(d => d.Speciality).WithMany(p => p.StudyPlans)
                .HasForeignKey(d => d.SpecialityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudyPlan_Speciality");

            entity.HasOne(d => d.Subject).WithMany(p => p.StudyPlans)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudyPlan_Subject");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.ToTable("Subject");

            entity.Property(e => e.Name).HasMaxLength(50);
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

            entity.HasOne(d => d.Subject).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_Subject");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_Teacher");

            entity.HasOne(d => d.Group).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_Group");
        });

        modelBuilder.Entity<TimetableRecord>(entity =>
        {
            entity.ToTable("TimetableRecord");

            entity.Property(e => e.Time).HasPrecision(0);

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
