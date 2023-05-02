using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class LessonType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StudyPlanRecord> StudyPlanRecords { get; set; } = new List<StudyPlanRecord>();
}
