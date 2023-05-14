using CollegeStatictics.ViewModels.Attributes;

using System;

namespace CollegeStatictics.DataTypes.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class SubtableButtonFormElementAttribute : Attribute
{
    public string Text { get; }
    public string CommandName { get; }

    public SubtableButtonFormElementAttribute(string text, string commandName)
    {
        Text = text;
        CommandName = commandName.EndsWith("Command") ? commandName : $"{commandName}Command";
    }
}
