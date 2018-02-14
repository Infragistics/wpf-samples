using System.Windows;

namespace IgExcel.Infrastructure.Dialogs
{
    public class MessageBoxService : IMessageBoxService
    {
        public InteractionResult Show(string title, string message)
        {
            return Show(title, message, MessageBoxButtons.OkCancel);
        }

        public InteractionResult Show(string title, string message, MessageBoxButtons buttons)
        {
            System.Windows.MessageBoxButton msgButtons;

            switch (buttons)
            {
                case MessageBoxButtons.Ok: msgButtons = System.Windows.MessageBoxButton.OK; break;
                case MessageBoxButtons.OkCancel: msgButtons = System.Windows.MessageBoxButton.OKCancel; break;
                case MessageBoxButtons.YesNo: msgButtons = System.Windows.MessageBoxButton.YesNo; break;
                case MessageBoxButtons.YesNoCancel: msgButtons = System.Windows.MessageBoxButton.YesNoCancel; break;
                default: msgButtons = System.Windows.MessageBoxButton.OK; break;
            }

            var result = MessageBox.Show(message, title, msgButtons);

            switch (result)
            {
                case MessageBoxResult.OK: return InteractionResult.Ok;
                case MessageBoxResult.No: return InteractionResult.No;
                case MessageBoxResult.Yes: return InteractionResult.Yes;
                case MessageBoxResult.Cancel: return InteractionResult.Cancel;
                default: return InteractionResult.None;
            }

        }
    }
}
