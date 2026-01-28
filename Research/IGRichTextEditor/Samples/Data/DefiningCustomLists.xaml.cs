using System.Windows.Media;
using IGRichTextEditor.Resources;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Data
{
    public partial class DefiningCustomLists : SampleContainer
    {
        public DefiningCustomLists()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            // Create a paragraph settings with no top and bottom spacing
            ParagraphSettings listParagraphSettings = new ParagraphSettings();
            listParagraphSettings.Spacing = new ParagraphSpacingSettings()
            {
                AfterParagraph = new ParagraphVerticalSpacing(new Extent(0, ExtentUnitType.LogicalPixels)),
                BeforeParagraph = new ParagraphVerticalSpacing(new Extent(0, ExtentUnitType.LogicalPixels)),
            };
            listParagraphSettings.Indentation = new ParagraphIndentationSettings()
            {
                Start = new Indentation(new Extent(20, ExtentUnitType.LogicalPixels)),
            };

            // create e list template with one level definition which is using
            // decimals, bold style and green color for the numbering
            ListTemplate greenListTemplate = new ListTemplate()
            {
                LevelDefinitions = new ListLevelDefinitionCollection() { 
                    new ListLevelDefinition()
                    {
                        Level = 0,
                        LevelText = "%1 TAKE",
                        NumberFormat = NumberFormat.DecimalEnclosedFullstop,
                        NumberSuffix = NumberSuffix.Space,
                        NumberSettings = new CharacterSettings()
                        {
                            Bold = true,
                            Color = new ColorInfo(new Color() { A = 255, R = 118, G = 146, B = 60 })
                        },
                        ParagraphSettings = listParagraphSettings,
                    }
                }
            };

            // create a rich text list using the above template
            RichTextList greenRichTextList = new RichTextList()
            {
                Id = "MyGreenRichTextList",
                Template = greenListTemplate
            };
            this.xamRichTextEditor1.Document.RootNode.Lists.Add(greenRichTextList);

            CreateParagraph(false, false, new Extent(18, ExtentUnitType.LogicalPixels), RichTextEditorStrings.lblThingsToTake);

            CreateParagraph("MyGreenRichTextList", 0, RichTextEditorStrings.lblSunglasses);
            CreateParagraph("MyGreenRichTextList", 0, RichTextEditorStrings.lblSwimsuit);
            CreateParagraph("MyGreenRichTextList", 0, RichTextEditorStrings.lblSunUmbrella);
            CreateParagraph("MyGreenRichTextList", 0, RichTextEditorStrings.lblFriends);
            CreateParagraph("MyGreenRichTextList", 0, RichTextEditorStrings.lblGoodMood);



            // create e list template with one level definition which is using
            // decimals, bold style and red color for the numbering
            ListTemplate redListTemplate = new ListTemplate()
            {
                LevelDefinitions = new ListLevelDefinitionCollection() { 
                    new ListLevelDefinition()
                    {
                        Level = 0,
                        LevelText = "%1 FORGET",
                        NumberFormat = NumberFormat.DecimalEnclosedFullstop,
                        NumberSuffix = NumberSuffix.Space,
                        
                        NumberSettings = new CharacterSettings ()
                        {
                            Bold = true,
                            Color = new ColorInfo(new Color() { A = 255, R = 204, G = 0, B = 0 })
                        },
                        ParagraphSettings = listParagraphSettings,
                    }
                }
            };

            // create list style using the above template
            ListStyle redListStyle = new ListStyle()
            {
                Id = "MyRedListStyle",
                Template = redListTemplate
            };
            this.xamRichTextEditor1.Document.RootNode.Styles.Add(redListStyle);

            // create a rich text list using the list style
            RichTextList redRichTextList = new RichTextList()
            {
                Id = "MyRedRichTextList",
                ListStyleId = "MyRedListStyle"
            };
            this.xamRichTextEditor1.Document.RootNode.Lists.Add(redRichTextList);

            CreateParagraph(null, 0, string.Empty);
            CreateParagraph(false, false, new Extent(18, ExtentUnitType.LogicalPixels), RichTextEditorStrings.lblThingsToForget);

            CreateParagraph("MyRedRichTextList", 0, RichTextEditorStrings.lblWork);
            CreateParagraph("MyRedRichTextList", 0, RichTextEditorStrings.lblComputer);
            CreateParagraph("MyRedRichTextList", 0, RichTextEditorStrings.lblProjects);
            CreateParagraph("MyRedRichTextList", 0, RichTextEditorStrings.lblTargetDates);
            CreateParagraph("MyRedRichTextList", 0, RichTextEditorStrings.lblBadMood);
        }

        private void CreateParagraph(string listId, int listLevel, string content)
        {
            ParagraphNode pn = new ParagraphNode();
            pn.SetText(content);
            if (listId != null)
            {
                pn.Settings = new ParagraphSettings()
                {
                    ListId = listId,
                    ListLevel = listLevel,
                };
            }
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);
        }

        private void CreateParagraph(bool bold, bool italic, Extent fontSize, string content)
        {
            CharacterSettings cs = new CharacterSettings();
            cs.Bold = bold;
            cs.Italics = italic;
            if (fontSize != null)
                cs.FontSize = fontSize;
            RunNode rn = new RunNode();
            rn.SetText(content);
            rn.Settings = cs;
            ParagraphNode pn = new ParagraphNode();
            pn.ChildNodes.Add(rn);
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);
        }
    }
}
