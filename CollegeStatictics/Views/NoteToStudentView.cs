using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes.Attributes;
using CollegeStatictics.ViewModels.Attributes;
using CollegeStatictics.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;

namespace CollegeStatictics.Views;

[MaxHeight(600)]
[ViewTitle("Заметка к студенту")]
public partial class NoteToStudentView : ItemDialog<NoteToStudent>
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

    private Student _ownerStudent;

    public NoteToStudentView(NoteToStudent? item) : base(item)
    {
        _ownerStudent = Item.Student;

        if (string.IsNullOrWhiteSpace(Description))
            Item.Student = null;

        AddCommand.NotifyCanExecuteChanged();
        RemoveCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanAdd))]
    private void Add()
    {
        Item.Student = _ownerStudent;
        IsReadOnly = false;
        AddCommand.NotifyCanExecuteChanged();
        RemoveCommand.NotifyCanExecuteChanged();
    }
    private bool CanAdd() => Item.Student == null;

    [RelayCommand(CanExecute = nameof(CanRemove))]
    private void Remove()
    {
        Item.Student = null;
        Description = "";
        IsReadOnly = true;
        AddCommand.NotifyCanExecuteChanged();
        RemoveCommand.NotifyCanExecuteChanged();
    }
    private bool CanRemove() => Item.Student != null;

    protected override IEnumerable<FrameworkElement> CreateViewElements()
    {
        var headerTb = new TextBlock
        {
            Text = "Заметка к студенту",
            FontSize = 22
        };
        yield return headerTb;

        var dockPanel = new DockPanel();

        var studentFullNameTb = new TextBlock
        {
            Text = $"{_ownerStudent}",
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 16
        };
        studentFullNameTb.SetValue(DockPanel.DockProperty, Dock.Left);

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

        dockPanel.Children.Add(studentFullNameTb);
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
