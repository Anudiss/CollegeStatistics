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
using ModernWpf.Controls.Primitives;

namespace CollegeStatictics.Views;

[MaxHeight(600)]
[ViewTitle("Нештатная ситуация")]
public partial class EmergencySituationView : ItemDialog<EmergencySituation>
{
    [MinHeight(200)]
    [Label("Описание")]
    [TextBoxFormElement(AcceptsReturn = true)]
    public string Description
    {
        get => Item.Description;
        set
        {
            Item.Description = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsReadOnly));
        }
    }

    [ObservableProperty]
    private bool _isReadOnly;

    private Lesson _ownerLesson;

    public EmergencySituationView(EmergencySituation? item) : base(item)
    {
        _ownerLesson = Item.Lesson;

        if (Description == "")
        {
            _ownerLesson.EmergencySituation = null;
            IsReadOnly = true;
        }

        AddCommand.NotifyCanExecuteChanged();
        RemoveCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanAdd))]
    private void Add()
    {
        _ownerLesson.EmergencySituation = Item;
        IsReadOnly = false;
        AddCommand.NotifyCanExecuteChanged();
        RemoveCommand.NotifyCanExecuteChanged();
    }
    private bool CanAdd() => _ownerLesson.EmergencySituation == null;

    [RelayCommand(CanExecute = nameof(CanRemove))]
    private void Remove()
    {
        _ownerLesson.EmergencySituation = null;
        Description = "";
        IsReadOnly = true;
        AddCommand.NotifyCanExecuteChanged();
        RemoveCommand.NotifyCanExecuteChanged();
    }
    private bool CanRemove() => _ownerLesson.EmergencySituation != null;

    protected override IEnumerable<FrameworkElement> CreateViewElements()
    {
        var header = new TextBlock
        {
            Text = "Нештатная ситуация",
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
    }

    private TextBox _descriptionTextBox;
}
