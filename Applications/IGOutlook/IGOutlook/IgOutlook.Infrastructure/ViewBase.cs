using IgOutlook.Infrastructure.Prism;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace IgOutlook.Infrastructure
{
    public class ViewBase : UserControl, IViewBase, INavigationAware, IRegionManagerAware
    {
        public IRegionManager RegionManager { get; set; }
        public IEventAggregator EventAggregator { get; private set; }
        public IList<IRibbonTabItem> RibbonTabs { get; private set; }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }

        public ViewBase()
        {
            RibbonTabs = new List<IRibbonTabItem>();
        }

        public ViewBase(IViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public ViewBase(IRegionManager regionManager)
            : this()
        {
            RegionManager = regionManager;
        }

        public ViewBase(IViewModel viewModel, IRegionManager regionManager)
            : this(viewModel)
        {
            RegionManager = regionManager;
        }

        public ViewBase(IViewModel viewModel, IEventAggregator eventAggregator)
            : this(viewModel)
        {
            EventAggregator = eventAggregator;
        }

        public ViewBase(IViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator)
            : this(viewModel, regionManager)
        {
            EventAggregator = eventAggregator;
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if (RegionManager != null)
            {
                IRegion tabRegion = RegionManager.Regions[RegionNames.RibbonTabRegion];
                RibbonTabs.ToList().ForEach(tabRegion.Remove);
            }
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (RegionManager != null)
            {
                IRegion tabRegion = RegionManager.Regions[RegionNames.RibbonTabRegion];
                RibbonTabs.ToList().ForEach(tab => tabRegion.Add(tab));
            }
        }
    }
}
