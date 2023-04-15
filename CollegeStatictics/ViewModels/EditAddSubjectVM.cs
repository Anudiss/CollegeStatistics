using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;

namespace CollegeStatictics.ViewModels
{
    public partial class EditAddSubjectVM : WindowViewModelBase
    {
        #region [ Commands ]
        [RelayCommand(CanExecute = nameof(CanSave))]
        public void Save()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            if (_subject.Id == 0)
                DatabaseContext.Entities.Subjects.Local.Add(_subject);

            DatabaseContext.Entities.SaveChanges();
        }

        private bool CanSave() => !HasErrors;
        #endregion

        private readonly Subject _subject;

        public int Id
        {
            get
            {
                return _subject.Id != 0 ? _subject.Id : ((DatabaseContext.Entities.Subjects.Local.LastOrDefault()?.Id + 1) ?? 1);
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Name
        {
            get => _subject.Name;
            set
            {
                _subject.Name = value;
                OnPropertyChanged();
                ValidateProperty(value);

                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        public EditAddSubjectVM(Subject? subject = null)
        {
            DatabaseContext.Entities.Subjects.Load();

            _subject = subject ?? new();
        }
    }
}
