using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGRichTextEditor.Resources;
using IGRichTextEditor.Samples.Helpers;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Editors.Primitives;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Editing
{
    public partial class ClipboardSupport : SampleContainer
    {
        private int innerCounter;

        public ClipboardSupport()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            Stream fs = FilesProvider.GetStreamForFile(RichTextEditorStrings.fileDocxRichContent);
            RichTextDocument doc = new RichTextDocument();
            doc.LoadFromWord(fs);
            this.xamRichTextEditor1.Document = doc;

            // add handlers for the clipboard events
            this.xamRichTextEditor1.ClipboardOperationExecuting += xamRichTextEditor1_ClipboardOperationExecuting;
            this.xamRichTextEditor1.ClipboardOperationExecuted += xamRichTextEditor1_ClipboardOperationExecuted;
        }

        void xamRichTextEditor1_ClipboardOperationExecuting(object sender, RichTextClipboardOperationCancelEventArgs e)
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

        void xamRichTextEditor1_ClipboardOperationExecuted(object sender, RichTextClipboardOperationEventArgs e)
        {
            AddToLog("ClipboardOperationExecuted (" + e.ClipboardOperation + ")");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            innerCounter = 0;
            txtEvents.Text = string.Empty;
            this.xamRichTextEditor1.Focus();
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
            this.xamRichTextEditor1.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button b = (Button)sender;
                if ("Cut".Equals(b.Tag))
                {
                    // execute CUT using a command in code
                    //RichTextEditorCommand rteCommand = new RichTextEditorCommand(RichTextEditorCommandType.Cut);
                    //rteCommand.Execute(this.xamRichTextEditor1);
                    
                    // execute CUT using a method
                    //this.xamRichTextEditor1.ActiveDocumentView.Selection.Cut();
                }
                else if ("Copy".Equals(b.Tag))
                {
                    // execute COPY using a command in code
                    //RichTextEditorCommand rteCommand = new RichTextEditorCommand(RichTextEditorCommandType.Copy);
                    //rteCommand.Execute(this.xamRichTextEditor1);

                    // execute COPY using a method
                    //this.xamRichTextEditor1.ActiveDocumentView.Selection.Copy();
                }
                else if ("Paste".Equals(b.Tag))
                {
                    // execute PASTE using a command in code
                    //RichTextEditorCommand rteCommand = new RichTextEditorCommand(RichTextEditorCommandType.Paste);
                    //rteCommand.Execute(this.xamRichTextEditor1);

                    // execute PASTE using a method
                    //this.xamRichTextEditor1.ActiveDocumentView.Selection.Paste();
                }
            }
        }

    }
}
