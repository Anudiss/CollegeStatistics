namespace CollegeStatictics.Database
{
    public partial class DatabaseContext
    {
        private static DatabaseContext _entities;
        public static DatabaseContext Entities => _entities ??= new();
    }
}
