using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;

namespace IGSyntaxParsingEngine.Samples.Data
{
    public partial class LanguageFromEBNF : SampleContainer
    {
        public LanguageFromEBNF()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            // Obtain the XML EBNF content as string
            string xmlEbnfLocation = "/IGSyntaxParsingEngine;component/Samples/Languages/XML.ebnf";
            var xmlEbnfUri = new Uri(xmlEbnfLocation, UriKind.Relative);
            var streamInfo = Application.GetResourceStream(xmlEbnfUri);
            var stream = streamInfo.Stream;
            var sr = new StreamReader(stream);
            string xmlEbnf = sr.ReadToEnd();

            // Show the EBNF in the upper text box
            this.tbEBNF.Text = xmlEbnf;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a grammar from the EBNF script.
            var result = Grammar.LoadEbnf(this.tbEBNF.Text);

            if (result.Errors.Count == 0)
            {
                CodeFileFormat outputFormat = CodeFileFormat.CSharp;
                if (((ComboBoxItem)this.cbOL.SelectedItem).Tag.Equals("VB"))
                {
                    outputFormat = CodeFileFormat.VisualBasic;
                }

                // Create the arguments for the language generation
                var args = new LanguageGenerationParams(
                    result.Grammar, // the gramar
                    "CustomXMLLanguage", // name of the class
                    "IGSyntaxParsingEngine.Samples.Languages", // namespace
                    outputFormat); // output language

                // Generate the code for a LanguageBase-derived class from the grammar.
                string languageClassSource = LanguageGenerator.GenerateClass(args);

                // Put the created LanguageBase class in the lower text box
                if (languageClassSource != null)
                {
                    this.tbLang.Foreground = new SolidColorBrush(Colors.Black);
                    this.tbLang.Text = languageClassSource;
                }
            }
            else
            {
                this.tbLang.Foreground = new SolidColorBrush(Colors.Red);
                this.tbLang.Text = string.Empty;
                foreach (EbnfLoadError err in result.Errors)
                {
                    this.tbLang.Text += err.Description + "\r\n";
                }
            }
        }
    }
}
