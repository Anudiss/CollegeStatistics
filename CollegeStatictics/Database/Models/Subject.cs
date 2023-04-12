using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}
