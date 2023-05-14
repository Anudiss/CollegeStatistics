using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace CollegeStatictics.DataTypes.Attributes;

public class CheckBoxColumn : TextColumnAttribute
{
    public CheckBoxColumn(string path, string header) : base(path, header)
    {
    }

    public override DataGridColumn ToDataGridColumn()
    {
        var trigger = new Trigger
        {
            Property = UIElement.IsMouseOverProperty,
            Value = true
        };

        trigger.Setters.Add(new Setter()
        {
            Property = DataGridCell.IsEditingProperty,
            Value = true
        });

        var style = new Style()
        {
            TargetType = typeof(DataGridCell),
            BasedOn = (Style)Application.Current.FindResource("EditingCell")
        };

        style.Triggers.Add(trigger);

        return new DataGridCheckBoxColumn()
        {
            Header = Header,
            Binding = new Binding(Path)
            {
                Mode = IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            },

            IsReadOnly = IsReadOnly,

            CellStyle = style
        };
    }
}
