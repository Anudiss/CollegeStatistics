using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CollegeStatictics.ViewModels
{
    public partial class EditAddTeacherVM : Base.WindowViewModelBase
    {
        #region [ Commands ]
        [RelayCommand(CanExecute = nameof(CanSave))]
        public void Save()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            if (_teacher.Id == 0)
                DatabaseContext.Entities.Teachers.Local.Add(_teacher);

            DatabaseContext.Entities.SaveChanges();
        }

        private bool CanSave() => !HasErrors;
        #endregion

        private readonly Teacher _teacher;

        public int Id
        {
            get
            {
                return _teacher.Id != 0 ? _teacher.Id : ((DatabaseContext.Entities.Teachers.Local.LastOrDefault()?.Id + 1) ?? 1);
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Surname
        {
            get => _teacher.Surname;
            set
            {
                _teacher.Surname = value;
                OnPropertyChanged();
                ValidateProperty(value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Name
        {
            get => _teacher.Name;
            set
            {
                _teacher.Name = value;
                OnPropertyChanged();
                ValidateProperty(value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Patronymic
        {
            get => _teacher.Patronymic;
            set
            {
                _teacher.Patronymic = value;
                OnPropertyChanged();
                ValidateProperty(value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Login
        {
            get => _teacher.Login;
            set
            {
                _teacher.Login = value;
                OnPropertyChanged();
                ValidateProperty(value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Password
        {
            get => _teacher.Password;
            set
            {
                _teacher.Password = value;
                OnPropertyChanged();
                ValidateProperty(value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        public EditAddTeacherVM(Teacher? teacher = null)
        {
            DatabaseContext.Entities.Teachers.Load();

            _teacher = teacher ?? new();
        }
    }
}
