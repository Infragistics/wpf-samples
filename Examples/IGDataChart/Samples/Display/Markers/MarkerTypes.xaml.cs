using System.Windows;

namespace IGDataChart.Samples.Display.Markers
{
    public partial class MarkerTypes : Infragistics.Samples.Framework.SampleContainer
    {
        public MarkerTypes()
        {
            InitializeComponent();
        }
        private void OnPrevButtonClick(object sender, RoutedEventArgs e)
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
        private void OnNextButtonClick(object sender, RoutedEventArgs e)
        {
            this.cmbMarkerType.SelectedIndex = (this.cmbMarkerType.SelectedIndex + 1) % this.cmbMarkerType.Items.Count;
        }
    }

}
