using CollegeStatictics.ViewModels;
using ModernWpf.Controls;
using System.Windows.Controls;

namespace CollegeStatictics.Views
{
    /// <summary>
    /// Логика взаимодействия для MainVIew.xaml
    /// </summary>
    public partial class MainVIew : UserControl
    {
        public MainVIew()
        {
            InitializeComponent();
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var navItem = (NavigationViewItem)args.SelectedItem;

            var pageHeader = (string)navItem.Content;
            dynamic pageBuilder = navItem.Tag;

            ((MainVM)DataContext).CurrentViewHeader = pageHeader;
            ((MainVM)DataContext).CurrentView = pageBuilder();
        }
    }
}
