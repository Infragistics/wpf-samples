using System;
using IGRichTextEditor.Resources;
using Infragistics.Controls.Editors;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Data
{
    public partial class DefiningHyperlinks : SampleContainer
    {
        public DefiningHyperlinks()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            // attach event handler to the HyperlinkExecuting event
            this.xamRichTextEditor1.HyperlinkExecuting += xamRichTextEditor1_HyperlinkExecuting;

            // adding the defualt hyperlink style to the document's root nodes styles collection
            if (!this.xamRichTextEditor1.Document.RootNode.Styles.Contains("Hyperlink"))
            {
                this.xamRichTextEditor1.Document.RootNode.Styles.Add(
                    this.xamRichTextEditor1.Document.AvailableStyles["Hyperlink"].Clone() as CharacterStyle);
            }

            // add the content
            ParagraphNode pn = new ParagraphNode();
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);
            AddText(pn, RichTextEditorStrings.txtLinkText1);
            AddLinkCreatedUsingNodes(pn, RichTextEditorStrings.txtLink1, RichTextEditorStrings.txtLink1Url);
            AddText(pn, RichTextEditorStrings.txtLinkText2);
            AddLinkCreatedUsingInserthyperlink(pn, RichTextEditorStrings.txtLink2, RichTextEditorStrings.txtLink2Url);
            AddText(pn, ".");
        }

        void AddText(ParagraphNode pn, string content)
        {
            RunNode rn = new RunNode();
            rn.SetText(content);
            pn.ChildNodes.Add(rn);
        }

        void AddLinkCreatedUsingNodes(ParagraphNode pn, string linkText, string linkUri)
        {
            var hNode = new HyperlinkNode();
            hNode.Tooltip = RichTextEditorStrings.lblCreatedInCode;
            hNode.Uri = linkUri;
            hNode.SetText(linkText);
            hNode.TrackHistory = true;
            pn.ChildNodes.Add(hNode);
        }

        void AddLinkCreatedUsingInserthyperlink(ParagraphNode pn, string linkText, string linkUri)
        {
            string error;
            this.xamRichTextEditor1.Document.InsertHyperlink(
                this.xamRichTextEditor1.Document.AggregateOffsetCount - 1,
                linkUri, out error, linkText,
                RichTextEditorStrings.lblCreateUsingMethod);
        }

        void xamRichTextEditor1_HyperlinkExecuting(object sender, HyperlinkExecutingEventArgs e)
        {
            if (this.cbCancelEvents.IsChecked == true)
            {
                e.Cancel = true;
            }
        }
    }
}
