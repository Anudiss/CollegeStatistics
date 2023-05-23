using CollegeStatictics.DataTypes;
using CollegeStatictics.ViewModels;

using ModernWpf.Controls;

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace CollegeStatictics.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void NavigationView_SelectionChanged( NavigationView sender, NavigationViewSelectionChangedEventArgs args )
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

            ( (MainVM)DataContext ).CurrentViewHeader = title;
            ( (MainVM)DataContext ).CurrentView = view;
        }
    }
}
