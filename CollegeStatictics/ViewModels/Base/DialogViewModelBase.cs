using CommunityToolkit.Mvvm.ComponentModel;

namespace CollegeStatictics.ViewModels.Base
{
    public partial class DialogViewModelBase : WindowViewModelBase
    {
        [ObservableProperty]
        private object? content;
    }

    public enum DialogButton
    {
        None      = 1 << 0,
        Primary   = 1 << 1,
        Secondary = 1 << 2
    }
}
