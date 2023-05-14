using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Homework
{
    public int Id { get; set; }

    public string Topic { get; set; } = null!;

    public string Text { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<LessonHomework> LessonHomeworks { get; set; } = new List<LessonHomework>();
}
