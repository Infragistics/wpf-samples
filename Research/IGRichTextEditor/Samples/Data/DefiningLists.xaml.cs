using IGRichTextEditor.Resources;
using IGRichTextEditor.Samples.Helpers;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Data
{
    public partial class DefiningLists : SampleContainer
    {
        public DefiningLists()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            CharacterSettings csTitle = new CharacterSettings()
            {
                FontSize = new Extent(18, ExtentUnitType.LogicalPixels),
            };
            CharacterSettings csLevel0 = new CharacterSettings()
            {
                Bold = true,
            };



            CreateParagraph(csTitle, RichTextEditorStrings.lblCarsAvailableForRent);

            // create lists of different predefined style types

            CreateParagraph(null, null);
            CreateParagraph(csLevel0, RichTextEditorStrings.lblCatFamilyVans);
            CreateList(
                new string[] { "Honda Odyssey", "Toyota Sienna", "Chrysler Town & Country"},
                "Bullet");

            CreateParagraph(null, null);
            CreateParagraph(csLevel0, RichTextEditorStrings.lblCatSedans);
            CreateList(
                new string[] { "Chevrolet Impala", "Honda Civic", "Ford Fusion", "BMW 6 Series"},
                "BulletCircle");

            CreateParagraph(null, null);
            CreateParagraph(csLevel0, RichTextEditorStrings.lblCatHatchbacks);
            CreateList(
                new string[] { "Audi A3", "Ford Fiesta", "Honda Fit" },
                "BulletSquare");

            CreateParagraph(null, null);
            CreateParagraph(csLevel0, RichTextEditorStrings.lblCatSUVs);
            CreateList(
                new string[] { "Honda CR-V", "Jeep Grand Cherokee", "Toyota RAV4" },
                "BulletArrow");

            CreateParagraph(null, null);
            CreateParagraph(csLevel0, RichTextEditorStrings.lblCatEVs);
            CreateList(
                new string[] { "Nissan Leaf", "Chevrolet Volt", "Toyota Prius" },
                "Bullet_Check");

            CreateParagraph(null, null);
            CreateParagraph(csLevel0, RichTextEditorStrings.lblCatSports);
            CreateList(
                new string[] { "Chevrolet Camaro", "Dodge Viper", "Porsche 911", "Ford Mustang" },
                "BulletDiamond");

            // Note:
            // The predefined style types can be obtained using:
            // this.xamRichTextEditor1.Document.DefaultAvailableStyles
        }

        private void CreateParagraph(CharacterSettings cs, string content)
        {
            RunNode rn = new RunNode();
            if (content != null) rn.SetText(content);
            if (cs != null) rn.Settings = cs;
            ParagraphNode pn = new ParagraphNode();
            pn.ChildNodes.Add(rn);
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);
        }

        private void CreateList(string[] items, string listType)
        {
            DocumentSpan? startSpan = null;
            DocumentSpan? endSpan = null;
            foreach (string item in items)
            {
                TextNode tn = new TextNode();
                tn.Text = item;
                RunNode rn = new RunNode();
                rn.ChildNodes.Add(tn);
                ParagraphNode pn = new ParagraphNode();
                pn.ChildNodes.Add(rn);
                this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);
                if (!startSpan.HasValue)
                {
                    startSpan = pn.GetDocumentSpan();
                }
                endSpan = pn.GetDocumentSpan();
            }
            if (listType != null)
            {
                DocumentSpan mergedSpan = DocumentSpanHelper.GetMergedSpan(startSpan.Value, endSpan.Value);
                string error;
                this.xamRichTextEditor1.Document.ApplyParagraphListStyle(mergedSpan, listType, out error);
            }
        }
    }
}
