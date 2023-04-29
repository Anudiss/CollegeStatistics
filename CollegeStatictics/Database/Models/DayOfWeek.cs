﻿using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class DayOfWeek
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TimetableRecord> TimetableRecords { get; set; } = new List<TimetableRecord>();
}