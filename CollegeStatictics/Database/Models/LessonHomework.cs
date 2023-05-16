using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class LessonHomework
{
    public int LessonId { get; set; }

    public int HomeworkId { get; set; }

    public DateTime Deadline { get; set; }

    public DateTime IssueDate { get; set; }

    public virtual Homework Homework { get; set; } = null!;

    public virtual Lesson Lesson { get; set; } = null!;
}
