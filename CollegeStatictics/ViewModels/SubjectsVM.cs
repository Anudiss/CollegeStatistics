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
    public partial class SubjectsVM : ObservableObject
    {
        #region [ Commands ]
        [RelayCommand]
        private void OpenEditAddSubjectWindow(Subject? subject)
        {
            EditAddSubjectVM vm = new(subject);
            var contentDialog = new RichContentDialog()
            {
                Title = subject == null ? "Добавление" : "Редактирование",
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

                Subjects.Refresh();
                DatabaseContext.CancelChanges();
            });

            contentDialog.Closing += refreshSubjects;

            contentDialog.ShowAsync();
        }

        [RelayCommand]
        private async void RemoveSubject(Subject subject)
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

        #region [ Properties ]

        public FilteredObservableCollection<Subject> Subjects { get; set; } = null!;

        #endregion

        public SubjectsVM()
        {
            RefreshSubjects();
        }

        private void RefreshSubjects()
        {
            DatabaseContext.Entities.Subjects.Load();
            Subjects = new FilteredObservableCollectionBuilder<Subject>(DatabaseContext.Entities.Subjects.Local.ToObservableCollection())
                       .AddSearching(new Searching<Subject>(subject => subject.Name))
                       .AddSearching(new Searching<Subject>(subject => $"{subject.Id}"))
                       .Build();
        }
    }
}
