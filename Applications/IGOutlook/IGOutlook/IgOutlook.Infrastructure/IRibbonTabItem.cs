
namespace IgOutlook.Infrastructure
{
    public interface IRibbonTabItem
    {
        IViewModel ViewModel { get; set; }
        bool IsSelected { get; set; }
    }
}
