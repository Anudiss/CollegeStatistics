namespace CollegeStatictics.Database
{
    public partial class DatabaseContext
    {
        private static DatabaseContext _entities = null!;
        public static DatabaseContext Entities => _entities ??= new();
    }
}
