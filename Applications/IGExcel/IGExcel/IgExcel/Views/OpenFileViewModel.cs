using IgExcel.Business;
using IgExcel.Infrastructure;
using IgExcel.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace IgExcel.Views
{
    public class OpenFileViewModel : ViewModelBase
    {
        private IEventAggregator eventAggragator;
        private IRegionManager regionManager;

        private ObservableCollection<SystemInfoCustom> recentFolders;
        public ObservableCollection<SystemInfoCustom> RecentFolders
        {
            get { return recentFolders; }
            set { SetProperty<ObservableCollection<SystemInfoCustom>>(ref recentFolders, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }


        public OpenFileViewModel(IEventAggregator eventAggragator, IRecentFilesService recentFilesService, IRegionManager regionManager)
        {
            RecentFolders = recentFilesService.RecentFolders;

            NavigateCommand = new DelegateCommand<string>(Navigate);

            this.eventAggragator = eventAggragator;
            this.regionManager = regionManager;
        }

        private void Navigate(string navigationPath)
        {
            if (string.IsNullOrWhiteSpace(navigationPath))
                return;

            if (!String.IsNullOrWhiteSpace(navigationPath))
                this.regionManager.RequestNavigate("ContentRegion", navigationPath);
        }
    }
}
