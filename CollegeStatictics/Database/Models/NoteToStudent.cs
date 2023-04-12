using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class NoteToStudent
{
    public int Id { get; set; }

    public int LessonId { get; set; }

    public int StudentId { get; set; }

    public string Text { get; set; } = null!;

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
