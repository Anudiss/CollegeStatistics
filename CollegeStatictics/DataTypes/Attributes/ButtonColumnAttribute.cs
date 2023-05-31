using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace CollegeStatictics.DataTypes.Attributes;

public class ButtonColumnAttribute : TextColumnAttribute
{
    public string CommandName { get; set; }

    public object? CommandContext { get; set; }

    public ButtonColumnAttribute(string path, string header, string commandPath) : base(path, header)
    {
        CommandName = commandPath;
    }

    public override DataGridColumn ToDataGridColumn()
    {
        var factory = new FrameworkElementFactory(typeof(Button));

        if (CommandContext != null)
        {
            var command = CommandContext.GetType().GetProperty(CommandName)!.GetValue(CommandContext);
            factory.SetValue(ButtonBase.CommandProperty, command);
            factory.SetBinding(ButtonBase.CommandParameterProperty, new Binding(Path) { Mode = BindingMode.OneWay});
        }

        factory.SetValue(ButtonBase.ContentProperty, Header);
        factory.SetValue(ButtonBase.PaddingProperty, new Thickness(5));
        factory.SetValue(ButtonBase.MarginProperty, new Thickness(2.5));

        return new DataGridTemplateColumn()
        {
            Header = Header,
            CellTemplate = new DataTemplate()
            {
                VisualTree = factory
            },
        };
    }
}
