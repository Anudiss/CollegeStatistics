using CollegeStatictics.Database.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CollegeStatictics.ViewModels
{
    public partial class TimetableRecordElement : ObservableObject
    {
        [ObservableProperty]
        private DayOfWeek dayOfWeek;

        [ObservableProperty]
        private Subject subject;

        [ObservableProperty]
        private int lessonIndex;

        [ObservableProperty]
        private Timetable timetable;
    }
}
