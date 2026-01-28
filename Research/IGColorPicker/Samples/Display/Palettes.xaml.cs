using System.Windows;
using System.Windows.Media;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;

namespace IGColorPicker.Samples.Display
{
    public partial class Palettes : SampleContainer
    {
        public Palettes()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            //Create Custom Palette
            ColorPalette MyColorPalette = new ColorPalette();
            MyColorPalette.Colors.Add(Colors.Red);
            MyColorPalette.Colors.Add(Colors.Orange);
            MyColorPalette.Colors.Add(Colors.Yellow);
            MyColorPalette.Colors.Add(Colors.Green);
            MyColorPalette.Colors.Add(Colors.Blue);
            MyColorPalette.Colors.Add(Colors.Purple);
            MyColorPalette.Colors.Add(Colors.Magenta);
            MyColorPalette.Colors.Add(Colors.Red);
            MyColorPalette.Colors.Add(Colors.Orange);
            MyColorPalette.Colors.Add(Colors.Yellow);

            if (this.MyColorPicker.ColorPalettes.Count <= 2)
                this.MyColorPicker.ColorPalettes.Add(MyColorPalette);
        }
    }
}
