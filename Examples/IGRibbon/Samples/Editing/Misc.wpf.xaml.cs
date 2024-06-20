using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Ribbon.Events;
using System.Globalization;

namespace IGRibbon.Samples.Editing
{
    public partial class Misc : SampleContainer
    {
        #region Fields

        private bool isSelectionValid;
        private MemoryStream oldTextInfo;

        #endregion // Fields

        #region Constructor
        public Misc()
        {
            InitializeComponent();
        }
        #endregion Constructor

        #region Properties

        #region IsSelectionValid

        /// <summary>
        /// Gets/sets whether the any text is currently selected in the associated RichTextBox 
        /// </summary>
        public bool IsSelectionValid
        {
            get
            {
                return this.isSelectionValid;
            }
            set
            {
                if (this.isSelectionValid != value)
                {
                    this.isSelectionValid = value;
                    this.xamRibbon.ContextualTabGroups[0].IsVisible = this.isSelectionValid;
                }
            }
        }
        #endregion IsSelectionValid

        #endregion Properties

        #region Private Methods

        #region ParseGalleryItemTag

        private void ParseGalleryItemTag(string tag, out FontFamily fontFamily, out double fontSize, out SolidColorBrush foreBrush, out string fontStyle)
        {
            string[] styleInfo = tag.Split(';');

            // Parse the font family
            fontFamily = new FontFamily(null, styleInfo[0]);
            fontSize = double.Parse(styleInfo[1], CultureInfo.InvariantCulture);

            // Parse the fore-color
            string[] rgb = styleInfo[2].Split(',');
            Color fontColor = Color.FromRgb(byte.Parse(rgb[0]), byte.Parse(rgb[1]), byte.Parse(rgb[2]));
            foreBrush = new SolidColorBrush(fontColor);

            fontStyle = styleInfo[3];
        }
        #endregion ParseGalleryItemTag

        #region ApplyStyleToEditor

        private void ApplyStyleToEditor(FontFamily fontFamily, double fontSize, SolidColorBrush foreBrush, string fontStyle)
        {
            // Check to see whether we should apply the new style only to the selected text
            if (this.IsSelectionValid)
            {
                TextPointer start = this.richTextBox.Selection.Start;
                TextPointer end = this.richTextBox.Selection.End;

                TextRange range = new TextRange(start, end);

                if (fontFamily != null)
                    range.ApplyPropertyValue(RichTextBox.FontFamilyProperty, fontFamily);

                if (fontSize > 0)
                    range.ApplyPropertyValue(RichTextBox.FontSizeProperty, fontSize);

                if (foreBrush != null)
                    range.ApplyPropertyValue(RichTextBox.ForegroundProperty, foreBrush);

                if (fontStyle == "Bold")
                    range.ApplyPropertyValue(RichTextBox.FontWeightProperty, FontWeights.Bold);
                else
                    range.ApplyPropertyValue(RichTextBox.FontWeightProperty, RichTextBox.FontWeightProperty.DefaultMetadata.DefaultValue);

                if (fontStyle == "Italic")
                    range.ApplyPropertyValue(RichTextBox.FontStyleProperty, FontStyles.Italic);
                else
                    range.ApplyPropertyValue(RichTextBox.FontStyleProperty, RichTextBox.FontStyleProperty.DefaultMetadata.DefaultValue);
            }
            else
            {
                TextRange range = new TextRange(this.richTextBox.Document.ContentStart, this.richTextBox.Document.ContentEnd);

                if (fontFamily != null)
                    range.ApplyPropertyValue(RichTextBox.FontFamilyProperty, fontFamily);

                if (fontSize > 0)
                    range.ApplyPropertyValue(RichTextBox.FontSizeProperty, fontSize);

                if (foreBrush != null)
                    range.ApplyPropertyValue(RichTextBox.ForegroundProperty, foreBrush);

                if (fontStyle == "Bold")
                    range.ApplyPropertyValue(RichTextBox.FontWeightProperty, FontWeights.Bold);
                else
                    this.richTextBox.ClearValue(RichTextBox.FontWeightProperty);

                if (fontStyle == "Italic")
                    range.ApplyPropertyValue(RichTextBox.FontStyleProperty, FontStyles.Italic);
                else
                    this.richTextBox.ClearValue(RichTextBox.FontStyleProperty);
            }
        }
        #endregion //ApplyStyleToEditor

