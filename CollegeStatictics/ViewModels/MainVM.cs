﻿using CollegeStatictics.Database.Models;
using CollegeStatictics.Utilities;
using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Internal;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace CollegeStatictics.ViewModels
{
    public partial class MainVM : WindowViewModelBase
    {
        public static readonly Dictionary<string, Func<dynamic>> PageBuilders = new()
        {
            { "Преподаватели", () => new ItemsContainerBuilder<Teacher, TeacherView>()

                               .AddColumn(nameof(Teacher.Id), "Id")
                               .AddColumn(nameof(Teacher.Surname), "Фамилия")
                               .AddColumn(nameof(Teacher.Name), "Имя")
                               .AddColumn(nameof(Teacher.Patronymic), "Отчество")

                               .AddSearching(new Searching<Teacher>(teacher => $"{teacher.Surname} {teacher.Name} {teacher.Patronymic}"))
                               
                               .Build()
            },
            { "Предметы", () => new ItemsContainerBuilder<Subject, SubjectView>()
                            .AddColumn(nameof(Subject.Id), "Id")
                            .AddColumn(nameof(Subject.Name), "Название")

                            .AddSearching(new Searching<Subject>(subject => subject.Name))

                            .Build()
            },
            { "Специальности", () => new ItemsContainerBuilder<Speciality, SpecialityView>()

                               .AddColumn(nameof(Speciality.Id), "Id")
                               .AddColumn(nameof(Speciality.Name), "Название")
                               .AddColumn(nameof(Speciality.Department), "Подразделение")

                               .AddSearching(new Searching<Speciality>(speciality => speciality.Name))
                               .AddSearching(new Searching<Speciality>(speciality => speciality.Department.Name))

                               .AddFilter(new Filter<Speciality, Department>("Подразделение", speciality => speciality.Department))

                               .Build()
            },
            { "Подразделения", () => new ItemsContainerBuilder<Department, DepartmentView>()
            
                               .AddColumn(nameof(Department.Id), "Id")
                               .AddColumn(nameof(Department.Name), "Название")

                               .AddSearching(new Searching<Department>(department => department.Name))

                               .Build()
            },
            { "Студенты", () => new ItemsContainerBuilder<Student, StudentView>()
                                
                                .AddColumn(nameof(Student.Id), "Id")
                                .AddColumn(nameof(Student.Surname), "Фамилия")
                                .AddColumn(nameof(Student.Name), "Имя")
                                .AddColumn(nameof(Student.Patronymic), "Отчество")
                                .AddColumn(nameof(Student.Group), "Группа")

                                .AddSearching(new Searching<Student>(student => $"{student.Surname} {student.Name} {student.Patronymic}"))
                                
                                .AddFilter(new Filter<Student, Group>("Группа", student => student.Group))

                                .Build()
            },
            { "Группы" , () => new ItemsContainerBuilder<Group, GroupView>()
                               
                               .AddColumn(nameof(Group.Id), "Id")
                               .AddColumn(nameof(Group.Number), "Номер")
                               .AddColumn(nameof(Group.EducationForm), "Форма обучения")
                               .AddColumn(nameof(Group.Speciality), "Специальность")
                               .AddColumn(nameof(Group.Curator), "Куратор")

                               /* Here should be some sort of student list */

                               .AddSearching(new Searching<Group>(group => $"{group.Number}"))
                               .AddSearching(new Searching<Group>(group => $"{group.Curator.Surname} {group.Curator.Name} {group.Curator.Patronymic}"))

                               .AddFilter(new Filter<Group, EducationForm>("Форма обучения", group => group.EducationForm))
                               .AddFilter(new Filter<Group, Speciality>("Специальность", group => group.Speciality))

                               //.AddGrouping(new Grouping<Group>("EducationForm"))
            
                               .Build()
            },
            { "Расписание", () => new ItemsContainerBuilder<Timetable, TimetableView>()
                            
                                .AddColumn(nameof(Timetable.Id), "Id")
                                .AddColumn(nameof(Timetable.Teacher), "Преподаватель")
                                .AddColumn(nameof(Timetable.Subject), "Предмет")
                                .AddColumn(nameof(Timetable.Group), "Группа")

                                .AddFilter(new Filter<Timetable, Teacher>("Преподаватель", timetable => timetable.Teacher))
                                .AddFilter(new Filter<Timetable, Group>("Группа", timetable => timetable.Group))
                                .AddFilter(new Filter<Timetable, Subject>("Специальность", timetable => timetable.Subject))

                                .Build()
            },
            { "Учебный план", () => new ItemsContainerBuilder<StudyPlan, StudyPlanView>()
                                    
                                    .AddColumn(nameof(StudyPlan.Id), "Id")
                                    .AddColumn(nameof(StudyPlan.Course), "Курс")
                                    .AddColumn(nameof(StudyPlan.StartDate), "Дата начала", @"dd-MM-yyyy")
                                    .AddColumn(nameof(StudyPlan.Speciality), "Специальность")
                                    .AddColumn(nameof(StudyPlan.Subject), "Предмет")

                                    .AddFilter(new Filter<StudyPlan, Speciality>("Специальность", studyPlan => studyPlan.Speciality))
                                    .AddFilter(new Filter<StudyPlan, Subject>("Предмет", studyPlan => studyPlan.Subject))

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
                "Подразделения",
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
            }
        };

        [ObservableProperty]
        private object? _currentView;

        [ObservableProperty]
        private string _currentViewHeader;

        public MainVM()
        {
            Title = "Главная";

            CurrentViewHeader = PageBuilders.First().Key;
            CurrentView = PageBuilders.First().Value();
        }
    }
}