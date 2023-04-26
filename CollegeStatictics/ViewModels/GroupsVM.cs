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
    public partial class GroupsVM : ObservableObject
    {
        #region [ Commands ]

        [RelayCommand]
        private void OpenEditAddGroupWindow(Group? group)
        {
            EditAddGroupVM vm = new(group);
            var contentDialog = new RichContentDialog()
            {
                Title = group == null ? "Добавление" : "Редактирование",
                Content = vm,

                PrimaryButtonText = "Сохранить",
                PrimaryButtonCommand = vm.SaveCommand,

                SecondaryButtonText = "Отмена",

                DefaultButton = ContentDialogButton.Primary
            };
            var refreshGroups = new TypedEventHandler<ContentDialog, ContentDialogClosingEventArgs>((_, args) =>
            {
                if (vm.HasErrors)
                {
                    args.Cancel = true;
                    return;
                }

                Groups.Refresh();
                DatabaseContext.CancelChanges();
            });

            contentDialog.Closing += refreshGroups;

            contentDialog.ShowAsync();
        }

        [RelayCommand]
        private async void RemoveGroup(Group group)
        {
            var dialog = new ContentDialog()
            {
                Title = "Уведомление",
                Content = $"Вы действительно хотите удалить группу №{group.Id}",

                PrimaryButtonText = "Да",

                SecondaryButtonText = "Нет"
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                MessageBox.Show($"Группа №{group.Id} удалена");
        }
        #endregion

        public FilteredObservableCollection<Group> Groups { get; set; } = null!;

        public GroupsVM() => RefreshGroups();

        private void RefreshGroups()
        {
            DatabaseContext.Entities.Groups.Load();
            Groups = new FilteredObservableCollectionBuilder<Group>(DatabaseContext.Entities.Groups.Local.ToObservableCollection())
                       .AddSearching(new Searching<Group>(group => $"{group.Id}"))
                       .AddSearching(new Searching<Group>(group => $"{group.CreationYear}"))
                       .AddSearching(new Searching<Group>(group => $"{group.EducationForm}"))
                       .AddSearching(new Searching<Group>(group => $"{group.Curator}"))
                       .AddSearching(new Searching<Group>(group => $"{group.Speciality}"))
                       .AddFilter(new Filter<Group, EducationForm>("Форма обучения", group => group.EducationForm))
                       .AddFilter(new Filter<Group, Speciality>("Специальность", group => group.Speciality))
                       .Build();
        }
    }
}
