using System;

namespace IgExcel.Infrastructure.Dialogs
{
    public interface IDialogAware
    {
        bool CanCloseDialog();
        event Action RequestClose;
    }
}
