using System;
using System.Windows;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;

namespace IGSyntaxEditor.Samples.Editing
{
    public partial class ClipboardSupport : SampleContainer
    {
        private int innerCounter;

        public ClipboardSupport()
        {
            InitializeComponent();
            this.UseDefaultTheme = true;
            this.xamSyntaxEditor1.ClipboardOperationExecuting += new System.EventHandler<ClipboardOperationExecutingEventArgs>(xamSyntaxEditor1_ClipboardOperationExecuting);
            this.xamSyntaxEditor1.ClipboardOperationExecuted += new System.EventHandler<ClipboardOperationExecutedEventArgs>(xamSyntaxEditor1_ClipboardOperationExecuted);
        }

        public void xamSyntaxEditor1_ClipboardOperationExecuting(object sender, ClipboardOperationExecutingEventArgs e)
        {
            if (this.CancelEvents.IsChecked.HasValue && this.CancelEvents.IsChecked.Value)
            {
                e.Cancel = true;
                AddToLog("ClipboardOperationExecuting (" + e.ClipboardOperation + ") - Canceled");
            }
            else
            {
                AddToLog("ClipboardOperationExecuting (" + e.ClipboardOperation + ")");
            }
        }

        public void xamSyntaxEditor1_ClipboardOperationExecuted(object sender, ClipboardOperationExecutedEventArgs e)
        {
            AddToLog("ClipboardOperationExecuted (" + e.ClipboardOperation + ")");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            innerCounter = 0;
            txtEvents.Text = string.Empty;
            this.xamSyntaxEditor1.Focus();
        }

        private bool AddToLog(string Text)
        {
            if (txtEvents != null)
            {
                innerCounter++;
                txtEvents.Text = innerCounter + DateTime.Now.ToString(" - hh:mm:ss - ") + Text + Environment.NewLine + txtEvents.Text;
            }
            return txtEvents != null;
        }

        private void CancelEvents_Click(object sender, RoutedEventArgs e)
        {
            this.xamSyntaxEditor1.Focus();
        }
    }
}
