using Microsoft.Win32;
using Prism.Regions;
using System.IO;

namespace IgWord.Infrastructure.Dialogs
{
    public class DialogService : IDialogService
    {
        IRegionManager regionManager;
         
        public DialogService(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void ShowIgDialog(string navigationUri, NavigationParameters parameters = null, bool isModal = true)
        {
            IgDialog dialog = new IgDialog();

            var newRegionManager = regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(dialog, newRegionManager);

            if (parameters != null)
                newRegionManager.RequestNavigate(RegionNames.ContentRegion, navigationUri, parameters);
            else
                newRegionManager.RequestNavigate(RegionNames.ContentRegion, navigationUri);

            dialog.Owner = System.Windows.Application.Current.MainWindow;
            dialog.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;

            dialog.ShowInTaskbar = true;
            dialog.ShowActivated = true;

            dialog.Show(isModal);
        }

        public InteractionResult ShowOpenFileDialog(string initialDirectory, out string fileName, string filters = "")
        {
            fileName = string.Empty;

            var dlg = new OpenFileDialog { Multiselect = false, Filter = filters };

            if (!string.IsNullOrEmpty(initialDirectory) && Directory.Exists(initialDirectory))
                dlg.InitialDirectory = initialDirectory;

            if (dlg.ShowDialog() == true)
            {
                fileName = dlg.FileName;
                return InteractionResult.Ok;
            }
            else
            {
                return InteractionResult.Cancel;
            }
        }

        public InteractionResult ShowSaveFileDialog(string initialDirectory, out string fileName, string filters = "")
        {
            fileName = string.Empty;

            var dlg = new SaveFileDialog { Filter = filters };

            if (!string.IsNullOrEmpty(initialDirectory) && Directory.Exists(initialDirectory))
                dlg.InitialDirectory = initialDirectory;

            if (dlg.ShowDialog() == true)
            {
                fileName = dlg.FileName;
                return InteractionResult.Ok;
            }
            else
            {
                return InteractionResult.Cancel;
            }
        }
    }
}
