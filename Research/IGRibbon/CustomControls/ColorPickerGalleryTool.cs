using System;
using System.Windows;
using System.Windows.Media;
using IGRibbon.Resources;
using Infragistics.Windows.Ribbon;

namespace IGRibbon.CustomControls
{
    public class ColorPickerGalleryTool : GalleryTool
    {
        #region Members

        // The number of columns we want to have in the color picker
        private const int GalleryColumnItems = 10;

        // The list of colors that will exist in the "Theme Colors" group.
        // These colors are arranged as they will appear in the color picker
        // (i.e. the first item in the third row (216,216,216) is the same color
        // that appears in the same position in the color picker)
        private static Color[] ThemeColors = new Color[] 
        {
            Color.FromRgb(255,255,255),Color.FromRgb(000,000,000),Color.FromRgb(238,236,225),Color.FromRgb(031,073,125),
            Color.FromRgb(079,129,189),Color.FromRgb(192,080,077),Color.FromRgb(155,187,089),Color.FromRgb(128,100,162),
            Color.FromRgb(075,172,198),Color.FromRgb(247,150,070),
            Color.FromRgb(242,242,242),Color.FromRgb(127,127,127),Color.FromRgb(221,217,195),Color.FromRgb(198,217,240),
            Color.FromRgb(219,229,241),Color.FromRgb(242,220,219),Color.FromRgb(235,241,221),Color.FromRgb(229,224,236),
            Color.FromRgb(219,238,243),Color.FromRgb(253,234,218),
            Color.FromRgb(216,216,216),Color.FromRgb(089,089,089),Color.FromRgb(196,189,151),Color.FromRgb(141,179,226),
            Color.FromRgb(184,204,228),Color.FromRgb(229,185,183),Color.FromRgb(215,227,188),Color.FromRgb(204,193,217),
            Color.FromRgb(183,221,232),Color.FromRgb(251,213,181),
            Color.FromRgb(191,191,191),Color.FromRgb(063,063,063),Color.FromRgb(147,137,083),Color.FromRgb(084,141,212),
            Color.FromRgb(149,179,215),Color.FromRgb(217,150,148),Color.FromRgb(195,214,155),Color.FromRgb(178,162,199),
            Color.FromRgb(146,205,220),Color.FromRgb(250,192,143),
            Color.FromRgb(165,165,165),Color.FromRgb(038,038,038),Color.FromRgb(073,068,041),Color.FromRgb(023,054,093),
            Color.FromRgb(054,096,146),Color.FromRgb(149,055,052),Color.FromRgb(118,146,060),Color.FromRgb(095,073,122),
            Color.FromRgb(049,133,155),Color.FromRgb(227,108,009),
            Color.FromRgb(127,127,127),Color.FromRgb(012,012,012),Color.FromRgb(029,027,016),Color.FromRgb(015,036,062),
            Color.FromRgb(036,064,097),Color.FromRgb(099,036,035),Color.FromRgb(079,097,040),Color.FromRgb(063,049,081),
            Color.FromRgb(032,088,103),Color.FromRgb(151,072,006),

        };

        // The list of standard colors, whose length should be equal to GalleryColumnItems
        private static Color[] StandardColors = new Color[] 
        {
            Color.FromRgb(192,000,000), Color.FromRgb(255,000,000), Color.FromRgb(255,192,000), Color.FromRgb(255,255,000), Color.FromRgb(146,208,080),
            Color.FromRgb(000,176,080), Color.FromRgb(000,176,240), Color.FromRgb(000,112,192), Color.FromRgb(000,032,096), Color.FromRgb(112,048,160),
        };

        #endregion //Members

        #region Constructors

        static ColorPickerGalleryTool()
        {
            // Override the default values for MinDropDownColumns and MaxDropDownColumns.
            // This has the benefit over simply setting the propery in the constructor in that if
            // the user decides to change this property and then later reset it through a call to
            // ClearValue(...), it will be reset to our new default instead of the GalleryTool default.
            MinDropDownColumnsProperty.OverrideMetadata(typeof(ColorPickerGalleryTool), new FrameworkPropertyMetadata(GalleryColumnItems));
            MaxDropDownColumnsProperty.OverrideMetadata(typeof(ColorPickerGalleryTool), new FrameworkPropertyMetadata(GalleryColumnItems));
        }

        public ColorPickerGalleryTool()
            : base()
        {
            // We never want to display any text, so set the TextDisplayMode so that there
            // will not be any space reserved for a text element
            this.ItemSettings = new GalleryItemSettings();
            this.ItemSettings.TextDisplayMode = GalleryItemTextDisplayMode.Never;

            // Create the border pen and the image rectangle here since they will be the same
            // for all of the items
            Pen borderPen = new Pen(new SolidColorBrush(Color.FromRgb(197, 197, 197)), 1.0);
            RectangleGeometry imageRect = new RectangleGeometry(new System.Windows.Rect(0, 0, 13, 13));

            #region Create Themed Colors Group

            GalleryItemGroup themeColorGroup = new GalleryItemGroup();
            themeColorGroup.Title = RibbonStrings.InteractingWithTheRibbon_Misc_ColorPickerGalleryTool_ThemeColors;
            for (int i = 0; i < ThemeColors.Length; i++)
            {
                Color themeColor = ThemeColors[i];

                // Create a new GalleryItem for the color and store the color in the Tag
                GalleryItem themeItem = new GalleryItem();
                themeItem.Key = String.Format("Themed Color {0}", i.ToString());
                themeItem.Tag = themeColor;

                // Create the drawing that will display the color of the item
                GeometryDrawing imageGeometryDrawing = new GeometryDrawing(
                    new SolidColorBrush(themeColor),
                    borderPen,
                    imageRect);

                themeItem.Image = new DrawingImage(imageGeometryDrawing);

                // Add the key of the item to the group and add the item itself to the tool's
                // collection of items.
                themeColorGroup.ItemKeys.Add(themeItem.Key);
                this.Items.Add(themeItem);
            }
            this.Groups.Add(themeColorGroup);

            #endregion //Create Themed Colors Group

            #region Create Standard Colors Group

            GalleryItemGroup standardColorGroup = new GalleryItemGroup();
            standardColorGroup.Title = RibbonStrings.InteractingWithTheRibbon_Misc_ColorPickerGalleryTool_StandardColors;
            for (int j = 0; j < StandardColors.Length; j++)
            {
                Color standardColor = StandardColors[j];

                // Create a new GalleryItem for the color and store the color in the Tag
                GalleryItem standardItem = new GalleryItem();
                standardItem.Key = String.Format("Standard Color {0}", j.ToString());
                standardItem.Tag = standardColor;

                // Create the drawing that will display the color of the item
                GeometryDrawing imageGeometryDrawing = new GeometryDrawing(
                    new SolidColorBrush(standardColor),
                    borderPen,
                    imageRect);

                standardItem.Image = new DrawingImage(imageGeometryDrawing);

                // Add the key of the item to the group and add the item itself to the tool's
                // collection of items.
                standardColorGroup.ItemKeys.Add(standardItem.Key);
                this.Items.Add(standardItem);
            }
            this.Groups.Add(standardColorGroup);

            #endregion //Create Standard Colors Group
        }
        #endregion //Constructors
    }
}