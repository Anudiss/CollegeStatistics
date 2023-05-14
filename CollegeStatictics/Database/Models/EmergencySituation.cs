using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class EmergencySituation
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual Lesson Lesson { get; set; } = null!;
}
