using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Infragistics.Samples.Shared.Extensions
{
    public static class CollectionExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            var collection = new ObservableCollection<T>();
            foreach (var e in list)
                collection.Add(e);
            return collection;
        }
    }
}

namespace System.Windows.Data
{
    public static class CollectionViewSourceEx
    {
        public static CollectionViewSource SortBy(this CollectionViewSource viewSource,
           string propertyName, ListSortDirection direction = ListSortDirection.Descending)
        {
            viewSource.SortDescriptions.Add(new SortDescription(propertyName, direction));
            return viewSource;
        }
    }
}
