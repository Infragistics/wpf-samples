using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using IGExtensions.Common.Converters;
using IGExtensions.Common.Models;
using IGExtensions.Common.Services;

namespace IGShowcase.EarthQuake.ViewModels
{
	public sealed class TimeLineViewModel : INotifyPropertyChanged, IPropertyChangedListener
	{
		#region Private Member Variables
		private readonly EarthQuakesService _service;
		private IEnumerable<EarthQuakeViewModel> _earthQuakes;
		private readonly FilterViewModel _regionsFilter;

		private DateTime _minTime;
		private DateTime _maxTime;
		private DateTime _selectedTime;

		private double _scrollPosition;
		private double _scrollScale;
		private WarpGradientParameters _warpGradientParameters;
		#endregion Private Member Variables

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeLineViewModel"/> class.
		/// </summary>
		public TimeLineViewModel()
		{
			var now = DateTime.UtcNow;
			_maxTime = now;
			_minTime = now.AddHours(-24);

			_scrollPosition = 0;
			_scrollScale = 1.0;
			_warpGradientParameters = CalculateSkyGradient();

			_service = ServiceLocator.GetInstance<EarthQuakesService>();
			//NOTE: service is null when Blend calls this constructor when opening the TimeLineView.xaml
			if (_service != null)
			{
                _service.RequestMode = EarthQuakeRequestMode.LastWeek;
                _regionsFilter = new FilterViewModel(_service.EartQuakeRegionNames, _service.SelectedRegions);
				_regionsFilter.FilterChanged += RegionsFilterPropertyChanged;

				//Register using a weak event on the service PropertyChanged event.
				WeakPropertyChangedListener.CreateIfNecessary(_service, this);
				UpdateEarthQuakeData(_service.EarthQuakes);

                if (!_service.IsStarted)
                {
                    _service.StartService();
                }
			}
		}
		#endregion Constructors

		#region Public Properties
		/// <summary>
		/// Gets the earthquakes collection.
		/// </summary>
		/// <value>The earthquakes.</value>
		public IEnumerable<EarthQuakeViewModel> EarthQuakes
		{
			get { return _earthQuakes; }
			private set
			{
				if (value != _earthQuakes)
				{
					_earthQuakes = value;
					OnPropertyChanged("EarthQuakes");
				}
			}
		}

		/// <summary>
		/// Gets or sets the min time.
		/// </summary>
		/// <value>The min time.</value>
		public DateTime MinTime
		{
			get { return _minTime; }
			set
			{
				if (_minTime != value)
				{
					_minTime = value;
					OnPropertyChanged("MinTime");
				}
			}
		}

		/// <summary>
		/// Gets or sets the max time.
		/// </summary>
		/// <value>The max time.</value>
		public DateTime MaxTime
		{
			get { return _maxTime; }
			set
			{
				if (_maxTime != value)
				{
					_maxTime = value;
					OnPropertyChanged("MaxTime");
				}
			}
		}

		/// <summary>
		/// Gets or sets the selected time.
		/// </summary>
		/// <value>The selected time.</value>
		public DateTime SelectedTime
		{
			get { return _selectedTime; }
			set
			{
				if (_selectedTime != value)
				{
					_selectedTime = value;
					OnPropertyChanged("SelectedTime");
				}
			}
		}

		/// <summary>
		/// Gets or sets the scroll position.
		/// </summary>
		/// <value>The scroll position.</value>
		public double ScrollPosition
		{
			get { return _scrollPosition; }
			set
			{
				if (_scrollPosition != value)
				{
					_scrollPosition = value;
					OnPropertyChanged("ScrollPosition");
					WarpGradientParameters = CalculateSkyGradient();
				}
			}
		}

		/// <summary>
		/// Gets or sets the scroll scale.
		/// </summary>
		/// <value>The scroll scale.</value>
		public double ScrollScale
		{
			get { return _scrollScale; }
			set
			{
				if (_scrollScale != value)
				{
					_scrollScale = value;
					OnPropertyChanged("ScrollScale");
					WarpGradientParameters = CalculateSkyGradient();
				}
			}
		}

		/// <summary>
		/// Gets warp gradient parameters that control the sky gradient.
		/// </summary>
		/// <value>The warp gradient parameters.</value>
		public WarpGradientParameters WarpGradientParameters
		{
			get { return _warpGradientParameters; }
			private set
			{
				if (_warpGradientParameters != value)
				{
					_warpGradientParameters = value;
					OnPropertyChanged("WarpGradientParameters");
				}
			}
		}


