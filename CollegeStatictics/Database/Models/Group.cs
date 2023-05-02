using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Group
{
    public int Id { get; set; }

    public short CreationYear { get; set; }

    public int EducationFormId { get; set; }

    public int TeacherId { get; set; }

    public int SpecialityId { get; set; }

    public int Number { get; set; }

    public int? GroupLeaderId { get; set; }

    public virtual EducationForm EducationForm { get; set; } = null!;

    public virtual Student? GroupLeader { get; set; }

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Teacher Curator { get; set; } = null!;

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}
