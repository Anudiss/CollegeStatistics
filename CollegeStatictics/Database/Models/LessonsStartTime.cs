using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class LessonsStartTime
{
    public int Id { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public TimeSpan Time { get; set; }
}
