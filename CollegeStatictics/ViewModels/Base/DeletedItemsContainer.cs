using CollegeStatictics.Database;
using CollegeStatictics.DataTypes.Classes;
using CollegeStatictics.DataTypes.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Linq;

namespace CollegeStatictics.ViewModels.Base
{
    public partial class DeletedItemsContainer : ObservableObject
    {
        [RelayCommand]
        private void UnmarkDeletedItem(IList items)
        {
            foreach (IDeletable item in items)
                item.UnmarkToDelete();
        }

        public FilteredObservableCollection<IDeletable> DeletedItems { get; set; }

        public DeletedItemsContainer()
        {
            DeletedItems = new FilteredObservableCollectionBuilder<IDeletable>(DatabaseContext.Entities.Departments.Local.Cast<IDeletable>().ToList())
                            .AddFilter(new Selection<IDeletable>(d => d.IsDeleted))
                            .Build();
        }
    }
}
