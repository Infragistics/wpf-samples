using System.IO;
using IGRichTextEditor.Resources;
using IGRichTextEditor.Samples.Helpers;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Editing
{
    public partial class MultipleSelection : SampleContainer
    {
        public MultipleSelection()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            Stream fs = FilesProvider.GetStreamForFile(RichTextEditorStrings.fileDocxTextList);
            RichTextDocument doc = new RichTextDocument();
            doc.LoadFromWord(fs);
            this.xamRichTextEditor1.Document = doc;
        }

		private void xamRichTextEditor1_SelectionChanged(object sender, Infragistics.Controls.Editors.RichTextSelectionChangedEventArgs e)
        {
            this.selectedText.Text = string.Empty;

            foreach (Range selRange in this.xamRichTextEditor1.ActiveDocumentView.Selection.Ranges)
            {
                string infoLine =
                    RichTextEditorStrings.lblStart + " " + selRange.Start + " " +
                    RichTextEditorStrings.lblEnd + " " + selRange.End + " " +
                    RichTextEditorStrings.lblContent + " " + selRange.Text + "\r\n";
                this.selectedText.Text += infoLine;
            }
        }
    }
}
