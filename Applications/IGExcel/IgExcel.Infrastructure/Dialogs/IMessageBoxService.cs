
namespace IgExcel.Infrastructure.Dialogs
{
    public interface IMessageBoxService
    {
        InteractionResult Show(string title, string message);
        InteractionResult Show(string title, string message, MessageBoxButtons buttons);
    }
}
