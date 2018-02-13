using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;
using IGExtensions.Common.Models;
using IGShowcase.AirplaneSeatingChart.ViewModels;

namespace IGShowcase.AirplaneSeatingChart.Models
{
    //new code starts
    public class PlaneElement
    {
        public PlaneElement()
        {
            this.Shape = new List<List<Point>>();
        }
        public PlaneElement(string id, List<List<Point>> shape)
        {
            this.Id = id;
            this.Shape = shape;
        }
        private Point GetShapeCenter()
        {
            var geoCenter = this.Shape.ToShapeRect().Center(); 
            return geoCenter;
        }
        public Point ShapeCenter { get { return this.GetShapeCenter(); } }
        public List<List<Point>> Shape { get; set; }
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>The row.</value>
        public string Row { get; set; }
        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>The column.</value>
        public string Column { get; set; }
        /// <summary>
        /// Gets or sets the class.
        /// </summary>
        /// <value>The class.</value>
        public string Class { get; set; }
    }

    /// <summary>
	/// The root class for the plane data (amenities, seat classes and seats) that's de-serialized from XAML.
	/// </summary>
	public class PlaneData
	{
		#region Public Properties

		#region Amenities
		/// <summary>
		/// Gets or sets a value indicating whether this instance has food.
		/// </summary>
		/// <value><c>true</c> if this instance has food; otherwise, <c>false</c>.</value>
		public bool HasFood { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance has audio.
		/// </summary>
		/// <value><c>true</c> if this instance has audio; otherwise, <c>false</c>.</value>
		public bool HasAudio { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance has video.
		/// </summary>
		/// <value><c>true</c> if this instance has video; otherwise, <c>false</c>.</value>
		public bool HasVideo { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance has power.
		/// </summary>
		/// <value><c>true</c> if this instance has power; otherwise, <c>false</c>.</value>
		public bool HasPower { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance has internet.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has internet; otherwise, <c>false</c>.
		/// </value>
		public bool HasInternet { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance has infant.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has infant; otherwise, <c>false</c>.
		/// </value>
		public bool HasInfant { get; set; }
		#endregion Amenities

		/// <summary>
		/// Gets or sets the classes.
        // Classes prpoerty is of type non-generic ArrayList because we are populating it from the data in Xaml which does not support
        //the generic list to load SeaVMs directly
        /// <value>The classes.</value>
        /// </summary>
        public ArrayList Classes { get; set; }

		/// <summary>
		/// Gets or sets the seats.
        // SeatVMs prpoerty is of type non-generic ArrayList because we are populating it from the data in Xaml which does not support
        //the generic list to load SeaVMs directly
        /// </summary>
		/// <value>The seats.</value>
        public ArrayList SeatVMs { get; set; }
        //Rather than use the ArrayList elesewhere in the application we have added a GetSeatVM method below for a caller to get
        //a desired SeatVM object form the ArrayList.
        public SeatViewModel GetSeatVM(string Row, string Col)
        {
            SeatViewModel seatVM;
            foreach (Object o in this.SeatVMs)
            {
                seatVM = o as SeatViewModel;
                if (seatVM == null)
                    continue;
                if (seatVM.Row == Row && seatVM.Column == Col)
                    return seatVM;
            }
            return null;
        }
        #endregion Public Properties

		#region Static Methods
		/// <summary>
		/// Loads an instance of PlaneData class from a XAML file with the specified URI.
		/// </summary>
		/// <returns></returns>
		public static PlaneData Load()
		{
		    var planeData = new PlaneData();

#if SILVERLIGHT 
            planeData = (PlaneData)(XamlReader.Load(Resources.PlaneData.Xaml));
#else  // WPF
            using (var sr = new StringReader(Resources.PlaneData.Xaml))
            {
                using (var xmlReader = XmlReader.Create(sr))
                {
                    planeData = (PlaneData)(XamlReader.Load(xmlReader));
                }
            }
#endif
            planeData.AddAllClass();
            planeData.PopulateCustomerSatisfaction();
            return planeData;
        }
        private void AddAllClass()
        {
            var _allClass = new SeatClass
            {
                SeatCategory = Resources.AppStrings.All,
                Description = Resources.AppStrings.Seats,
                //CustomerSatisfaction = new CustomerSatisfactionDataCollection()
                CustomerSatisfactionArrayList = new ArrayList()
            };

            this.Classes.Insert(0,_allClass);
        }
        private void PopulateCustomerSatisfaction ()
        {

            foreach (SeatClass s in this.Classes)
            {
                s.PopulateCustomerSatsfaction();
            }
        }

		#endregion Static Methods
	}
}
