using CollegeStatictics.DataTypes.Interfaces;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Windows;
using System.Windows.Controls;

namespace CollegeStatictics.DataTypes.Classes;

public delegate void SelectedItemChangedEventHandler<T>(object sender, T item);

[ObservableObject]
public abstract partial class EntitySelectorBox<T> : Control, IEntitySelectorBox
{
    public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(T), typeof(EntitySelectorBox<T>));

    public static readonly RoutedEvent SelectedItemChangedEvent =
        EventManager.RegisterRoutedEvent(nameof(SelectedItemChanged), RoutingStrategy.Bubble, typeof(SelectedItemChangedEventHandler<T>), typeof(EntitySelectorBox<T>));

    public event SelectedItemChangedEventHandler<T> SelectedItemChanged
    {
        add => AddHandler(SelectedItemChangedEvent, value);
        remove => RemoveHandler(SelectedItemChangedEvent, value);
    }

    static EntitySelectorBox() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EntitySelectorBox<T>), new FrameworkPropertyMetadata(typeof(EntitySelectorBox<T>)));


    [RelayCommand]
    private void OpenSelectorDialog()
    {
        SelectedItem = OpenSelectorItemDialog();
    }

    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public abstract object? OpenSelectorItemDialog();
}
