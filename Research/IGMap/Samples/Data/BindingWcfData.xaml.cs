using System.Collections.Generic;
using System.Windows;
using Infragistics;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using IGMap.Models;
using Infragistics.Samples.Shared.DataProviders;

namespace IGMap.Samples.Data
{
    public partial class BindingWcfData : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingWcfData()
        {
            InitializeComponent();
            this.accessingDataMessage.Visibility = Visibility.Visible;
            
            InitializeMap();
        }
        private WorldDataProvider _worldDataWcfClient;

        private void InitializeMap()
        {
            (this.theMap.Layers[0] as MapLayer).Imported += OnMapLayerImported;

            // WorldDataProvider is actually simulation of WCF service for receiving data
            // and its purpose is to show you how to bind the xamMap control to 
            // data that was loaded using WCF Service   
            _worldDataWcfClient = new WorldDataProvider();
            _worldDataWcfClient.GetWorldDataCompleted += OnGetWorldDataCompleted;
            _worldDataWcfClient.GetWorldDataAsync();

            // initialize xamMap boundary to specific map region
            MapAdapter.SetMapBoundary(theMap, GeoRegions.WorldFullRegion);
            MapAdapter.ZoomMapToRegion(theMap, GeoRegions.WorldNonPolarRegion);
        }
        private void OnGetWorldDataCompleted(object sender, GetWorldDataCompletedEventArgs e)
        {
            var data = e.Result as List<CountryDataModel>;

            // bind the xamMap to data received from WorldDataProvider (WCF service)
            var dataConverter = new DataMapping.Converter();
            this.theMap.Layers[0].DataMapping = dataConverter.ConvertFromString("Name = CountryCode; Value = GdpPerCapita") as DataMapping;
            this.theMap.Layers[0].DataSource = data; 
            this.theMap.Layers[0].DataBind();
        }

        private void OnMapLayerImported(object sender, MapLayerImportEventArgs e)
        {
         
            if (e.Action == MapLayerImportAction.End)
            {
                this.accessingDataMessage.Visibility = Visibility.Collapsed;
            }
        }

      
    }
}