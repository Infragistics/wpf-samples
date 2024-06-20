using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace IGDataChart.Samples.Helpers
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

        private static List<string> _fontStretches;
        public static List<string> FontStretches
        {
            get
            {
                if (_fontStretches == null)
                {
                    _fontStretches = new List<string>();
                    _fontStretches.Add("Normal");
                    _fontStretches.Add("Condensed");
                }
                return _fontStretches;
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

        private static List<string> _fontWeights;
        public static List<string> FontWeights
        {
            get
            {
                if (_fontWeights == null)
                {
                    _fontWeights = new List<string>();
                    _fontWeights.Add("Regular");
                    _fontWeights.Add("Bold");
                }                     
                return _fontWeights;
            }
        }

        private static List<string> _colorsItemsSource;
        public static List<string> ForeColors
        {
            get
            {
                if (_colorsItemsSource == null)
                {
                    _colorsItemsSource = new List<string>();
                    _colorsItemsSource.Add("Black");
                    _colorsItemsSource.Add("Blue");
                    _colorsItemsSource.Add("Brown");
                    _colorsItemsSource.Add("Green");
                    _colorsItemsSource.Add("Magenta");
                    _colorsItemsSource.Add("Orange");
                    _colorsItemsSource.Add("Purple");
                    _colorsItemsSource.Add("Red");
                    _colorsItemsSource.Add("Yellow");
                    _colorsItemsSource.Add("White");
                    _colorsItemsSource.Add("LightGray");
                    _colorsItemsSource.Add("DarkGray");
                }
                return _colorsItemsSource;
            }
        }

        private static List<SolidColorBrush> _highlightColors;
        public static List<SolidColorBrush> HighlightColors
        {
            get
            {
                if (_highlightColors == null)
                {
                    _highlightColors = new List<SolidColorBrush>();
                    _highlightColors.Add(new SolidColorBrush(Colors.Transparent));
                    _highlightColors.Add(new SolidColorBrush(Colors.Black));
                    _highlightColors.Add(new SolidColorBrush(Colors.Blue));
                    _highlightColors.Add(new SolidColorBrush(Colors.Cyan));
                    _highlightColors.Add(new SolidColorBrush(Colors.Green));
                    _highlightColors.Add(new SolidColorBrush(Colors.Magenta));
                    _highlightColors.Add(new SolidColorBrush(Colors.Red));
                    _highlightColors.Add(new SolidColorBrush(Colors.Yellow));
                    _highlightColors.Add(new SolidColorBrush(Colors.White));
                    _highlightColors.Add(new SolidColorBrush(Colors.DarkGray));
                    _highlightColors.Add(new SolidColorBrush(Colors.LightGray));
                }
                return _highlightColors;
            }
        }
    }
}
