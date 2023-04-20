using CollegeStatictics.Utilities;
using CollegeStatictics.ViewModels;
using System.CodeDom;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CollegeStatictics.Controls
{
    [TemplatePart(Name = "PART_OpenWindowButton", Type = typeof(Button))]
    public class EntitySelectorBox : Control
    {
        /*
         * Readonly textBox
         * Button
         * 
         * 
         */
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(EntitySelectorBox));

        public static readonly DependencyProperty DisplayMemberProperty =
            DependencyProperty.Register(nameof(DisplayMember), typeof(object), typeof(EntitySelectorBox));

        static EntitySelectorBox() =>
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EntitySelectorBox), new FrameworkPropertyMetadata(typeof(EntitySelectorBox)));

        public object? SelectedItem
        {
            get => (object?)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public object DisplayMember
        {
            get => GetValue(DisplayMemberProperty);
            set => SetValue(DisplayMemberProperty, value);
        }

        public override void OnApplyTemplate()
        {
            Button openWindowButton = (Button)GetTemplateChild("PART_OpenWindowButton");

            openWindowButton.Click += (_, _) =>
            {
                /*var contentDialog = new RichContentDialog()
                {
                    Content = 
                };*/
            };
        }
    }
}
