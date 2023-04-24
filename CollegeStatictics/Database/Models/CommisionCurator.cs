using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class CommisionCurator
{
    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;
}
