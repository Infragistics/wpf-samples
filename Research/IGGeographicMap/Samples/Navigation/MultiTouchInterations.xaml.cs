
using IGGeographicMap.Models;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Samples
{
    public partial class MultiTouchInterations : Infragistics.Samples.Framework.SampleContainer
    {
        public MultiTouchInterations()
        {
            InitializeComponent();

            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class 
        }

        protected override void OnSampleLoaded(System.EventArgs e)
        {
            // load Metro themes that provides style for controls with bigger target sizes 
            // that are designed for touch screen interactions
            ThemeLoader.SetTheme(this, ThemeType.MetroLight);
        }

        private void ShowCrosshairTootlipsOnDragCheckBox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var isChecked = this.ShowCrosshairTootlipsOnDragCheckBox.IsChecked;
            this.GeoMap.IsDragCrosshairEnabled = isChecked.HasValue ? isChecked.Value : false;
        }

        private void ShowCrosshairLinesCheckBox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var isChecked = this.ShowCrosshairLinesCheckBox.IsChecked;
            this.GeoMap.CrosshairVisibility = isChecked.HasValue && isChecked.Value ? 
                System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
    }

 
}
