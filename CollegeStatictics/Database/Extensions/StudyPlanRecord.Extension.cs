using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Interfaces;

using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CollegeStatictics.Database.Models
{
    public partial class StudyPlanRecord : ITable, IDeletable
    {
        [NotMapped]
        public bool IsDeleted { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override string ToString() => Topic;
    }
}
