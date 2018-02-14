//-------------------------------------------------------------------------
// <copyright file="SeatingMapView.xaml.cs" company="Infragistics">
//
// Copyright (c) 2010 Infragistics, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using IGShowcase.AirplaneSeatingChart.Models;
using IGShowcase.AirplaneSeatingChart.ViewModels;
using IGExtensions.Framework.Controls;
using Infragistics.Controls.Maps;
using IGExtensions.Common.Models;
using Infragistics;
using System.Diagnostics;
using System.Reflection;
using System.Threading;


namespace IGShowcase.AirplaneSeatingChart.Views
{
	public partial class SeatingMapView : NavigationPage
	{
		#region Private Variables
		private readonly SeatingMapViewModel _vm;
        private ShapefileConverter ShapeFileBodyLin;
        private ShapefileConverter ShapeFileAreasPol;
        private ShapefileConverter ShapeFileSeatsPol;
        private ShapefileConverter ShapeFileSeatLabelsLin;
        private ShapefileConverter ShapeFileAreaIconsLin;
        private Rect ShapeFilesMaxBounds = new Rect(0, 0, 1, 1);
        #endregion Private Variables


		#region Private Event Handlers
        /// <summary>
        /// Gets the navigation context of the current page. 
        /// </summary>
        public override string GetNavigationContext()
        {
            var context = base.GetNavigationContext();
            //TODO: add logic for creating navigation context
            //context = "?" + "regions=[AU,NA,EU]&minValue=[1.0]";
            return context;
        }
        // Executes when the user navigates to this page.
        protected override void OnPageNavigated(NavigationEventArgs e)
        {
            base.OnPageNavigated(e);
            //TODO: add logic for parsing navigation query  
            //double value = 0.0;
            //if (this.NavigationContext.QueryString.ContainsKey("minValue"))
            //{
            //    if (double.TryParse(this.NavigationContext.QueryString["minMag"], out value)) _vm.Value = value;
            //}
        }

        /// <summary>
        /// The following event handler shows how to handle user interaction for any series displayed in the GeographicMap control.
        /// Note that to disallow user interaction on a series, IsHitTestVisible must be set to false as done in GeographicShapeSeries for Body in Xaml in this sample. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void geoMap_SeriesMouseLeftButtonUp(object sender, Infragistics.Controls.Charts.DataChartMouseButtonEventArgs e)
        {
            var seriesName = e.Series.Name;
            if (seriesName == "SeatsPolSeries")
            {
                var item = e.Item as SeatViewModel;
                if (item != null) _vm.SelectSeat(item);
            }
            else
            {
                //nop
            }
        }
        bool MapInitialized = false;
        void geoMap_GridAreaRectChanged(object sender, RectChangedEventArgs e)
        {
            if (!MapInitialized)
            {
                MapInitialized = true;
                LoadShapeFiles();
            }
        }
		#endregion Private Event Handlers

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SeatingMapView"/> class.
        /// </summary>
        public SeatingMapView()
        {
            InitializeComponent();

            _vm = (SeatingMapViewModel)DataContext;

            this.geoMap.GridAreaRectChanged += geoMap_GridAreaRectChanged;
        }
        #endregion Constructors

