using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Interfaces;

namespace CollegeStatictics.Database.Models
{
    public partial class StudyPlan : ITable, IDeletable
    {
        public void RemoveLinkedData()
        {
            foreach (var record in StudyPlanRecords)
                DatabaseContext.Entities.StudyPlanRecords.Remove(record);
        }
    }
}
