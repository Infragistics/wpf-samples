using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using IGExtensions.Common.Models;
using IGExtensions.Common.Services;
using IGExtensions.Framework.Controls;

namespace IGShowcase.EarthQuake.Views
{
    public partial class TestView : NavigationPage
    {
        public TestView()
        {
            InitializeComponent();

            Service.EarthQuakesChanged += Service_EarthQuakesChanged;
        }

        void Service_EarthQuakesChanged(object sender, EventArgs e)
        {
            OutputEarthQuakes(Service.EarthQuakes);

        }
        private void OutputEarthQuakes(IList<EarthQuakeData> earthQuakes)
        {
            var time = DateTime.Now;
            var timeStamp = time.Hour + ":" + time.Minute + ":" + time.Second + " - ";
            timeStamp += earthQuakes.Count + Environment.NewLine;

            this.EarthquakeSummary.Text += timeStamp;

            if (earthQuakes.Count < 200)
            {
                foreach (var earthQuake in earthQuakes)
                {
                    this.EarthquakeSummary.Text += earthQuake.ToString() + Environment.NewLine;

                }
            }
            this.EarthquakeSummary.Text += "-------------------------------" + Environment.NewLine;
        }

        protected EarthQuakesService Service = new EarthQuakesService();

        private void LoadEarthquakes1w_OnClick(object sender, RoutedEventArgs e)
        {
            //Service.RequestEarthQuakes(OnRequestSummary);
            Service.RequestMode = EarthQuakeRequestMode.LastWeek;
            Service.StartService();
        }
        private void LoadEarthquakes1d_OnClick(object sender, RoutedEventArgs e)
        {
            //Service.RequestEarthQuakes(OnRequestSummary);
            Service.RequestMode = EarthQuakeRequestMode.LastDay;
            Service.StartService();
        }
        private void LoadEarthquakes1h_OnClick(object sender, RoutedEventArgs e)
        {
            //Service.RequestEarthQuakes(OnRequestSummary);
            Service.RequestMode = EarthQuakeRequestMode.LastHour;
            Service.StartService();
        }
        private void LoadEarthquakes1m_OnClick(object sender, RoutedEventArgs e)
        {
            //Service.RequestEarthQuakes(OnRequestSummary);
            Service.RequestMode = EarthQuakeRequestMode.LastMonth;
            Service.StartService();
        }

        /// <summary>
        /// Called when [refresh details].
        /// </summary>
        /// <param name="earthQuakes">The data.</param>
        private void OnRequestSummary(IList<EarthQuakeData> earthQuakes)
        {
            OutputEarthQuakes(earthQuakes);
        }

        private void EarthquakeProcessButton_OnClick(object sender, RoutedEventArgs e)
        {
            var text = EarthquakeOutput.Text;
            var bytes = Encoding.ASCII.GetBytes(text);
            var stream = new MemoryStream(bytes); 
 
            //Service.MagnitudeFilter.Min = 0.0;
            Service.RequestMode = EarthQuakeRequestMode.LastMonth;
            Service.ProcessEarthQuakes(stream, true);

            OutputEarthQuakes(Service.EarthQuakes);
        }

        private void MagSliderMin_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Service != null)
            {
                Service.MagnitudeFilter.Min = e.NewValue;
            }

        }

        private void MagSliderMax_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Service != null)
            {
                Service.MagnitudeFilter.Max = e.NewValue;
            }

        }

        private void DepthSliderMin_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Service != null)
            {
                Service.DepthFilter.Min = e.NewValue;
            }
        }
        private void DepthSliderMax_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Service != null)
            {
                Service.DepthFilter.Max = e.NewValue;
            }
        }
    }
}
