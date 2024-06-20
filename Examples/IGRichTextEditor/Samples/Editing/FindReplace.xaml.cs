using System.IO;
using System.Windows;
using IGRichTextEditor.Resources;
using IGRichTextEditor.Samples.Helpers;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Editing
{
    public partial class FindReplace : SampleContainer
    {
        public FindReplace()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            Stream fs = FilesProvider.GetStreamForFile(RichTextEditorStrings.fileDocxText);
            RichTextDocument doc = new RichTextDocument();
            doc.LoadFromWord(fs);
            this.xamRichTextEditor1.Document = doc;
        }

        private void bFind_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbFind.Text))
            {
                MessageBox.Show(RichTextEditorStrings.errTextToFindEmpty);
                return;
            }

            // construct FindCriteria objects based in the checkboxes in the UI
            FindCriteria fc = new FindCriteria();
            fc.CaseSensitive = this.cbMC.IsChecked ?? false;
            fc.Operator = (this.cbUW.IsChecked.HasValue && this.cbUW.IsChecked.Value) ?
                FindOperator.Wildcard : FindOperator.PlainText;

            string errorMsg;
            this.xamRichTextEditor1.FindReplaceManager.FindInDocument(tbFind.Text, out errorMsg, fc);
            this.xamRichTextEditor1.FindReplaceManager.SelectNextMatch();          
        }

        private void bReplace_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbFind.Text))
            {
                MessageBox.Show(RichTextEditorStrings.errTextToFindEmpty);
                return;
            }

            // construct FindCriteria objects based in the checkboxes in the UI
            FindCriteria fc = new FindCriteria();
            fc.CaseSensitive = this.cbMC.IsChecked ?? false;
            fc.Operator = (this.cbUW.IsChecked.HasValue && this.cbUW.IsChecked.Value) ?
                FindOperator.Wildcard : FindOperator.PlainText;

            // replace an occurence
            string errorMsg;
            ReplaceResult rr = this.xamRichTextEditor1.Document.ReplaceAll(
                this.xamRichTextEditor1.ActiveDocumentView.Selection.DocumentSpan,
                this.tbFind.Text,
                this.tbReplace.Text,
                out errorMsg,
                fc);

            // find next
            this.bFind_Click(sender, e);
        }
    }
}
