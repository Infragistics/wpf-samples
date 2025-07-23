using System;
using System.Windows.Controls;
using Infragistics.Controls.Editors;

namespace IGInputs.Samples.Editing
{
    public partial class Events : Infragistics.Samples.Framework.SampleContainer
    {
        public Events()
        {
            InitializeComponent();
        }

        private void dateTimeInput_ValidationError(object sender, EditModeValidationErrorEventArgs e)
        {
            eventsTextBox.Text += PrintLog("ValidationError :" + e.ErrorMessage);
        }

        private void dateTimeInput_TextChanged(object sender, EventArgs e)
        {
            eventsTextBox.Text += PrintLog("TextChanged");
        }

        private void dateTimeInput_ValueChanged(object sender, EventArgs e)
        {
            eventsTextBox.Text += PrintLog("ValueChanged");
        }

        private void dateTimeInput_InvalidChar(object sender, InvalidCharEventArgs e)
        {
            eventsTextBox.Text += PrintLog("InvalidChar: " + e.Char);
        }

        private void dateTimeInput_DropDownOpened(object sender, EventArgs e)
        {
            eventsTextBox.Text += PrintLog("DropDownOpened");
        }

        private void dateTimeInput_DropDownClosed(object sender, EventArgs e)
        {
            eventsTextBox.Text += PrintLog("DropDownClosed");
        }

        private static string PrintLog(string msg)
        {
            string logMsg = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + msg + "\n";

            return logMsg;
        }

        private void eventsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //eventsTextBox.ScrollToEnd();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.dateTimeInput.Value = DateTime.Now;
        }
    }
}
