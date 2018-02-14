using Infragistics.Controls.Editors;
using Infragistics.Documents.RichText;
using Infragistics.Documents.RichText.Rtf;
using System.IO;
using System.Text;
using System.Windows;


namespace IgOutlook.Infrastructure.Prism
{
    public class XamRichTextEditorBehavior : DependencyObject
    {
        #region InsertRtfContent

        public static readonly DependencyProperty InsertRtfContentProperty = DependencyProperty.RegisterAttached("InsertRtfContent", typeof(string), typeof(XamRichTextEditorBehavior), new PropertyMetadata(null, OnInsertRtfContentChanged));

        public static void SetInsertRtfContent(XamRichTextEditor editor, object value)
        {
            editor.SetValue(InsertRtfContentProperty, value);
        }

        public static string GetInsertRtfContent(XamRichTextEditor editor)
        {
            return editor.GetValue(InsertRtfContentProperty) as string;
        }

        private static void OnInsertRtfContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XamRichTextEditor editor = (XamRichTextEditor)d;
            editor.DocumentContentChanged -= Document_ContentChanged;
            editor.DocumentContentChanged += Document_ContentChanged;
        }

        static void Document_ContentChanged(object sender, DocumentContentChangedEventArgs e)
        {
            if (e.ChangeType == Infragistics.Documents.RichText.DocumentChangeType.Content)
            {
                var xamRichTextEditor = sender as XamRichTextEditor;

                xamRichTextEditor.DocumentContentChanged -= Document_ContentChanged;

                var rtfString = (string)xamRichTextEditor.GetValue(XamRichTextEditorBehavior.InsertRtfContentProperty);

                RichTextDocument doc = new RichTextDocument();
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(rtfString));
                doc.LoadFromRtf(stream);

                string error;

                xamRichTextEditor.Document.InsertContent(0, doc.RootNode, out error, true, true);
            }
        }

        #endregion //InsertRtfContent
    }
}
