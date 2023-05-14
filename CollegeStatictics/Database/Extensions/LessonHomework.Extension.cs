using CollegeStatictics.DataTypes;

using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeStatictics.Database.Models;

public partial class LessonHomework : ITable
{
    [NotMapped]
    public int Id { get; set; }
}
