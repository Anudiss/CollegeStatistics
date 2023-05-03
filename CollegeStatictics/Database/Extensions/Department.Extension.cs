using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Interfaces;

namespace CollegeStatictics.Database.Models
{
    public partial class Department : ITable, IDeletable
    {
        public override string ToString() => Name;
    }
}
