using System.ComponentModel;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGOrgChart.Samples.Navigation
{
    public partial class Zooming : SampleContainer
    {
        public Zooming()
        {
            InitializeComponent();
        }

        private void OrgChart_Loaded(object sender, RoutedEventArgs e)
        {
            SliderZoomLevel.ValueChanged += SliderZoomLevel_ValueChanged;
            SliderZoomLevel.Value = 1;
        }

        private void ButtonScaleToFit_Click(object sender, RoutedEventArgs e)
        {
            //Scale the contents to fit the OrgChart.
            OrgChart.ScaleToFit();
        }

        private void ButtonZoomTo100_Click(object sender, RoutedEventArgs e)
        {
            //Zoom the nodes to 100%.
            OrgChart.ZoomTo100();
        }

        private void ButtonZoomIn_Click(object sender, RoutedEventArgs e)
        {
            OrgChart.ZoomIn();
        }

        private void ButtonZoomOut_Click(object sender, RoutedEventArgs e)
        {
            OrgChart.ZoomOut();
        }

        private void SliderZoomLevel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            OrgChart.ZoomLevel = SliderZoomLevel.Value;
        }

        private void OrgChart_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ZoomLevel")
            {
                SliderZoomLevel.Value = OrgChart.ZoomLevel;
            }
        }
    }
}
