using IGSpreadsheet.Resources;
using Infragistics.Controls.Grids;

namespace IGSpreadsheet.Shared
{
    public static class Utils
    {
        public static void SetDefaultDialogTitle(XamSpreadsheet xamSpreadsheet)
        {
            xamSpreadsheet.UserPromptDisplaying += XamSpreadsheet_UserPromptDisplaying;
        }

        private static void XamSpreadsheet_UserPromptDisplaying(object sender, SpreadsheetUserPromptDisplayingEventArgs e)
        {
            e.Caption = SpreadsheetStrings.lblSamplesBrowser;

            // The following code can be used to hide
            // the default message dialog and show a custom one

            /*
            // cancel the xamSpreadsheet's message box
            e.DisplayMessage = false;

            // check what was the buttons in the xamSpreadsheet's message box and 
            var buttons = e.CanCancel ? MessageBoxButton.OKCancel : MessageBoxButton.OK;

            // show the message with customized caption
            MessageBoxResult res = MessageBox.Show(
                e.Message,
                SpreadsheetStrings.lblSamplesBrowser,
                buttons,
                MessageBoxImage.None,
                MessageBoxResult.OK);

            // process canceling of the dialog (if such occured)
            if (res == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            */

        }
    }
}
