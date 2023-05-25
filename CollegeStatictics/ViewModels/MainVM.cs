using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.Utils;
using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Views;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace CollegeStatictics.ViewModels
{
    public partial class MainVM : WindowViewModelBase
    {
        public static readonly Dictionary<string, Func<IContent>> PageBuilders = new()
        {
            { "Преподаватели", () => new ItemsContainerBuilder<Teacher, TeacherView>()

                               .AddTextBoxColumn(nameof(Teacher.Surname), "Фамилия")
                               .AddTextBoxColumn(nameof(Teacher.Name), "Имя")
                               .AddTextBoxColumn(nameof(Teacher.Patronymic), "Отчество")

                               .AddSearching(new Searching<Teacher>(teacher => $"{teacher.Surname} {teacher.Name} {teacher.Patronymic}"))

                               .AddFilter(new Selection<Teacher>(teacher => teacher.IsDeleted == false))

                               .Build()
            },
            { "Предметы", () => new ItemsContainerBuilder<Subject, SubjectView>()
                            .AddTextBoxColumn(nameof(Subject.Name), "Название")

                            .AddSearching(new Searching<Subject>(subject => subject.Name))

                            .AddFilter(new Selection<Subject>(subject => subject.IsDeleted == false))

                            .Build()
            },
            { "Специальности", () => new ItemsContainerBuilder<Speciality, SpecialityView>()

                               .AddTextBoxColumn(nameof(Speciality.Name), "Название")
                               .AddTextBoxColumn(nameof(Speciality.Department), "Подразделение")

                               .AddSearching(new Searching<Speciality>(speciality => speciality.Name))
                               .AddSearching(new Searching<Speciality>(speciality => speciality.Department.Name))

                               .AddFilter(new Filter<Speciality, Department>("Подразделение", speciality => speciality.Department))
                               .AddFilter(new Selection<Speciality>(speciality => speciality.IsDeleted == false))

                               .Build()
            },
            { "Отделения", () => new ItemsContainerBuilder<Department, DepartmentView>()

                               .AddTextBoxColumn(nameof(Department.Name), "Название")

                               .AddSearching(new Searching<Department>(department => department.Name))

                               .AddFilter(new Selection<Department>(department => department.IsDeleted == false))

                               .Build()
            },
            { "Студенты", () => new ItemsContainerBuilder<Student, StudentView>()

                                .AddTextBoxColumn(nameof(Student.Surname), "Фамилия")
                                .AddTextBoxColumn(nameof(Student.Name), "Имя")
                                .AddTextBoxColumn(nameof(Student.Patronymic), "Отчество")
                                .AddTextBoxColumn(nameof(Student.Group), "Группа")

                                .AddSearching(new Searching<Student>(student => $"{student.Surname} {student.Name} {student.Patronymic}"))

                                .AddFilter(new Filter<Student, Group>("Группа", student => student.Group))
                                .AddFilter(new Selection<Student>(student => student.IsDeleted == false))

                                .Build()
            },
            { "Группы" , () => new ItemsContainerBuilder<Group, GroupView>()

                               .AddTextBoxColumn(nameof(Group.Number), "Номер")
                               .AddTextBoxColumn(nameof(Group.EducationForm), "Форма обучения")
                               .AddTextBoxColumn(nameof(Group.Speciality), "Специальность")
                               .AddTextBoxColumn(nameof(Group.Curator), "Куратор")

                               /* Here should be some sort of student list */

                               .AddSearching(new Searching<Group>(group => $"{group.Number}"))
                               .AddSearching(new Searching<Group>(group => $"{group.Curator.Surname} {group.Curator.Name} {group.Curator.Patronymic}"))

                               .AddFilter(new Filter<Group, EducationForm>("Форма обучения", group => group.EducationForm))
                               .AddFilter(new Filter<Group, Speciality>("Специальность", group => group.Speciality))

                               .AddFilter(new Selection<Group>(group => group.IsDeleted == false))

                               .Build()
            },
            { "Расписание", () => new ItemsContainerBuilder<Timetable, TimetableView>()

                                .AddTextBoxColumn(nameof(Timetable.Teacher), "Преподаватель")
                                .AddTextBoxColumn(nameof(Timetable.StudyPlan), "Предмет")
                                .AddTextBoxColumn(nameof(Timetable.Group), "Группа")

                                .AddFilter(new Filter<Timetable, Teacher>("Преподаватель", timetable => timetable.Teacher))
                                .AddFilter(new Filter<Timetable, Group>("Группа", timetable => timetable.Group))
                                .AddFilter(new Filter<Timetable, StudyPlan>("Учебный план", timetable => timetable.StudyPlan))

                                .AddFilter(new Selection<Timetable>(timetable => timetable.IsDeleted == false))

                                .Build()
            },
            { "Учебный план", () => new ItemsContainerBuilder<StudyPlan, StudyPlanView>()

                                    .AddTextBoxColumn(nameof(StudyPlan.Course), "Курс")
                                    .AddTextBoxColumn(nameof(StudyPlan.StartDate), "Дата начала", "{0:dd.MM.yyyy}")
                                    .AddTextBoxColumn(nameof(StudyPlan.Speciality), "Специальность")
                                    .AddTextBoxColumn(nameof(StudyPlan.Subject), "Предмет")

                                    .AddFilter(new Filter<StudyPlan, Speciality>("Специальность", studyPlan => studyPlan.Speciality))
                                    .AddFilter(new Filter<StudyPlan, Subject>("Предмет", studyPlan => studyPlan.Subject))

                                    .AddFilter(new Selection<StudyPlan>(studyPlan => studyPlan.IsDeleted == false))

                                    .Build()
            },
            { "Занятия", () => new ItemsContainerBuilder<Lesson, LessonView>()

                                   .AddTextBoxColumn(nameof(LessonView.Date), "Дата", "{0:dd.MM.yyyy}")
                                   .AddTextBoxColumn(nameof(LessonView.Time), "Время")
                                   .AddTextBoxColumn(nameof(LessonView.StudyPlanRecord), "Тема")
                                   .AddTextBoxColumn($"{nameof(LessonView.StudyPlanRecord)}.{nameof(StudyPlanRecord.LessonType)}", "Тип пары")
                                   .AddTextBoxColumn($"{nameof(LessonView.StudyPlanRecord)}.{nameof(StudyPlanRecord.StudyPlan)}.{nameof(StudyPlan.Subject)}", "Предмет")
                                   .AddCheckBoxColumn(nameof(LessonView.IsRestoring), "Восстановление")

                                   .AddFilter(new Selection<Lesson>(lesson => lesson.IsDeleted == false))

                                   .BanCreate()

                                   .Build()
            },
            { "Домашняя работа", () => new ItemsContainerBuilder<Homework, HomeworkView>()

                                    .AddTextBoxColumn(nameof(HomeworkView.Topic), "Тема")

                                    .Build()
            },
            { "Остаток по часам", () => new ReportBuilder<StudyPlanRecord>()

                                    .SetTitle("Остаток по часам")

                                    .AddColumn("Выделено", record => (double)record.DurationInLessons)
                                    .AddColumn("Проведено", record => (double)record.Lessons.Count)

                                    .AddPropertySelection((record, parameters) => record.StudyPlan)
                                        .Build()

                                    .AddPropertySelection((record, parameters) => record.StudyPlan[parameters[0] as Teacher, parameters[1] as Group])
                                        .Bind(typeof(Teacher))
                                        .Bind(typeof(Group))
                                        .Build()

                                    .HasFinalRow()

                                    .SetFinalFunction(FinalFunction.Sum)

                                    .Build()
            },
            { "Отчёт успеваемости", () => new ReportBuilder<Attendance>()
                                    .SetTitle("Отчёт успеваемости")

                                    .BindColumnHeader<Subject>( attendance => attendance.Lesson.StudyPlanRecord.StudyPlan.Subject, attendance => (double?)attendance.Mark )
                                        .Build()

                                    .GroupBy(attendance => attendance.Student)

                                    .AddSelection(new Selection<Attendance>(attendance => attendance.Mark != null))
                                    .AddPropertySelection((attendance, parameters) => attendance.Student.Group)
                                        .Build()

                                    .HasFinalColumn()

                                    .HasFinalRow()
                                    .HasFinalColumn()

                                    .SetFinalFunction(FinalFunction.Average)

                                    .Build()
            },
            { "Отчёт посещаемости", () => new ReportBuilder<Group>()

                                .SetTitle("Отчёт посещаемости")

                                .HasFinalColumn()

                                .SetFinalFunction(FinalFunction.Sum)

                                .Build()
            }
        };

        public static readonly Dictionary<string, string> PageIcons = new()
        {
            {
                "Преподаватели",
                "M192 96a48 48 0 1 0 0-96 48 48 0 1 0 0 96zm-8 384V352h16V480c0 17.7 14.3 32 32 32s32-14.3 32-32V192h56 64 16c17.7 0 32-14.3 32-32s-14.3-32-32-32H384V64H576V256H384V224H320v48c0 26.5 21.5 48 48 48H592c26.5 0 48-21.5 48-48V48c0-26.5-21.5-48-48-48H368c-26.5 0-48 21.5-48 48v80H243.1 177.1c-33.7 0-64.9 17.7-82.3 46.6l-58.3 97c-9.1 15.1-4.2 34.8 10.9 43.9s34.8 4.2 43.9-10.9L120 256.9V480c0 17.7 14.3 32 32 32s32-14.3 32-32z"
            },
            {
                "Предметы",
                "M96 0C43 0 0 43 0 96V416c0 53 43 96 96 96H384h32c17.7 0 32-14.3 32-32s-14.3-32-32-32V384c17.7 0 32-14.3 32-32V32c0-17.7-14.3-32-32-32H384 96zm0 384H352v64H96c-17.7 0-32-14.3-32-32s14.3-32 32-32zm32-240c0-8.8 7.2-16 16-16H336c8.8 0 16 7.2 16 16s-7.2 16-16 16H144c-8.8 0-16-7.2-16-16zm16 48H336c8.8 0 16 7.2 16 16s-7.2 16-16 16H144c-8.8 0-16-7.2-16-16s7.2-16 16-16z"
            },
            {
                "Специальности",
                "M320 32c-8.1 0-16.1 1.4-23.7 4.1L15.8  137.4C6.3 140.9 0 149.9 0 160s6.3 19.1 15.8 22.6l57.9 20.9C57.3 229.3 48 259.8 48 291.9v28.1c0 28.4-10.8 57.7-22.3 80.8c-6.5 13-13.9 25.8-22.5 37.6C0 442.7-.9 448.3 .9 453.4s6 8.9 11.2 10.2l64 16c4.2 1.1 8.7 .3 12.4-2s6.3-6.1 7.1-10.4c8.6-42.8 4.3-81.2-2.1-108.7C90.3 344.3 86 329.8 80 316.5V291.9c0-30.2 10.2-58.7 27.9-81.5c12.9-15.5 29.6-28 49.2-35.7l157-61.7c8.2-3.2 17.5 .8 20.7 9s-.8 17.5-9 20.7l-157 61.7c-12.4 4.9-23.3 12.4-32.2 21.6l159.6 57.6c7.6 2.7 15.6 4.1 23.7 4.1s16.1-1.4 23.7-4.1L624.2 182.6c9.5-3.4 15.8-12.5 15.8-22.6s-6.3-19.1-15.8-22.6L343.7 36.1C336.1 33.4 328.1 32 320 32zM128 408c0 35.3 86 72 192 72s192-36.7 192-72L496.7 262.6 354.5 314c-11.1 4-22.8 6-34.5 6s-23.5-2-34.5-6L143.3 262.6 128 408z"
            },
            {
                "Отделения",
                "M48 0C21.5 0 0 21.5 0 48V464c0 26.5 21.5 48 48 48h96V432c0-26.5 21.5-48 48-48s48 21.5 48 48v80h96c26.5 0 48-21.5 48-48V48c0-26.5-21.5-48-48-48H48zM64 240c0-8.8 7.2-16 16-16h32c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16H80c-8.8 0-16-7.2-16-16V240zm112-16h32c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16H176c-8.8 0-16-7.2-16-16V240c0-8.8 7.2-16 16-16zm80 16c0-8.8 7.2-16 16-16h32c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16H272c-8.8 0-16-7.2-16-16V240zM80 96h32c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16H80c-8.8 0-16-7.2-16-16V112c0-8.8 7.2-16 16-16zm80 16c0-8.8 7.2-16 16-16h32c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16H176c-8.8 0-16-7.2-16-16V112zM272 96h32c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16H272c-8.8 0-16-7.2-16-16V112c0-8.8 7.2-16 16-16z"
            },
            {
                "Студенты",
                "M224 256A128 128 0 1 1 224 0a128 128 0 1 1 0 256zM209.1 359.2l-18.6-31c-6.4-10.7 1.3-24.2 13.7-24.2H224h19.7c12.4 0 20.1 13.6 13.7 24.2l-18.6 31 33.4 123.9 36-146.9c2-8.1 9.8-13.4 17.9-11.3c70.1 17.6 121.9 81 121.9 156.4c0 17-13.8 30.7-30.7 30.7H285.5c-2.1 0-4-.4-5.8-1.1l.3 1.1H168l.3-1.1c-1.8 .7-3.8 1.1-5.8 1.1H30.7C13.8 512 0 498.2 0 481.3c0-75.5 51.9-138.9 121.9-156.4c8.1-2 15.9 3.3 17.9 11.3l36 146.9 33.4-123.9z"
            },
            {
                "Группы",
                "M96 128a128 128 0 1 1 256 0A128 128 0 1 1 96 128zM0 482.3C0 383.8 79.8 304 178.3 304h91.4C368.2 304 448 383.8 448 482.3c0 16.4-13.3 29.7-29.7 29.7H29.7C13.3 512 0 498.7 0 482.3zM609.3 512H471.4c5.4-9.4 8.6-20.3 8.6-32v-8c0-60.7-27.1-115.2-69.8-151.8c2.4-.1 4.7-.2 7.1-.2h61.4C567.8 320 640 392.2 640 481.3c0 17-13.8 30.7-30.7 30.7zM432 256c-31 0-59-12.6-79.3-32.9C372.4 196.5 384 163.6 384 128c0-26.8-6.6-52.1-18.3-74.3C384.3 40.1 407.2 32 432 32c61.9 0 112 50.1 112 112s-50.1 112-112 112z"
            },
            {
                "Расписание",
                "M96 32V64H48C21.5 64 0 85.5 0 112v48H448V112c0-26.5-21.5-48-48-48H352V32c0-17.7-14.3-32-32-32s-32 14.3-32 32V64H160V32c0-17.7-14.3-32-32-32S96 14.3 96 32zM448 192H0V464c0 26.5 21.5 48 48 48H400c26.5 0 48-21.5 48-48V192z"
            },
            {
                "Учебный план",
                "M337.8 5.4C327-1.8 313-1.8 302.2 5.4L166.3 96H48C21.5 96 0 117.5 0 144V464c0 26.5 21.5 48 48 48H592c26.5 0 48-21.5 48-48V144c0-26.5-21.5-48-48-48H473.7L337.8 5.4zM256 416c0-35.3 28.7-64 64-64s64 28.7 64 64v96H256V416zM96 192h32c8.8 0 16 7.2 16 16v64c0 8.8-7.2 16-16 16H96c-8.8 0-16-7.2-16-16V208c0-8.8 7.2-16 16-16zm400 16c0-8.8 7.2-16 16-16h32c8.8 0 16 7.2 16 16v64c0 8.8-7.2 16-16 16H512c-8.8 0-16-7.2-16-16V208zM96 320h32c8.8 0 16 7.2 16 16v64c0 8.8-7.2 16-16 16H96c-8.8 0-16-7.2-16-16V336c0-8.8 7.2-16 16-16zm400 16c0-8.8 7.2-16 16-16h32c8.8 0 16 7.2 16 16v64c0 8.8-7.2 16-16 16H512c-8.8 0-16-7.2-16-16V336zM232 176a88 88 0 1 1 176 0 88 88 0 1 1 -176 0zm88-48c-8.8 0-16 7.2-16 16v32c0 8.8 7.2 16 16 16h32c8.8 0 16-7.2 16-16s-7.2-16-16-16H336V144c0-8.8-7.2-16-16-16z"
            },
            {
                "Занятия",
                "M256 0a256 256 0 1 1 0 512A256 256 0 1 1 256 0zM232 120V256c0 8 4 15.5 10.7 20l96 64c11 7.4 25.9 4.4 33.3-6.7s4.4-25.9-6.7-33.3L280 243.2V120c0-13.3-10.7-24-24-24s-24 10.7-24 24z"
            },
            {
                "Домашняя работа",
                "M218.3 8.5c12.3-11.3 31.2-11.3 43.4 0l208 192c6.7 6.2 10.3 14.8 10.3 23.5H336c-19.1 0-36.3 8.4-48 21.7V208c0-8.8-7.2-16-16-16H208c-8.8 0-16 7.2-16 16v64c0 8.8 7.2 16 16 16h64V416H112c-26.5 0-48-21.5-48-48V256H32c-13.2 0-25-8.1-29.8-20.3s-1.6-26.2 8.1-35.2l208-192zM352 304V448H544V304H352zm-48-16c0-17.7 14.3-32 32-32H560c17.7 0 32 14.3 32 32V448h32c8.8 0 16 7.2 16 16c0 26.5-21.5 48-48 48H544 352 304c-26.5 0-48-21.5-48-48c0-8.8 7.2-16 16-16h32V288z"
            },
            {
                "Остаток по часам",
                "M176 0c-17.7 0-32 14.3-32 32s14.3 32 32 32h16V98.4C92.3 113.8 16 200 16 304c0 114.9 93.1 208 208 208s208-93.1 208-208c0-41.8-12.3-80.7-33.5-113.2l24.1-24.1c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L355.7 143c-28.1-23-62.2-38.8-99.7-44.6V64h16c17.7 0 32-14.3 32-32s-14.3-32-32-32H224 176zm72 192V320c0 13.3-10.7 24-24 24s-24-10.7-24-24V192c0-13.3 10.7-24 24-24s24 10.7 24 24z"
            },
            {
                "Отчёт успеваемости",
                "M32.5 58.3C35.3 43.1 48.5 32 64 32H256c17.7 0 32 14.3 32 32s-14.3 32-32 32H90.7L70.3 208H184c75.1 0 136 60.9 136 136s-60.9 136-136 136H100.5c-39.4 0-75.4-22.3-93-57.5l-4.1-8.2c-7.9-15.8-1.5-35 14.3-42.9s35-1.5 42.9 14.3l4.1 8.2c6.8 13.6 20.6 22.1 35.8 22.1H184c39.8 0 72-32.2 72-72s-32.2-72-72-72H32c-9.5 0-18.5-4.2-24.6-11.5s-8.6-16.9-6.9-26.2l32-176z"
            },
            {
                "Отчёт посещаемости",
                "M112 48a48 48 0 1 1 96 0 48 48 0 1 1 -96 0zm40 304V480c0 17.7-14.3 32-32 32s-32-14.3-32-32V256.9L59.4 304.5c-9.1 15.1-28.8 20-43.9 10.9s-20-28.8-10.9-43.9l58.3-97c17.4-28.9 48.6-46.6 82.3-46.6h29.7c33.7 0 64.9 17.7 82.3 46.6l44.9 74.7c-16.1 17.6-28.6 38.5-36.6 61.5c-1.9-1.8-3.5-3.9-4.9-6.3L232 256.9V480c0 17.7-14.3 32-32 32s-32-14.3-32-32V352H152zm136 16a144 144 0 1 1 288 0 144 144 0 1 1 -288 0zm211.3-43.3c-6.2-6.2-16.4-6.2-22.6 0L416 385.4l-28.7-28.7c-6.2-6.2-16.4-6.2-22.6 0s-6.2 16.4 0 22.6l40 40c6.2 6.2 16.4 6.2 22.6 0l72-72c6.2-6.2 6.2-16.4 0-22.6z"
            }
        };

        [ObservableProperty]
        private IContent? _currentView;

        [ObservableProperty]
        private string _currentViewHeader;

        public MainVM()
        {
            Title = "Главная";

            CurrentViewHeader = PageBuilders.First().Key;
            CurrentView = PageBuilders.First().Value();

            /*WatchTablesLoading();*/
            LoadAllTables();
        }

        public static string GetViewByType<T>() => GetViewByType(typeof(T));

        public static string GetViewByType( Type type ) =>
            PageBuilders.FirstOrDefault(builder =>
            {
                var content = builder.Value();
                return content.GetType().GetGenericArguments()[0] == type;
            }).Key;

        private static void WatchTablesLoading()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            LoadAllTables();

            stopWatch.Stop();
            MessageBox.Show($"Lessons: {stopWatch.ElapsedMilliseconds}");
        }

        private static void LoadAllTables()
        {
            DatabaseContext.Entities.Attendances.Load();
            DatabaseContext.Entities.DayOfWeeks.Load();
            DatabaseContext.Entities.Departments.Load();
            DatabaseContext.Entities.EducationForms.Load();
            DatabaseContext.Entities.EmergencySituations.Load();
            DatabaseContext.Entities.Groups.Load();
            DatabaseContext.Entities.HomeworkExecutionStatuses.Load();
            DatabaseContext.Entities.Homeworks.Load();
            DatabaseContext.Entities.HomeworkStudents.Load();
            DatabaseContext.Entities.LessonHomeworks.Load();
            DatabaseContext.Entities.Lessons.Load();
            DatabaseContext.Entities.LessonTypes.Load();
            DatabaseContext.Entities.NoteToLessons.Load();
            DatabaseContext.Entities.NoteToStudents.Load();
            DatabaseContext.Entities.Specialities.Load();
            DatabaseContext.Entities.Students.Load();
            DatabaseContext.Entities.StudyPlanRecords.Load();
            DatabaseContext.Entities.StudyPlans.Load();
            DatabaseContext.Entities.Subjects.Load();
            DatabaseContext.Entities.Teachers.Load();
            DatabaseContext.Entities.TimetableRecords.Load();
            DatabaseContext.Entities.Timetables.Load();
        }
    }
}