using System;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Data;

namespace CollegeStatictics.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class TextColumnAttribute : Attribute
    {
        public string Path { get; }
        public string Header { get; }
        public bool IsReadOnly { get; set; } = true;
        public string StringFormat { get; set; } = "{0}";

        public TextColumnAttribute(string path, string header)
        {
            Path = path;
            Header = header;
        }

        public virtual DataGridColumn ToDataGridColumn() =>
            new DataGridTextColumn()
            {
                Header = Header,
                Binding = new Binding(Path)
                {
                    Mode = IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
                    StringFormat = StringFormat,
                    ConverterCulture = new System.Globalization.CultureInfo("ru-RU"),
                    TargetNullValue = "Нет"
                },

                IsReadOnly = IsReadOnly
            };
    }
}
