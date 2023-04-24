using ModernWpf.Controls;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CollegeStatictics.Utilities
{
    public class RichContentDialog : ContentDialog
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                return;

            base.OnKeyDown(e);
        }
    }

    public static class ContentDialogMaker
    {
        public static async void CreateContentDialog(RichContentDialog Dialog, bool awaitPreviousDialog) { await CreateDialog(Dialog, awaitPreviousDialog); }
        public static async Task CreateContentDialogAsync(RichContentDialog Dialog, bool awaitPreviousDialog) { await CreateDialog(Dialog, awaitPreviousDialog); }

        static async Task CreateDialog(RichContentDialog Dialog, bool awaitPreviousDialog)
        {
            if (ActiveDialog != null)
            {
                if (awaitPreviousDialog)
                {
                    await DialogAwaiter.Task;
                    DialogAwaiter = new TaskCompletionSource<bool>();
                }
                else ActiveDialog.Hide();
            }
            ActiveDialog = Dialog;
            ActiveDialog.Closed += ActiveDialog_Closed;
            await ActiveDialog.ShowAsync();
            ActiveDialog.Closed -= ActiveDialog_Closed;
        }

        public static RichContentDialog ActiveDialog;
        static TaskCompletionSource<bool> DialogAwaiter = new TaskCompletionSource<bool>();
        private static void ActiveDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args) { DialogAwaiter.SetResult(true); }
    }

}
