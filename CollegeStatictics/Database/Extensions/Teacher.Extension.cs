using CollegeStatictics.DataTypes;

namespace CollegeStatictics.Database.Models
{
    public partial class Teacher : ITable
    {
        public override string ToString() => $"{Surname} {Name} {Patronymic}";
    }
}
