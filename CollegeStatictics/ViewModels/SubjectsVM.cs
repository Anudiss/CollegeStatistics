using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.Utilities;
using CollegeStatictics.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using ModernWpf;
using ModernWpf.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace CollegeStatictics.ViewModels
{
    public partial class SubjectsVM : ObservableObject, IPage<Subject>
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

        IEnumerable<Subject> IPage<Subject>.SelectedItems { get; set; } = Enumerable.Empty<Subject>();

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
