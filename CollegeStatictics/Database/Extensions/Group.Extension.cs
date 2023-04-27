using CollegeStatictics.DataTypes;
using System.Linq;

namespace CollegeStatictics.Database.Models
{
    public partial class Group : ITable
    {
        public Student? GroupLeader => Students.FirstOrDefault(student => student.GroupLeader != null);

        public override string ToString() => $"{Id}";
    }
}
