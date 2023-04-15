using CollegeStatictics.ViewModels;
using ModernWpf.Controls;
using System;
using System.Diagnostics;
using System.Windows;
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
            var type = (args.SelectedItem as NavigationViewItem)?.Tag as Type;
            var constructor = type?.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
                return;

            ((MainVM)DataContext).CurrentView = constructor.Invoke(Type.EmptyTypes);
        }
    }
}
