namespace CollegeStatictics.Database.Models
{
    public partial class Teacher : User
    {
        public override string ToString() => $"{Surname} {Name} {Patronymic}";
    }
}
