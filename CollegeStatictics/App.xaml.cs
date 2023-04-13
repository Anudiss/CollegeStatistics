using CollegeStatictics.Database.Models;
using CollegeStatictics.ViewModels;
using CollegeStatictics.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace CollegeStatictics
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    [ObservableObject]
    public partial class App : Application
    {
        #region [Properties]

        [ObservableProperty]
        private static User? _currentUser;

        #endregion

        #region [Event handlers]
        private void Application_Startup(object sender, StartupEventArgs e) =>
            new WindowViewModel(new AuthVM()).Show();

        #endregion

        #region [Initialization]

        public static App Instance { get; private set; } = null!;

        public App() => Instance = this;

        #endregion
    }
}
