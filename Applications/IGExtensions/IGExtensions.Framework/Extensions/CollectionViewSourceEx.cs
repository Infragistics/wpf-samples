using System.ComponentModel;
 

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