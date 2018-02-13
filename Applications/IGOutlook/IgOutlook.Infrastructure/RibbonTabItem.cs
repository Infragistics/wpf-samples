using Infragistics.Controls.Menus;

namespace IgOutlook.Infrastructure
{
    public class RibbonTabItem : Infragistics.Windows.Ribbon.RibbonTabItem, IRibbonTabItem
    {
        public RibbonTabItem()
        {
            this.SetResourceReference(StyleProperty, typeof(Infragistics.Windows.Ribbon.RibbonTabItem));
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
