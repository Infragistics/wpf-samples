using System;

namespace IGExtensions.Common.Services
{
    ///<summary>
    /// Represents class with information about an images from Yahoo pipes
    ///</summary>
    public sealed class FlickrData
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrData"/> class.
        /// </summary>
        /// <param name="imgSrc">The img SRC.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="dateTaken">The date taken.</param>
        public FlickrData(string imgSrc, double longitude, double latitude, DateTime dateTaken)
        {
            ImgSrc = imgSrc;
            Longitude = longitude;
            Latitude = latitude;
            DateTaken = dateTaken;
        }
        #endregion Constructors

        #region Public Properties
        ///<summary>
        /// Gets source path to an image
        ///</summary>
        public string ImgSrc { get; private set; }
        ///<summary>
        /// Gets Latitude of geographic location associated with the image
        ///</summary>
        public double Latitude { get; private set; }
        ///<summary>
        /// Gets Longitude of geographic location associated with the image
        ///</summary>
        public double Longitude { get; private set; }
        ///<summary>
        /// Gets DateTime when the image was taken
        ///</summary>
        public DateTime DateTaken { get; private set; }
        #endregion  
    }
}
