using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class HomeworkExecutionStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
}
