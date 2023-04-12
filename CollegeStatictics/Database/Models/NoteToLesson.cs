using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class NoteToLesson
{
    public int Id { get; set; }

    public int LessonId { get; set; }

    public string Text { get; set; } = null!;

    public virtual Lesson Lesson { get; set; } = null!;
}
