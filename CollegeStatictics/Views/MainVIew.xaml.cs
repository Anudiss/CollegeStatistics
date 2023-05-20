using CollegeStatictics.Database;
using CollegeStatictics.DataTypes;
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

            object? content = navItem.Content;

            IContent view = null!;
            string title = null!;
            if (content is string name)
            {
                view = MainVM.PageBuilders[name]();
                title = name;
            }
            else
            {
                view = (IContent)content;
                title = view.Title;
            }

            ((MainVM)DataContext).CurrentViewHeader = title;
            ((MainVM)DataContext).CurrentView = view;
        }
    }
}
