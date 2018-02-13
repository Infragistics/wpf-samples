using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using IGExtensions.Common.Models;
using IGExtensions.Common.Services;

namespace IGShowcase.EarthQuake.ViewModels
{
    public class MapViewModel : INotifyPropertyChanged, IPropertyChangedListener
	{
		#region Private Variables
		private readonly EarthQuakesService _service;

        private EarthQuakeViewModel					_selectedEarthQuake;
        private IEnumerable<EarthQuakeViewModel>	_earthQuakes;
        private readonly FilterViewModel			_regionsFilter;
        private bool _isMyLocationEnabled = true;
        
		#endregion Private Variables

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="MapViewModel"/> class.
		/// </summary>
		public MapViewModel()
		{
            _earthQuakeRadiusScaleMin = 10;
            _earthQuakeRadiusScaleMax = 90;
            _earthQuakeColorScaleMin = 2.5;
            _earthQuakeColorScaleMax = 10;

            _service = ServiceLocator.GetInstance<EarthQuakesService>();
            // NOTE: service is null when Blend calls this constructor when opening the TimeLineView.xaml
            if (_service != null)
            {
                _service.RequestMode = EarthQuakeRequestMode.LastWeek;
                _regionsFilter = new FilterViewModel(_service.EartQuakeRegionNames, _service.SelectedRegions);
				_regionsFilter.FilterChanged += RegionsFilterPropertyChanged;

                WeakPropertyChangedListener.CreateIfNecessary(_service, this);
                GetEarthQuakeData(_service.EarthQuakes);

                if (!_service.IsStarted)
                {
                    _service.StartService();
                }
            }
        }
		#endregion Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the min magnitude.
		/// </summary>
		/// <value>The min magnitude.</value>
		public double MinMagnitude
        {
            get { return _service.MagnitudeFilter.Min; }
            set { _service.MagnitudeFilter.Min = value; }
        }

		/// <summary>
		/// Gets or sets the max magnitude.
		/// </summary>
		/// <value>The max magnitude.</value>
        public double MaxMagnitude 
        {
            get { return _service.MagnitudeFilter.Max; }
            set { _service.MagnitudeFilter.Max = value; }
        }

        public FilterViewModel Regions
        {
            get { return _regionsFilter; }
        }

        /// <summary>
        /// Gets or sets the earth quakes.
        /// </summary>
        /// <value>The earth quakes.</value>
        public IEnumerable<EarthQuakeViewModel> EarthQuakes
        {
            get { return _earthQuakes; }
            private set
            {
                if (value == _earthQuakes) return;

                _earthQuakes = value;
                OnPropertyChanged("EarthQuakes");
            }
        }

        private double _earthQuakeRadiusScaleMin;
        public double EarthQuakeRadiusScaleMin
        {
            get { return _earthQuakeRadiusScaleMin; }
            set 
            {
                if (_earthQuakeRadiusScaleMin == value) return;
                _earthQuakeRadiusScaleMin = value;
                OnPropertyChanged("EarthQuakeRadiusScaleMin"); 
            }
        }
        private double _earthQuakeRadiusScaleMax;
        public double EarthQuakeRadiusScaleMax
        {
            get { return _earthQuakeRadiusScaleMax; }
            set
            {
                if (_earthQuakeRadiusScaleMax == value) return;
                _earthQuakeRadiusScaleMax = value;
                OnPropertyChanged("EarthQuakeRadiusScaleMax");
            }
        }

        private double _earthQuakeColorScaleMin;
        public double EarthQuakeColorScaleMin
        {
            get { return _earthQuakeColorScaleMin; }
            set
            {
                if (_earthQuakeColorScaleMin == value) return;
                _earthQuakeColorScaleMin = value;
                OnPropertyChanged("EarthQuakeColorScaleMin");
            }
        }
        private double _earthQuakeColorScaleMax;
        public double EarthQuakeColorScaleMax
        {
            get { return _earthQuakeColorScaleMax; }
            set
            {
                if (_earthQuakeColorScaleMax == value) return;
                _earthQuakeColorScaleMax = value;
                OnPropertyChanged("EarthQuakeColorScaleMax");
            }
        }
		/// <summary>
		/// Gets or sets the selected earth quake.
		/// </summary>
		/// <value>The selected earth quake.</value>
        public EarthQuakeViewModel SelectedEarthQuake 
        { 
            get { return _selectedEarthQuake;}
            set
            {
            	if (_selectedEarthQuake == value) return;

            	_selectedEarthQuake = value;
            	OnPropertyChanged("SelectedEarthQuake");
            }
        }

        /// <summary>
        /// Gets or sets whether the user's location was found accurately.
        /// </summary>
        public bool IsMyLocationEnabled
        {
            get { return _isMyLocationEnabled; }
            set
            {
                if (_isMyLocationEnabled == value) return;

                _isMyLocationEnabled = value;
                OnPropertyChanged("IsMyLocationEnabled");
            }
        }
		#endregion Public Properties

		#region Public Methods
		/// <summary>
		/// Gets the bounding box.
		/// </summary>
		/// <param name="continent">The continent.</param>
		/// <param name="minLon">The min lon.</param>
		/// <param name="minLat">The min lat.</param>
		/// <param name="maxLon">The max lon.</param>
		/// <param name="maxLat">The max lat.</param>
        public void GetRegionBounds(string continent, out double minLon, out double minLat, out double maxLon, out double maxLat)
		{
            _service.GetRegionBounds(continent, out minLon, out minLat, out maxLon, out maxLat);
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// Regions the filter property changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void RegionsFilterPropertyChanged(object sender, EventArgs e)
		{
			_service.SelectedRegions = _regionsFilter.SelectedItems;
            
            // check if SelectedEarthQuake belongs to SelectedRegions
            if (!IsEarthQuakeInSelectedRegions(this.SelectedEarthQuake))
            {
                this.SelectedEarthQuake = null;
            }
		    
		}
        private bool IsEarthQuakeInSelectedRegions(EarthQuakeViewModel earthquake)
        {
            if (_regionsFilter == null || _regionsFilter.SelectedItems == null) return false;
            if (earthquake == null || earthquake.Model == null) return false;

            foreach (string region in _regionsFilter.SelectedItems)
            {
                if (region.Equals(earthquake.Model.Region))
                    return true;
            }
            return false;
        }

		/// <summary>
		/// Gets the earth quake data.
		/// </summary>
		/// <param name="quakes">The quakes.</param>
        private void GetEarthQuakeData(IEnumerable<EarthQuakeData> quakes)
		{
			if (quakes == null) return;

            var items = quakes.Select(eq => new EarthQuakeViewModel(eq)).ToArray();
		    this.EarthQuakes = items;
		}

    	#endregion Private Methods

		#region IPropertyChangedListener Members
		void IPropertyChangedListener.OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "EarthQuakes")
            {
				GetEarthQuakeData(((EarthQuakesService)sender).EarthQuakes);
            }
            else if (e.PropertyName == "MagnitudeFilter")
            {
                OnPropertyChanged("MinMagnitude");
                OnPropertyChanged("MaxMagnitude");
            }
            //else if(e.PropertyName=="MinMagnitude" || e.PropertyName=="MaxMagnitude")
            //{
            //    OnPropertyChanged(e.PropertyName);
            //}
        }
        #endregion

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
    }
}
