using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CommisionCurator> CommisionCurators { get; set; } = new List<CommisionCurator>();

    public virtual ICollection<Speciality> Specialities { get; set; } = new List<Speciality>();
}
