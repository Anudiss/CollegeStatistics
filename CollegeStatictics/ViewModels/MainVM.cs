using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.Utilities;
using CollegeStatictics.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeStatictics.ViewModels
{
    public partial class MainVM : WindowViewModelBase
    {
        public static readonly Dictionary<string, Func<dynamic>> Pages = new()
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
                               .AddColumn("Department.Name", "Подразделение")

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
                               
                               .AddColumn(nameof(Group.EducationForm), "Форма обучения")
                                
                               .Build()
            }
        };

        public static IEnumerable<NavigationViewItem> NavigationItems =>
            Pages.Select(pair => new NavigationViewItem()
            {
                Content = pair.Key,
                Tag = pair.Value
            });

        [ObservableProperty]
        private object? currentView;

        public MainVM()
        {
            Title = "Главная";

            CurrentView = Pages.First().Value();
        }
    }
}
