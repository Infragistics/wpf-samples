using IgWord.Business;
using IgWord.Infrastructure;
using IgWord.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace IgWord.Views
{
    public class OpenFileViewModel : ViewModelBase
    {
        private IEventAggregator eventAggragator;
        private IRegionManager regionManager;
        private IRecentFilesService recentFilesService;

        private ObservableCollection<SystemInfoCustom> recentFolders;

        public ObservableCollection<SystemInfoCustom> RecentFolders
        {
            get { return recentFolders; }
            set { recentFolders = value; NotifyPropertyChanged(); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }


        public OpenFileViewModel(IEventAggregator eventAggragator, IRecentFilesService recentFilesService, IRegionManager regionManager)
        {
            this.eventAggragator = eventAggragator;
            this.regionManager = regionManager;
            this.recentFilesService = recentFilesService;

            RecentFolders = recentFilesService.RecentFolders;

            NavigateCommand = new DelegateCommand<string>(Navigate);

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
