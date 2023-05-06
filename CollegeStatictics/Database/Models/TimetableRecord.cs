using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class TimetableRecord
{
    public int Id { get; set; }

    public int Couple { get; set; }

    public int DayOfWeekId { get; set; }

    public int TimetableId { get; set; }

    public virtual DayOfWeek DayOfWeek { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual Timetable Timetable { get; set; } = null!;
}
