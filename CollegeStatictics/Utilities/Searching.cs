using System;

namespace CollegeStatictics.Utilities
{
    public class Searching<T>
    {
        private readonly Func<T, string> propertyGetter;

        public Searching(Func<T, string> propertyGetter) =>
            this.propertyGetter = propertyGetter;

        public bool IsAccepted(T item, string textToSearch) => propertyGetter(item)!.Trim().ToLower().Contains(textToSearch.Trim().ToLower()) != false;
    }
}
