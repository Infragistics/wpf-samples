using System;
using System.Windows.Media;
using IGRichTextEditor.Resources;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Data
{
    public partial class DefiningText : SampleContainer
    {
        public DefiningText()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            CharacterSettings csBold = new CharacterSettings() { Bold = true };
            CharacterSettings csItalicUnderline = new CharacterSettings() { Italics = true, UnderlineType = UnderlineType.Single };
            CharacterSettings csFColor = new CharacterSettings() { Color = new ColorInfo(Colors.Magenta) };
            CharacterSettings csBColor = new CharacterSettings() { HighlightColor = HighlightColor.Green };
            CharacterSettings csBigSize = new CharacterSettings() { FontSize = new Extent(24, ExtentUnitType.LogicalPixels) };
            CharacterSettings csFont = new CharacterSettings() { FontSettings = new FontSettings() { Ascii = "Consolas" } };

            ParagraphNode pn = this.createParagraphNode();

            // unstyled text
            AddText(pn, null, "This is some sample text to show how text can added and styled in code. If you need the text ");
            AddText(pn, csBold, "to be bold ");
            AddText(pn, null, "or it can be ");
            AddText(pn, csItalicUnderline, "italic and underline");
            AddText(pn, null, ". You can also use ");
            AddText(pn, csFColor, "some fancy color");
            AddText(pn, null, ", ");
            AddText(pn, csBColor, "highlight ");
            AddText(pn, null, ", ");
            AddText(pn, csBigSize, "bigger size ");
            AddText(pn, null, "or ");
            AddText(pn, csFont, "different font ");
            AddText(pn, null, "to make the text more noticeable.");
        }

        private void AddText(ParagraphNode pn, CharacterSettings cs, string content)
        {
            RunNode rn = new RunNode();
            rn.SetText(content);
            if (cs != null) rn.Settings = cs;
            pn.ChildNodes.Add(rn);
        }

        private ParagraphNode createParagraphNode()
        {
            ParagraphNode pn = new ParagraphNode();
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);
            return pn;
        }
    }
}
