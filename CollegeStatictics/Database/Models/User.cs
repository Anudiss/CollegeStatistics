﻿using CollegeStatictics.DataTypes;

namespace CollegeStatictics.Database.Models
{
    public class User : ITable
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
