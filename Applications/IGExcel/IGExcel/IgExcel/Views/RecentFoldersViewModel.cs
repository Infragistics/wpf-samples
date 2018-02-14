using IgExcel.Business;
using IgExcel.Infrastructure;
using IgExcel.Infrastructure.Dialogs;
using IgExcel.Services;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.IO;

namespace IgExcel.Views
{
    public class RecentFoldersViewModel : ViewModelBase
    {
        private IEventAggregator eventAggragator;
        private IDialogService dialogService;
        private IRecentFilesService recentFilesService;

        private ObservableCollection<SystemInfoCustom> recentFolders;
        public ObservableCollection<SystemInfoCustom> RecentFolders
        {
            get { return recentFolders; }
            set { SetProperty<ObservableCollection<SystemInfoCustom>>(ref recentFolders, value); }
        }

        public DelegateCommand<string> OpenFileCommand { get; private set; }


        public RecentFoldersViewModel(IEventAggregator eventAggragator, IRecentFilesService recentFilesService, IDialogService dialogService)
        {
            this.eventAggragator = eventAggragator;
            this.dialogService = dialogService;
            this.recentFilesService = recentFilesService;

            this.recentFilesService.SetRepositories(Properties.Settings.Default, p => Properties.Settings.Default.RecentFilesRepo, p => Properties.Settings.Default.RecentFoldersRepo);

            this.recentFilesService.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "RecentFolders")
                {
                    this.RecentFolders = this.recentFilesService.RecentFolders;
                }
            };

            this.RecentFolders = this.recentFilesService.RecentFolders;

            OpenFileCommand = new DelegateCommand<string>(OpenFile);
        }

        private void OpenFile(string path)
        {
            string selectedFilePath;
            var filters = ShellParameters.FileDialogFilter;

            var result = dialogService.ShowOpenFileDialog(path, out selectedFilePath, filters);

            if (result == InteractionResult.Ok)
            {
                this.recentFilesService.AddFile(selectedFilePath, true);
                this.recentFilesService.AddFolder(Path.GetDirectoryName(selectedFilePath), true);

                eventAggragator.GetEvent<FileOpenedEvent>().Publish(selectedFilePath);
            }
        }
    }
}
