using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using IGMap.Models;
using IGMap.Resources;
using Infragistics;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGMap.Samples.Data
{
    public partial class BindingWcfDataFiltering : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingWcfDataFiltering()
        {
            InitializeComponent();
          
            this.OptionsPanel.Visibility = Visibility.Collapsed;
            this.accessingDataMessage.Visibility = Visibility.Visible;

            InitializeMap();

            this.lbDataSources.SelectionChanged += OnDataSelectionChanged;
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

            // initialize map boundary to specific map region
            MapAdapter mapAdapter = new MapAdapter(this.theMap);
            mapAdapter.SetMapBoundary(GeoRegions.WorldFullRegion);
            mapAdapter.ZoomMapToRegion(GeoRegions.WorldNonPolarRegion);
        }

        private void OnGetWorldDataCompleted(object sender, GetWorldDataCompletedEventArgs e)
        {
            var data = e.Result as List<CountryDataModel>;
            // bind the xamMap to data received from WorldDataProvider (WCF service)
            this.theMap.Layers[0].DataSource = data; 
            this.theMap.Layers[0].DataBind();

            this.lbDataSources.SelectedIndex = 0;
        }

        private void OnDataSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selectedItem = ((ListBox) sender).SelectedItem as ListBoxItem;
            string selectedText = selectedItem.Tag as string;
            DataMapping.Converter converter = new DataMapping.Converter();
            this.mapTitle = this.theMap.FindName("mapTitle") as TextBlock;
            switch (selectedText)
            {
                case "Birth Rate":
                    this.theMap.Layers[0].DataMapping =
                        converter.ConvertFromString("Name = CountryCode; Value = BirthRate") as DataMapping;
                    this.mapTitle.Text = MapStrings.XWM_WorldDataWCF_MapTitle_Birth;
                    this.theMap.Layers[0].ValueScale.MinimumValue = 7.37;
                    this.theMap.Layers[0].ValueScale.MaximumValue = 49.62;
                    break;
                case "Electricity Production":
                    this.theMap.Layers[0].DataMapping =
                        converter.ConvertFromString("Name = CountryCode; Value = ElectricityProduction") as DataMapping;
                    this.mapTitle.Text = MapStrings.XWM_WorldDataWCF_MapTitle_Electric;
                    this.theMap.Layers[0].ValueScale.MinimumValue = 0;
                    this.theMap.Layers[0].ValueScale.MaximumValue = 4062000;
                    break;
                case "Internet Users":
                    this.theMap.Layers[0].DataMapping =
                        converter.ConvertFromString("Name = CountryCode; Value = InternetUsers") as DataMapping;
                    this.mapTitle.Text = MapStrings.XWM_WorldDataWCF_MapTitle_Internet;
                    this.theMap.Layers[0].ValueScale.MinimumValue = 93;
                    this.theMap.Layers[0].ValueScale.MaximumValue = 88110000;
                    break;
                case "Median Age":
                    this.theMap.Layers[0].DataMapping =
                        converter.ConvertFromString("Name = CountryCode; Value = MedianAge") as DataMapping;
                    this.mapTitle.Text = MapStrings.XWM_WorldDataWCF_MapTitle_MedianAge;
                    this.theMap.Layers[0].ValueScale.MinimumValue = 15;
                    this.theMap.Layers[0].ValueScale.MaximumValue = 45.5;
                    break;
                case "Oil Production":
                    this.theMap.Layers[0].DataMapping =
                        converter.ConvertFromString("Name = CountryCode; Value = OilProduction") as DataMapping;
                    this.mapTitle.Text = MapStrings.XWM_WorldDataWCF_MapTitle_Oil;
                    this.theMap.Layers[0].ValueScale.MinimumValue = 0;
                    this.theMap.Layers[0].ValueScale.MaximumValue = 11000000;
                    break;
                case "Population":
                    this.theMap.Layers[0].DataMapping =
                        converter.ConvertFromString("Name = CountryCode; Value = Population") as DataMapping;
                    this.mapTitle.Text = MapStrings.XWM_WorldDataWCF_MapTitle_Population;
                    this.theMap.Layers[0].ValueScale.MinimumValue = 40;
                    this.theMap.Layers[0].ValueScale.MaximumValue = 1330000000;
                    break;
                case "Public Debt":
                    this.theMap.Layers[0].DataMapping =
                        converter.ConvertFromString("Name = CountryCode; Value = PublicDebt") as DataMapping;
                    this.mapTitle.Text = MapStrings.XWM_WorldDataWCF_MapTitle_Debt;
                    this.theMap.Layers[0].ValueScale.MinimumValue = 1.6;
                    this.theMap.Layers[0].ValueScale.MaximumValue = 218.2;
                    break;
                case "Televisions":
                    this.theMap.Layers[0].DataMapping =
                        converter.ConvertFromString("Name = CountryCode; Value = Televisions") as DataMapping;
                    this.mapTitle.Text = MapStrings.XWM_WorldDataWCF_MapTitle_Televisions;
                    this.theMap.Layers[0].ValueScale.MinimumValue = 300;
                    this.theMap.Layers[0].ValueScale.MaximumValue = 86500000;
                    break;
                case "Unemployment Rate":
                    this.theMap.Layers[0].DataMapping =
                        converter.ConvertFromString("Name = CountryCode; Value = UnemploymentRate") as DataMapping;
                    this.mapTitle.Text = MapStrings.XWM_WorldDataWCF_MapTitle_Unemployment;
                    this.theMap.Layers[0].ValueScale.MinimumValue = 0;
                    this.theMap.Layers[0].ValueScale.MaximumValue = 90;
                    break;
            }
            this.theMap.Layers[0].DataBind();
        }

        private void OnMapLayerImported(object sender, MapLayerImportEventArgs e)
        {
            if (e.Action == MapLayerImportAction.End)
            {
                this.OptionsPanel.Visibility = Visibility.Visible;
                this.accessingDataMessage.Visibility = Visibility.Collapsed;
            }
        }
    }
}