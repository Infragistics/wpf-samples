using System.Windows;
using System.Windows.Input;
using IGMap.Models;                             // WorldDrillDownViewModel
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Extensions;   // Rect extensions
using Infragistics.Samples.Shared.Models;       // GeoRegion

namespace IGMap.Samples.Navigation
{
    public partial class MapDrillDown : SampleContainer
    {
        public MapDrillDown()
        {
            InitializeComponent();
        }

        protected MapAdapter MapAdapter;
        public WorldDrillDownViewModel ViewModel { get; set; }

        private void OnMapLayerImported(object sender, MapLayerImportEventArgs e)
        {
            // If Map Layer is imported
            if (e.Action == MapLayerImportAction.End)
            {
                MapLayer ml = (MapLayer)sender;
                      
                switch (ml.LayerName)
                {
                    case "regionsLayer":
                        // synchronize all map layers
                        shapeMap.WindowRect = shapeMap.WorldRect = shapeMap.Layers["statesLayer"].WorldRect;
                        shapeMap.Layers["regionsLayer"].WorldRect = shapeMap.WorldRect;

                        // initalize World Drill-Down ViewModel with the default map view of a shape file of the US regions
                        WorldMapViewModel initialMapView = new WorldMapViewModel();
                        initialMapView.WorldDrillDownLevel = WorldDrillDownLevel.CountryRegions;
                        initialMapView.WorldRect = shapeMap.WorldRect;
                        this.ViewModel = new WorldDrillDownViewModel(initialMapView);
            
                        break;
                }
            }
        }

        private void ZoomToRegion(GeoRegion statesRegion)
        {
            Rect currentWindowRect = this.shapeMap.WindowRect;
            Rect geoRect = statesRegion.ToRect();
            this.shapeMap.WindowRect = geoRect;
        }

        private void OnMapElementClick(object sender, MapElementClickEventArgs e)
        {
            MapElement selectedMapElement = e.Element;
            // adjust zoom rect for selected element
            double width = selectedMapElement.WorldRect.Width;
            double height = selectedMapElement.WorldRect.Height;
            double x = selectedMapElement.WorldRect.X - (width * 0.05);
            double y = selectedMapElement.WorldRect.Y - (height * 0.05);

            // scale up the zoom rect for selected element by a factor of 5.0 percents
            Rect worldRect = selectedMapElement.WorldRect.Scale(5.0);

            // Zoom to selected element
            shapeMap.WindowRect = worldRect;  

            // Change selected element 
            switch (e.Element.Layer.LayerName)
            {

                // map drill down: regions -> states
                case "regionsLayer":
                    // get unique identifier for selected map element 
                    string selectedRegioneName = selectedMapElement.GetProperty("RegionName").ToString();
                    // save current map view as a world drill-down view
                    this.ViewModel.DrillDown(new WorldMapViewModel()
                    {
                        WorldDrillDownLevel = WorldDrillDownLevel.CountryStates,
                        WorldIdentifier = selectedRegioneName,
                        WorldRect = worldRect
                    });
                 
                    shapeMap.Layers["regionsLayer"].IsVisible = false;
                    // Make collapsed all the states that are not in the selected region
                    foreach (MapElement mapElement in shapeMap.Layers["statesLayer"].Elements)
                    {
                        string currentRegionName = mapElement.GetProperty("StateRegion").ToString();
                        mapElement.Visibility = currentRegionName == selectedRegioneName ? Visibility.Visible : Visibility.Collapsed;
                    }
                    shapeMap.Layers["statesLayer"].IsVisible = true;
                    break;
                // map drill down: states -> counties
                case "statesLayer":
                    // get unique identifier for selected map element 
                    string selectedStateCode = selectedMapElement.GetProperty("StateCode").ToString();
                    // skip drill-down for Alaska and Hawaii due to lack of counties for these states
                    if (selectedStateCode == "AK" || selectedStateCode == "HI") return;

                    // save current map view as a world drill-down view
                    this.ViewModel.DrillDown(new WorldMapViewModel()
                    {
                        WorldDrillDownLevel = WorldDrillDownLevel.CountryCounties,
                        WorldIdentifier = selectedStateCode,
                        WorldRect = worldRect
                    });
                 
                    shapeMap.Layers["statesLayer"].IsVisible = false;
                    // Make collapsed all states that are not selected 
                    foreach (MapElement mapElement in shapeMap.Layers["countiesLayer"].Elements)
                    {
                        string currentStateName = mapElement.GetProperty("StateCode").ToString();
                        mapElement.Visibility = currentStateName == selectedStateCode ? Visibility.Visible : Visibility.Collapsed;
                    }
                    shapeMap.Layers["countiesLayer"].IsVisible = true;

                    break;
                // map drill down: counties -> a single county
                case "countiesLayer":
                    // get unique identifier for selected map element 
                    string selectedCountyName = selectedMapElement.GetProperty("CountyName").ToString();
                    string selectedCountyState = selectedMapElement.GetProperty("StateCode").ToString();
                    // save current map view as a world drill-down view
                    this.ViewModel.DrillDown(new WorldMapViewModel()
                    {
                        WorldDrillDownLevel = WorldDrillDownLevel.CountryCities,
                        WorldIdentifier = selectedCountyState,
                        WorldRect = worldRect
                    });

                    // Make collapsed all counties that are not selected 
                    foreach (MapElement mapElement in shapeMap.Layers["countiesLayer"].Elements)
                    {
                        string currentCountyName = mapElement.GetProperty("CountyName").ToString();
                        string currentCountyState = mapElement.GetProperty("StateCode").ToString();
                        // check for counties with the same name in different states
                        bool isSameCountyName = currentCountyName == selectedCountyName;
                        bool isSameCountyState = selectedCountyState == currentCountyState;
                        mapElement.Visibility = isSameCountyName && isSameCountyState ? Visibility.Visible : Visibility.Collapsed;
                    }
                    break;
            }
        }
         
