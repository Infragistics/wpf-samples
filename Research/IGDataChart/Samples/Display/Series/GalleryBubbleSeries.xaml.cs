
using System.Windows.Data;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Samples.Display.Series
{
    public partial class GalleryBubbleSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GalleryBubbleSeries()
        {
            InitializeComponent();
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
