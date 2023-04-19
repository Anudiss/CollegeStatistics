using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class GroupLeader
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Student? Student { get; set; }
}
