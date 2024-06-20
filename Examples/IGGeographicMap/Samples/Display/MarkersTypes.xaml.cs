using System.Windows.Controls;
using IGGeographicMap.Models;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Samples
{
    public partial class MarkersTypes : Infragistics.Samples.Framework.SampleContainer
    {
        public MarkersTypes()
        {
            InitializeComponent();
            this.MarkerTypeComboBox.SelectionChanged += OnMarkerTypeComboBoxSelectionChanged;
            this.SampleLoaded += OnSampleLoaded;
        }
        
        void OnSampleLoaded(object sender, System.EventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        }

        void OnMarkerTypeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var series = (GeographicSymbolSeries)this.GeoMap.Series[0];
            var markerType = (MarkerType)this.MarkerTypeComboBox.SelectedItem;
            series.MarkerType = markerType;
        }
    }
}
