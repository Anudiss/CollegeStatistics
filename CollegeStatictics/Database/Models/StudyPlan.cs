using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class StudyPlan
{
    public int SpecialityId { get; set; }

    public int SubjectId { get; set; }

    public byte Course { get; set; }

    public int LessonTypeId { get; set; }

    public int DurationInLessons { get; set; }

    public string Topic { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public virtual LessonType LessonType { get; set; } = null!;

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
