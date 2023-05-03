using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Interfaces;

namespace CollegeStatictics.Database.Models;

public partial class Student : ITable, IDeletable
{
    public override string ToString() => $"{Surname} {Name} {Patronymic}";
}