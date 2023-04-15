using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CollegeStatictics.Utilities
{
    public partial class Filter<T, R> : ObservableObject, IFilter<T>
    {
        // Properties:
        //  + Possible values collection
        //  + Selected values collection

        // Methods:
        //  + IsAccespted (T value)
        //  + Add value to selected items collection
        //  + Remove value from selected items collection

        public string Name { get; set; }
        public ObservableCollection<R> SelectedValues { get; }

        public Func<T, R> PropertyGetter;

        // new Builder(SubjectCOllection).AddFilter(new Filter<Subject, string>("Название", subject => subject.Name)).Build();

        public Filter(string name, Func<T, R> propertyGetter)
        {
            Name = name;
            PropertyGetter = propertyGetter;

            SelectedValues = new();
        }

        public bool IsAccepted(T item) =>
            !SelectedValues.Any() || SelectedValues.Contains(PropertyGetter(item));

        public void Add(object value)
        {
            if (value is R r)
                SelectedValues.Add(r);
        }

        public void Remove(object value)
        {
            if (value is R r)
                SelectedValues.Add(r);
        }
    }

    public interface IFilter<T>
    {
        public string Name { get; set; }

        public bool IsAccepted(T item);
        public void Add(object value);
        public void Remove(object value);
    }
}
