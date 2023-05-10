using System.Collections.Generic;
using System;

namespace CollegeStatictics.DataTypes
{
    public static class Constants
    {

        private static IEnumerable<TimeSpan> _regularDayLessonStartTimes = new[]
        {
            TimeSpan.Parse("08:00"),
            TimeSpan.Parse("09:40"),
            TimeSpan.Parse("11:50"),
            TimeSpan.Parse("13:40"),
            TimeSpan.Parse("15:20"),
            TimeSpan.Parse("17:00"),
            TimeSpan.Parse("18:40"),
        };

        private static IEnumerable<TimeSpan> _saturdayLessonStartTimes = new[]
        {
            TimeSpan.Parse("08:00"),
            TimeSpan.Parse("09:40"),
            TimeSpan.Parse("11:30"),
            TimeSpan.Parse("13:10"),
            TimeSpan.Parse("14:50"),
            TimeSpan.Parse("16:20"),
            TimeSpan.Parse("18:00"),
        };

        public static readonly IDictionary<DayOfWeek, IEnumerable<TimeSpan>> LessonStartTimes = new Dictionary<DayOfWeek, IEnumerable<TimeSpan>>()
        {
            { DayOfWeek.Monday, _regularDayLessonStartTimes },
            { DayOfWeek.Tuesday, _regularDayLessonStartTimes },
            { DayOfWeek.Wednesday, _regularDayLessonStartTimes },
            { DayOfWeek.Thursday, _regularDayLessonStartTimes },
            { DayOfWeek.Friday, _regularDayLessonStartTimes },
            { DayOfWeek.Saturday, _regularDayLessonStartTimes },
        };
    }
}
