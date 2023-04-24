using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace CollegeStatictics.ViewModels.Base
{
    public partial class WindowViewModelBase : ObservableValidator
    {
        [ObservableProperty]
        private string? title;

        public Action? CloseWindowMethod;

        public void CloseWindow() => CloseWindowMethod?.Invoke();
        public virtual bool OnClosing() => true;
    }
}
