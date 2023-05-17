using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CollegeStatictics.DataTypes.Classes
{
    public class Searching<T>
    {
        private readonly Func<T, string> propertyGetter;

        public Searching(Func<T, string> propertyGetter) =>
            this.propertyGetter = propertyGetter;


        public bool IsAccepted(T item, string textToSearch) =>
            Regex.Split(textToSearch.Trim().ToLower(), @"\s+").All(text => propertyGetter(item)!.Trim().ToLower().Contains(text) != false);
    }
}
