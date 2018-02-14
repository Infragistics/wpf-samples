using System;

namespace IgOutlook.Infrastructure.Dialogs
{
    public interface IDialogAware
    {
        bool CanCloseDialog();
        //InteractionResult DialogResult { get; }
        event Action RequestClose;
    }
}
