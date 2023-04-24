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
            dynamic tag = (args.SelectedItem as NavigationViewItem)!.Tag;

            ((MainVM)DataContext).CurrentView = tag();
        }
    }
}
