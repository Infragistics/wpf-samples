using Prism.Events;
using Prism.Regions;
using System.Windows.Controls;

namespace IgExcel.Infrastructure
{
    public class ViewBase : UserControl, IViewBase, INavigationAware, IRegionManagerAware
    {
        public IRegionManager RegionManager { get; set; }
        public IEventAggregator EventAggregator { get; private set; }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }

        public ViewBase()
        {
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
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
