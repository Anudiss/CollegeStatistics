using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CollegeStatictics.Windows
{
    /// <summary>
    /// Логика взаимодействия для Notification.xaml
    /// </summary>
    public partial class Notification : Window
    {
        public string Text { get; }
        public NotificationButton Buttons { get; }
        public NotificationIcon NotificationIcon { get; }
        public NotificationResult NotificationResult { get; private set; }

        private Notification(string text, string title, NotificationButton buttons, NotificationIcon icon)
        {
            Title = title;

            InitializeComponent();
            Buttons = buttons;
            NotificationIcon = icon;
        }

        public new static NotificationResult Show(string text, string title = "Уведомление", NotificationButton buttons = default, NotificationIcon icon = default)
        {
            Notification notification = new(text, title, buttons, icon);
            notification.ShowDialog();
            return notification.NotificationResult;
        }
    }

    public enum NotificationIcon
    {
        Notification,
        Warning,
        Question,
        Error
    }

    [Flags]
    public enum NotificationButton
    {
        Ok = 1 << 0,
        Yes = 1 << 1,
        No = 1 << 2,
        Cancel = 1 << 3
    }

    public enum NotificationResult
    {
        Ok = 1,
        Yes,
        No,
        Cancel
    }
}
