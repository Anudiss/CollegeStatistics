using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Lesson
{
    public int Id { get; set; }

    public int TimetableRecordId { get; set; }

    public TimeSpan Time { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsRestoring { get; set; }

    public DateTime Date { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<EmergencySituation> EmergencySituations { get; set; } = new List<EmergencySituation>();

    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();

    public virtual ICollection<NoteToLesson> NoteToLessons { get; set; } = new List<NoteToLesson>();

    public virtual ICollection<NoteToStudent> NoteToStudents { get; set; } = new List<NoteToStudent>();

    public virtual TimetableRecord TimetableRecord { get; set; } = null!;
}
