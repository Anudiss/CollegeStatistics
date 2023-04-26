using ModernWpf.Controls;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CollegeStatictics.Windows
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public static readonly DependencyProperty ContentsProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(DialogWindow));

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
            get { return (object)GetValue(ContentsProperty); }
            set { SetValue(ContentsProperty, value); }
        }

        public new DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentsTemplateProperty); }
            set { SetValue(ContentsTemplateProperty, value); }
        }

        public DialogWindow()
        {
            InitializeComponent();
        }

        public new void Show() => ShowDialog();

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
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
