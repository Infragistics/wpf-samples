using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Infragistics;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Samples.Data
{
    public partial class BindingWcfData : SampleContainer
    {
        public BindingWcfData()
        {
            InitializeComponent();
            this.LoadingControl.Visibility = Visibility.Visible;

            // WorldDataProvider is actually simulation of WCF Service for receiving data
            // and its purpose is to show you how to bind the xamDataChart control to 
            // data that was loaded using WCF Service   
            _worldDataWcfClient = new WorldDataProvider();
            _worldDataWcfClient.GetWorldDataCompleted += OnGetWorldDataCompleted;
            _worldDataWcfClient.GetWorldDataAsync();

        }
        private readonly WorldDataProvider _worldDataWcfClient;

        private void OnGetWorldDataCompleted(object sender, GetWorldDataCompletedEventArgs e)
        {
            var data = e.Result as List<CountryDataModel>;

            // sort country data points based on GdpPerCapita value so that
            // bubble markers with smaller radius (lower GdpPerCapita) will be on top of 
            // bubble markers with bigger radius (higher GdpPerCapita) 
            data.Sort(delegate(CountryDataModel n1, CountryDataModel n2)
            {
                if (n1.GdpPerCapita == n2.GdpPerCapita) return 0;
                if (n1.GdpPerCapita > n2.GdpPerCapita) return -1;
                return 1;
            });

            //foreach (var item in data)
            //{
            //    item.Population = item.Population * 1000;
            //}

            this.BindWorldData(data);
            this.LoadingControl.Visibility = Visibility.Collapsed;
        }

        private void BindWorldData(IEnumerable<CountryDataModel> data)
        {
            var series = DataChart.Series.OfType<BubbleSeries>().First();
            // bind the xamDataChart to data received from WorldDataProvider (WCF service)
            series.XMemberPath = "Population";
            series.YMemberPath = "PublicDebt";
            series.RadiusMemberPath = "GdpPerCapita";
            series.FillMemberPath = "GdpPerCapita";
            series.ItemsSource = data;
            // apply custom marker template for the BubbleSeries
            series.MarkerTemplate = this.Resources["BubbleMarkerTemplate"] as DataTemplate;
        }
 
    }

    public class SampleWorldData : List<CountryDataModel>
    {
        public SampleWorldData()
        {

            this.Add(new CountryDataModel
            {
                CountryCode = "BR",
                CountryName = "Brazil",
                PublicDebt = 45,
                GdpPerCapita = 2000,
                Population = 196342592
            });

            this.Add(new CountryDataModel
            {
                CountryCode = "FI",
                CountryName = "Finland",
                PublicDebt = 36,
                GdpPerCapita = 22000,
                Population = 5244749
            });

            this.Add(new CountryDataModel
            {
                CountryCode = "DE",
                CountryName = "Germany",
                PublicDebt = 65,
                GdpPerCapita = 42000,
                Population = 82369552
            });

            this.Add(new CountryDataModel
            {
                CountryCode = "GT",
                CountryName = "Guatemala",
                PublicDebt = 21,
                GdpPerCapita = 500,
                Population = 13002206
            });
        }
    }

}
