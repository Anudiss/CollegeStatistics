﻿using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Speciality
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();
}
