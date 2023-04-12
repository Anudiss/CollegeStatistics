using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Base;
using CollegeStatictics.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CollegeStatictics.ViewModels
{
    public partial class AuthVM : WindowViewModelBase
    {
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(AuthorizeCommand))]
        [Required(ErrorMessage = "Обязательное поле")]
        [ObservableProperty]
        private string _login;

        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(AuthorizeCommand))]
        [Required(ErrorMessage = "Обязательное поле")]
        [ObservableProperty]
        private string _password;

        #region [ Commands ]

        [RelayCommand(CanExecute = nameof(CanAuthorize))]
        private void Authorize()
        {
            IEnumerable<User> users = DatabaseContext.Entities.Teachers.Cast<User>().Concat(
                                      DatabaseContext.Entities.CommisionCurators.Cast<User>()).Concat(
                                      DatabaseContext.Entities.GroupLeaders.Cast<User>());

            var authorizatedUser = users.FirstOrDefault(user => user.Login == Login && user.Password == Password);
            if (authorizatedUser == null)
                return;
                
        }

        private bool CanAuthorize() => !HasErrors;
        #endregion

        public AuthVM()
        {
            Notification.Show("Хуй");
        }
    }
}
