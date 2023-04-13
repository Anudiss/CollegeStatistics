using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CollegeStatictics.Windows.Notification
{
    /// <summary>
    /// Логика взаимодействия для Notification.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public string Text { get; }

        public NotificationButton Buttons
        {
            get => (NotificationButton)GetValue(NotificationButtonsProperty);
            set => SetValue(NotificationButtonsProperty, value);
        }

        public NotificationIcon NotificationIcon
        {
            get => (NotificationIcon)GetValue(NotificationIconProperty);
            set => SetValue(NotificationIconProperty, value);
        }

        public NotificationResult NotificationResult { get; private set; }

        private NotificationWindow(string text, string title, NotificationButton buttons, NotificationIcon icon)
        {
            Title = title;
            Buttons = buttons;
            NotificationIcon = icon;

            InitializeComponent();
        }

        public new static NotificationResult Show(string text, string title = "Уведомление", NotificationButton buttons = default, NotificationIcon icon = default)
        {
            NotificationWindow notification = new(text, title, buttons, icon);
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

    public enum NotificationButton
    {
        Ok,
        YesNo,
        Cancel,
        YesNoCancel
    }

    public enum NotificationResult
    {
        Ok = 1,
        Yes,
        No,
        Cancel
    }
}
