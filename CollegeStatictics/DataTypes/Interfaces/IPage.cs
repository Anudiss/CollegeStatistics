using CollegeStatictics.DataTypes.Classes;
using System.Collections;
using System.Windows;

namespace CollegeStatictics.DataTypes
{
    public interface IItemSelector<T> where T : class
    {
        public IList SelectedItems { get; set; }
        public FilteredObservableCollection<T> Items { get; set; }

        #region [ Commands ]
        
        /*
                [RelayCommand]
                private void OpenEditAddItemWindow(T? item)
                {
                    EditAddDepartmentVM vm = new(item);
                    var contentDialog = new RichContentDialog()
                    {
                        Title = item == null ? "Добавление" : "Редактирование",
                        Content = vm,

                        PrimaryButtonText = "Сохранить",
                        PrimaryButtonCommand = vm.SaveCommand,

                        SecondaryButtonText = "Отмена",

                        DefaultButton = ContentDialogButton.Primary
                    };
                    var refreshSubjects = new TypedEventHandler<ContentDialog, ContentDialogClosingEventArgs>((_, args) => { Items.Refresh(); DatabaseContext.CancelChanges(); });

                    contentDialog.Closing += refreshSubjects;

                    contentDialog.ShowAsync();
                }

                [RelayCommand]
                private async void RemoveItem(T item)
                {
                    var dialog = new ContentDialog()
                    {
                        Title = "Уведомление",
                        Content = $"Вы действительно хотите удалить {((dynamic)item).Name}",

                        PrimaryButtonText = "Да",

                        SecondaryButtonText = "Нет"
                    };

                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                        MessageBox.Show($"{((dynamic)item).Name} удалён (Нихуя)");
                }*/
        #endregion
    }

    public interface IContent
    {
        public string Title { get; }

        public DataTemplate ContentTemplate { get; }
    }
}
