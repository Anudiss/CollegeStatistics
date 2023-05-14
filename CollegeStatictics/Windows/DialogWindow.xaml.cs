using CollegeStatictics.DataTypes.Attributes;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CollegeStatictics.Windows
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>

    public delegate bool CanDialogWindowCloseDeterminant();

    public partial class DialogWindow : Window
    {
        public CanDialogWindowCloseDeterminant? CanClose { get; set; }

        public static readonly DependencyProperty ContentsProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(DialogWindow), new PropertyMetadata(OnContentChanged));

        public static readonly DependencyProperty ContentsTemplateProperty =
            DependencyProperty.Register(nameof(ContentTemplate), typeof(DataTemplate), typeof(DialogWindow));

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
            set { SetValue(PrimaryButtonTextProperty, value == null ? null : value.Trim() == "" ? null : value); }
        }

        public string SecondaryButtonText
        {
            get { return (string)GetValue(SecondaryButtonTextProperty); }
            set { SetValue(SecondaryButtonTextProperty, value == null ? null : value.Trim() == "" ? null : value); }
        }

        public string TertiaryButtonText
        {
            get { return (string)GetValue(TertiaryButtonTextProperty); }
            set { SetValue(TertiaryButtonTextProperty, value == null ? null : value.Trim() == "" ? null : value); }
        }

        public new object Content
        {
            get { return GetValue(ContentsProperty); }
            set { SetValue(ContentsProperty, value); }
        }

        public new DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentsTemplateProperty); }
            set { SetValue(ContentsTemplateProperty, value); }
        }

        public DialogResult Result { get; private set; }

        public DialogWindow()
        {
            InitializeComponent();
        }

        public static void OnContentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var dialogWindow = (DialogWindow)obj;
            var minHeightAttribute = args.NewValue.GetType().GetCustomAttribute<MinHeightAttribute>();
            if (minHeightAttribute != null)
                dialogWindow.MinHeight = minHeightAttribute.Height;

            var minWidthAttribute = args.NewValue.GetType().GetCustomAttribute<MinWidthAttribute>();
            if (minWidthAttribute != null)
                dialogWindow.MinWidth = minWidthAttribute.Width;
        }

        public static void ShowMessage(string text)
        {
            new DialogWindow()
            {
                Content = new TextBlock
                {
                    Text = text,
                    FontSize = 16
                },

                PrimaryButtonText = "Ок"
            }.Show();
        }

        public new void Show() => ShowDialog();

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void PrimaryButton_Click(object sender, RoutedEventArgs e)
        {
            if (PrimaryButtonCommand?.CanExecute(null) != false)
                PrimaryButtonCommand?.Execute(null);

            if (CanClose?.Invoke() != false)
            {
                Result = Windows.DialogResult.Primary;
                Close();
            }
        }

        private void SecondaryButton_Click(object sender, RoutedEventArgs e)
        {
            if (SecondaryButtonCommand?.CanExecute(null) != false)
                SecondaryButtonCommand?.Execute(null);

            if (CanClose?.Invoke() != false)
            {
                Result = Windows.DialogResult.Secondary;
                Close();
            }
        }

        private void TertiaryButton_Click(object sender, RoutedEventArgs e)
        {
            if (TertiaryButtonCommand?.CanExecute(null) != false)
                TertiaryButtonCommand?.Execute(null);

            if (CanClose?.Invoke() != false)
            {
                Result = Windows.DialogResult.Tertiary;
                Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Result != Windows.DialogResult.None)
                return;

            e.Cancel = !CanClose?.Invoke() ?? false;
        }
    }

    public enum DialogResult
    {
        None,
        Primary,
        Secondary,
        Tertiary
    }
}
