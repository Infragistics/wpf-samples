using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using IGSyntaxEditor.Resources;
using Infragistics.Documents;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;

namespace IGSyntaxEditor.Samples.Display
{
    public partial class LineNumberMargin : SampleContainer
    {
        public LineNumberMargin()
        {
            InitializeComponent();
            InitializeSample();
        }

        private void InitializeSample()
        {
            this.UseDefaultTheme = true;
            var td = new TextDocument();
            td.Language = PlainTextLanguage.Instance;
            td.InitializeText(SyntaxEditorStrings.LoremIpsum);
            this.xamSyntaxEditor1.Document = td;

            // fill colors in the foreground and background combo boxes
            var colorList = new List<Color>();
            colorList.Add(Colors.Black);
            colorList.Add(Colors.DarkOrange);
            colorList.Add(Colors.OrangeRed);
            colorList.Add(Colors.Crimson);
            colorList.Add(Colors.DeepPink);
            colorList.Add(Colors.Purple);
            colorList.Add(Colors.DodgerBlue);
            colorList.Add(Colors.DarkGreen);
            colorList.Add(Colors.YellowGreen);
            colorList.Add(Colors.White);
            this.cbFColor.ItemsSource = colorList;
            this.cbFColor.SelectedIndex = 0; // select initially black
            this.cbBColor.ItemsSource = colorList;
            this.cbBColor.SelectedIndex = 9; // select initially white

            // select "Consolas" as initial font
            this.comboFontFamily.SelectedIndex = 2;
        }

        private void cbFColor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.xamSyntaxEditor1.LineNumberMarginForeground = new SolidColorBrush((Color)this.cbFColor.SelectedItem);
        }

        private void cbBColor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.xamSyntaxEditor1.LineNumberMarginBackground = new SolidColorBrush((Color)this.cbBColor.SelectedItem);
        }

        private void comboFontFamily_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var ff = new FontFamily((String)this.comboFontFamily.SelectedItem);
            this.xamSyntaxEditor1.LineNumberMarginFontFamily = ff;
        }

        private void cbBold_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbBold.IsChecked.HasValue)
            {
                if (this.cbBold.IsChecked.Value == true)
                {
                    this.xamSyntaxEditor1.LineNumberMarginFontWeight = FontWeights.Bold;
                }
                else
                {
                    this.xamSyntaxEditor1.LineNumberMarginFontWeight = FontWeights.Normal;
                }
            }
        }

        private void cbItalic_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbItalic.IsChecked.HasValue)
            {
                if (this.cbItalic.IsChecked.Value == true)
                {
                    this.xamSyntaxEditor1.LineNumberMarginFontStyle = FontStyles.Italic;
                }
                else
                {
                    this.xamSyntaxEditor1.LineNumberMarginFontStyle = FontStyles.Normal;
                }
            }
        }

    }
}
