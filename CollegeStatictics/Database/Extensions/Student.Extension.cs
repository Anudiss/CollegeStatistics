using CollegeStatictics.DataTypes;

namespace CollegeStatictics.Database.Models;

public partial class Student : ITable
{
    public override string ToString() => $"{Surname} {Name} {Patronymic}";
}