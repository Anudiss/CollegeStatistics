using CollegeStatictics.Database.Models;
using CollegeStatictics.Database;
using CollegeStatictics.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernWpf.Controls;
using ModernWpf;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using CollegeStatictics.Windows;

namespace CollegeStatictics.ViewModels
{
    public partial class SpecialitiesVM : ObservableObject
    {
        #region [ Commands ]
        [RelayCommand]
        private void OpenEditAddSpecialityWindow(Speciality? speciality)
        {
            EditAddSpecialityVM vm = new(speciality);
            var contentDialog = new DialogWindow()
            {
                Title = speciality == null ? "Добавление" : "Редактирование",
                Content = vm,

                PrimaryButtonText = "Сохранить",
                PrimaryButtonCommand = vm.SaveCommand,

                SecondaryButtonText = "Отмена",

                //DefaultButton = ContentDialogButton.Primary
            };
            var refreshSubjects = new TypedEventHandler<ContentDialog, ContentDialogClosingEventArgs>((_, args) =>
            {
                if (vm.HasErrors)
                {
                    args.Cancel = true;
                    return;
                }

                Specialities.Refresh();
                DatabaseContext.CancelChanges();
            });

            //contentDialog.Closing += refreshSubjects;

            contentDialog.Show();
        }

        [RelayCommand]
        private async void RemoveSpeciality(Speciality speciality)
        {
            var dialog = new ContentDialog()
            {
                Title = "Уведомление",
                Content = $"Вы действительно хотите удалить {speciality.Name}",

                PrimaryButtonText = "Да",

                SecondaryButtonText = "Нет"
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                MessageBox.Show($"{speciality.Name} удалён (Нихуя)");
        }
        #endregion

        public FilteredObservableCollection<Speciality> Specialities { get; set; }

        public SpecialitiesVM()
        {
            RefreshSubjects();
        }

        private void RefreshSubjects()
        {
            DatabaseContext.Entities.Specialities.Load();
            DatabaseContext.Entities.Departments.Load();

            Specialities = new FilteredObservableCollectionBuilder<Speciality>(DatabaseContext.Entities.Specialities.Local.ToObservableCollection())
                           .AddSearching(new Searching<Speciality>(speciality => speciality.Name))
                           .AddSearching(new Searching<Speciality>(speciality => $"{speciality.Id}"))
                           .AddSearching(new Searching<Speciality>(speciality => speciality.Department.Name))
                           .AddFilter(new Filter<Speciality, Department>("Отделение", speciality => speciality.Department))
                           .Build();
        }
    }
}
