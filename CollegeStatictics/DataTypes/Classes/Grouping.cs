using CollegeStatictics.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace CollegeStatictics.DataTypes.Classes
{
    public class Grouping<T>
    {
        public string PropertyPath { get; }

        public Grouping(string propertyPath)
        {
            DatabaseContext.LoadEntities(typeof(T));

            PropertyPath = propertyPath;
        }
    }
}
