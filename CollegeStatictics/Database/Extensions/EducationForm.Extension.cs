using CollegeStatictics.DataTypes;

namespace CollegeStatictics.Database.Models
{
    public partial class EducationForm : ITable
    {
        public override string ToString() => Name;
    }
}
