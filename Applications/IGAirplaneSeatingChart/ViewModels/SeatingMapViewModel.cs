//-------------------------------------------------------------------------
// <copyright file="SeatingMapViewModel.cs" company="Infragistics">
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
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using IGShowcase.AirplaneSeatingChart.Models;
using Infragistics;
using Infragistics.Controls.Maps;

namespace IGShowcase.AirplaneSeatingChart.ViewModels
{
	public class SeatingMapViewModel : INotifyPropertyChanged
	{
		#region Private Variables

        private PlaneData PlaneData {get;set;}
		private readonly SeatClass _allClass;
		private IEnumerable<SeatClass> _classes;

		private SeatClass _selectedClass;
		private SeatViewModel _selectedSeat;


		#endregion Private Variables

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="SeatingMapViewModel"/> class.
		/// </summary>
		public SeatingMapViewModel()
		{
			//Load the plane data from the XAML file. This is to illustrate merging data available from the .dbf file of Shapefile with
            //other external dta sources such as from Xaml as shown in this sample.
            PlaneData = PlaneData.Load();
            Classes = this.PlaneData.Classes.Cast<SeatClass>().ToList();
            _allClass = this.Classes.FirstOrDefault(c => c.SeatCategory == Resources.AppStrings.All);
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets or sets the brushes to use for the map coloring.Used to show matching colrs in the legend for SeatType.
		/// </summary>
        public BrushCollection SeatBrushes { get; set; }
		/// <summary>
		/// Gets the seat classes. Data source for the classes grid and the customer satisfaction chart.
		/// </summary>
		public IEnumerable<SeatClass> Classes
		{
			get { return _classes; }
			private set
			{
				if (_classes == value) return;

				_classes = value;
				OnPropertyChanged("Classes");
			}
		}

		/// <summary>
		/// Gets or sets the selected seat class.
		/// </summary>
		public SeatClass SelectedClass
		{
			get { return _selectedClass; }
			set
			{
				if (_selectedClass == value) return;

				_selectedClass = value;
				OnSelectedClassChanged();
				OnPropertyChanged("SelectedClass");
			}
		}
        private List<PlaneElementViewModel> _seatVMs;
        /// <summary>
        /// Gets or sets the itemsSource for the Seat Polygon series.
        /// </summary>
        public List<PlaneElementViewModel> SeatVMs
        {
            get { return _seatVMs; }
            private set
            {
                if (_seatVMs == value) return;

                _seatVMs = value;
            }
        }

		/// <summary>
		/// Gets or sets the selected seat.
		/// </summary>
		public SeatViewModel SelectedSeat
		{
			get { return _selectedSeat; }
			private set
			{
				if (_selectedSeat == value) return;

				if (value != null)
				{
					value.IsSelected = true;
				}

				SeatViewModel prevSelectedSeat = _selectedSeat;
				_selectedSeat = value;

				if (prevSelectedSeat != null)
				{
					prevSelectedSeat.IsSelected = false;
				}

				OnPropertyChanged("SelectedSeat");
			}
		}

		#region Amenities
		/// <summary>
		/// Gets a value indicating whether this instance has food.
		/// </summary>
		/// <value><c>true</c> if this instance has food; otherwise, <c>false</c>.</value>
        public bool HasFood { get { return PlaneData.HasFood; } }
		/// <summary>
		/// Gets a value indicating whether this instance has audio.
		/// </summary>
		/// <value><c>true</c> if this instance has audio; otherwise, <c>false</c>.</value>
        public bool HasAudio { get { return PlaneData.HasAudio; } }
		/// <summary>
		/// Gets a value indicating whether this instance has video.
		/// </summary>
		/// <value><c>true</c> if this instance has video; otherwise, <c>false</c>.</value>
        public bool HasVideo { get { return PlaneData.HasVideo; } }
		/// <summary>
		/// Gets a value indicating whether this instance has power.
		/// </summary>
		/// <value><c>true</c> if this instance has power; otherwise, <c>false</c>.</value>
        public bool HasPower { get { return PlaneData.HasPower; } }
		/// <summary>
		/// Gets a value indicating whether this instance has internet.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has internet; otherwise, <c>false</c>.
		/// </value>
        public bool HasInternet { get { return PlaneData.HasInternet; } }
		/// <summary>
		/// Gets a value indicating whether this instance has infant.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has infant; otherwise, <c>false</c>.
		/// </value>
        public bool HasInfant { get { return PlaneData.HasInfant; } }
		#endregion
		#endregion Properties

		#region Public Methods

        public void SelectSeat(SeatViewModel seatVM)
        {
            if (SelectedSeat == null || SelectedSeat != seatVM)
            {
                if (!seatVM.IsDisabled && !seatVM.IsOccupied)
                {
                    SelectedSeat = seatVM;
                    //RefreshMapElements();
                }
            }
        }

        #endregion Public Methods

        #region Private Mehods
        /// <summary>
		/// Called when selected class is changed to disable selection of any seat other than the selected class. 
		/// </summary>
		private void OnSelectedClassChanged()
		{
            if (SelectedSeat != null)
            {
                if (SelectedSeat.Class != _selectedClass.SeatCategory)
                {
                    SelectedSeat = null;
                }
            }

            foreach (SeatViewModel seatVM in this.SeatVMs)
			{
                seatVM.IsDisabled = _selectedClass != _allClass && seatVM.Class != _selectedClass.SeatCategory;
			}
		}
        #endregion Private Mehods

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
        internal MapCartesianProjection MapProjector { get; set; }
        internal System.Windows.Rect GetCartesianBounds(ShapefileConverter shapefileConverter)
        {
            return this.MapProjector.GetCartesianBounds(shapefileConverter);
        }
        private Rect ShapeFilesMaxBounds = new Rect(0, 0, 1, 1);
        internal void InitializeMapBounds(ShapefileConverter outer)
        {
            //This sets the outermost bound (smallest rectangle) such that the points, lines and polygons contained in all the 
            //the shapefiles are within this bounding rectangle.
            //In this sample, the shapefile used by the BodyLin series (to draw the Airplane outline) contains all other series within it.
            //If a single series were not to contain all the other series, a union of rectangles containg such partially overlapping
            //series should be taken to compute the outermost dinding rectangle for the entire map.
            this.ShapeFilesMaxBounds = this.GetCartesianBounds(outer);
            //Add a little extra space all around the bounding rectangle 
            this.ShapeFilesMaxBounds = this.ShapeFilesMaxBounds.Expand(new Thickness(10, 10, 10, 10));

            this.MapProjector.InitializeMapBounds(this.ShapeFilesMaxBounds);
        }

        internal List<PlaneElementViewModel> GetDataForAllOtherSeries(ShapefileConverter shapeFileSeatsPol)
        {
            //If the itemsSource for a series does not need any  data other than the points and id, we don't need to have a derived viewmodel class. 
            //Instead we can use a list of base class viemodels, i.e. PlaneElementViewModel as the itemsSource of a series.
            var planeElementVMs = new List<PlaneElementViewModel>();
            foreach (ShapefileRecord record in shapeFileSeatsPol)
            {
                //create PlaneElementViewModel for each record in the passed shapefileconverter and
                //add it to list of PlaneElementViewModels. This list will be the data source for the associated series.
                var planeElementVM = new PlaneElementViewModel();
                planeElementVM.Shape = this.MapProjector.ProjectToGeographic(record.Points);
                planeElementVM.Id = record.Fields["ID"].ToString();
                planeElementVMs.Add(planeElementVM);
            }

            return planeElementVMs;
        }
        private Dictionary<string, Rect> BoundingBoxForSeatClass = new Dictionary<string, Rect>();
        internal List<PlaneElementViewModel> GetDataForSeatsPolSeries(ShapefileConverter shapeFileSeatsPol)
        {
            this.SeatVMs = new List<PlaneElementViewModel>();
            foreach (ShapefileRecord record in shapeFileSeatsPol)
            {
                //create SeatViewModel for each record in the passed shapefileconverter and
                //add it to list of seatViewmodels. This list will be the data source for the seatPolseries.
                var seatVM = new SeatViewModel();
                seatVM.Id = record.Fields["ID"].ToString();
                seatVM.Shape = this.MapProjector.ProjectToGeographic(record.Points);
                seatVM.Row = record.Fields["ROW"].ToString();
                seatVM.Column = record.Fields["COLUMN"].ToString();
                //Get the seat position and class from the shape file.
                seatVM.Class = record.Fields["CLASS"].ToString(); 
                //string seatClassName = record.Fields["CLASS"].ToString();

                //Lookup the seat information from the XAML data file and create default if not present
                //as it is assumed the shape file holds the plane structure information.
                SeatViewModel seatVMFromPlaneData = PlaneData.GetSeatVM(seatVM.Row,seatVM.Column) ??
                                                    new SeatViewModel { Row = seatVM.Row, 
                                                                        Column = seatVM.Column,
                                                                        IsOccupied = false,
                                                                        SeatType = SeatType.StandardSeat };
                //Values of Properties like the SeatType are not available from the .dbf file of shapefile. 
                //Set such properties from the PlaneData object.
                seatVM.SeatType = seatVMFromPlaneData.SeatType;
                seatVM.IsOccupied = seatVMFromPlaneData.IsOccupied;
                this.SeatVMs.Add(seatVM);

                //Increment Seat Count for the seatClass as we go through the shape file elements.
                IncrementSeatCountByClass(seatVM.Class);
            }
            _allClass.SeatCount = shapeFileSeatsPol.Count();
            SelectedClass = _allClass;
            return this.SeatVMs;
        }

        private void IncrementSeatCountByClass(string seatClassName)
        {
            SeatClass seatClass = this.Classes.FirstOrDefault(x => x.SeatCategory == seatClassName);
            if (seatClass == null)
            {
                throw new ArgumentException("Seat class not found in data file.");
            }

            ++seatClass.SeatCount;
        }
        internal List<PlaneElementViewModel> GetDataForSeatLabelsLinSeries(ShapefileConverter shapeFileSeatLabelsLin)
        {
            //If the itemesSource for a series does not need any  data other than the points and id, we don't need to have a derived viewmodel class. 
            //Instead we can use a list of base class viemodel, i.e. PlaneElementViewModel as the itemsSource of a series.
            var planeElementVMs = new List<PlaneElementViewModel>();
            foreach (ShapefileRecord record in shapeFileSeatLabelsLin)
            {
                //create PlaneElementViewModel for each record in the passed shapefileconverter and
                //add it to list of PlaneElementViewModels. This list will be the data source for the associated series.
                var planeElementVM = new SeatLabelViewModel();
                planeElementVM.Shape = this.MapProjector.ProjectToGeographic(record.Points);
                planeElementVM.Id = record.Fields["ID"].ToString();
                planeElementVM.Label = record.Fields["LABEL"].ToString();
                planeElementVMs.Add(planeElementVM);
            }

            return planeElementVMs;
        }
        internal List<PlaneElementViewModel> GetDataForAreaIconsLinSeries(ShapefileConverter shapeFileAreaIconsLin)
        {
            //If the itemesSource for a series does not need any  data other than the points and id, we don't need to have a derived viewmodel class. 
            //Instead we can use a list of base class viemodel, i.e. PlaneElementViewModel as the itemsSource of a series.
            var planeElementVMs = new List<PlaneElementViewModel>();
            foreach (ShapefileRecord record in shapeFileAreaIconsLin)
            {
                //create PlaneElementViewModel for each record in the passed shapefileconverter and
                //add it to list of PlaneElementViewModels. This list will be the data source for the associated series.
                var planeElementVM = new AreaIconsViewModel();
                planeElementVM.Shape = this.MapProjector.ProjectToGeographic(record.Points);
                planeElementVM.Id = record.Fields["ID"].ToString();
                planeElementVM.Type = record.Fields["TYPE"].ToString();
                planeElementVMs.Add(planeElementVM);
            }
            return planeElementVMs;
        }

    }
}
