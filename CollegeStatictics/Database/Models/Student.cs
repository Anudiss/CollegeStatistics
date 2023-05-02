using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public int? GroupId { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Group? Group { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<NoteToStudent> NoteToStudents { get; set; } = new List<NoteToStudent>();
}
