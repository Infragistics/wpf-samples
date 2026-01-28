using System.Windows.Media;
using IGRichTextEditor.Resources;
using IGRichTextEditor.Samples.Helpers;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Data
{
    public partial class DefiningHierarchicalLists : SampleContainer
    {
        public DefiningHierarchicalLists()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            // create character settings for use accros the lists
            CharacterSettings csLevel10 = new CharacterSettings()
            {
                FontSize = new Extent(18, ExtentUnitType.LogicalPixels),
                Color = new ColorInfo(new Color() { A = 255, R = 33, G = 88, B = 104 })
            };
            CharacterSettings csLevel11 = new CharacterSettings()
            {
                FontSize = new Extent(16, ExtentUnitType.LogicalPixels),
                Bold = true,
                Color = new ColorInfo(new Color() { A = 255, R = 49, G = 132, B = 172 })
            };
            CharacterSettings csLevel121 = new CharacterSettings()
            {
                Italics = true,
            };
            CharacterSettings csLevel122 = new CharacterSettings()
            {
                Bold = true,
                Color = new ColorInfo(new Color() { A = 255, R = 49, G = 132, B = 155 })
            };
            CharacterSettings csLevel20 = new CharacterSettings()
            {
                FontSize = new Extent(18, ExtentUnitType.LogicalPixels)
            };
            CharacterSettings csLevel21 = new CharacterSettings()
            {
                FontSize = new Extent(16, ExtentUnitType.LogicalPixels),
            };
            CharacterSettings csLevel222 = new CharacterSettings()
            {
                Bold = true
            };

            CreateListItem(null, RichTextEditorStrings.lblPeriodicTableElements, csLevel20);

            DocumentSpan startSpan;
            DocumentSpan endSpan;
            DocumentSpan mergedSpan;
            string error = null;

            // create hierarchical bulleted list
            startSpan = CreateListItem(0, RichTextEditorStrings.lblNobleGases, csLevel10);
            CreateListItem(1, RichTextEditorStrings.lblHelium, csLevel11);
            CreateListItem(2, RichTextEditorStrings.lblSymbol, "He", csLevel121, csLevel122);
            CreateListItem(2, RichTextEditorStrings.lblAtomicWeight, "4.0026", csLevel121, csLevel122);
            CreateListItem(1, RichTextEditorStrings.lblNeon, csLevel11);
            CreateListItem(2, RichTextEditorStrings.lblSymbol, "Ne", csLevel121, csLevel122);
            CreateListItem(2, RichTextEditorStrings.lblAtomicWeight, "20.1797", csLevel121, csLevel122);
            CreateListItem(1, RichTextEditorStrings.lblArgon, csLevel11);
            CreateListItem(2, RichTextEditorStrings.lblSymbol, "Ar", csLevel121, csLevel122);
            CreateListItem(2, RichTextEditorStrings.lblAtomicWeight, "39.9480", csLevel121, csLevel122);

            CreateListItem(0, RichTextEditorStrings.lblPreciousMetals, csLevel10);
            CreateListItem(1, RichTextEditorStrings.lblPalladium, csLevel11);
            CreateListItem(2, RichTextEditorStrings.lblSymbol, "Pd", csLevel121, csLevel122);
            CreateListItem(2, RichTextEditorStrings.lblAtomicWeight, "106.4000", csLevel121, csLevel122);
            CreateListItem(1, RichTextEditorStrings.lblIridium, csLevel11);
            CreateListItem(2, RichTextEditorStrings.lblSymbol, "Ir", csLevel121, csLevel122);
            CreateListItem(2, RichTextEditorStrings.lblAtomicWeight, "192.2170", csLevel121, csLevel122);
            CreateListItem(1, RichTextEditorStrings.lblPlatinum, csLevel11);
            CreateListItem(2, RichTextEditorStrings.lblSymbol, "Pt", csLevel121, csLevel122);
            endSpan = CreateListItem(2, RichTextEditorStrings.lblAtomicWeight, "195.0840", csLevel121, csLevel122);
        
            // create a merged document span for all paragraphs used in this list
            mergedSpan = DocumentSpanHelper.GetMergedSpan(startSpan, endSpan);
            // apply the list style
            this.xamRichTextEditor1.Document.ApplyParagraphListStyle(mergedSpan, "Bullet", out error);

            // create an empty separator paragraph
            CreateListItem(null, string.Empty, null);

            // create hierarchical numbered list
            startSpan = CreateListItem(0, RichTextEditorStrings.lblHalogens, csLevel20);
            CreateListItem(1, RichTextEditorStrings.lblFluorine, csLevel21);
            CreateListItem(2, RichTextEditorStrings.lblSymbol, "F", null, csLevel222);
            CreateListItem(2, RichTextEditorStrings.lblAtomicWeight, "18.9984", null, csLevel222);
            CreateListItem(1, RichTextEditorStrings.lblChlorine, csLevel21);
            CreateListItem(2, RichTextEditorStrings.lblSymbol, "Cl", null, csLevel222);
            CreateListItem(2, RichTextEditorStrings.lblAtomicWeight, "35.4530", null, csLevel222);
            CreateListItem(1, RichTextEditorStrings.lblBromine, csLevel21);
            CreateListItem(2, RichTextEditorStrings.lblSymbol, "Br", null, csLevel222);
            CreateListItem(2, RichTextEditorStrings.lblAtomicWeight, "79.9040", null, csLevel222);

            CreateListItem(0, RichTextEditorStrings.lblCoinageMetal, csLevel20);
            CreateListItem(1, RichTextEditorStrings.lblCopper, csLevel21);
            CreateListItem(2, RichTextEditorStrings.lblSymbol, "Cu", null, csLevel222);
            CreateListItem(2, RichTextEditorStrings.lblAtomicWeight, "63.5460", null, csLevel222);
            CreateListItem(1, RichTextEditorStrings.lblSilver, csLevel21);
            CreateListItem(2, RichTextEditorStrings.lblSymbol, "Ag", null, csLevel222);
            CreateListItem(2, RichTextEditorStrings.lblAtomicWeight, "107.8682", null, csLevel222);
            CreateListItem(1, RichTextEditorStrings.lblGold, csLevel21);
            CreateListItem(2, RichTextEditorStrings.lblSymbol, "Au", null, csLevel222);
            endSpan = CreateListItem(2, RichTextEditorStrings.lblAtomicWeight, "196.9665", null, csLevel222);

            // create a merged document span for all paragraphs used in this list
            mergedSpan = DocumentSpanHelper.GetMergedSpan(startSpan, endSpan);
            // apply the list style
            this.xamRichTextEditor1.Document.ApplyParagraphListStyle(mergedSpan, "Decimal", out error);

            // Note:
            // The predefined style types can be obtained using:
            // this.xamRichTextEditor1.Document.DefaultAvailableStyles
        }

        private DocumentSpan CreateListItem(int? listLevel, string text, CharacterSettings cs)
        {
            RunNode rn = new RunNode();
            rn.SetText(text);
            if (cs != null) rn.Settings = cs;
            ParagraphNode pn = new ParagraphNode();
            pn.ChildNodes.Add(rn);
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);
            if (listLevel.HasValue)
            {
                // set the list item level
                ParagraphSettings settings = new ParagraphSettings();
                settings.ListLevel = listLevel.Value;
                pn.Settings = settings;
            }
            return pn.GetDocumentSpan();
        }

        private DocumentSpan CreateListItem(int? listLevel, string text1, string text2, CharacterSettings cs1, CharacterSettings cs2)
        {
            ParagraphNode pn = new ParagraphNode();
            RunNode rn = new RunNode();
            rn.SetText(text1);
            if (cs1 != null) rn.Settings = cs1;
            pn.ChildNodes.Add(rn);
            rn = new RunNode();
            rn.SetText(text2);
            if (cs2 != null) rn.Settings = cs2;
            pn.ChildNodes.Add(rn);
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);
            if (listLevel.HasValue)
            {
                // set the list item level
                ParagraphSettings settings = new ParagraphSettings();
                settings.ListLevel = listLevel.Value;
                pn.Settings = settings;
            }
            return pn.GetDocumentSpan();
        }
    }
}
