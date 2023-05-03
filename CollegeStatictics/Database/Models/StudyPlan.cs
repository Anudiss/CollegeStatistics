using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class StudyPlan
{
    public int Id { get; set; }

    public int SpecialityId { get; set; }

    public int SubjectId { get; set; }

    public byte Course { get; set; }

    public DateTime StartDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual ICollection<StudyPlanRecord> StudyPlanRecords { get; set; } = new List<StudyPlanRecord>();

    public virtual Subject Subject { get; set; } = null!;
}
