using CollegeStatictics.Database.Models;
using CollegeStatictics.Database;
using CollegeStatictics.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.ObjectModel;

namespace CollegeStatictics.ViewModels
{
    public partial class EditAddSpecialityVM : WindowViewModelBase
    {
        #region [ Commands ]
        [RelayCommand(CanExecute = nameof(CanSave))]
        public void Save()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            if (_speciality.Id == 0)
                DatabaseContext.Entities.Specialities.Local.Add(_speciality);

            DatabaseContext.Entities.SaveChanges();
        }

        private bool CanSave() => !HasErrors;
        #endregion

        private readonly Speciality _speciality;

        public int Id
        {
            get
            {
                return _speciality.Id != 0 ? _speciality.Id : ((DatabaseContext.Entities.Specialities.Local.LastOrDefault()?.Id + 1) ?? 1);
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Name
        {
            get => _speciality.Name;
            set
            {
                _speciality.Name = value;
                OnPropertyChanged();
                ValidateProperty(value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        
        [Required(ErrorMessage = "Обязательное поле")]
        public Department Department
        {
            get => _speciality.Department;
            set
            {
                _speciality.Department = value;

                OnPropertyChanged();
                ValidateProperty(value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        public static ObservableCollection<Department> Departments => DatabaseContext.Entities.Departments.Local.ToObservableCollection();

        public EditAddSpecialityVM(Speciality? speciality = null)
        {
            DatabaseContext.Entities.Specialities.Load();
            DatabaseContext.Entities.Departments.Load();

            _speciality = speciality ?? new();
        }
    }
}
