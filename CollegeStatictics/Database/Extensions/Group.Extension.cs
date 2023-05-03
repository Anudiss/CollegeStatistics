using CollegeStatictics.DataTypes;
using CollegeStatictics.DataTypes.Interfaces;
using System.Linq;

namespace CollegeStatictics.Database.Models
{
    public partial class Group : ITable, IDeletable
    {
        public override string ToString() => $"{Number}";
    }
}
