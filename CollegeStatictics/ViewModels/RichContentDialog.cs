using CommunityToolkit.Mvvm.Input;
using ModernWpf.Controls;

namespace CollegeStatictics.ViewModels
{
    internal class RichContentDialog
    {
        public RichContentDialog()
        {
        }

        public string Title { get; set; }
        public EditAddSubjectVM Content { get; set; }
        public string PrimaryButtonText { get; set; }
        public IRelayCommand PrimaryButtonCommand { get; set; }
        public string SecondaryButtonText { get; set; }
        public ContentDialogButton DefaultButton { get; set; }
    }
}