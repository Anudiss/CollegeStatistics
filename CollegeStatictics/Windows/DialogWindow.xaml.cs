using System.Windows;
using System.Windows.Input;

namespace CollegeStatictics.Windows
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public static readonly DependencyProperty PrimaryButtonTextProperty =
            DependencyProperty.Register(nameof(PrimaryButtonText), typeof(string), typeof(DialogWindow));

        public static readonly DependencyProperty SecondaryButtonTextProperty =
            DependencyProperty.Register(nameof(SecondaryButtonText), typeof(string), typeof(DialogWindow));

        public static readonly DependencyProperty TertiaryButtonTextProperty =
            DependencyProperty.Register(nameof(TertiaryButtonText), typeof(string), typeof(DialogWindow));

        public static readonly DependencyProperty PrimaryButtonCommandProperty =
            DependencyProperty.Register(nameof(PrimaryButtonCommand), typeof(ICommand), typeof(DialogWindow));

        public static readonly DependencyProperty SecondaryButtonCommandProperty =
            DependencyProperty.Register(nameof(SecondaryButtonCommand), typeof(ICommand), typeof(DialogWindow));

        public static readonly DependencyProperty TertiaryButtonCommandProperty =
            DependencyProperty.Register(nameof(TertiaryButtonCommand), typeof(ICommand), typeof(DialogWindow));

        public ICommand PrimaryButtonCommand
        {
            get { return (ICommand)GetValue(PrimaryButtonCommandProperty); }
            set { SetValue(PrimaryButtonCommandProperty, value); }
        }

        public ICommand SecondaryButtonCommand
        {
            get { return (ICommand)GetValue(SecondaryButtonCommandProperty); }
            set { SetValue(SecondaryButtonCommandProperty, value); }
        }

        public ICommand TertiaryButtonCommand
        {
            get { return (ICommand)GetValue(TertiaryButtonCommandProperty); }
            set { SetValue(TertiaryButtonCommandProperty, value); }
        }

        public string PrimaryButtonText
        {
            get { return (string)GetValue(PrimaryButtonTextProperty); }
            set { SetValue(PrimaryButtonTextProperty, value); }
        }

        public string SecondaryButtonText
        {
            get { return (string)GetValue(SecondaryButtonTextProperty); }
            set { SetValue(SecondaryButtonTextProperty, value); }
        }

        public string TertiaryButtonText
        {
            get { return (string)GetValue(TertiaryButtonTextProperty); }
            set { SetValue(TertiaryButtonTextProperty, value); }
        }

        public DialogWindow()
        {
            InitializeComponent();
        }
    }

    public enum DialogButton
    {
        None,
        Primary,
        Secondary,
        Tertiary
    }
}
