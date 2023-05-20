﻿// <auto-generated />
using System;
using CollegeStatictics.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CollegeStatictics.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230517145704_first")]
    partial class first
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("CollegeStatictics.Database.Models.Attendance", b =>
                {
                    b.Property<int>("LessonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAttented")
                        .HasColumnType("INTEGER");

                    b.HasKey("LessonId", "StudentId")
                        .HasName("PK_Attendance_1");

                    b.HasIndex("StudentId");

                    b.ToTable("Attendance", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.DayOfWeek", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Reduction")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DayOfWeek", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Department", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.EducationForm", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EducationForm", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.EmergencySituation", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasName("PK_EmergencySituation_1");

                    b.ToTable("EmergencySituation", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<short>("CreationYear")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EducationFormId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GroupLeaderId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpecialityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeacherId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EducationFormId");

                    b.HasIndex("GroupLeaderId");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Group", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Homework", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.HomeworkExecutionStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasName("PK_ExecutionStatus");

                    b.ToTable("HomeworkExecutionStatus", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.HomeworkStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("HomeworkExecutionStatusId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LessonId")
                        .HasColumnType("INTEGER");

                    b.Property<short?>("Mark")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id")
                        .HasName("PK_HomeworkStudent_1");

                    b.HasIndex("HomeworkExecutionStatusId");

                    b.HasIndex("LessonId");

                    b.HasIndex("StudentId");

                    b.ToTable("HomeworkStudent", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRestoring")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudyPlanRecordId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("Time")
                        .HasPrecision(0)
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasName("PK_Lesson_1");

                    b.HasIndex("GroupId");

                    b.HasIndex("StudyPlanRecordId");

                    b.ToTable("Lesson", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.LessonHomework", b =>
                {
                    b.Property<int>("LessonId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("smalldatetime");

                    b.Property<int>("HomeworkId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("smalldatetime");

                    b.HasKey("LessonId")
                        .HasName("PK_LessonHomework_1");

                    b.HasIndex("HomeworkId");

                    b.ToTable("LessonHomework", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.LessonType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LessonType", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.NoteToLesson", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasName("PK_NoteToLesson_1");

                    b.ToTable("NoteToLesson", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.NoteToStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("LessonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("StudentId");

                    b.ToTable("NoteToStudent", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Speciality", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Student", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.StudyPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Course")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpecialityId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("SubjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id")
                        .HasName("PK_StudyPlan_1");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudyPlan", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.StudyPlanRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DurationInLessons")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LessonTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudyPlanId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LessonTypeId");

                    b.HasIndex("StudyPlanId");

                    b.ToTable("StudyPlanRecord", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Subject", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Teacher", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Timetable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudyPlanId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeacherId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("StudyPlanId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Timetable", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.TimetableRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Couple")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DayOfWeekId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimetableId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DayOfWeekId");

                    b.HasIndex("TimetableId");

                    b.ToTable("TimetableRecord", (string)null);
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Attendance", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.Lesson", "Lesson")
                        .WithMany("Attendances")
                        .HasForeignKey("LessonId")
                        .IsRequired()
                        .HasConstraintName("FK_Attendance_Lesson1");

                    b.HasOne("CollegeStatictics.Database.Models.Student", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK_Attendance_Student");

                    b.Navigation("Lesson");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.EmergencySituation", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.Lesson", "Lesson")
                        .WithOne("EmergencySituation")
                        .HasForeignKey("CollegeStatictics.Database.Models.EmergencySituation", "Id")
                        .IsRequired()
                        .HasConstraintName("FK_EmergencySituation_Lesson");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Group", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.EducationForm", "EducationForm")
                        .WithMany("Groups")
                        .HasForeignKey("EducationFormId")
                        .IsRequired()
                        .HasConstraintName("FK_Group_EducationForm");

                    b.HasOne("CollegeStatictics.Database.Models.Student", "GroupLeader")
                        .WithMany("Groups")
                        .HasForeignKey("GroupLeaderId")
                        .HasConstraintName("FK_Group_Student");

                    b.HasOne("CollegeStatictics.Database.Models.Speciality", "Speciality")
                        .WithMany("Groups")
                        .HasForeignKey("SpecialityId")
                        .IsRequired()
                        .HasConstraintName("FK_Group_Speciality");

                    b.HasOne("CollegeStatictics.Database.Models.Teacher", "Curator")
                        .WithMany("Groups")
                        .HasForeignKey("TeacherId")
                        .IsRequired()
                        .HasConstraintName("FK_Group_Teacher");

                    b.Navigation("Curator");

                    b.Navigation("EducationForm");

                    b.Navigation("GroupLeader");

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.HomeworkStudent", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.HomeworkExecutionStatus", "HomeworkExecutionStatus")
                        .WithMany("HomeworkStudents")
                        .HasForeignKey("HomeworkExecutionStatusId")
                        .IsRequired()
                        .HasConstraintName("FK_LessonHomework_HomeworkExecutionStatus");

                    b.HasOne("CollegeStatictics.Database.Models.Lesson", "Lesson")
                        .WithMany("HomeworkStudents")
                        .HasForeignKey("LessonId")
                        .IsRequired()
                        .HasConstraintName("FK_HomeworkStudent_Lesson");

                    b.HasOne("CollegeStatictics.Database.Models.Student", "Student")
                        .WithMany("HomeworkStudents")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK_HomeworkStudent_Student");

                    b.Navigation("HomeworkExecutionStatus");

                    b.Navigation("Lesson");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Lesson", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.Group", "Group")
                        .WithMany("Lessons")
                        .HasForeignKey("GroupId")
                        .IsRequired()
                        .HasConstraintName("FK_Lesson_Group");

                    b.HasOne("CollegeStatictics.Database.Models.StudyPlanRecord", "StudyPlanRecord")
                        .WithMany("Lessons")
                        .HasForeignKey("StudyPlanRecordId")
                        .IsRequired()
                        .HasConstraintName("FK_Lesson_StudyPlanRecord");

                    b.Navigation("Group");

                    b.Navigation("StudyPlanRecord");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.LessonHomework", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.Homework", "Homework")
                        .WithMany("LessonHomeworks")
                        .HasForeignKey("HomeworkId")
                        .IsRequired()
                        .HasConstraintName("FK_LessonHomework_Homework1");

                    b.HasOne("CollegeStatictics.Database.Models.Lesson", "Lesson")
                        .WithOne("LessonHomework")
                        .HasForeignKey("CollegeStatictics.Database.Models.LessonHomework", "LessonId")
                        .IsRequired()
                        .HasConstraintName("FK_LessonHomework_Lesson");

                    b.Navigation("Homework");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.NoteToLesson", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.Lesson", "Lesson")
                        .WithOne("NoteToLesson")
                        .HasForeignKey("CollegeStatictics.Database.Models.NoteToLesson", "Id")
                        .IsRequired()
                        .HasConstraintName("FK_NoteToLesson_Lesson");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.NoteToStudent", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.Lesson", "Lesson")
                        .WithMany("NoteToStudents")
                        .HasForeignKey("LessonId")
                        .IsRequired()
                        .HasConstraintName("FK_NoteToStudent_Lesson");

                    b.HasOne("CollegeStatictics.Database.Models.Student", "Student")
                        .WithMany("NoteToStudents")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK_NoteToStudent_Student");

                    b.Navigation("Lesson");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Speciality", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.Department", "Department")
                        .WithMany("Specialities")
                        .HasForeignKey("DepartmentId")
                        .IsRequired()
                        .HasConstraintName("FK_Speciality_Department");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Student", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK_Student_Group");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.StudyPlan", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.Speciality", "Speciality")
                        .WithMany("StudyPlans")
                        .HasForeignKey("SpecialityId")
                        .IsRequired()
                        .HasConstraintName("FK_StudyPlan_Speciality");

                    b.HasOne("CollegeStatictics.Database.Models.Subject", "Subject")
                        .WithMany("StudyPlans")
                        .HasForeignKey("SubjectId")
                        .IsRequired()
                        .HasConstraintName("FK_StudyPlan_Subject");

                    b.Navigation("Speciality");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.StudyPlanRecord", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.LessonType", "LessonType")
                        .WithMany("StudyPlanRecords")
                        .HasForeignKey("LessonTypeId")
                        .IsRequired()
                        .HasConstraintName("FK_StudyPlanRecord_LessonType");

                    b.HasOne("CollegeStatictics.Database.Models.StudyPlan", "StudyPlan")
                        .WithMany("StudyPlanRecords")
                        .HasForeignKey("StudyPlanId")
                        .IsRequired()
                        .HasConstraintName("FK_StudyPlanRecord_StudyPlan");

                    b.Navigation("LessonType");

                    b.Navigation("StudyPlan");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Timetable", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.Group", "Group")
                        .WithMany("Timetables")
                        .HasForeignKey("GroupId")
                        .IsRequired()
                        .HasConstraintName("FK_Timetable_Group");

                    b.HasOne("CollegeStatictics.Database.Models.StudyPlan", "StudyPlan")
                        .WithMany("Timetables")
                        .HasForeignKey("StudyPlanId")
                        .IsRequired()
                        .HasConstraintName("FK_Timetable_StudyPlan");

                    b.HasOne("CollegeStatictics.Database.Models.Teacher", "Teacher")
                        .WithMany("Timetables")
                        .HasForeignKey("TeacherId")
                        .IsRequired()
                        .HasConstraintName("FK_Timetable_Teacher");

                    b.Navigation("Group");

                    b.Navigation("StudyPlan");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.TimetableRecord", b =>
                {
                    b.HasOne("CollegeStatictics.Database.Models.DayOfWeek", "DayOfWeek")
                        .WithMany("TimetableRecords")
                        .HasForeignKey("DayOfWeekId")
                        .IsRequired()
                        .HasConstraintName("FK_TimetableRecord_DayOfWeek");

                    b.HasOne("CollegeStatictics.Database.Models.Timetable", "Timetable")
                        .WithMany("TimetableRecords")
                        .HasForeignKey("TimetableId")
                        .IsRequired()
                        .HasConstraintName("FK_TimetableRecord_Timetable");

                    b.Navigation("DayOfWeek");

                    b.Navigation("Timetable");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.DayOfWeek", b =>
                {
                    b.Navigation("TimetableRecords");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Department", b =>
                {
                    b.Navigation("Specialities");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.EducationForm", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Group", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Students");

                    b.Navigation("Timetables");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Homework", b =>
                {
                    b.Navigation("LessonHomeworks");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.HomeworkExecutionStatus", b =>
                {
                    b.Navigation("HomeworkStudents");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Lesson", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("EmergencySituation");

                    b.Navigation("HomeworkStudents");

                    b.Navigation("LessonHomework");

                    b.Navigation("NoteToLesson");

                    b.Navigation("NoteToStudents");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.LessonType", b =>
                {
                    b.Navigation("StudyPlanRecords");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Speciality", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("StudyPlans");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Student", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Groups");

                    b.Navigation("HomeworkStudents");

                    b.Navigation("NoteToStudents");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.StudyPlan", b =>
                {
                    b.Navigation("StudyPlanRecords");

                    b.Navigation("Timetables");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.StudyPlanRecord", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Subject", b =>
                {
                    b.Navigation("StudyPlans");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Teacher", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Timetables");
                });

            modelBuilder.Entity("CollegeStatictics.Database.Models.Timetable", b =>
                {
                    b.Navigation("TimetableRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
