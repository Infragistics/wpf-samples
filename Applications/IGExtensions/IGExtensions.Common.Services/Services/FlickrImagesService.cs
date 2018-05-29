using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Windows;

namespace IGExtensions.Common.Services
{
#if SILVERLIGHT
    ///<summary>
    /// Represents a service for quarrying images from Yahoo pipes
    ///</summary>
    public sealed class FlickrImagesService : IApplicationService
#else // if WPF
    /// <summary>
    /// Represents a service for quarrying images from Yahoo pipes
    /// </summary>
    public sealed class FlickrImagesService
#endif
    {
        #region Private Memeber Variables
        private const string ServiceUrl = "http://pipes.yahooapis.com/pipes/pipe.run?_id=d7071fcba844771dea335d8445556992&_render=json&q={0}&loc={1}&max={2}";
        #endregion Private Memeber Variables

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrImagesService"/> class.
        /// </summary>
        public FlickrImagesService()
        {
        }
        #endregion Constructors

        #region Public Methods
        /// <summary>
        /// Gets the flickr data.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="location">The location.</param>
        /// <param name="max">The max.</param>
        /// <param name="callback">The callback.</param>
        public void GetFlickrData(string query, string location, int max, Action<IEnumerable<FlickrData>> callback)
        {
            //var uri = new Uri(string.Format(ServiceUrl,
            //        System.Windows.Browser.HttpUtility.UrlEncode(query),
            //        System.Windows.Browser.HttpUtility.UrlEncode(location),
            //        max));

            var uri = new Uri(Uri.EscapeUriString(string.Format(ServiceUrl, query, location, max)));


            var client = new WebClient();

            client.OpenReadCompleted += OpenReadCompleted;
            client.OpenReadAsync(uri, callback);
        }

        #region IApplicationService Members

#if SILVERLIGHT 
        /// <summary>
        /// Called by an application in order to initialize the application extension service.
        /// </summary>
        /// <param name="context">Provides information about the application state.</param>
        public void StartService(ApplicationServiceContext context)
        {
        }

        /// <summary>
        /// Called by an application in order to stop the application extension service.
        /// </summary>
        public void StopService()
        {
        }
#endif

        #endregion
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Opens the read completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private static void OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            IEnumerable<FlickrData> result = null;
            if (!e.Cancelled && e.Error == null)
            {
                using (Stream stream = e.Result)
                {
                    result = ProcessResult(stream);
                }
            }

            var callback = (Action<IEnumerable<FlickrData>>)e.UserState;
            SynchronizationContext.Current.Post(x => callback((IEnumerable<FlickrData>)x), result);
        }

        /// <summary>
        /// Processes the result.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        private static IEnumerable<FlickrData> ProcessResult(Stream stream)
        {
            var serializer = new DataContractJsonSerializer(typeof(JsonRoot),
                new[] {
				   typeof (JsonRoot),
				   typeof (JsonContent),
				   typeof (JsonImage),
				   typeof (JsonImage[]),
				   typeof (List<JsonImage>)
			});

            var result = (JsonRoot)(serializer.ReadObject(stream));
            return result.value.items.Select(x => new FlickrData(x.ImgSrc, x.Lon, x.Lat, new DateTime(x.Year, x.Month, x.Day)));
        }
        #endregion Private Methods

        #region Public Classes
        [DataContract]
        public sealed class JsonRoot
        {
            [DataMember(Name = "value")]
            public JsonContent value;
        }

        [DataContract]
        public sealed class JsonContent
        {
            [DataMember(Name = "items")]
            public JsonImage[] items;
        }

        [DataContract]
        public sealed class JsonImage
        {
            [DataMember(Name = "ImgSrc")]
            public string ImgSrc;

            [DataMember(Name = "Lon")]
            public double Lon;

            [DataMember(Name = "Lat")]
            public double Lat;

            [DataMember(Name = "Day")]
            public int Day;

            [DataMember(Name = "Month")]
            public int Month;

            [DataMember(Name = "Year")]
            public int Year;
        }
        #endregion Public Classes
    }
}
