using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Interfaces;

namespace CollegeStatictics.Database.Models;

public partial class Speciality : ITable, IDeletable
{
    public override string ToString() => Name;
}
