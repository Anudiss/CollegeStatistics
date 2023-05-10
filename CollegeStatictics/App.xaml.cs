using CollegeStatictics.ViewModels;
using CollegeStatictics.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CollegeStatictics
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region [Event handlers]
        private void Application_Startup(object sender, StartupEventArgs e) =>
            new WindowViewModel(new MainVM()).Show();

        #endregion

        #region [Initialization]

        public static App Instance { get; private set; } = null!;

        public App() => Instance = this;

        #endregion

        private void ItemsContainer_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            dynamic itemsContainer = ((DataGrid)sender).DataContext;

            e.Row.InputBindings.Add(new MouseBinding
            {
                Gesture = new MouseGesture(MouseAction.LeftDoubleClick),
                Command = itemsContainer.OpenDialogCommand,
                CommandParameter = e.Row.Item
            });
        }
    }
}
