using System;
using System.IO;
using System.Windows;
using IGSyntaxEditor.Resources;
using Infragistics.Documents;
using Infragistics.Samples.Framework;
using Microsoft.Win32;
using Infragistics.Documents.Parsing;

namespace IGSyntaxEditor.Samples.Navigation
{
    public partial class NavigatingDocumentTokens : SampleContainer
    {
        private TextDocumentSnapshotScanner _scanner;

        public NavigatingDocumentTokens()
        {
            InitializeComponent();
            this.UseDefaultTheme = true;
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Open File";
            dialog.Filter = "All Files |*.*";

            bool? open = dialog.ShowDialog();
            if (open.HasValue && open.Value == true)
            {
                var fi = new FileInfo(dialog.FileName);
                try
                {
                    // set a language to the document depending of the file extension
                    if (fi.Name.EndsWith(".cs"))
                    {
                        this.xamSyntaxEditor1.Document.Language = CSharpLanguage.Instance;
                    }
                    else if (fi.Name.EndsWith(".vb"))
                    {
                        this.xamSyntaxEditor1.Document.Language = VisualBasicLanguage.Instance;
                    }
                    else
                    {
                        this.xamSyntaxEditor1.Document.Language = PlainTextLanguage.Instance;
                    }

                    // read the file
                    FileStream fs = fi.OpenRead();
                    this.xamSyntaxEditor1.Document.Load(fs);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error '{0}' while loading file '{1}'", ex.Message, dialog.FileName), "Error Loading File", MessageBoxButton.OK);
                }
            }
        }

        private void btnCreateScanner_Click(object sender, RoutedEventArgs e)
        {
            TextDocumentSnapshot snapShot = this.xamSyntaxEditor1.Document.CurrentSnapshot;
			_scanner = snapShot.CreateScanner(splitMultilineTokensByLine: true);
            this.btnCreateScanner.IsEnabled = false;
            this.btnDestroyScanner.IsEnabled = true;
            this.btnPrevToken.IsEnabled = true;
            this.btnNextToken.IsEnabled = true;
            UpdateFieldsFromScanner();
        }

        private void btnDestroyScanner_Click(object sender, RoutedEventArgs e)
        {
            _scanner = null;
            tb1.Text = "";
            this.btnCreateScanner.IsEnabled = true;
            this.btnDestroyScanner.IsEnabled = false;
            this.btnPrevToken.IsEnabled = false;
            this.btnNextToken.IsEnabled = false;
        }

        private void btnPrevToken_Click(object sender, RoutedEventArgs e)
        {
            _scanner.SeekToToken(TokenScanType.PreviousTokenStart);
            UpdateFieldsFromScanner();
        }

        private void btnNextToken_Click(object sender, RoutedEventArgs e)
        {
            _scanner.SeekToToken(TokenScanType.NextTokenStart);
            UpdateFieldsFromScanner();
        }

        private void UpdateFieldsFromScanner()
        {
            string result = SyntaxEditorStrings.lblCurrentCharacter + FormatChar(_scanner.CurrentCharacter);
            result += "\r\n" + SyntaxEditorStrings.lblCurrentOffset + _scanner.CurrentOffset;
            result += "\r\n" + SyntaxEditorStrings.lblCurrentToken + FormatToken(_scanner.CurrentToken.Text);
            result += "\r\n" + SyntaxEditorStrings.lblCurrentWord + (_scanner.CurrentWord.HasValue ? _scanner.CurrentWord.Value.Text : SyntaxEditorStrings.msgNoWordPresentation);
            this.tb1.Text = result;
        }

        private static string FormatChar(char ch)
        {
            if (ch == '\r') return "[CR]";
            if (ch == '\n') return "[LF]";
            if (ch == ' ') return "[SPACE]";
            return "" + ch;
        }

        private static string FormatToken(string str)
        {
            str = str.Replace("\r", "[CR]");
            str = str.Replace("\n", "[LF]");
            return str;
        }

    }
}
