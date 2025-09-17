using System.Collections.Generic;
using System.Windows.Media;
using IGSyntaxEditor.Resources;
using Infragistics.Documents;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;

namespace IGSyntaxEditor.Samples.Display
{
    public partial class EditorCustomizations : SampleContainer
    {
        public EditorCustomizations()
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
            colorList.Add(Colors.LightGray);
            colorList.Add(Colors.DarkGray);

            this.cbWSBColor.ItemsSource = colorList;
            this.cbWSBColor.SelectedIndex = 5; // select initially orange

            this.cbCLHBack.ItemsSource = colorList;
            this.cbCLHBack.SelectedIndex = 8; // select initially yellow

            this.cbCLHBorder.ItemsSource = colorList;
            this.cbCLHBorder.SelectedIndex = 7; // select initially red
        }

        private void cbWSBColor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.xamSyntaxEditor1.WhitespaceIndicatorBrush = new SolidColorBrush((Color)this.cbWSBColor.SelectedItem);
        }

        private void cbCLHBack_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.xamSyntaxEditor1.CurrentLineHighlightBackgroundBrush = new SolidColorBrush((Color)this.cbCLHBack.SelectedItem);
        }

        private void cbCLHBorder_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.xamSyntaxEditor1.CurrentLineHighlightBorderBrush = new SolidColorBrush((Color)this.cbCLHBorder.SelectedItem);
        }

    }
}
