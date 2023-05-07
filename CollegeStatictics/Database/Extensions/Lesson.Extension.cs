using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Interfaces;
using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Lesson : ITable, IDeletable
{
    public override string ToString() => $"Пара {Timetable.Subject} {Timetable.Group} {Datetime:F}";
}
