using System.Windows.Data;
using IGZoombar.Resources;
using Infragistics.Samples.Shared.Models;

namespace IGZoombar.Samples.Display
{
    public partial class DataChartZoombars : Infragistics.Samples.Framework.SampleContainer
    {
        public DataChartZoombars()
        {
            InitializeComponent();
            this.Loaded += GalleryBubbleSeries_Loaded;
        }

        void GalleryBubbleSeries_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.Chart != null)
            {
                this.Chart.Title = ZoombarStrings.XWDC_WorldData_ChartTitle;
                this.Chart.Subtitle = ZoombarStrings.XWDC_WorldData_ChartSubtitle;

                this.Chart.Axes[0].Title = ZoombarStrings.XWDC_WorldData_Types_Population;
                this.Chart.Axes[1].Title = ZoombarStrings.XWDC_WorldData_Types_PublicDebt;
            }
        }

        private void OnFilterHighIncome(object sender, FilterEventArgs e)
        {
            var model = e.Item as CountryDataModel;
            if (model != null)
            {
                e.Accepted = model.GdpPerCapita >= 40000;
            }
        }
        private void OnFilterMediumIncome(object sender, FilterEventArgs e)
        {
            var model = e.Item as CountryDataModel;
            if (model != null)
            {
                e.Accepted = model.GdpPerCapita > 15000 &&
                             model.GdpPerCapita < 40000;
            }
        }
        private void OnFilterLowIncome(object sender, FilterEventArgs e)
        {
            var model = e.Item as CountryDataModel;
            if (model != null)
            {
                e.Accepted = model.GdpPerCapita <= 15000;
            }
        }
    }
}
