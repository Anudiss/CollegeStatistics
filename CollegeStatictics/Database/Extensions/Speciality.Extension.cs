﻿using CollegeStatictics.DataTypes;

namespace CollegeStatictics.Database.Models;

public partial class Speciality : ITable
{
    public override string ToString() => Name;
}
