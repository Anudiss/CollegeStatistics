using CollegeStatictics.Database;
using CollegeStatictics.Database.Models;
using CollegeStatictics.DataTypes;
using CollegeStatictics.Utilities;
using CollegeStatictics.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CollegeStatictics.ViewModels
{
    public partial class DepartmentsVM
    {
        public FilteredObservableCollection<Department> Items { get; set; }
        public IEnumerable<Department> SelectedItems { get; set; }

        public DepartmentsVM()
        {
            RefreshSubjects();
        }
        
        private void RefreshSubjects()
        {
            var values = DatabaseContext.Entities.Set<Department>();
            values.Load();

            Items = new FilteredObservableCollectionBuilder<Department>(values.Local.ToObservableCollection())
                          .AddSearching(new Searching<Department>(department => department.Name))
                          .AddSearching(new Searching<Department>(department => $"{department.Id}"))
                          .Build();
        }
    }
}
