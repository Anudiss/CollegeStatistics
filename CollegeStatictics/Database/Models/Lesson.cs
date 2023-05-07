using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Lesson
{
    public int Id { get; set; }

    public int TimetableId { get; set; }

    public DateTime Datetime { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Timetable Timetable { get; set; } = null!;

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<EmergencySituation> EmergencySituations { get; set; } = new List<EmergencySituation>();

    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();

    public virtual ICollection<NoteToLesson> NoteToLessons { get; set; } = new List<NoteToLesson>();

    public virtual ICollection<NoteToStudent> NoteToStudents { get; set; } = new List<NoteToStudent>();
}
