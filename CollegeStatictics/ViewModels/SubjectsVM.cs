using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.Utilities;
using CollegeStatictics.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using ModernWpf;
using ModernWpf.Controls;
using System.Windows;
using System.Windows.Data;

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
            var refreshSubjects = new TypedEventHandler<ContentDialog, ContentDialogClosingEventArgs>((_, args) => { Subjects.Refresh(); DatabaseContext.CancelChanges(); });

            contentDialog.Closing += refreshSubjects;

            contentDialog.ShowAsync();
        }
        #endregion

        public FilteredObservableCollection<Subject> Subjects { get; set; }

        public SubjectsVM()
        {
            RefreshSubjects();
        }

        private void RefreshSubjects()
        {
            DatabaseContext.Entities.Subjects.Load();
            Subjects = new FilteredObservableCollectionBuilder<Subject>(DatabaseContext.Entities.Subjects.Local.ToObservableCollection())
                       .AddSearching(new Searching<Subject>(subject => subject.Name))
                       .Build();
        }
    }
}
