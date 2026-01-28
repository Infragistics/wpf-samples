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
    public partial class Customizations : SampleContainer
    {
        public Customizations()
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

        private void cb_DropDownClosed(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                ComboBox cb = sender as ComboBox;
                if (cb.Tag.Equals("cbCaret"))
                {
                    object obj = cb.SelectedItem;
                    if (obj is Color)
                    {
                        Color c = (Color)obj;
                        this.xamRichTextEditor1.CaretColor = new ColorInfo( c );
                        this.xamRichTextEditor1.Focus();
                    }
                }
            }
        }

    }
}
