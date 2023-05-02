using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Timetable
{
    public int Id { get; set; }

    public int TeacherId { get; set; }

    public int SubjectId { get; set; }

    public int GroupId { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;

    public virtual ICollection<TimetableRecord> TimetableRecords { get; set; } = new List<TimetableRecord>();
}
