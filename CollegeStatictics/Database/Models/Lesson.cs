using System;
using System.Collections.Generic;

namespace CollegeStatictics.Database.Models;

public partial class Lesson
{
    public int Id { get; set; }

    public int StudyPlanRecordId { get; set; }

    public int GroupId { get; set; }

    public TimeSpan Time { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsRestoring { get; set; }

    public DateTime Date { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual EmergencySituation? EmergencySituation { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<HomeworkStudent> HomeworkStudents { get; set; } = new List<HomeworkStudent>();

    public virtual LessonHomework LessonHomework { get; set; } = null!;

    public virtual NoteToLesson? NoteToLesson { get; set; }

    public virtual ICollection<NoteToStudent> NoteToStudents { get; set; } = new List<NoteToStudent>();

    public virtual StudyPlanRecord StudyPlanRecord { get; set; } = null!;
}
