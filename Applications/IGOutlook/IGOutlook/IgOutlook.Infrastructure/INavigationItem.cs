
namespace IgOutlook.Infrastructure
{
    public interface INavigationItem
    {
        string NavigationPath { get; set; }
        bool CanNavigate { get; set; }
        object DataItem { get; set; }
    }
}
