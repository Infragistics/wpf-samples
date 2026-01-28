using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGRibbon.Resources;
using Infragistics.Samples.Framework;

namespace IGRibbon.Samples.Display
{
    public partial class ApplicationMenu2010 : SampleContainer
    {
        public ApplicationMenu2010()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DefaultColorCombo.ItemsSource = loadItems();
        }

        private void DefaultColorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedColor = ((ComboBox)sender).SelectedValue;

            this.MyRibbon.ApplicationMenu2010.TrimColor = new SolidColorBrush((Color)selectedColor);
        }

        private void AddItems_Click(object sender, RoutedEventArgs e)
        {
            this.MyRibbon.ApplicationMenu2010.Caption = this.CurrentCaptionName.Text;
        }

        private List<ColorString> loadItems()
        {
            var brushList = new List<ColorString>
            {
                new ColorString() {Color = Colors.Red, ColorName = RibbonStrings.XWR_Red},
                new ColorString() {Color = Colors.Blue, ColorName = RibbonStrings.XWR_Blue},
                new ColorString() {Color = Colors.Yellow, ColorName = RibbonStrings.XWR_Yellow},
                new ColorString() {Color = Colors.Green, ColorName = RibbonStrings.XWR_Green}
            };

            return brushList;
        }
    }

    public class ColorString
    {
        public Color Color { get; set; }
        public string ColorName { get; set; }
    }
}
