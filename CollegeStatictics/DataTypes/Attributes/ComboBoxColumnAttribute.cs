using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace CollegeStatictics.DataTypes.Attributes;

public class ComboBoxColumnAttribute : TextColumnAttribute
{
    public string ItemsSourcePath { get; }

    public ComboBoxColumnAttribute(string path, string header, string itemsSourcePath) : base(path, header)
    {
        ItemsSourcePath = itemsSourcePath;
    }

    public override DataGridColumn ToDataGridColumn()
    {
        var factory = new FrameworkElementFactory(typeof(ComboBox));

        factory.SetBinding(Selector.SelectedItemProperty, new Binding(Path)
        {
            Mode = BindingMode.TwoWay
        });
        factory.SetBinding(ItemsControl.ItemsSourceProperty, new Binding(ItemsSourcePath)
        {
            Mode = BindingMode.TwoWay
        });

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
