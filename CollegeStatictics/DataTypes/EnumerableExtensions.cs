using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        if (enumerable == null)
            throw new ArgumentNullException(nameof(enumerable), "Should not be null.");
        else if (action == null)
            throw new ArgumentNullException(nameof(action), "Should not be null.");

        foreach (var item in enumerable)
            action.Invoke(item);
    }
}