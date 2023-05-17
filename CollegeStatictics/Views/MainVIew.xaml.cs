using CollegeStatictics.Database;
using CollegeStatictics.ViewModels;

using Microsoft.EntityFrameworkCore;

using ModernWpf.Controls;

using System.Diagnostics;
using System.Windows;
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

            var content = (string)navItem.Content;

            ((MainVM)DataContext).CurrentViewHeader = content;
            ((MainVM)DataContext).CurrentView = MainVM.PageBuilders[content]();
        }
    }
}
