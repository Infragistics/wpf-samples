

namespace IGDataChart.Samples.Display.Markers
{
    public partial class BubbleMarkers : Infragistics.Samples.Framework.SampleContainer
    {
        public BubbleMarkers()
        {
            InitializeComponent();
        }

        private void OnPreviousElementClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.cmbMarkerType.SelectedIndex == 0)
            {
                this.cmbMarkerType.SelectedIndex = this.cmbMarkerType.Items.Count - 1;
            }
            else
            {
                this.cmbMarkerType.SelectedIndex -= 1;
            }
        }

        private void OnNextElementClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.cmbMarkerType.SelectedIndex = (this.cmbMarkerType.SelectedIndex + 1) % this.cmbMarkerType.Items.Count;
        }
    }
}
