namespace CollegeStatictics.DataTypes.Interfaces;

public interface IEntitySelectorBox
{
    public object? SelectedItem { get; set; }

    public object? OpenSelectorItemDialog();
}
