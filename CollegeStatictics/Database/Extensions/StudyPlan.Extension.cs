using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Interfaces;

using System.Linq;

namespace CollegeStatictics.Database.Models
{
    public partial class StudyPlan : ITable, IDeletable
    {
        public override string ToString() => $"Учебный план №{Id} от {StartDate:dd.MM.yyyy}";

        public void RemoveLinkedData()
        {
            foreach (var record in StudyPlanRecords)
                DatabaseContext.Entities.StudyPlanRecords.Remove(record);
        }

        public bool this[Teacher? teacher, Group? group] => Timetables.Where(t => teacher == null || t.Teacher == teacher)
                                                                      .Where(t => group == null || t.Group == group)
                                                                      .Any();
    }
}
