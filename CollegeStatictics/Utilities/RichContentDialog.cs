using ModernWpf.Controls;
using System.Windows;
using System.Windows.Input;

namespace CollegeStatictics.Utilities
{
    public class RichContentDialog : ContentDialog
    {
        public new ICommand PrimaryButtonCommand
        {
            get => (ICommand)GetValue(PrimaryButtonCommandProperty);
            set
            {
                SetValue(PrimaryButtonCommandProperty, value);

                value.CanExecuteChanged += delegate { IsPrimaryButtonEnabled = value.CanExecute(PrimaryButtonCommandParameter); };
            }
        }

        public new ICommand SecondaryButtonCommand
        {
            get => (ICommand)GetValue(SecondaryButtonCommandProperty);
            set
            {
                SetValue(SecondaryButtonCommandProperty, value);

                value.CanExecuteChanged += delegate { IsSecondaryButtonEnabled = value.CanExecute(SecondaryButtonCommandParameter); };
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                return;

            base.OnKeyDown(e);
        }
    }
}
