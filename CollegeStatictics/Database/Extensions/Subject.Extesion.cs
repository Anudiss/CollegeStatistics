using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Interfaces;

using System;
using System.Collections;

namespace CollegeStatictics.Database.Models;

public partial class Subject : ITable, IDeletable, IComparable
{
    public int CompareTo( object? obj ) => obj.ToString().CompareTo(ToString());
    public override string ToString() => Name;
}
