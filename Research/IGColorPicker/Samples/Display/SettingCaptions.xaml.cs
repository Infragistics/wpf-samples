using System.Windows;
using Infragistics.Samples.Framework;

namespace IGColorPicker.Samples.Display
{
    public partial class SettingCaptions : SampleContainer
    {
        public SettingCaptions()
        {
            InitializeComponent();
        }

        private void AddItems_Click(object sender, RoutedEventArgs e)
        {
            // Set Color Palette Caption
            this.MyColorPicker.CurrentColorCaption = this.CurrentColorName.Text;

            // Set Current Palette Caption
            this.MyColorPicker.CurrentPaletteCaption = this.CurrentPaletteName.Text;

            // Set Derived Palette Caption
            this.MyColorPicker.DerivedColorPalettesCaption = this.DerivedPaletteName.Text;

            //Set Recent Palette Caption
            this.MyColorPicker.RecentColorPaletteCaption = this.RecentColorName.Text;
        }
    }
}
