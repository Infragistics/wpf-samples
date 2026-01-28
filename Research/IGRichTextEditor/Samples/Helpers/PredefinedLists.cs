using System.Collections.Generic;
using System.Windows.Media;
using Infragistics.Documents.RichText;

namespace IGRichTextEditor.Samples.Helpers
{
    public class PredefinedLists
    {

        private static List<string> _fontNames;
        public static List<string> FontNames
        {
            get
            {
                if (_fontNames == null)
                {
                    _fontNames = new List<string>();
                    _fontNames.Add("Arial");
                    _fontNames.Add("Calibri");
                    _fontNames.Add("Consolas");
                    _fontNames.Add("Courier New");
                    _fontNames.Add("Tahoma");
                    _fontNames.Add("Times New Roman");
                    _fontNames.Add("Verdana");
                }
                return _fontNames;
            }
        }

        private static List<int> _fontSizes;
        public static List<int> FontSizes
        {
            get
            {
                if (_fontSizes == null)
                {
                    _fontSizes = new List<int>();
                    _fontSizes.Add(8);
                    _fontSizes.Add(9);
                    _fontSizes.Add(10);
                    _fontSizes.Add(11);
                    _fontSizes.Add(12);
                    _fontSizes.Add(14);
                    _fontSizes.Add(16);
                    _fontSizes.Add(18);
                    _fontSizes.Add(20);
                    _fontSizes.Add(22);
                    _fontSizes.Add(24);
                    _fontSizes.Add(28);
                    _fontSizes.Add(36);
                    _fontSizes.Add(48);
                    _fontSizes.Add(72);
                }
                return _fontSizes;
            }
        }

        private static List<Color> _colorsItemsSource;
        public static List<Color> ForeColors
        {
            get
            {
                if (_colorsItemsSource == null)
                {
                    _colorsItemsSource = new List<Color>();
                    _colorsItemsSource.Add(Colors.Black);
                    _colorsItemsSource.Add(Colors.Blue);
                    _colorsItemsSource.Add(Colors.Brown);
                    _colorsItemsSource.Add(Colors.Green);
                    _colorsItemsSource.Add(Colors.Magenta);
                    _colorsItemsSource.Add(Colors.Orange);
                    _colorsItemsSource.Add(Colors.Purple);
                    _colorsItemsSource.Add(Colors.Red);
                    _colorsItemsSource.Add(Colors.Yellow);
                    _colorsItemsSource.Add(Colors.White);
                    _colorsItemsSource.Add(Colors.LightGray);
                    _colorsItemsSource.Add(Colors.DarkGray);
                }
                return _colorsItemsSource;
            }
        }

        private static List<HighlightColor> _highlightColors;
        public static List<HighlightColor> HighlightColors
        {
            get
            {
                if (_highlightColors == null)
                {
                    _highlightColors = new List<HighlightColor>();
                    _highlightColors.Add(HighlightColor.None);
                    _highlightColors.Add(HighlightColor.Black);
                    _highlightColors.Add(HighlightColor.Blue);
                    _highlightColors.Add(HighlightColor.Cyan);
                    _highlightColors.Add(HighlightColor.Green);
                    _highlightColors.Add(HighlightColor.Magenta);
                    _highlightColors.Add(HighlightColor.Red);
                    _highlightColors.Add(HighlightColor.Yellow);
                    _highlightColors.Add(HighlightColor.White);
                    _highlightColors.Add(HighlightColor.DarkBlue);
                    _highlightColors.Add(HighlightColor.DarkCyan);
                    _highlightColors.Add(HighlightColor.DarkGreen);
                    _highlightColors.Add(HighlightColor.DarkMagenta);
                    _highlightColors.Add(HighlightColor.DarkRed);
                    _highlightColors.Add(HighlightColor.DarkYellow);
                    _highlightColors.Add(HighlightColor.DarkGray);
                    _highlightColors.Add(HighlightColor.LightGray);
                }
                return _highlightColors;
            }
        }


    }
}
