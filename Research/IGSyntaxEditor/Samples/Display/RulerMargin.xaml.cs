using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using IGSyntaxEditor.Resources;
using Infragistics.Documents;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;

namespace IGSyntaxEditor.Samples.Display
{
    public partial class RulerMargin : SampleContainer
    {
        public RulerMargin()
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
            colorList.Add(Colors.LightGray);
            colorList.Add(Colors.DimGray);

            this.cbRMBack.ItemsSource = colorList;
            this.cbRMBack.SelectedIndex = 10;

            this.cbRMBorder.ItemsSource = colorList;
            this.cbRMBorder.SelectedIndex = 11;

            this.cbRMTickMark.ItemsSource = colorList;
            this.cbRMTickMark.SelectedIndex = 0;

            this.cbRMCaret.ItemsSource = colorList;
            this.cbRMCaret.SelectedIndex = 7;

            this.cbRMText.ItemsSource = colorList;
            this.cbRMText.SelectedIndex = 0;
        }

        private void cbRMBack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.xamSyntaxEditor1.RulerMarginBackground = new SolidColorBrush((Color)this.cbRMBack.SelectedItem);
        }

        private void cbRMBorder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.xamSyntaxEditor1.RulerMarginBorderBrush = new SolidColorBrush((Color)this.cbRMBorder.SelectedItem);
        }

        private void cbRMTickMark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.xamSyntaxEditor1.RulerMarginTickMarkBrush = new SolidColorBrush((Color)this.cbRMTickMark.SelectedItem);
        }

        private void cbRMCaret_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.xamSyntaxEditor1.RulerMarginCaretPositionHighlightBrush = new SolidColorBrush((Color)this.cbRMCaret.SelectedItem);
        }

        private void cbRMText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.xamSyntaxEditor1.RulerMarginTextBrush = new SolidColorBrush((Color)this.cbRMText.SelectedItem);
        }
    }
}
