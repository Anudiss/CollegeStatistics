using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Timetable
{
    public int Id { get; set; }

    public int TeacherId { get; set; }

    public int LessonTypeId { get; set; }

    public int SubjectId { get; set; }

    public int GroupId { get; set; }

    public int DayOfWeekId { get; set; }

    public TimeSpan Time { get; set; }

    public virtual Subject Subject { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