        private string GetPath()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var component = assembly.GetAssemblyComponent() + @"/Assets/ShapeFiles/Airplane/";
            return component;
        }
        void LoadShapeFiles()
        {
            var shapefilesPath = GetPath();
            //ShapeFile for polygons for entire body of Airplane
            ShapeFileBodyLin = new ShapefileConverter();
            ShapeFileBodyLin.ImportCompleted += OnShapefileImportCompleted;
            ShapeFileBodyLin.ShapefileSource = new Uri(shapefilesPath + "Body_lin.shp", UriKind.RelativeOrAbsolute);
            ShapeFileBodyLin.DatabaseSource = new Uri(shapefilesPath + "Body_lin.dbf", UriKind.RelativeOrAbsolute);

            //ShapeFile for rectangles around Lavatory, Gally etc., underneath Exit signs and in the nose of the airplane
            ShapeFileAreasPol = new ShapefileConverter();
            ShapeFileAreasPol.ImportCompleted += OnShapefileImportCompleted;
            ShapeFileAreasPol.ShapefileSource = new Uri(shapefilesPath + "Areas_pol.shp", UriKind.RelativeOrAbsolute);
            ShapeFileAreasPol.DatabaseSource = new Uri(shapefilesPath + "Areas_pol.dbf", UriKind.RelativeOrAbsolute);

            //ShapeFile for Seat polygons of all Classes
            ShapeFileSeatsPol = new ShapefileConverter();
            ShapeFileSeatsPol.ImportCompleted += OnShapefileImportCompleted;
            ShapeFileSeatsPol.ShapefileSource = new Uri(shapefilesPath + "Seats_pol.shp", UriKind.RelativeOrAbsolute);
            ShapeFileSeatsPol.DatabaseSource = new Uri(shapefilesPath + "Seats_pol.dbf", UriKind.RelativeOrAbsolute);

            //ShapeFile for seat rows number displayed to the left of the seats
            ShapeFileSeatLabelsLin = new ShapefileConverter();
            ShapeFileSeatLabelsLin.ImportCompleted += OnShapefileImportCompleted;
            ShapeFileSeatLabelsLin.ShapefileSource = new Uri(shapefilesPath + "SeatLabels_lin.shp", UriKind.RelativeOrAbsolute);
            ShapeFileSeatLabelsLin.DatabaseSource = new Uri(shapefilesPath + "SeatLabels_lin.dbf", UriKind.RelativeOrAbsolute);

            //ShapeFile for Icons for Lavatory, Exit , Galley etc.
            ShapeFileAreaIconsLin = new ShapefileConverter();
            ShapeFileAreaIconsLin.ImportCompleted += OnShapefileImportCompleted;
            ShapeFileAreaIconsLin.ShapefileSource = new Uri(shapefilesPath + "AreaIcons_lin.shp", UriKind.RelativeOrAbsolute);
            ShapeFileAreaIconsLin.DatabaseSource = new Uri(shapefilesPath + "AreaIcons_lin.dbf", UriKind.RelativeOrAbsolute);
        }

        #region parsing methods
        private void OnShapefileImportCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ParseAllShapefiles();
        }
        /// <summary>
        ///Initialize the map and create itemssource for each of the shalpe files and 
        /// </summary>
        private void ParseAllShapefiles()
        {
            //Wait till all the ShaleFiles have been imported
            if (this.ShapeFileBodyLin != null && this.ShapeFileBodyLin.Count > 0 &&
                this.ShapeFileAreasPol != null && this.ShapeFileAreasPol.Count > 0 &&
                this.ShapeFileSeatsPol != null && this.ShapeFileSeatsPol.Count > 0 &&
                this.ShapeFileSeatLabelsLin != null && this.ShapeFileSeatLabelsLin.Count > 0 &&
                this.ShapeFileAreaIconsLin != null && this.ShapeFileAreaIconsLin.Count > 0)
            {
                //Geographic map instance must be passed to the MapCartesianProjection object after importing all the shape files    
                MapCartesianProjection mapProjector = new MapCartesianProjection();
                mapProjector.InitializeMap(this.geoMap);
                //Both, the viewmodel and this code behind must use the same MapCartesianProjection instance created here because it holds state information 
                _vm.MapProjector = mapProjector;
                //Let the mapProjector compute the min/max of X and Y axis on the Cartesian coordinates
                _vm.InitializeMapBounds(this.ShapeFileBodyLin);

                SetItemsSourceForAllSeries();
            }
        }
        private void SetItemsSourceForAllSeries()
        {
            var shapeControlseries = this.geoMap.Series["SeatsPolSeries"] as GeographicShapeControlSeries;
            if (shapeControlseries != null)
                shapeControlseries.ItemsSource = _vm.GetDataForSeatsPolSeries(this.ShapeFileSeatsPol);

            var shapeSeries = this.geoMap.Series["AreasPolSeries"] as GeographicShapeSeries;
            if (shapeSeries != null)
                shapeSeries.ItemsSource = _vm.GetDataForAllOtherSeries(this.ShapeFileAreasPol);

            shapeSeries = this.geoMap.Series["SeatLabelsLinSeries"] as GeographicShapeSeries;
            if (shapeSeries != null) 
                shapeSeries.ItemsSource = _vm.GetDataForSeatLabelsLinSeries(this.ShapeFileSeatLabelsLin);

            shapeSeries = this.geoMap.Series["AreaIconsLinSeries"] as GeographicShapeSeries;
            if (shapeSeries != null)
                shapeSeries.ItemsSource = _vm.GetDataForAreaIconsLinSeries(this.ShapeFileAreaIconsLin);

            shapeSeries = this.geoMap.Series["BodyLinSeries"] as GeographicShapeSeries;
            if (shapeSeries != null)
                shapeSeries.ItemsSource = _vm.GetDataForAllOtherSeries(this.ShapeFileBodyLin);
        }
        #endregion

    }
}

