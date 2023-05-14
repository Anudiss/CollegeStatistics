using CollegeStatictics.DataTypes;

namespace CollegeStatictics.Database.Models
{
    public partial class StudyPlanRecord : ITable
    {
        public override string ToString() => Topic;
    }
}
