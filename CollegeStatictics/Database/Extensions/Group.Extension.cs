using System.Linq;

namespace CollegeStatictics.Database.Models
{
    public partial class Group
    {
        public Student? GroupLeader => Students.FirstOrDefault(student => student.GroupLeader != null);
    }
}
