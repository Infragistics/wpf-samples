using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using IGExtensions.Common.Data;
using IGExtensions.Common.Models;
using IGExtensions.Framework.Controls;
using IGShowcase.HospitalFloorPlan.Models;
using IGShowcase.HospitalFloorPlan.ViewModels;
using Infragistics;
using Infragistics.Controls.Maps;
using System.Windows;

namespace IGShowcase.HospitalFloorPlan.Views
{
	public partial class HospitalView : UserControl
	{
		#region Constructor
        public HospitalView()
		{
			InitializeComponent();
            MapProjector = new MapCartesianProjection();
            this.Loaded += OnHospitalViewLoaded;
    	}
        protected MapCartesianProjection MapProjector { get; set; }
        protected ShapefileConverter ShapeFileRooms;
        protected ShapefileConverter ShapeFileBeds;
        protected ShapefileConverter ShapeFileItems;
        protected Rect ShapeFilesMaxBounds = new Rect(0, 0, 1, 1);
        protected HospitalFloorPlanViewModel ViewModel;

        private void OnHospitalViewLoaded(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion Constructor

        #region Parsing Methods
        private void ParseAllShapefiles()
        {
            if (this.ShapeFileRooms != null && this.ShapeFileRooms.Count > 0 &&
                this.ShapeFileItems != null && this.ShapeFileItems.Count > 0 &&
                this.ShapeFileBeds != null && this.ShapeFileBeds.Count > 0)
            {
                var roomsBounds = this.MapProjector.GetCartesianBounds(this.ShapeFileRooms);
                var roomsBeds = this.MapProjector.GetCartesianBounds(this.ShapeFileBeds);

                var union = roomsBounds.UnionMax(roomsBeds);

                this.ShapeFilesMaxBounds.Union(roomsBounds);
                this.ShapeFilesMaxBounds.Expand(new Thickness(10, 10, 10, 10));

                this.MapProjector.InitializeMapBounds(this.ShapeFilesMaxBounds);

                ParseShapefileRooms();
                ParseShapefileBeds();
                ParseShapefileItems();
            }
            
        }
        private void ParseShapefileItems()
        {
            if (this.ShapeFileItems.Count == 0) return;
            var elements = new HospitalElementList();
            foreach (ShapefileRecord record in this.ShapeFileItems)
            {
                var id = record.Fields["ID"].ToString();
                var points = this.MapProjector.ProjectToGeographic(record.Points);
                elements.Add(new HospitalElement(id, points));
            }

            var series = this.geoMap.Series["HosptialItemSeries"] as GeographicShapeControlSeries;
            if (series != null) series.ItemsSource = elements;
        }
        private void ParseShapefileRooms()
        {
            if (this.ShapeFileRooms.Count == 0) return;
            var elements = new HospitalElementList();
            foreach (ShapefileRecord record in this.ShapeFileRooms)
            {
                var id = record.Fields["ID"].ToString();
                var points = this.MapProjector.ProjectToGeographic(record.Points);
                elements.Add(new HospitalElement(id, points));
            }

            var series = this.geoMap.Series["HosptialRoomSeries"] as GeographicShapeControlSeries;
            if (series != null) series.ItemsSource = elements;
        }
	    private void ParseShapefileBeds()
        {
            if (this.ShapeFileBeds.Count == 0) return;

            var beds = new HospitalElementList();
            
            foreach (ShapefileRecord record in this.ShapeFileBeds)
            {
                var id = record.Fields["ID"].ToString();
                var points = this.MapProjector.ProjectToGeographic(record.Points);
                beds.Add(new HospitalElement(id, points));
            }
            
            if (this.ViewModel != null)
            {
                ViewModel.RegisterElements(beds);

                var series = this.geoMap.Series["HosptialBedSeries"] as GeographicShapeControlSeries;
                if (series != null) series.ItemsSource = this.ViewModel.MapElements;

            }
        }
        private void OnShapefileImportCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ParseAllShapefiles();
        }
        private void OnGridAreaRectChanged(object sender, Infragistics.RectChangedEventArgs e)
        {
            this.ViewModel = DataContext as HospitalFloorPlanViewModel;
            this.MapProjector.InitializeMap(this.geoMap);
            if (!NavigationApp.Current.IsInDesingMode()) //(DesignerProperties.GetIsInDesignMode(this))
            {
                var shapefilesPath = DataStorageProvider.ShapefilesPath;
                ShapeFileRooms = new ShapefileConverter();
                ShapeFileRooms.ImportCompleted += OnShapefileImportCompleted;
                ShapeFileRooms.ShapefileSource = new Uri(shapefilesPath + "custom/hospital/rooms.shp", UriKind.RelativeOrAbsolute);
                ShapeFileRooms.DatabaseSource = new Uri(shapefilesPath + "custom/hospital/rooms.dbf", UriKind.RelativeOrAbsolute);

                ShapeFileBeds = new ShapefileConverter();
                ShapeFileBeds.ImportCompleted += OnShapefileImportCompleted;
                ShapeFileBeds.ShapefileSource = new Uri(shapefilesPath + "custom/hospital/beds.shp", UriKind.RelativeOrAbsolute);
                ShapeFileBeds.DatabaseSource = new Uri(shapefilesPath + "custom/hospital/beds.dbf", UriKind.RelativeOrAbsolute);

                ShapeFileItems = new ShapefileConverter();
                ShapeFileItems.ImportCompleted += OnShapefileImportCompleted;
                ShapeFileItems.ShapefileSource = new Uri(shapefilesPath + "custom/hospital/items.shp", UriKind.RelativeOrAbsolute);
                ShapeFileItems.DatabaseSource = new Uri(shapefilesPath + "custom/hospital/items.dbf", UriKind.RelativeOrAbsolute);
            }
        }
	    #endregion  
		
        private void geoMap_SeriesMouseLeftButtonUp(object sender, Infragistics.Controls.Charts.DataChartMouseButtonEventArgs e)
        {
            var seriesName = e.Series.Name;
            if (seriesName == "HosptialBedSeries")
            {
                var item = e.Item as MapElementViewModel;
                if (item != null) ViewModel.SelectElement(item.Name);

                UpdateDetailsControlPosition();
            }
            else
            {
                detailsControl.Hide();
                ViewModel.UnselectElement();
            }
        }
        private void UpdateDetailsControlPosition()
        {
            if (this.ViewModel != null && this.ViewModel.SelectedElement != null && detailsControl != null)
            {
                try
                {
                    detailsControl.Show();
                    var shapeRect = this.ViewModel.SelectedElement.BedShape.ToShapeRect();

                    Point p = shapeRect.Center();
                    p = this.geoMap.GetMapPosition(p);

                    double left = p.X + 6;
                    double top = p.Y + 6;
                    double maxWidth = canvas.ActualWidth;
                    double maxHeight = canvas.ActualHeight;

                    double width = detailsControl.Width;
                    double height = detailsControl.Height;

                    if (left + width > maxWidth)
                    {
                        left = left - width - 24;
                        if (left < 0) left = 0;
                    }

                    if (top + height > maxHeight)
                    {
                        top = top - height - 24;
                        if (top < 0) top = 0;
                    }

                    detailsControl.SetValue(Canvas.LeftProperty, Math.Round(left));
                    detailsControl.SetValue(Canvas.TopProperty, Math.Round(top));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
	}
}