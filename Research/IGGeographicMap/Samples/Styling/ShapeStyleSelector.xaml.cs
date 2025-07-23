using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Controls;
using IGGeographicMap.Models;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System.Windows;

namespace IGGeographicMap.Samples.Styling
{
    public partial class ShapeStyleSelector : Infragistics.Samples.Framework.SampleContainer
    {
        public ShapeStyleSelector()
        {
            InitializeComponent();
          
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //    zoom in geo - map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        }


        //protected List<MapShapeElement> ShapeRegionsMapElements = new List<MapShapeElement>();

        //private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        //{
        //    this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_LoadedItems + " " + this.LoadedItemsCount;
        //    this.MapLoadingContainer.Visibility = System.Windows.Visibility.Collapsed;

        //    zoom in geo - map a specific geographic region of the world
        //    this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        //    this.GeoMap.Visibility = System.Windows.Visibility.Visible;

        //    var shapeFileConverter = this.LayoutRoot.Resources["shapeFileSource"] as ShapefileConverter;
        //    if (shapeFileConverter != null)
        //    {
        //        foreach (ShapefileRecord record in shapeFileConverter)
        //        {
        //            var mapElement = new MapShapeElement();
        //            mapElement.ShapeRecord = record;
        //            this.ShapeRegionsMapElements.Add(mapElement);
        //        }
        //    }

        //}
        //private void OnShapefileCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    this.LoadedItemsCount += e.NewItems.Count;
        //    this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_LoadingItem + " " + this.LoadedItemsCount + "...";
        //}
        //protected int LoadedItemsCount;
    }

    public class StyleSelectorList : List<StyleSelectorItem>
    { }
    public class StyleSelectorItem
    {
        public StyleSelector StyleSelector { get; set; }
        public string DisplayName { get; set; }
    }

}
