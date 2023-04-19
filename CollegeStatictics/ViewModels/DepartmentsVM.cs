using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using ModernWpf;
using ModernWpf.Controls;
using System.Windows;

namespace CollegeStatictics.ViewModels
{
    public partial class DepartmentsVM : ObservableObject
    {
        #region [ Commands ]
        [RelayCommand]
        private void OpenEditAddDepartmentWindow(Department? department)
        {
            EditAddDepartmentVM vm = new(department);
            var contentDialog = new RichContentDialog()
            {
                Title = department == null ? "Добавление" : "Редактирование",
                Content = vm,

                PrimaryButtonText = "Сохранить",
                PrimaryButtonCommand = vm.SaveCommand,

                SecondaryButtonText = "Отмена",

                DefaultButton = ContentDialogButton.Primary
            };
            var refreshSubjects = new TypedEventHandler<ContentDialog, ContentDialogClosingEventArgs>((_, args) => { Departments.Refresh(); DatabaseContext.CancelChanges(); });

            contentDialog.Closing += refreshSubjects;

            contentDialog.ShowAsync();
        }

        [RelayCommand]
        private async void RemoveDepartment(Department department)
        {
            var dialog = new ContentDialog()
            {
                Title = "Уведомление",
                Content = $"Вы действительно хотите удалить {department.Name}",

                PrimaryButtonText = "Да",

                SecondaryButtonText = "Нет"
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                MessageBox.Show($"{department.Name} удалён (Нихуя)");
        }
        #endregion

        public FilteredObservableCollection<Department> Departments { get; set; }

        public DepartmentsVM()
        {
            RefreshSubjects();
        }

        private void RefreshSubjects()
        {
            DatabaseContext.Entities.Departments.Load();
            Departments = new FilteredObservableCollectionBuilder<Department>(DatabaseContext.Entities.Departments.Local.ToObservableCollection())
                          .AddSearching(new Searching<Department>(department => department.Name))
                          .AddSearching(new Searching<Department>(department => $"{department.Id}"))
                          .Build();
        }
    }
}
