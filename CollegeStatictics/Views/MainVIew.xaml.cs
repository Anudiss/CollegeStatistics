using CollegeStatictics.ViewModels;
using ModernWpf.Controls;
using System.Windows.Controls;

namespace CollegeStatictics.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
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
