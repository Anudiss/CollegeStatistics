using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Windows;
using CollegeStatictics.Windows.Notification;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernWpf.Controls;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;

namespace CollegeStatictics.ViewModels
{
    public partial class AuthVM : WindowViewModelBase
    {
        #region [Properties]

        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(AuthorizeCommand))]
        [Required(ErrorMessage = "Обязательное поле")]
        [ObservableProperty]
        private string _login = null!;

        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(AuthorizeCommand))]
        [Required(ErrorMessage = "Обязательное поле")]
        [ObservableProperty]
        private string _password = null!;

        #endregion

        #region [ Commands ]

        [RelayCommand(CanExecute = nameof(CanAuthorize))]
        private async void Authorize()
        {
            //IEnumerable<User> users = DatabaseContext.Entities.Teachers.Cast<User>().Concat(
            //                          DatabaseContext.Entities.CommisionCurators.Cast<User>()).Concat(
            //                          DatabaseContext.Entities.GroupLeaders.Cast<User>());

            //var authorizatedUser = users.FirstOrDefault(user => user.Login == Login && user.Password == Password);
            //if (authorizatedUser == null)
            //{
            //    MessageBox.Show("Error");
            //NotificationWindow.Show("Хуй", icon: NotificationIcon.Question, title: "Хуйхыфвцв");
            //    return;
            //}

            //App.Instance.CurrentUser = authorizatedUser;




            #region [ Это диалоговое окно Ильназ посмотри ]

            var result = await new ContentDialog()
            {
                Content = "Ошибка: неверный логин или пароль",
                PrimaryButtonText = "Ок",
                SecondaryButtonText = "Отмена",
                DefaultButton = ContentDialogButton.Primary
            }.ShowAsync();

            if (result != ContentDialogResult.Primary)
                return;
            #endregion




            new WindowViewModel(new MainVM()).Show();
            CloseWindow();
        }

        private bool CanAuthorize() => !HasErrors;

        #endregion

        public AuthVM()
            => Title = "Авторизация";
    }
}
