using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGRichTextEditor.Resources;
using IGRichTextEditor.Samples.Helpers;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;
using Microsoft.Win32;

namespace IGRichTextEditor.Samples.Data
{
    public partial class ExportContent : SampleContainer
    {
        public ExportContent()
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

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem ci = this.cb_lang.SelectedItem as ComboBoxItem;
            if (ci == null) return;
            if (ci.Tag is string)
            {
                string fileType = ci.Tag as string;
                var dialog = new SaveFileDialog();
                dialog.Title = RichTextEditorStrings.btnSaveFile;
                if ("TypeText".Equals(fileType))
                {
                    dialog.Filter = RichTextEditorStrings.filterText;
                }
                else if ("TypeRtf".Equals(fileType))
                {
                    dialog.Filter = RichTextEditorStrings.filterRtf;
                }
                else if ("TypeDocx".Equals(fileType))
                {
                    dialog.Filter = RichTextEditorStrings.filterDocx;
                }
                else if ("TypeHtml".Equals(fileType))
                {
                    dialog.Filter = RichTextEditorStrings.filterHtml;
                }
                bool? confirm = dialog.ShowDialog();
                if (confirm.HasValue && confirm.Value == true)
                {
                    var fi = new FileInfo(dialog.FileName);
                    try
                    {
                        FileStream fs = null;
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                        fs = fi.Create();
                        if ("TypeText".Equals(fileType))
                        {
                            this.xamRichTextEditor1.Document.SaveToPlainText(fs);
                        }
                        else if ("TypeRtf".Equals(fileType))
                        {
                            this.xamRichTextEditor1.Document.SaveToRtf(fs);
                        }
                        else if ("TypeDocx".Equals(fileType))
                        {
                            this.xamRichTextEditor1.Document.SaveToWord(fs);
                        }
                        else if ("TypeHtml".Equals(fileType))
                        {
                            this.xamRichTextEditor1.Document.SaveToHtml(fs);
                        }
                        fs.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error '{0}' while saving file '{1}'", ex.Message, dialog.FileName), "Error Saving File", MessageBoxButton.OK);
                    }
                }
            }
        }
    }
}
