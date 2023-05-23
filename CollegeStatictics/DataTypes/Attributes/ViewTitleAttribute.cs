using System;

namespace CollegeStatictics.DataTypes.Attributes;

public class ViewTitleAttribute : Attribute
{
    public string Title { get; set; }

    public ViewTitleAttribute(string title) => Title = title;
}
