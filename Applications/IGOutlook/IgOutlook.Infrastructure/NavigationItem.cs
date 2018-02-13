using System.Collections.ObjectModel;

namespace IgOutlook.Infrastructure
{
    public class NavigationItem: INavigationItem
    {
        public string Caption { get; set; }
        public bool CanNavigate { get; set; }
        public bool IsExpanded { get; set; }
        public string NavigationPath { get; set; }
        public object DataItem { get; set; }
        public ObservableCollection<NavigationItem> Items { get; set; }

        public NavigationItem()
        {
            IsExpanded = true;
            CanNavigate = true;
            Items = new ObservableCollection<NavigationItem>();
        }
    }
}
