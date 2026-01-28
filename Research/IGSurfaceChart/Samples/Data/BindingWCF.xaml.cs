using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Windows;
using IGSurfaceChart.Samples.Models;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework; 
using System.ComponentModel;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGSurfaceChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for LiveData.xaml
    /// </summary>
    public partial class BindingShapefiles : SampleContainer, INotifyPropertyChanged
    { 

        public BindingShapefiles()
        {
            InitializeComponent();
            Client = new WorldDataProvider();
            Client.GetWorldDataCompleted += OnGetWorldDataCompleted;
            Client.GetWorldDataAsync();
        }
        public readonly WorldDataProvider Client;

        private void OnGetWorldDataCompleted(object sender, GetWorldDataCompletedEventArgs e)
        {
            var data = e.Result as List<CountryDataModel>;
            this.SurfaceChart.ItemsSource = data;

            //// sort country data points based on GdpPerCapita value so that
            //// bubble markers with smaller radius (lower GdpPerCapita) will be on top of 
            //// bubble markers with bigger radius (higher GdpPerCapita) 
            //data.Sort(delegate(CountryDataModel n1, CountryDataModel n2)
            //{
            //    if (n1.GdpPerCapita == n2.GdpPerCapita) return 0;
            //    if (n1.GdpPerCapita > n2.GdpPerCapita) return -1;
            //    return 1;
            //});
             
        }
         
 

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, ea);
            }
        }


    }


    
}
