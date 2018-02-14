using System;

namespace IgWord.Infrastructure.Dialogs
{
    public interface IDialogAware
    {
        bool CanCloseDialog();
        event Action RequestClose;
    }
}
