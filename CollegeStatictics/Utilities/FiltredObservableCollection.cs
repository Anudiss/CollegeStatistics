using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace CollegeStatictics.Utilities
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

        public IEnumerable<IFilter<T>> Filters { get; }
        public IEnumerable<Searching<T>> Searchings { get; }

        public ICollectionView View { get; }

        public FilteredObservableCollection(IList<T> sourceCollection, IEnumerable<IFilter<T>> filters, IEnumerable<Searching<T>> searchings)
        {
            Filters = filters;
            Searchings = searchings;

            View = CollectionViewSource.GetDefaultView(sourceCollection);

            View.Filter = IsAccepted;
        }

        public bool IsAccepted(object item) => item is T t && (!Filters.Any() || Filters.All(filter => filter.IsAccepted(t))) &&
                                                              (!Searchings.Any() || Searchings.Any(searching => searching.IsAccepted(t, SearchingText)));

        public void Refresh()
        {
            View.Refresh();
            OnPropertyChanged(nameof(View));
        }
    }

    public class FilteredObservableCollectionBuilder<T>
    {
        private readonly IList<T> _sourceCollection;
        private readonly List<IFilter<T>> _filters;
        private readonly List<Searching<T>> _searchings;

        public FilteredObservableCollectionBuilder(IList<T> sourceCollection)
        {
            _sourceCollection = sourceCollection;
            _filters = new();
            _searchings = new();
        }

        public FilteredObservableCollectionBuilder<T> AddFilter(IFilter<T> filter)
        {
            _filters.Add(filter);
            return this;
        }

        public FilteredObservableCollectionBuilder<T> AddSearching(Searching<T> searching)
        {
            _searchings.Add(searching);
            return this;
        }

        public FilteredObservableCollection<T> Build() => new(_sourceCollection, _filters, _searchings);
    }
}
