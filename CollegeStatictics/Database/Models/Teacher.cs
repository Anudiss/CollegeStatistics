using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}
