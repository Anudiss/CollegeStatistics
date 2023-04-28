using CollegeStatictics.Database.Models;
using CollegeStatictics.Utilities;
using CollegeStatictics.ViewModels.Base;
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
                ""
            },
            {
                "Подразделения",
                ""
            },
            {
                "Студенты",
                ""
            },
            {
                "Группы",
                ""
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