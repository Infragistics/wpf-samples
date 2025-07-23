using IGRichTextEditor.Resources;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Data
{
    public partial class DefiningDropCap : SampleContainer
    {
        public DefiningDropCap()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            CreateDropCapCharacter(RichTextEditorStrings.txtDropCap11);
            CreateParagraph(RichTextEditorStrings.txtDropCap12);
            CreateDropCapCharacter(RichTextEditorStrings.txtDropCap21);
            CreateParagraph(RichTextEditorStrings.txtDropCap22);
        }

        void CreateDropCapCharacter(string content)
        {
            // create a paragraph for the drop cap letter
            ParagraphNode pn = new ParagraphNode();
            pn.Settings = new ParagraphSettings();
            pn.Settings.Frame = new TextFrameSettings();
            pn.Settings.Frame.DropCap = TextFrameDropCap.Drop; // enable the drop cap feature
            RunNode rn = new RunNode();
            rn.Settings = new CharacterSettings();
            rn.Settings.FontSize = new Extent(48, ExtentUnitType.Points); // set the font size for the drop cap letter
            rn.SetText(content);
            pn.ChildNodes.Add(rn);
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);
        }

        void CreateParagraph(string content)
        {
            ParagraphNode pn = new ParagraphNode();
            pn.SetText(content);
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);
        }
    }
}
