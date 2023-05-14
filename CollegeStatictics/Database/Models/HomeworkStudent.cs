using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class HomeworkStudent
{
    public int Id { get; set; }

    public int LessonId { get; set; }

    public int StudentId { get; set; }

    public int HomeworkExecutionStatusId { get; set; }

    public short Mark { get; set; }

    public virtual HomeworkExecutionStatus HomeworkExecutionStatus { get; set; } = null!;

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
