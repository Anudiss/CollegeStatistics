namespace CollegeStatictics.DataTypes.Interfaces
{
    public interface IDeletable
    {
        public bool IsDeleted { get; set; }

        public void MarkToDelete()
        {
            IsDeleted = true;
        }

        public void Delete();
    }
}
