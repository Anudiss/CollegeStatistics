using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace CollegeStatictics.ViewModels.Base
{
    public partial class WindowViewModelBase : ObservableValidator
    {
        [ObservableProperty]
        private string? _title;

        public Action? CloseWindowMethod;

        public void CloseWindow() => CloseWindowMethod?.Invoke();
        public bool OnClosing() => true;
    }
}
