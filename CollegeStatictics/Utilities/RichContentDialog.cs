using ModernWpf.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        static async Task CreateDialog(RichContentDialog dialog, bool awaitPreviousDialog)
        {
            if (ActiveDialog != null)
            {
                if (awaitPreviousDialog)
                {
                    await DialogAwaiter.Task;
                    DialogAwaiter = new TaskCompletionSource<bool>();
                }
                else
                {
                    ActiveDialog.Hide();
                    ActiveDialogs.Pop();
                }
            }

            ActiveDialogs.Push(dialog);
            ActiveDialog.Closed += ActiveDialog_Closed;
            await ActiveDialog.ShowAsync();
            ActiveDialog.Closed -= ActiveDialog_Closed;
        }

        public static Stack<RichContentDialog> ActiveDialogs = new();
        public static RichContentDialog ActiveDialog => ActiveDialogs.Any() ? ActiveDialogs.Peek() : null!;

        static TaskCompletionSource<bool> DialogAwaiter = new();
        private static void ActiveDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args) { DialogAwaiter.SetResult(true); }
    }

}