        #endregion Private Methods

        #region Event Handlers

        #region OnGalleryItemActivated

        // We want to set either the selected text or the entire document's style to whatever
        // the associated style of the gallery item is, which is the Live Preview feature in Office 2007
        private void OnGalleryItemActivated(object sender, GalleryItemEventArgs e)
        {
            FontFamily newfontFamily;
            double newFontSize;
            SolidColorBrush newForeBrush;
            string newFontStyle;

            if (e.Item != null && e.Item.Tag != null)
            {
                // Parse out the style information from the tag and apply it to the selection/document
                this.ParseGalleryItemTag(e.Item.Tag.ToString(), out newfontFamily, out newFontSize, out newForeBrush, out newFontStyle);
                this.ApplyStyleToEditor(newfontFamily, newFontSize, newForeBrush, newFontStyle);
            }
            else
            {
                // Reset the selection/document to the previous stored style
                if (this.oldTextInfo != null)
                {
                    if (this.IsSelectionValid)
                        this.richTextBox.Selection.Load(this.oldTextInfo, DataFormats.XamlPackage);
                    else
                    {
                        TextRange range = new TextRange(this.richTextBox.Document.ContentStart, this.richTextBox.Document.ContentEnd);
                        range.Load(this.oldTextInfo, DataFormats.XamlPackage);
                    }
                }
            }
        }
        #endregion OnGalleryItemActivated

        #region OnGalleryItemSelected

        private void OnGalleryItemSelected(object sender, GalleryItemEventArgs e)
        {
            FontFamily fontFamily;
            double fontSize;
            SolidColorBrush foreBrush;
            string fontStyle;

            this.ParseGalleryItemTag(e.Item.Tag.ToString(), out fontFamily, out fontSize, out foreBrush, out fontStyle);
            this.ApplyStyleToEditor(fontFamily, fontSize, foreBrush, fontStyle);
        }
        #endregion OnGalleryItemSelected

        #region OnGalleryMenuOpened

        // We want to initialize the styling information for the live preview here so that
        // when a user activates a GalleryItem and then moves away from the area (or the 
        // menu is closed without selecting something), we can revert the text to the 
        // previous value
        private void OnGalleryMenuOpened(object sender, RoutedEventArgs e)
        {
            if (this.oldTextInfo != null)
                this.oldTextInfo.Close();

            this.oldTextInfo = new MemoryStream();
            if (this.IsSelectionValid)
                this.richTextBox.Selection.Save(this.oldTextInfo, DataFormats.XamlPackage);
            else
            {
                TextRange range = new TextRange(this.richTextBox.Document.ContentStart, this.richTextBox.Document.ContentEnd);
                range.Save(this.oldTextInfo, DataFormats.XamlPackage);
            }
        }
        #endregion OnGalleryMenuOpened

        #region OnColorPicked

        private void OnColorPicked(object sender, GalleryItemEventArgs e)
        {
            if (e.Item == null || e.Item.Tag == null)
                return;

            Color newColor = (Color)e.Item.Tag;
            SolidColorBrush newBrush = new SolidColorBrush(newColor);
            if (e.GalleryTool.Id == "globalColorPicker")
            {
                this.richTextBox.SetValue(RichTextBox.ForegroundProperty, newBrush);
            }
            else if (e.GalleryTool.Id == "selectionColorPicker")
            {
                this.richTextBox.Selection.ApplyPropertyValue(RichTextBox.ForegroundProperty, newBrush);
            }
        }
        #endregion OnColorPicked

        #endregion Event Handlers
    }
}