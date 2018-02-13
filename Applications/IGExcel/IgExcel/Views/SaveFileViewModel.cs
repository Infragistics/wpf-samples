using IgExcel.Business;
using IgExcel.Infrastructure;
using IgExcel.Infrastructure.Dialogs;
using IgExcel.Services;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;

namespace IgExcel.Views
{
    public class SaveFileViewModel : ViewModelBase
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

        public DelegateCommand<string> SaveFileCommand { get; private set; }

        public SaveFileViewModel(IEventAggregator eventAggragator, IRecentFilesService recentFilesService, IDialogService dialogService)
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

            SaveFileCommand = new DelegateCommand<string>(SaveFile);
        }

        private void SaveFile(string path)
        {
            var fileName = string.Empty;
            var filters = ShellParameters.FileDialogFilter;

            if (dialogService.ShowSaveFileDialog(path, out fileName, filters) == InteractionResult.Ok)
            {
                this.eventAggragator.GetEvent<FileSavedEvent>().Publish(fileName);
            }
        }
    }
}
