using CollegeStatictics.DataTypes;

namespace CollegeStatictics.Database.Models
{
    public partial class Department : ITable
    {
        public override string ToString() => Name;
    }
}
