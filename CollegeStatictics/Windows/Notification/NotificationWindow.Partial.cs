using System.Windows;

namespace CollegeStatictics.Windows.Notification
{
    public partial class NotificationWindow
    {
        public static readonly DependencyProperty NotificationIconProperty =
            DependencyProperty.Register(nameof(NotificationIcon), typeof(NotificationIcon), typeof(NotificationWindow));

        public static readonly DependencyProperty NotificationButtonsProperty =
            DependencyProperty.Register(nameof(Buttons), typeof(NotificationButton), typeof(NotificationWindow));
    }
}
