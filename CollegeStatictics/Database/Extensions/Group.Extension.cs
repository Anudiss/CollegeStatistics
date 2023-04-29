using CollegeStatictics.DataTypes;
using System.Linq;

namespace CollegeStatictics.Database.Models
{
    public partial class Group : ITable
    {
        public override string ToString() => $"{Id}";
    }
}
