using IgWord.Business;
using IgWord.Infrastructure;
using IgWord.Infrastructure.Dialogs;
using IgWord.Services;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;

namespace IgWord.Views
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
            set { recentFolders = value; NotifyPropertyChanged(); }
        }

        public DelegateCommand<string> SaveFileCommand { get; private set; }

        public SaveFileViewModel(IEventAggregator eventAggragator, IRecentFilesService recentFilesService, IDialogService dialogService)
        {
            this.eventAggragator = eventAggragator;
            this.dialogService = dialogService;
            this.recentFilesService = recentFilesService;

            this.recentFilesService.SetRepositories(Properties.Settings.Default, p => Properties.Settings.Default.RecentFilesRepo, p => Properties.Settings.Default.RecentFoldersRepo);

            this.RecentFolders = this.recentFilesService.RecentFolders;

            this.recentFilesService.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "RecentFolders")
                {
                    this.RecentFolders = this.recentFilesService.RecentFolders;
                }
            };

            SaveFileCommand = new DelegateCommand<string>(SaveFile);
        }

        private void SaveFile(string path)
        {
            var fileName = string.Empty;
            var filters = ShellParameters.FileDialogWordFilter;

            if (dialogService.ShowSaveFileDialog(path, out fileName, filters) == InteractionResult.Ok)
            {
                this.eventAggragator.GetEvent<FileSavedEvent>().Publish(fileName);
            }
        }
    }
}
