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
    public partial class EditorFont : SampleContainer
    {
        public EditorFont()
        {
            InitializeComponent();
            InitializeSample();
        }

        public void InitializeSample()
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
            this.cbBColor.ItemsSource = colorList;
            this.cbBColor.SelectedIndex = 9; // select initially white

            // select "Consolas" as initial font
            this.comboFontFamily.SelectedIndex = 2;

            // select font size 12 as initial
            this.comboFontSize.SelectedIndex = 3;
        }

        private void comboFontFamily_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var ff = new FontFamily((String)this.comboFontFamily.SelectedItem);
            this.xamSyntaxEditor1.FontFamily = ff;
        }

        private void comboFontSize_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            double fs = double.Parse(this.comboFontSize.SelectedItem as string);
            this.xamSyntaxEditor1.FontSize = fs;
        }

        private void cbBold_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbBold.IsChecked.HasValue)
            {
                if (this.cbBold.IsChecked.Value == true)
                {
                    this.xamSyntaxEditor1.FontWeight = FontWeights.Bold;
                }
                else
                {
                    this.xamSyntaxEditor1.FontWeight = FontWeights.Normal;
                }
            }
        }

        private void cbItalic_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbItalic.IsChecked.HasValue)
            {
                if (this.cbItalic.IsChecked.Value == true)
                {
                    this.xamSyntaxEditor1.FontStyle = FontStyles.Italic;
                }
                else
                {
                    this.xamSyntaxEditor1.FontStyle = FontStyles.Normal;
                }
            }
        }

        private void cbBColor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.xamSyntaxEditor1.Background = new SolidColorBrush((Color)this.cbBColor.SelectedItem);
        }
    }
}
