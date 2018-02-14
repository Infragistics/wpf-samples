using System;
using System.Windows;
using IGExtensions.Common.Models;

namespace IGShowcase.EarthQuake.ViewModels
{
	public sealed class EarthQuakeViewModel
	{
		#region Private Fields
		private readonly EarthQuakeData _data;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="EarthQuakeViewModel"/> class.
		/// </summary>
		/// <param name="data">The data.</param>
		public EarthQuakeViewModel(EarthQuakeData data)
		{
			_data	= data;
            Details = new DetailsViewModel(data);
		}
		#endregion Constructors

		#region Public Properties
		public DetailsViewModel     Details { get; private set; }
		public string			    Location { get { return Details.Location; } }
		public DateTime			    Updated { get { return _data.Updated; } }
        public double               Longitude { get { return _data.Longitude; } }
        public double               Latitude { get { return _data.Latitude; } }
		public double			    Magnitude { get { return _data.Magnitude; } }

	    public Point GeoLocation { get { return new Point(Longitude, Latitude); } }

		#endregion Public Properties

		#region Internal Properties
		internal EarthQuakeData Model { get { return _data; } }
		#endregion
	}
}
