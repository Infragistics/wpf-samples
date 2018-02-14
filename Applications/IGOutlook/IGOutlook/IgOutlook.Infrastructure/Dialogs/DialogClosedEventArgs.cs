using System;

namespace IgOutlook.Infrastructure.Dialogs
{
    public class DialogClosedEventArgs : EventArgs
    {
        public InteractionResult Result { get; set; }
        public object ViewModel { get; set; }

        public DialogClosedEventArgs(InteractionResult result)
        {
            Result = result;
        }

        public DialogClosedEventArgs(object viewModel)
        {
            ViewModel = viewModel;
        }

        public DialogClosedEventArgs(InteractionResult result, object viewModel)
        {
            Result = result;
            ViewModel = viewModel;
        }
    }
}
