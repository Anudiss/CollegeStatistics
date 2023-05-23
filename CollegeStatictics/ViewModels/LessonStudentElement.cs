using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

using System.Collections.Generic;
using System.Linq;

namespace CollegeStatictics.ViewModels;

public partial class AttendanceElement : ObservableObject
{
    [ObservableProperty]
    private Lesson _lesson;

    [ObservableProperty]
    private Student _student;

    [ObservableProperty]
    private bool _isAttended;

    [ObservableProperty]
    public short? _mark;

    partial void OnIsAttendedChanged(bool value)
    {
        if (!value)
            DatabaseContext.Entities.Attendances.Local.Add(new Attendance()
            {
                Lesson = Lesson,
                Student = Student,
                IsAttented = true
            });
        else
            DatabaseContext.Entities.Attendances.Local.Remove(Lesson.Attendances.First(a => a.Student == Student));
    }

    public static IEnumerable<AttendanceElement> GetFromLesson(Lesson lesson)
    {
        if (lesson.Group == null)
            return Enumerable.Empty<AttendanceElement>();

        return from student in lesson.Group.Students
               select new AttendanceElement()
               {
                   Lesson = lesson,
                   Student = student,
                   _isAttended = !lesson.Attendances.Any(a => a.Student == student),
                   _mark = lesson.Attendances.FirstOrDefault(a => a.Student == student)?.Mark
               };
    }
}