		/// <summary>
		/// Gets or sets the min magnitude for filtering.
		/// </summary>
		/// <value>The min magnitude.</value>
		public double MinMagnitude
		{
            get { return _service.MagnitudeFilter.Min; }
            set { _service.MagnitudeFilter.Min = value; }
		}

		/// <summary>
		/// Gets or sets the max magnitude for filtering.
		/// </summary>
		/// <value>The max magnitude.</value>
		public double MaxMagnitude
		{
            get { return _service.MagnitudeFilter.Max; }
            set { _service.MagnitudeFilter.Max = value; }
		}

		/// <summary>
		/// Gets the regions filter items.
		/// </summary>
		/// <value>The regions filter items.</value>
		public FilterViewModel Regions
		{
			get { return _regionsFilter; }
		}
		#endregion Public Properties

		#region Public events
		public event EventHandler Shake;
		#endregion

		#region IPropertyChangedListener Members
		void IPropertyChangedListener.OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            if (e.PropertyName == "EarthQuakes")
			{
				UpdateEarthQuakeData(((EarthQuakesService)sender).EarthQuakes);
			}
			else if (e.PropertyName == "LastUpdated")
			{
				OnLastUpdated(((EarthQuakesService)sender).LastUpdated);
			}
            else if (e.PropertyName == "LatestEarthQuake")
			{
				OnLatestEarthquakeChanged(((EarthQuakesService)sender).LatestEarthQuake);
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

		#region Private Methods
		/// <summary>
		/// Called every time the earthquake service checks for new data. Updates the sky gradient.
		/// </summary>
		/// <param name="lastUpdated">The UTC time of the last update</param>
		private void OnLastUpdated(DateTime lastUpdated)
		{
			//Adjust the range of the axis
			MaxTime = lastUpdated;
			MinTime = lastUpdated.AddHours(-24);
			
			//Recalculate the sky gradient parameters.
			WarpGradientParameters = CalculateSkyGradient();
		}

		/// <summary>
		/// Called when there's a new earthquake. Selects the latest earthquake if visible
		/// and runs the shake animation.
		/// </summary>
		/// <param name="latestEarthquake">The latest earthquake.</param>
		private void OnLatestEarthquakeChanged(EarthQuakeData latestEarthquake)
		{
			EarthQuakeViewModel firstEarthquake = EarthQuakes.ElementAtOrDefault(0);
			if (firstEarthquake != null && latestEarthquake.Equals(firstEarthquake.Model))
			{
				SelectedTime = firstEarthquake.Updated;
			}
			OnShake(EventArgs.Empty);
		}

		/// <summary>
		/// Updates the earthquakes.
		/// </summary>
		/// <param name="quakes">The quakes.</param>
		private void UpdateEarthQuakeData(IEnumerable<EarthQuakeData> quakes)
		{
			if (quakes != null)
			{
				EarthQuakeViewModel[] newQuakes = quakes.Select(x => new EarthQuakeViewModel(x)).ToArray();

                //TODO fix issue when changing regions filter after switching between timeline/map views, breaks binding timeline to EarthQuakes
                this.EarthQuakes = quakes.Select(eq => new EarthQuakeViewModel(eq)).ToArray();
                
                //this.EarthQuakes = newQuakes;
				if (!newQuakes.Any(x => x.Updated == SelectedTime))
				{
					SelectedTime = newQuakes.Length > 0 ? newQuakes[0].Updated : DateTime.MinValue;
				}
			}
		}

		/// <summary>
		/// Called when the regions filter is changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void RegionsFilterPropertyChanged(object sender, EventArgs e)
		{
			_service.SelectedRegions = _regionsFilter.SelectedItems;
		}

		/// <summary>
		/// Calculates the sky gradient.
		/// </summary>
		/// <returns></returns>
		private WarpGradientParameters CalculateSkyGradient()
		{
			//86400 number of seconds in the day
			double seconds = (MaxTime - MinTime).TotalSeconds;
			double offset = ScrollPosition * (1 - ScrollScale);
			offset = (offset * seconds + MinTime.TimeOfDay.TotalSeconds) / 86400;
			offset -= Math.Floor(offset);
			double scale = 86400 / (seconds * ScrollScale);
			return new WarpGradientParameters(offset, scale);
		}

		private void OnShake(EventArgs args)
		{
			if (Shake != null)
			{
				Shake(this, args);
			}
		}
		#endregion Private Methods

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
