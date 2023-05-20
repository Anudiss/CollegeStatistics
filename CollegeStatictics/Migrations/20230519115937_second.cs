using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeStatictics.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DayOfWeek",
                columns: new[] { "Id", "Name", "Reduction" },
                values: new object[,]
                {
                    { 0, "Воскресенье", "Вс" },
                    { 1, "Понедельник", "Пн" },
                    { 2, "Вторник", "Вт" },
                    { 3, "Среда", "Ср" },
                    { 4, "Четверг", "Чт" },
                    { 5, "Пятница", "Пт" },
                    { 6, "Суббота", "Сб" }
                });

            migrationBuilder.InsertData(
                table: "EducationForm",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 0, "Бюджет" },
                    { 1, "Коммерция" }
                });

            migrationBuilder.InsertData(
                table: "HomeworkExecutionStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "В работе" },
                    { 2, "Выполнено" },
                    { 3, "Не выполнено" }
                });

            migrationBuilder.InsertData(
                table: "LessonType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Лекция" },
                    { 1, "Практика" },
                    { 2, "Лабораторная работа" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DayOfWeek",
                keyColumn: "Id",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "DayOfWeek",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DayOfWeek",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DayOfWeek",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DayOfWeek",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DayOfWeek",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DayOfWeek",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EducationForm",
                keyColumn: "ID",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "EducationForm",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HomeworkExecutionStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HomeworkExecutionStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HomeworkExecutionStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LessonType",
                keyColumn: "Id",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "LessonType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LessonType",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