        private void shapeMap_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            // skip map drill-up operation if the map is already at lowest map drill-down level
            if (this.ViewModel.CurrentDrillDownLevel == WorldDrillDownLevel.CountryRegions) return;

            this.ViewModel.DrillUp();
            
            if (this.ViewModel.CurrentDrillDownLevel == WorldDrillDownLevel.CountryCounties)
            {
                string selectedStateCode = this.ViewModel.CurrentDrillDownView.WorldIdentifier;
                shapeMap.Layers["regionsLayer"].IsVisible = false;
                shapeMap.Layers["statesLayer"].IsVisible = false;
                shapeMap.Layers["countiesLayer"].IsVisible = false;

                // Make visible all counties that are in the selected state of U.S. 
                foreach (MapElement mapElement in shapeMap.Layers["countiesLayer"].Elements)
                {
                    string currentStateName = mapElement.GetProperty("StateCode").ToString();
                    mapElement.Visibility = currentStateName == selectedStateCode ? Visibility.Visible : Visibility.Collapsed;
                }
     
                shapeMap.Layers["countiesLayer"].IsVisible = true;
            }
            if (this.ViewModel.CurrentDrillDownLevel == WorldDrillDownLevel.CountryStates)
            {
                string selectedRegioneName = this.ViewModel.CurrentDrillDownView.WorldIdentifier;
                shapeMap.Layers["regionsLayer"].IsVisible = false;
                shapeMap.Layers["statesLayer"].IsVisible = false;
                shapeMap.Layers["countiesLayer"].IsVisible = false;

                // Make visible all states that are in the selected region 
                foreach (MapElement mapElement in shapeMap.Layers["statesLayer"].Elements)
                {
                    string currentRegionName = mapElement.GetProperty("StateRegion").ToString();
                    mapElement.Visibility = currentRegionName == selectedRegioneName ? Visibility.Visible : Visibility.Collapsed;
                }
                shapeMap.Layers["statesLayer"].IsVisible = true;
            }
            if (this.ViewModel.CurrentDrillDownLevel == WorldDrillDownLevel.CountryRegions)
            {
                string selectedRegioneName = this.ViewModel.CurrentDrillDownView.WorldIdentifier;

                shapeMap.Layers["regionsLayer"].IsVisible = false;
                shapeMap.Layers["statesLayer"].IsVisible = false;
                shapeMap.Layers["countiesLayer"].IsVisible = false;
                // Make visible all regions of U.S. 
                foreach (MapElement mapElement in shapeMap.Layers["regionsLayer"].Elements)
                {
                    mapElement.Visibility = Visibility.Visible;
                 }
                shapeMap.Layers["regionsLayer"].IsVisible = true;
            }
            shapeMap.WindowRect = this.ViewModel.CurrentDrillDownView.WorldRect;
        }
    }

}
