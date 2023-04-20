using CommunityToolkit.Mvvm.Input;
using ModernWpf.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CollegeStatictics.DataTypes
{
    public interface IPage<T> where T : class
    {
        IEnumerable<T> SelectedItems { get; set; }

        [RelayCommand]
        void SetSelectedItems(IList items)
        {
            if (items.Count == 0)
                SelectedItems = Enumerable.Empty<T>();

            if (items[0] is not T)
                throw new NotSupportedException($"Items in list should be of type {typeof(T).Name}.");

            SelectedItems = items.Cast<T>();
        }
    }
}
