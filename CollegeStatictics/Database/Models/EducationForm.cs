using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class EducationForm
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
