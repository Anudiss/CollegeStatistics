using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeStatictics.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayOfWeek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Reduction = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOfWeek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationForm",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationForm", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Homework",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Topic = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homework", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkExecutionStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Speciality_Department",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudyPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SpecialityId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    Course = table.Column<byte>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPlan_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyPlan_Speciality",
                        column: x => x.SpecialityId,
                        principalTable: "Speciality",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudyPlan_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudyPlanRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LessonTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    DurationInLessons = table.Column<int>(type: "INTEGER", nullable: false),
                    Topic = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    StudyPlanId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPlanRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyPlanRecord_LessonType",
                        column: x => x.LessonTypeId,
                        principalTable: "LessonType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudyPlanRecord_StudyPlan",
                        column: x => x.StudyPlanId,
                        principalTable: "StudyPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAttented = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance_1", x => new { x.LessonId, x.StudentId });
                });

            migrationBuilder.CreateTable(
                name: "EmergencySituation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencySituation_1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreationYear = table.Column<short>(type: "INTEGER", nullable: false),
                    EducationFormId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeacherId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecialityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupLeaderId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_EducationForm",
                        column: x => x.EducationFormId,
                        principalTable: "EducationForm",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Group_Speciality",
                        column: x => x.SpecialityId,
                        principalTable: "Speciality",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Group_Teacher",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudyPlanRecordId = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    Time = table.Column<TimeSpan>(type: "TEXT", precision: 0, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRestoring = table.Column<bool>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lesson_Group",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lesson_StudyPlanRecord",
                        column: x => x.StudyPlanRecordId,
                        principalTable: "StudyPlanRecord",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Group",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Timetable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeacherId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudyPlanId = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timetable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timetable_Group",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Timetable_StudyPlan",
                        column: x => x.StudyPlanId,
                        principalTable: "StudyPlan",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Timetable_Teacher",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LessonHomework",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeworkId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deadline = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonHomework_1", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_LessonHomework_Homework1",
                        column: x => x.HomeworkId,
                        principalTable: "Homework",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LessonHomework_Lesson",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NoteToLesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteToLesson_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteToLesson_Lesson",
                        column: x => x.Id,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HomeworkStudent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LessonId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeworkExecutionStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Mark = table.Column<short>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkStudent_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeworkStudent_Lesson",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HomeworkStudent_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LessonHomework_HomeworkExecutionStatus",
                        column: x => x.HomeworkExecutionStatusId,
                        principalTable: "HomeworkExecutionStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NoteToStudent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LessonId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteToStudent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteToStudent_Lesson",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NoteToStudent_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimetableRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Couple = table.Column<int>(type: "INTEGER", nullable: false),
                    DayOfWeekId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimetableId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimetableRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimetableRecord_DayOfWeek",
                        column: x => x.DayOfWeekId,
                        principalTable: "DayOfWeek",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimetableRecord_Timetable",
                        column: x => x.TimetableId,
                        principalTable: "Timetable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_StudentId",
                table: "Attendance",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_EducationFormId",
                table: "Group",
                column: "EducationFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_GroupLeaderId",
                table: "Group",
                column: "GroupLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_SpecialityId",
                table: "Group",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_TeacherId",
                table: "Group",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkStudent_HomeworkExecutionStatusId",
                table: "HomeworkStudent",
                column: "HomeworkExecutionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkStudent_LessonId",
                table: "HomeworkStudent",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkStudent_StudentId",
                table: "HomeworkStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_GroupId",
                table: "Lesson",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_StudyPlanRecordId",
                table: "Lesson",
                column: "StudyPlanRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonHomework_HomeworkId",
                table: "LessonHomework",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteToStudent_LessonId",
                table: "NoteToStudent",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteToStudent_StudentId",
                table: "NoteToStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Speciality_DepartmentId",
                table: "Speciality",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_GroupId",
                table: "Student",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlan_SpecialityId",
                table: "StudyPlan",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlan_SubjectId",
                table: "StudyPlan",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlanRecord_LessonTypeId",
                table: "StudyPlanRecord",
                column: "LessonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlanRecord_StudyPlanId",
                table: "StudyPlanRecord",
                column: "StudyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Timetable_GroupId",
                table: "Timetable",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Timetable_StudyPlanId",
                table: "Timetable",
                column: "StudyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Timetable_TeacherId",
                table: "Timetable",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TimetableRecord_DayOfWeekId",
                table: "TimetableRecord",
                column: "DayOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_TimetableRecord_TimetableId",
                table: "TimetableRecord",
                column: "TimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Lesson",
                table: "Attendance",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Student",
                table: "Attendance",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencySituation_Lesson",
                table: "EmergencySituation",
                column: "Id",
                principalTable: "Lesson",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Student",
                table: "Group",
                column: "GroupLeaderId",
                principalTable: "Student",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Student",
                table: "Group");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "EmergencySituation");

            migrationBuilder.DropTable(
                name: "HomeworkStudent");

            migrationBuilder.DropTable(
                name: "LessonHomework");

            migrationBuilder.DropTable(
                name: "NoteToLesson");

            migrationBuilder.DropTable(
                name: "NoteToStudent");

            migrationBuilder.DropTable(
                name: "TimetableRecord");

            migrationBuilder.DropTable(
                name: "HomeworkExecutionStatus");

            migrationBuilder.DropTable(
                name: "Homework");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "DayOfWeek");

            migrationBuilder.DropTable(
                name: "Timetable");

            migrationBuilder.DropTable(
                name: "StudyPlanRecord");

            migrationBuilder.DropTable(
                name: "LessonType");

            migrationBuilder.DropTable(
                name: "StudyPlan");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "EducationForm");

            migrationBuilder.DropTable(
                name: "Speciality");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
