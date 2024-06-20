using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGRichTextEditor.Resources;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Data
{
    public partial class ImportContent : SampleContainer
    {
        public ImportContent()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem ci = this.cb_lang.SelectedItem as ComboBoxItem;
            if (ci == null) return;
            if (ci.Tag is string)
            {
                string fileType = ci.Tag as string;
                var dialog = new OpenFileDialog();
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
                bool? open = dialog.ShowDialog();
                if (open.HasValue && open.Value == true)
                {
                    try
                    {
                        var fi = dialog.File;
                        Stream fs = fi.OpenRead();
                        var rtd = new RichTextDocument();
                        if ("TypeText".Equals(fileType))
                        {
                            rtd.LoadFromPlainText(fs);
                        }
                        if ("TypeRtf".Equals(fileType))
                        {
                            rtd.LoadFromRtf(fs);
                        }
                        else if ("TypeDocx".Equals(fileType))
                        {
                            rtd.LoadFromWord(fs);
                        }
                        else if ("TypeHtml".Equals(fileType))
                        {
                            rtd.LoadFromHtml(fs);
                        }
                        this.xamRichTextEditor1.Document = rtd;
                        fs.Close();
                        this.cb_lang.IsEnabled = false;
                        this.bOpen.IsEnabled = false;
                        this.bClose.IsEnabled = true;
                        this.xamRichTextEditor1.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error '{0}' while loading file '{1}'", ex.Message, dialog.File.FullName), "Error Loading File", MessageBoxButton.OK);
                    }
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.xamRichTextEditor1.Document = new RichTextDocument();
            this.cb_lang.IsEnabled = true;
            this.bOpen.IsEnabled = true;
            this.bClose.IsEnabled = false;
            this.xamRichTextEditor1.Focus();
        }
    }
}
