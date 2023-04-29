﻿using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CollegeStatictics.Utilities
{
    public partial class Filter<T, R> : ObservableObject, IFilter<T> where T : class, ITable where R : class, ITable
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

        public Func<T, R> PropertyGetter;
        public event Action SelectedValuesChanged;

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

            return values.Local.Select(entity => PropertyGetter(entity)).Distinct();
        }

        private IEnumerable LoadMenuItems()
            => LoadPossibleValues().Where(v => v != null).Select(v =>
            {
                MenuItem menuItem = new() { Header = v!.ToString() };

                menuItem.Checked += (_, _) => Add(v!);
                menuItem.Unchecked += (_, _) => Remove(v!);

                return menuItem;
            });

        public bool IsAccepted(T item) =>
            !SelectedValues.Any() || SelectedValues.Contains(PropertyGetter(item));

        public void Add(object value)
        {
            if (value is R r)
                SelectedValues.Add(r);
            SelectedValuesChanged?.Invoke();
        }

        public void Remove(object value)
        {
            if (value is R r)
                SelectedValues.Remove(r);
            SelectedValuesChanged?.Invoke();
        }
    }

    public interface IFilter<T>
    {
        public string Name { get; set; }
        IEnumerable PossibleValues { get; }

        public event Action SelectedValuesChanged;

        public bool IsAccepted(T item);
        public void Add(object value);
        public void Remove(object value);
    }
}
