using ModernWpf.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CollegeStatictics.DataTypes.Attributes;

public class NumberBoxColumnAttribute : TextColumnAttribute
{
    public double Min { get; }
    public double Max { get; }

    public NumberBoxColumnAttribute(string path, string header, double min, double max) : base(path, header)
    {
        Min = min;
        Max = max;
    }

    public override DataGridColumn ToDataGridColumn()
    {
        var factory = new FrameworkElementFactory(typeof(NumberBox));

        factory.SetBinding(NumberBox.ValueProperty, new Binding(Path)
        {
            Mode = IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
        });

        factory.SetValue(NumberBox.MinimumProperty, Min);
        factory.SetValue(NumberBox.MaximumProperty, Max);

        factory.SetValue(NumberBox.SpinButtonPlacementModeProperty, NumberBoxSpinButtonPlacementMode.Compact);

        return new DataGridTemplateColumn()
        {
            Header = Header,

            CellTemplate = new DataTemplate()
            {
                VisualTree = factory
            },

            IsReadOnly = IsReadOnly
        };
    }
}
