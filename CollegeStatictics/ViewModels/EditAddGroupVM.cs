using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CollegeStatictics.ViewModels
{
    public partial class EditAddGroupVM : Base.WindowViewModelBase
    {
        #region [ Commands ]
        [RelayCommand(CanExecute = nameof(CanSave))]
        public void Save()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            if (_group.Id == 0)
                DatabaseContext.Entities.Groups.Local.Add(_group);

            DatabaseContext.Entities.SaveChanges();
        }

        [RelayCommand]
        private void SelectEducationForm(int id)
            => _group.EducationFormId = id;

        private bool CanSave() => !HasErrors;
        #endregion

        #region [ Properties ]

        private readonly Group _group;

        public IEnumerable<EducationForm> EducationForms
            => DatabaseContext.Entities.EducationForms.Local;

        public int Id
        {
            get
            {
                return _group.Id != 0 ? _group.Id : ((DatabaseContext.Entities.Groups.Local.LastOrDefault()?.Id + 1) ?? 1);
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public short CreationYear
        {
            get => _group.CreationYear;
            set
            {
                _group.CreationYear = value;
                OnPropertyChanged();
                ValidateProperty(value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public Teacher Curator
        {
            get => _group.Curator;
            set
            {
                _group.Curator = value;
                OnPropertyChanged();
                ValidateProperty(value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public Speciality Speciality
        {
            get => _group.Speciality;
            set
            {
                _group.Speciality = value;
                OnPropertyChanged();
                ValidateProperty(value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        #endregion

        public EditAddGroupVM(Group? subject = null)
        {
            DatabaseContext.Entities.Groups.Load();
            DatabaseContext.Entities.EducationForms.Load();

            _group = subject ?? new()
            {
                CreationYear = (short)DateTime.Now.Year,
                EducationFormId = 1,

            };
        }
    }
}
