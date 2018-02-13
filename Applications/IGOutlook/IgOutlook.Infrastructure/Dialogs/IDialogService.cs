using Prism.Regions;
using System.Windows;

namespace IgOutlook.Infrastructure.Dialogs
{
    public interface IDialogService
    {
        FrameworkElement GetDialogInstance();

        void ShowRibbonDialog(string navigationUri, NavigationParameters parameters = null, bool isModal = true);
    }
}
