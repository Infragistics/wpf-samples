using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IGExtensions.Common.Models
{
    //sealed
    public class EarthQuakeData : GeoLocation
    {
        #region Constructors
        public EarthQuakeData()
        {
            Magnitude = 0;
            Depth = 0;
            Latitude = 0;
            Longitude = 0;
            Updated = DateTime.MaxValue;
            Time = DateTime.MaxValue;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EarthQuakeData"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="title">The title.</param>
        /// <param name="updated">The updated.</param>
        /// <param name="link">The link.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="magnitude">The magnitude.</param>
        public EarthQuakeData(string id, string title, DateTime updated, Uri link,
            double latitude, double longitude, double depth, double magnitude)
        {
            Code = id;
            Title = title;
            Updated = updated;
            Link = link;
            Latitude = latitude;
            Longitude = longitude;
            Depth = depth;
            Magnitude = magnitude;
        }
        #endregion Constructors

        #region Public Properties
        //public string Id { get; set; }
        public string Title { get; set; }
        public string Region { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Time { get; set; }
        public Uri Link { get; set; }
        public Uri Details { get; set; }
        public int Reports { get; set; }
        public int Stations { get; set; }
        public int Intensity { get; set; }
        public int Significance { get; set; }
        public bool Tsunami { get; set; }
        public string Code { get; set; }
        public string EventCodes { get; set; }
        public string Sources { get; set; }
        public string Network { get; set; }
      
        //public double Latitude { get; private set; }
        //public double Longitude { get; private set; }
        private double _depth;
        /// <summary>
        /// Gets or sets Depth property 
        /// </summary>
        public double Depth
        {
            get {  return _depth; }
            set { if (_depth == value) return; _depth = value; OnPropertyChanged("Depth"); }
        }
        private double _magnitude;
        /// <summary>
        /// Gets or sets Magnitude property 
        /// </summary>
        public double Magnitude
        {
            get {  return _magnitude; }
            set { if (_magnitude == value) return; _magnitude = value; OnPropertyChanged("Magnitude"); }
        }
        //public double Depth { get; private set; }
        //public double Magnitude { get; private set; }
        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
        public override bool Equals(object obj)
        {
            var eqd = obj as EarthQuakeData;
            if (eqd == null) return false;
            return Code == eqd.Code;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
        #endregion Pubblic Methods

        public new string ToString()
        {
            var result = new StringBuilder();
            result.Append("time " + this.Updated + ", ");
            result.Append("mag " + this.Magnitude + ", ");
            result.Append("place " + this.Title + ", ");
            result.Append("geo {" + this.ToPoint() + "}, ");
            result.Append("dep " + this.Depth + ", ");
          
            result.Append("reg " + this.Region + ", ");
            result.Append("rep " + this.Reports + ", ");
            result.Append("int " + this.Intensity + ", ");
            result.Append("sta " + this.Stations + ", ");
            result.Append("tsunami " + this.Tsunami + ", ");
            result.Append("net " + this.Network + ", ");
     
            return result.ToString();
        }
    }
    //public sealed class EarthQuakeList : List<EarthQuakeData>
    //{
    //    public void SortByMagnitude()
    //    {
    //        this.Sort((item1, item2) => -(Comparer<double>.Default.Compare(item1.Magnitude, item2.Magnitude)));
    //    }
    //    //public Comparison<T> Comparison()
    //    //{
            
    //    //}
    //}
    public class EarthQuakes : List<EarthQuakeData>
    {
        public EarthQuakes()
        {
            Initialize();
        }
        public void Initialize()
        {
            this.Magnitude = new DoubleRange();
            this.Depth = new DoubleRange();
            this.Updated = new DateTimeRange();
        }
        public void Recalculate()
        {
            Initialize();
            foreach (var item in this)
            {
                this.Magnitude.Update(item.Magnitude);
                this.Depth.Update(item.Depth);
                this.Updated.Update(item.Updated);
            }
        }
        public DoubleRange Magnitude { get; set; }
        public DoubleRange Depth { get; set; }
        public DateTimeRange Updated { get; set; }
         

    }
  
}