using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Interfaces;

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
    }
}
