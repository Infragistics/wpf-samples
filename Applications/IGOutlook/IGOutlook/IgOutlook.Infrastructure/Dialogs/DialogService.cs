using Prism.Regions;
using System.Windows;

namespace IgOutlook.Infrastructure.Dialogs
{
    public class DialogService : IDialogService
    {
        IRegionManager _regionManager;
        FrameworkElement _dialogInstance;

        public DialogService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void ShowRibbonDialog(string navigationUri, NavigationParameters parameters = null, bool isModal = true)
        {
            RibbonDialog dialog = new RibbonDialog();
            _dialogInstance = dialog;

            var newRegionManager = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(dialog, newRegionManager);

            if (parameters != null)
                newRegionManager.RequestNavigate(RegionNames.ContentRegion, navigationUri, parameters);
            else
                newRegionManager.RequestNavigate(RegionNames.ContentRegion, navigationUri);

            dialog.Owner = System.Windows.Application.Current.MainWindow;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            dialog.ShowInTaskbar = true;
            dialog.ShowActivated = true;            
            dialog.Show(isModal);
        }

        public FrameworkElement GetDialogInstance()
        {
            return _dialogInstance;
        }
    }
}
