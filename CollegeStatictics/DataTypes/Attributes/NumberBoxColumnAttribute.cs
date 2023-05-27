using ModernWpf.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        var editingCellFactory = new FrameworkElementFactory(typeof(NumberBox));

        editingCellFactory.SetBinding(NumberBox.TextProperty, new Binding(Path)
        {
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            TargetNullValue = "-",
        });

        editingCellFactory.SetValue(NumberBox.MinimumProperty, Min);
        editingCellFactory.SetValue(NumberBox.MaximumProperty, Max);

        editingCellFactory.SetValue(NumberBox.SpinButtonPlacementModeProperty, NumberBoxSpinButtonPlacementMode.Compact);

        var cellFactory = new FrameworkElementFactory(typeof(TextBlock));

        cellFactory.SetBinding(TextBox.TextProperty, new Binding(Path)
        {
            Mode = IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            TargetNullValue = "-",
        });

        return new DataGridTemplateColumn()
        {
            Header = Header,
            
            CellTemplate = new DataTemplate()
            {
                VisualTree = cellFactory
            },

            CellEditingTemplate = new DataTemplate()
            {
                VisualTree = editingCellFactory
            },

            IsReadOnly = IsReadOnly
        };
    }
}
