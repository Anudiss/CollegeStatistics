using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CollegeStatictics.ViewModels
{
    public partial class TimetableRecordElement : ObservableObject
    {
        [ObservableProperty]
        private int couple;

        [ObservableProperty]
        private Timetable timetable;

        [ObservableProperty]
        private bool isMondayChecked;

        [ObservableProperty]
        private bool isTuesdayChecked;

        [ObservableProperty]
        private bool isWednesdayChecked;

        [ObservableProperty]
        private bool isThursdayChecked;
        
        [ObservableProperty]
        private bool isFridayChecked;

        [ObservableProperty]
        private bool isSaturdayChecked;

        [ObservableProperty]
        private bool isSundayChecked;

        partial void OnIsMondayCheckedChanged(bool value) =>
            SyncronizeTimetableRecords(value, System.DayOfWeek.Monday);

        partial void OnIsTuesdayCheckedChanged(bool value) =>
            SyncronizeTimetableRecords(value, System.DayOfWeek.Tuesday);

        partial void OnIsWednesdayCheckedChanged(bool value) =>
            SyncronizeTimetableRecords(value, System.DayOfWeek.Wednesday);

        partial void OnIsThursdayCheckedChanged(bool value) =>
            SyncronizeTimetableRecords(value, System.DayOfWeek.Thursday);

        partial void OnIsFridayCheckedChanged(bool value) =>
            SyncronizeTimetableRecords(value, System.DayOfWeek.Friday);

        partial void OnIsSaturdayCheckedChanged(bool value) =>
            SyncronizeTimetableRecords(value, System.DayOfWeek.Saturday);

        partial void OnIsSundayCheckedChanged(bool value) =>
            SyncronizeTimetableRecords(value, System.DayOfWeek.Sunday);

        private void SyncronizeTimetableRecords(bool value, System.DayOfWeek dayOfWeek)
        {
            if (value)
                DatabaseContext.Entities.TimetableRecords.Local.Add(new()
                {
                    Timetable = Timetable,
                    Couple = Couple,
                    DayOfWeekId = (int)dayOfWeek
                });
            else
            {
                var records = DatabaseContext.Entities.TimetableRecords.Local.Where(r => r.DayOfWeekId == (int)dayOfWeek && 
                                                                                         r.Couple == Couple &&
                                                                                         r.Timetable == Timetable);
                if (records == null)
                    return;

                foreach (var record in records)
                    DatabaseContext.Entities.TimetableRecords.Local.Remove(record);
            }
        }

        public static ICollection<TimetableRecordElement> GetRecordElements(Timetable timetable)
        {
            DatabaseContext.Entities.DayOfWeeks.Load();
            DatabaseContext.Entities.TimetableRecords.Load();

            var elements = new List<TimetableRecordElement>();

            for (int couple = 1; couple <= 8; couple++)
            {
                var timetableRecords = timetable.TimetableRecords.Where(record => record.Couple == couple);

                TimetableRecordElement item = new()
                {
                    Timetable = timetable,
                    Couple = couple,

                    isMondayChecked = timetableRecords.Any(record => record.DayOfWeekId == (int)System.DayOfWeek.Monday),
                    isTuesdayChecked = timetableRecords.Any(record => record.DayOfWeekId == (int)System.DayOfWeek.Tuesday),
                    isWednesdayChecked = timetableRecords.Any(record => record.DayOfWeekId == (int)System.DayOfWeek.Wednesday),
                    isThursdayChecked = timetableRecords.Any(record => record.DayOfWeekId == (int)System.DayOfWeek.Thursday),
                    isFridayChecked = timetableRecords.Any(record => record.DayOfWeekId == (int)System.DayOfWeek.Friday),
                    isSaturdayChecked = timetableRecords.Any(record => record.DayOfWeekId == (int)System.DayOfWeek.Saturday),
                    isSundayChecked = timetableRecords.Any(record => record.DayOfWeekId == (int)System.DayOfWeek.Sunday)
                };

                elements.Add(item);
            }

            return elements;
        }
    }
}
