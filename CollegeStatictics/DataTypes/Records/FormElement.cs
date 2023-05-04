using CollegeStatictics.ViewModels.Attributes;
using System.Reflection;

namespace CollegeStatictics.DataTypes.Records
{
    public sealed record FormElement(PropertyInfo Property, FormElementAttribute Attribute);
}
