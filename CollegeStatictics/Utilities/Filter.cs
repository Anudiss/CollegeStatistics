using CollegeStatictics.Database;
using CollegeStatictics.DataTypes;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace CollegeStatictics.Utilities
{
    public partial class Filter<T, R> : ObservableObject, IFilter<T> where T : class, ITable 
                                                                     where R : class
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
        public IEnumerable PossibleValues => LoadPossibleValues();
        public IEnumerable MenuItems => LoadMenuItems();

        public Func<T, R> PropertyGetter = default!;
        public event Action SelectedValuesChanged = default!;

        // new Builder(SubjectCOllection).AddFilter(new Filter<Subject, string>("Название", subject => subject.Name)).Build();

        public Filter(string name, Func<T, R> propertyGetter)
        {
            Name = name;
            PropertyGetter = propertyGetter;

            SelectedValues = new();
        }

        private IEnumerable<R> LoadPossibleValues()
        {
            DatabaseContext.Entities.Set<R>().Load();

            var values = DatabaseContext.Entities.Set<T>();
            values.Load();

            var possibleValues = values.Local.Select(entity => PropertyGetter(entity)).Distinct();

            foreach (var selectedValue in SelectedValues)
                if (!possibleValues.Contains(selectedValue))
                    Remove(selectedValue);

            return possibleValues;
        }

        private IEnumerable LoadMenuItems()
            => LoadPossibleValues().Where(v => v != null).Select(v =>
            {
                MenuItem menuItem = new()
                {
                    Header = v!.ToString(),
                    IsChecked = SelectedValues.Contains(v)
                };

                menuItem.Checked += (_, _) => Add(v!);
                menuItem.Unchecked += (_, _) => Remove(v!);

                return menuItem;
            });

        public bool IsAccepted(T item) =>
            !SelectedValues.Any() || SelectedValues.Contains(PropertyGetter(item));

        public void Add(object value)
        {
            if (value is R r && !SelectedValues.Contains(r))
            {
                SelectedValues.Add(r);
                SelectedValuesChanged?.Invoke();
            }
        }

        public void Remove(object value)
        {
            if (value is R r)
                SelectedValues.Remove(r);
            SelectedValuesChanged?.Invoke();
        }

        public void Refresh()
        {
            OnPropertyChanged(nameof(MenuItems));


        }
    }

    public interface IFilter<T> : ISelection<T>
    {
        IEnumerable PossibleValues { get; }

        public event Action SelectedValuesChanged;

        public void Add(object value);
        public void Remove(object value);

        public void Refresh();
    }

    public class Selection<T> : ISelection<T>
    {
        private Predicate<T> _selectionPredicate;

        public Selection(Predicate<T> selectionPredicate) =>
            _selectionPredicate = selectionPredicate;

        public bool IsAccepted(T item) => _selectionPredicate?.Invoke(item) != false;
    }

    public interface ISelection<T>
    {
        public bool IsAccepted(T item);
    }
}
