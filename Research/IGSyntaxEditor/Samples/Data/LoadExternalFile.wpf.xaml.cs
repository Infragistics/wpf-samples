using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGSyntaxEditor.Resources;
using Infragistics.Documents;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;
using Microsoft.Win32;

namespace IGSyntaxEditor.Samples.Data
{
    /// <summary>
    /// Interaction logic for LoadExternalFile.xaml
    /// </summary>
    public partial class LoadExternalFile : SampleContainer
    {
        public LoadExternalFile()
        {
            InitializeComponent();
            InitializeSample();
        }

        private void InitializeSample()
        {
            this.UseDefaultTheme = true;
            var td = new TextDocument();
            td.Language = PlainTextLanguage.Instance;
            this.xamSyntaxEditor1.Document = td;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem ci = this.cb_lang.SelectedItem as ComboBoxItem;
            if (ci == null) return;

            this.xamSyntaxEditor1.Document = new TextDocument();
            switch (ci.Tag as string)
            {
                case "LangCS":
                    this.xamSyntaxEditor1.Document.Language = CSharpLanguage.Instance;
                    break;
                case "LangVB":
                    this.xamSyntaxEditor1.Document.Language = VisualBasicLanguage.Instance;
                    break;
                case "LangTSQL":
                    this.xamSyntaxEditor1.Document.Language = TSqlLanguage.Instance;
                    break;
                default:
                    this.xamSyntaxEditor1.Document.Language = null;
                    break;
            }

            var dialog = new OpenFileDialog();
            dialog.Title = SyntaxEditorStrings.OpenFile;
            if (this.xamSyntaxEditor1.Document.Language is CSharpLanguage)
            {
                dialog.Filter = "C# Files|*.cs";
            }
            else if (this.xamSyntaxEditor1.Document.Language is VisualBasicLanguage)
            {
                dialog.Filter = "VB Files|*.vb";
            }
            else if (this.xamSyntaxEditor1.Document.Language is TSqlLanguage)
            {
                dialog.Filter = "T-SQL Files|*.sql";
            }
            else
            {
                dialog.Filter = "Text Files|*.txt";
            }

            bool? open = dialog.ShowDialog();
            if (open.HasValue && open.Value == true)
            {
                var fi = new FileInfo(dialog.FileName);
                try
                {
                    FileStream fs = fi.OpenRead();
                    this.xamSyntaxEditor1.Document.Load(fs);
                    fs.Close();

                    this.cb_lang.IsEnabled = false;
                    this.bOpen.IsEnabled = false;
                    this.bClose.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error '{0}' while loading file '{1}'", ex.Message, dialog.FileName), "Error Loading File", MessageBoxButton.OK);
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.xamSyntaxEditor1.Document.InitializeText(string.Empty);
            this.xamSyntaxEditor1.Document.Language = null;
            this.cb_lang.IsEnabled = true;
            this.bOpen.IsEnabled = true;
            this.bClose.IsEnabled = false;
        }
    }
}
