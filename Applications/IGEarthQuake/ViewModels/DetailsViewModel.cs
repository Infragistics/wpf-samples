using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using IGExtensions.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using IGExtensions.Common.Services;

namespace IGShowcase.EarthQuake.ViewModels
{
	public sealed class DetailsViewModel : INotifyPropertyChanged
	{
		private const int ImagesCount = 3;
		private static readonly Regex RegexLocation = new Regex("\\b(south\\w*|southern\\w*|east\\w*|west\\w*|north\\w*northern\\w*|of\\w*|central\\w*|region\\w*|coast\\w*|border\\w*)\\s*", RegexOptions.IgnoreCase);

		#region Private Member Variables
		private readonly EarthQuakeData _data;

		private ObservableCollection<FlickrData> _flickrCollection;
		private Visibility _imgsNotFoundTextVisibility;
		private bool _isFlickrBusy;
		private bool _isFirstTime;
		#endregion Private Member Variables

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DetailsViewModel"/> class.
		/// </summary>
		/// <param name="data">The data.</param>
		public DetailsViewModel(EarthQuakeData data)
		{
			_data = data;
			Location = RegexLocation.Replace(_data.Title, "");

			SummaryLink = new Uri(string.Format("{0}#summary", data.Link));
			DetailsLink = new Uri(string.Format("{0}#details", data.Link));
			MapsLink = new Uri(string.Format("{0}#maps", data.Link));
			ScitechLink = new Uri(string.Format("{0}#scitech", data.Link));

			_isFirstTime = true;
			_imgsNotFoundTextVisibility = Visibility.Collapsed;
		}
		#endregion Constructors

		#region Public Properties
		#region Properties forwarded from EarthQuakeData
		public string Title
		{
			get { return _data.Title; }
		}

		public DateTime Updated
		{
			get { return _data.Updated; }
		}

		public Uri Link
		{
			get { return _data.Link; }
		}

		public double Latitude
		{
			get { return _data.Latitude; }
		}

		public double Longitude
		{
			get { return _data.Longitude; }
		}

		public double Depth
		{
			get { return _data.Depth / 1000; }
		}

		public double Magnitude
		{
			get { return _data.Magnitude; }
		}
		#endregion Properties forwarded from EarthQuakeData

		public string Location { get; private set; }

		public Uri SummaryLink { get; private set; }

		public Uri DetailsLink { get; private set; }

		public Uri MapsLink { get; private set; }

		public Uri ScitechLink { get; private set; }

		/// <summary>
		/// Gets the flickr collection.
		/// </summary>
		/// <value>The flickr collection.</value>
		public ObservableCollection<FlickrData> FlickrCollection
		{
			get
			{
				if (_isFirstTime)
				{
					_isFirstTime = false;
					IsFlickrBusy = true;

					FlickrImagesService service = ServiceLocator.GetInstance<FlickrImagesService>();
					service.GetFlickrData(Location + " earthquake", Location, ImagesCount,
						OnFlickDataReceived);
				}
				return _flickrCollection;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is flickr busy.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is flickr busy; otherwise, <c>false</c>.
		/// </value>
		public bool IsFlickrBusy
		{
			get { return _isFlickrBusy; }
			private set
			{
				if (_isFlickrBusy != value)
				{
					_isFlickrBusy = value;
					OnPropertyChanged("IsFlickrBusy");
				}
			}
		}

		/// <summary>
		/// Gets or sets the images not found text visibility.
		/// </summary>
		/// <value>The imgs text visibility.</value>
		public Visibility ImgsNotFoundTextVisibility
		{
			get { return _imgsNotFoundTextVisibility; }
			private set
			{
				if (_imgsNotFoundTextVisibility != value)
				{
					_imgsNotFoundTextVisibility = value;
					OnPropertyChanged("ImgsNotFoundTextVisibility");
				}
			}
		}
		#endregion Public Properties

		#region Private Methods
		/// <summary>
		/// Called when [flick data received]. If Flickr doesn't find pirtures, then make second call with different parameters.
		/// </summary>
		/// <param name="result">The result.</param>
		private void OnFlickDataReceived(IEnumerable<FlickrData> result)
		{
			if (result != null && result.Count() > 0)
			{
				ProcessFlickData(result);
				IsFlickrBusy = false;
			}
			else
			{
				FlickrImagesService service = ServiceLocator.GetInstance<FlickrImagesService>();
				service.GetFlickrData(Location, string.Empty, ImagesCount, OnFlickDataReceived2);
			}
		}

		/// <summary>
		/// Called when [flick data received2].If Flickr doesn't find pirtures, then show notification.
		/// </summary>
		/// <param name="result">The result.</param>
		private void OnFlickDataReceived2(IEnumerable<FlickrData> result)
		{
			if (result != null && result.Count() > 0)
			{
				ProcessFlickData(result);
			}
			else
			{
				ImgsNotFoundTextVisibility = Visibility.Visible;
			}
			IsFlickrBusy = false;
		}

		/// <summary>
		/// Fill the FlickrCollection with the results from flickr.
		/// </summary>
		/// <param name="result">The results from flickr.</param>
		private void ProcessFlickData(IEnumerable<FlickrData> result)
		{
			ObservableCollection<FlickrData>  fc = new ObservableCollection<FlickrData>();
			foreach (FlickrData data in result)
			{
				fc.Add(data);
			}
			_flickrCollection = fc;
			OnPropertyChanged("FlickrCollection");
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