using CollegeStatictics.ViewModels.Base;
using System.ComponentModel;
using System.Windows;

namespace CollegeStatictics.Windows
{
    /// <summary>
    /// Логика взаимодействия для ViewModelWindow.xaml
    /// </summary>
    public partial class WindowViewModel : Window
    {
        public WindowViewModelBase ViewModel { get; }

        public WindowViewModel(WindowViewModelBase viewModel)
        {
            DataContext = ViewModel = viewModel;

            ViewModel.CloseWindowMethod = Close;

            InitializeComponent();

            Loaded += OnLoaded;

        }

        private void OnLoaded(object? sender, RoutedEventArgs args)
        {
            Padding = new(0);

            Loaded -= OnLoaded;
        }

        protected override void OnClosing(CancelEventArgs e) =>
            e.Cancel = !ViewModel.OnClosing();
    }
}
