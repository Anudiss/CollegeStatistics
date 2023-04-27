using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class GroupLeader
{
    public int Id { get; set; }

    public virtual Student? Student { get; set; }
}
