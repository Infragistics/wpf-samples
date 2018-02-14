using Prism.Regions;

namespace IgWord.Infrastructure.Dialogs
{
    public interface IDialogService
    {
        void ShowIgDialog(string navigationUri, NavigationParameters parameters = null, bool isModal = true);
        InteractionResult ShowOpenFileDialog(string initialDirectory, out string fileName, string filetrs = "");
        InteractionResult ShowSaveFileDialog(string initialDirectory, out string fileName, string filters = "");
    }
}
