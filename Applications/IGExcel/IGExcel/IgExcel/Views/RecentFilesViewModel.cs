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
    public class RecentFilesViewModel : ViewModelBase
    {
        private IEventAggregator eventAggragator;
        private IMessageBoxService messageBoxService;
        private IRecentFilesService recentFilesService;

        private ObservableCollection<SystemInfoCustom> recentDocuments;
        public ObservableCollection<SystemInfoCustom> RecentDocuments
        {
            get { return recentDocuments; }
            set { SetProperty<ObservableCollection<SystemInfoCustom>>(ref recentDocuments, value); }
        }

        public DelegateCommand<string> OpenFileCommand { get; private set; }


        public RecentFilesViewModel(IEventAggregator eventAggragator, IRecentFilesService recentFilesService, IMessageBoxService messageBoxService)
        {
            this.eventAggragator = eventAggragator;
            this.messageBoxService = messageBoxService;
            this.recentFilesService = recentFilesService;

            this.recentFilesService.SetRepositories(Properties.Settings.Default, p => Properties.Settings.Default.RecentFilesRepo, p => Properties.Settings.Default.RecentFoldersRepo);

            this.recentFilesService.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "RecentFiles")
                {
                    this.RecentDocuments = this.recentFilesService.RecentFiles;
                }
            };

            this.RecentDocuments = this.recentFilesService.RecentFiles;

            OpenFileCommand = new DelegateCommand<string>(OpenFile);

            RecentDocuments = recentFilesService.RecentFiles;
        }



        private void OpenFile(string path)
        {
            if (File.Exists(path))
            {
                eventAggragator.GetEvent<FileOpenedEvent>().Publish(path);
            }
            else
            {
                var interactionResult = this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_Error, ResourceStrings.ResourceStrings.Msg_FileDoesNotExists, MessageBoxButtons.YesNoCancel);

                if (interactionResult == InteractionResult.Yes)
                {
                    recentFilesService.RemoveFile(path, true);
                }
            }
        }
    }
}
