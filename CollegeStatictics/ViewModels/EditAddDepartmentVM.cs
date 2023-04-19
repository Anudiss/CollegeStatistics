using CollegeStatictics.Database.Models;
using CollegeStatictics.Database;
using CollegeStatictics.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CollegeStatictics.ViewModels
{
    public partial class EditAddDepartmentVM : WindowViewModelBase
    {
        #region [ Commands ]
        [RelayCommand(CanExecute = nameof(CanSave))]
        public void Save()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            if (_department.Id == 0)
                DatabaseContext.Entities.Departments.Local.Add(_department);

            DatabaseContext.Entities.SaveChanges();
        }

        private bool CanSave() => !HasErrors;
        #endregion

        private readonly Department _department;

        public int Id
        {
            get
            {
                return _department.Id != 0 ? _department.Id : ((DatabaseContext.Entities.Departments.Local.LastOrDefault()?.Id + 1) ?? 1);
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Name
        {
            get => _department.Name;
            set
            {
                _department.Name = value;
                OnPropertyChanged();
                ValidateProperty(value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        public EditAddDepartmentVM(Department? department = null)
        {
            DatabaseContext.Entities.Departments.Load();

            _department = department ?? new();
        }
    }
}
