using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class LessonStartTime
{
    public int Id { get; set; }

    public int Couple { get; set; }

    public TimeSpan StartTime { get; set; }

    public bool IsShortened { get; set; }

    public virtual ICollection<DayOfWeek> DayOfWeeks { get; set; } = new List<DayOfWeek>();
}
