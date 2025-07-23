using System;
using IGSyntaxEditor.Resources;
using Infragistics.Documents;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;

namespace IGSyntaxEditor.Samples.Display
{
    public partial class HighlightingCustomizationXAML : SampleContainer
    {
        public HighlightingCustomizationXAML()
        {
            InitializeComponent();
            InitializeSample();
        }

        private void InitializeSample()
        {
            this.UseDefaultTheme = true;
            // Initialize a text document with C# language content in the xamSyntaxEditor
            var td = new TextDocument();
            td.Language = CSharpLanguage.Instance;
            td.InitializeText(SyntaxEditorStrings.SourceCS);
            this.xamSyntaxEditor1.Document = td;
        }
    }
}
