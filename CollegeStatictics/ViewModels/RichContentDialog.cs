using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CollegeStatictics.ViewModels
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
    }
}
