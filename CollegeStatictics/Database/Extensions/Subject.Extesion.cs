﻿using CollegeStatictics.DataTypes;

namespace CollegeStatictics.Database.Models;

public partial class Subject : ITable
{
    public override string ToString() => Name;
}
