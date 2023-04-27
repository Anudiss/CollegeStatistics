using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Attendance
{
    public int Id { get; set; }

    public int LessonId { get; set; }

    public int StudentId { get; set; }

    public bool IsAttended { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
