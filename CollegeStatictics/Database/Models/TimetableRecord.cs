using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class TimetableRecord
{
    public int Id { get; set; }

    public TimeSpan Time { get; set; }

    public int DayOfWeekId { get; set; }

    public int TimetableId { get; set; }

    public virtual DayOfWeek DayOfWeek { get; set; } = null!;

    public virtual Timetable Timetable { get; set; } = null!;
}
