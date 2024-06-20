using System.IO;
using System.Windows;
using System.Windows.Media;
using IGRichTextEditor.Resources;
using IGRichTextEditor.Samples.Helpers;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Editing
{
    public partial class UndoRedo : SampleContainer
    {
        public UndoRedo()
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
        }

        private void ButtonUndoStart_Click(object sender, RoutedEventArgs e)
        {
            this.xamRichTextEditor1.Document.StartUndoLogging();
        }

        private void ButtonUndoStop_Click(object sender, RoutedEventArgs e)
        {
            this.xamRichTextEditor1.Document.StopUndoLogging();
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            Stream fs = FilesProvider.GetStreamForFile(RichTextEditorStrings.fileDocxRichContent);
            RichTextDocument doc = this.xamRichTextEditor1.Document;
            doc.LoadFromWord(fs);
        }

        private void ButtonDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            RichTextDocument doc = this.xamRichTextEditor1.Document;
            DocumentSpan span = doc.GetNodeSpan(doc.RootNode);
            doc.Delete(span);
        }
        
        private void ButtonMakeRed_Click(object sender, RoutedEventArgs e)
        {
            this.xamRichTextEditor1.ActiveDocumentView.Selection.ApplyTextForecolor(Colors.Red);
        }

        private void ButtonMakeBold_Click(object sender, RoutedEventArgs e)
        {
            this.xamRichTextEditor1.ActiveDocumentView.Selection.ToggleBoldFormatting();
        }

        private void ButtonMakeItalic_Click(object sender, RoutedEventArgs e)
        {
            this.xamRichTextEditor1.ActiveDocumentView.Selection.ToggleItalicFormatting();
        }
    }
}
