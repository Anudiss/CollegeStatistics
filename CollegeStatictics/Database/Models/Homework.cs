using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Homework
{
    public int Id { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime Deadline { get; set; }

    public string Text { get; set; } = null!;

    public int ExecutionStatusId { get; set; }

    public int LessonId { get; set; }

    public virtual HomeworkExecutionStatus ExecutionStatus { get; set; } = null!;

    public virtual Lesson Lesson { get; set; } = null!;
}
