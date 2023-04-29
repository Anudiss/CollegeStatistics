using System;

namespace CollegeStatictics.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ColumnAttribute : Attribute
    {
        public string Path { get; }
        public string Header { get; }

        public ColumnAttribute(string path, string header)
        {
            Path = path;
            Header = header;
        }
    }
}
