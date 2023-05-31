using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernWpf.Controls.Primitives;

namespace CollegeStatictics.Views;

[MaxHeight(600)]
[ViewTitle("Заметка к занятию")]
public partial class NoteToLessonView : ItemDialog<NoteToLesson>
{
    [MinHeight(200)]
    [Label("Описание")]
    [TextBoxFormElement(AcceptsReturn = true)]
    public string Description
    {
        get => Item.Text;
        set
        {
            Item.Text = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsReadOnly));
        }
    }

    [ObservableProperty]
    private bool _isReadOnly;

    private Lesson _ownerLesson;

    public NoteToLessonView(NoteToLesson? item) : base(item)
    {
        _ownerLesson = Item.Lesson;

        if (Description == string.Empty)
            _ownerLesson.NoteToLesson = null;

        AddCommand.NotifyCanExecuteChanged();
        RemoveCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanAdd))]
    private void Add()
    {
        _ownerLesson.NoteToLesson = Item;
        IsReadOnly = false;
        AddCommand.NotifyCanExecuteChanged();
        RemoveCommand.NotifyCanExecuteChanged();
    }
    private bool CanAdd() => _ownerLesson.NoteToLesson == null;

    [RelayCommand(CanExecute = nameof(CanRemove))]
    private void Remove()
    {
        _ownerLesson.NoteToLesson = null;
        Description = "";
        IsReadOnly = true;
        AddCommand.NotifyCanExecuteChanged();
        RemoveCommand.NotifyCanExecuteChanged();
    }
    private bool CanRemove() => _ownerLesson.NoteToLesson != null;

    protected override IEnumerable<FrameworkElement> CreateViewElements()
    {
        var header = new TextBlock
        {
            Text = "Заметка к занятию",
            FontSize = 22
        };
        yield return header;

        var dockPanel = new DockPanel();
        var removeButton = new Button
        {
            Content = "Удалить",
            Command = RemoveCommand,
            HorizontalAlignment = HorizontalAlignment.Right
        };
        removeButton.SetValue(DockPanel.DockProperty, Dock.Right);
        var addButton = new Button
        {
            Content = "Добавить",
            Command = AddCommand,
            Margin = new(0, 0, 10, 0),
            HorizontalAlignment = HorizontalAlignment.Right
        };
        addButton.SetValue(DockPanel.DockProperty, Dock.Right);
        dockPanel.Children.Add(removeButton);
        dockPanel.Children.Add(addButton);
        yield return dockPanel;

        var descriptionTextBox = new TextBox
        {
            Height = 200,
            AcceptsReturn = true
        };
        TextBoxHelper.SetIsDeleteButtonVisible(descriptionTextBox, false);
        descriptionTextBox.SetBinding(TextBox.TextProperty, nameof(Description));
        descriptionTextBox.SetBinding(TextBox.IsReadOnlyProperty, nameof(IsReadOnly));
        _descriptionTextBox = descriptionTextBox;
        yield return descriptionTextBox;

        if (string.IsNullOrWhiteSpace(Description))
            IsReadOnly = true;
    }

    private TextBox _descriptionTextBox;
}
