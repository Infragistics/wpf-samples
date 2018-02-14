using System.Collections.Generic;

namespace IgOutlook.Infrastructure
{
    public interface IViewBase
    {
        IViewModel ViewModel { get; set; }
        IList<IRibbonTabItem> RibbonTabs { get; }
    }
}
