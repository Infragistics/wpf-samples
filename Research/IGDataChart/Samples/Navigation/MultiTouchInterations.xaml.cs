
using Infragistics.Samples.Framework;

namespace IGDataChart.Samples.Editing
{
    public partial class MultiTouchInterations : Infragistics.Samples.Framework.SampleContainer
    {
        public MultiTouchInterations()
        {
            InitializeComponent();
        }
        private void ShowCrosshairTootlipsOnDragCheckBox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var isChecked = this.ShowCrosshairTootlipsOnDragCheckBox.IsChecked;
            this.DataChart.IsDragCrosshairEnabled = isChecked.HasValue ? isChecked.Value : false;
        }

        private void ShowCrosshairLinesCheckBox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var isChecked = this.ShowCrosshairLinesCheckBox.IsChecked;
            this.DataChart.CrosshairVisibility = isChecked.HasValue && isChecked.Value ?
                System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        protected override void OnSampleLoaded(System.EventArgs e)
        {
            // load Metro themes that provides style for controls with bigger target sizes 
            // that are designed for touch screen interactions
            ThemeLoader.SetTheme(this, ThemeType.MetroLight);
        }
    }
}
