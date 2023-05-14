using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeStatictics.Database.Models;

public partial class Lesson : ITable, IDeletable
{
    [NotMapped]
    public bool IsConducted => IsDeleted == false && EmergencySituation == null;
}
