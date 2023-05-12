using System;
using System.Windows.Input;
using CollegeStatictics.ViewModels.Attributes;

namespace CollegeStatictics.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonElementAttribute : Attribute
    {
        public string Content { get; init; }

        public ButtonElementAttribute(string content)
        {
            Content = content;
        }
    }
}