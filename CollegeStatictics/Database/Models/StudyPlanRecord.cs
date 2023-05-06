using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class StudyPlanRecord
{
    public int Id { get; set; }

    public int LessonTypeId { get; set; }

    public int DurationInLessons { get; set; }

    public string Topic { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int StudyPlanId { get; set; }

    public virtual LessonType LessonType { get; set; } = null!;

    public virtual StudyPlan StudyPlan { get; set; } = null!;
}
