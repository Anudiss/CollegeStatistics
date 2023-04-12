using CollegeStatictics.ViewModels;
using CollegeStatictics.Windows;
using System.Windows;

namespace CollegeStatictics
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e) =>
            new WindowViewModel(new AuthVM()).Show();
    }
}
