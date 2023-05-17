using System.Collections.Generic;
using System.Linq;

namespace CollegeStatictics.Database.Models;

public partial class HomeworkStudent
{
    public static IEnumerable<HomeworkExecutionStatus> ExecutionStatuses
        => DatabaseContext.Entities.HomeworkExecutionStatuses.Local.ToList();
}
