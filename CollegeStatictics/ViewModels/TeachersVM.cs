﻿using CollegeStatictics.Database.Models;
using CollegeStatictics.Database;
using CollegeStatictics.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernWpf.Controls;
using ModernWpf;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace CollegeStatictics.ViewModels
{
    public partial class TeachersVM : ObservableObject
    {
        #region [ Commands ]
        [RelayCommand]
        private void OpenEditAddTeacherWindow(Teacher? teacher)
        {
            EditAddTeacherVM vm = new(teacher);
            var contentDialog = new RichContentDialog()
            {
                Title = teacher == null ? "Добавление" : "Редактирование",
                Content = vm,

                PrimaryButtonText = "Сохранить",
                PrimaryButtonCommand = vm.SaveCommand,

                SecondaryButtonText = "Отмена",

                DefaultButton = ContentDialogButton.Primary
            };
            var refreshSubjects = new TypedEventHandler<ContentDialog, ContentDialogClosingEventArgs>((_, args) =>
            {
                if (vm.HasErrors)
                {
                    args.Cancel = true;
                    return;
                }

                Teachers.Refresh();
                DatabaseContext.CancelChanges(); 
            });

            contentDialog.Closing += refreshSubjects;

            contentDialog.ShowAsync();
        }

        [RelayCommand]
        private async void RemoveTeacher(Subject subject)
        {
            var dialog = new ContentDialog()
            {
                Title = "Уведомление",
                Content = $"Вы действительно хотите удалить {subject.Name}",

                PrimaryButtonText = "Да",

                SecondaryButtonText = "Нет"
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                MessageBox.Show($"{subject.Name} удалён (Нихуя)");
        }
        #endregion

        public FilteredObservableCollection<Teacher> Teachers { get; set; }

        public TeachersVM()
        {
            RefreshTeachers();
        }

        private void RefreshTeachers()
        {
            DatabaseContext.Entities.Teachers.Load();
            Teachers = new FilteredObservableCollectionBuilder<Teacher>(DatabaseContext.Entities.Teachers.Local.ToObservableCollection())
                       .AddSearching(new Searching<Teacher>(teacher => teacher.Surname))
                       .AddSearching(new Searching<Teacher>(teacher => teacher.Name))
                       .AddSearching(new Searching<Teacher>(teacher => teacher.Patronymic))
                       .AddSearching(new Searching<Teacher>(teacher => $"{teacher.Id}"))
                       .Build();
        }
    }
}
