using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using IGSyntaxParsingEngine.Resources;
using IGSyntaxParsingEngine.Samples.Languages;
using Infragistics.Documents;
using Infragistics.Samples.Framework;
using Infragistics.Documents.Parsing;

namespace IGSyntaxParsingEngine.Samples.Data
{
    public partial class CustomLanguage : SampleContainer
    {
        public CustomLanguage()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            this.aTextBox.Text = SyntaxParsingEngineStrings.SampleXML;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a TextDocument and set the language to "CustomXMLLanguage"
            var td = new TextDocument();
            td.Language = CustomXMLLanguage.Instance;

            // Clear the document and add the content of the text box
            td.Delete();
            td.Append(this.aTextBox.Text);
            td.Parse();

            // Get the snapshot from which the tree was created
            TextDocumentSnapshot snapshot = td.SyntaxTree.Snapshot;

            // Get a token enumerator which includes all tokens
            IEnumerable<Token> tokens = snapshot.GetTokens();

            // Create a flow document for the Rich Text Box
            var doc = new FlowDocument();
            var paragraph = new Paragraph();
            doc.Blocks.Add(paragraph);
            Run run;

            // Iterate over the token produced during the parsing
            // Assign a color depending on their terminal symbol
            // Add the tokens in the Rich Text Box
            foreach (Token token in tokens)
            {
                if (token.Text.Length != 0)
                {
                    run = new Run();
                    run.Text = token.Text;
                    // Set the color for the token based on what we defined in the language.
                    Color c = CustomXMLLanguage.GetColor(token.TerminalSymbol);
                    run.Foreground = new SolidColorBrush(c);
                    paragraph.Inlines.Add(run);
                }
                else
                {
                    continue;
                }
            }
            this.aRichTextBox.Document = doc;
        }
    }
}
