using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using IGRichTextEditor.Resources;
using IGRichTextEditor.Samples.Helpers;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Display
{
    public partial class ContentEditing : SampleContainer
    {
        public ContentEditing()
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

        private void cb_DropDownClosed(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                ComboBox cb = sender as ComboBox;
                if (cb.Tag.Equals("cbFont"))
                {
                    object obj = cb.SelectedItem;
                    if (obj is string)
                    {
                        string i = (string)obj;
                        this.xamRichTextEditor1.ActiveDocumentView.Selection.ApplyFont(i);
                    }
                }
                if (cb.Tag.Equals("cbSize"))
                {
                    object obj = cb.SelectedItem;
                    if (obj is int)
                    {
                        int i = (int)obj;
                        this.xamRichTextEditor1.ActiveDocumentView.Selection.ApplyFontSize(i);
                    }
                }
                if (cb.Tag.Equals("cbFColor"))
                {
                    object obj = cb.SelectedItem;
                    if (obj is Color)
                    {
                        Color c = (Color)obj;
                        this.xamRichTextEditor1.ActiveDocumentView.Selection.ApplyTextForecolor(c);
                    }
                }
                else if (cb.Tag.Equals("cbBColor"))
                {
                    object obj = cb.SelectedItem;
                    if (obj is HighlightColor)
                    {
                        HighlightColor hc = (HighlightColor)obj;
                        this.xamRichTextEditor1.ActiveDocumentView.Selection.ApplyTextHighlightColor(hc);
                    }
                }
            }
        }

    }
}
