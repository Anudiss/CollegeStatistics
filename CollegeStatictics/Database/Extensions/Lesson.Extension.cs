using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Interfaces;
using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Lesson : ITable, IDeletable
{
    public override string ToString() => $"Пара {TimetableRecord.Timetable.Subject} {TimetableRecord.Timetable.Group} {Datetime:F}";
}
