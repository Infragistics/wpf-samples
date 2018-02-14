using IgOutlook.Infrastructure;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;

namespace IgOutlook
{
    public class ShellViewModel : ViewModelBase
    {
        private int itemsCount;
        public int ItemsCount
        {
            get { return itemsCount; }
            set { itemsCount = value; OnPropertyChanged(); }
        }

        string _applicationTitle = "IG Outlook";
        IRegionManager _regionManager;
        public DelegateCommand<String> NavigateCommand { get; set; }
        public DelegateCommand ExitCommand { get; private set; }

        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            Title = _applicationTitle;
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<String>(Navigate);
            ExitCommand = new DelegateCommand(Exit);
            Commands.NavigateCommand.RegisterCommand(NavigateCommand);

            eventAggregator.GetEvent<ViewActivateEvent>().Subscribe(ViewActivated);
            eventAggregator.GetEvent<ViewItemsCountChangedEvent>().Subscribe(ViewItemsCountChanged);
        }

        private void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Navigate(string navigationPath)
        {
            if (string.IsNullOrWhiteSpace(navigationPath))
                return;

            if (!String.IsNullOrWhiteSpace(navigationPath))
                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);
        }

        private void ViewActivated(string viewName)
        {
            Title = String.Format("{0} - {1}", viewName, _applicationTitle);
        }

        private void ViewItemsCountChanged(int count)
        {
            ItemsCount = count;
        }
    }
}
