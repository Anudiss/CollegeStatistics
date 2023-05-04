using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace CollegeStatictics.DataTypes.Classes
{
    public partial class FilteredObservableCollection<T> : ObservableObject
    {
        private string searchingText = "";

        public string SearchingText
        {
            get => searchingText;
            set
            {
                SetProperty(ref searchingText, value);
                Refresh();
            }
        }

        public IEnumerable<ISelection<T>> Selections { get; }
        public IEnumerable<IFilter<T>> Filters => Selections.Where(selection => selection is IFilter<T>).Cast<IFilter<T>>();
        public IEnumerable<Searching<T>> Searchings { get; }
        public IEnumerable<Grouping<T>> Groupings { get; }

        public ICollectionView View { get; }

        public FilteredObservableCollection(IList<T> sourceCollection, IEnumerable<ISelection<T>> selections, IEnumerable<Searching<T>> searchings, IEnumerable<Grouping<T>> groupings)
        {
            Selections = selections;
            Searchings = searchings;
            Groupings = groupings;

            View = CollectionViewSource.GetDefaultView(sourceCollection);

            foreach (var filter in Filters)
                filter.SelectedValuesChanged += Refresh;

            View.Filter = IsAccepted;

            View.GroupDescriptions.Clear();
            groupings.ToList().ForEach(grouping => View.GroupDescriptions.Add(new PropertyGroupDescription(grouping.PropertyPath)));

            Refresh();
        }

        public bool IsAccepted(object item) => item is T t && (!Selections.Any() || Selections.All(filter => filter.IsAccepted(t))) &&
                                                              (!Searchings.Any() || Searchings.Any(searching => searching.IsAccepted(t, SearchingText)));

        public void UpdateFilters()
        {
            foreach (var filter in Filters)
                filter.Refresh();
        }

        public void Refresh()
        {
            View.Refresh();
            OnPropertyChanged(nameof(View));
        }
    }

    public class FilteredObservableCollectionBuilder<T>
    {
        private readonly IList<T> _sourceCollection;
        private readonly List<ISelection<T>> _filters;
        private readonly List<Searching<T>> _searchings;
        private readonly List<Grouping<T>> _groupings;

        public FilteredObservableCollectionBuilder(IList<T> sourceCollection)
        {
            _sourceCollection = sourceCollection;
            _filters = new();
            _searchings = new();
            _groupings = new();
        }

        public FilteredObservableCollectionBuilder<T> AddFilter(ISelection<T> filter)
        {
            _filters.Add(filter);
            return this;
        }

        public FilteredObservableCollectionBuilder<T> AddSearching(Searching<T> searching)
        {
            _searchings.Add(searching);
            return this;
        }

        public FilteredObservableCollectionBuilder<T> AddGrouping(Grouping<T> grouping)
        {
            _groupings.Add(grouping);
            return this;
        }

        public FilteredObservableCollection<T> Build() => new(_sourceCollection, _filters, _searchings, _groupings);
    }
}
