using CollegeStatictics.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollegeStatictics.ViewModels
{
    public partial class MainVM : WindowViewModelBase
    {
        #region [ Commands ]
        [RelayCommand]
        public void OpenSubjectsPage() => CurrentView = new SubjectsVM();

        #endregion

        [ObservableProperty]
        private object? currentView;

        public MainVM()
        {
            Title = "Главная";
        }
    }
}
