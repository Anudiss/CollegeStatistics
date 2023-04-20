namespace CollegeStatictics.Database.Models
{
    public partial class Teacher : User
    {
        public string SurnameAndInitials => $"{Surname} {Name?[1]} {Patronymic?[1]}";
    }
}
