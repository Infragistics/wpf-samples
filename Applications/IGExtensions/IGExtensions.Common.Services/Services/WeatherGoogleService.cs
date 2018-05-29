using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;                           // HttpWebRequest
using System.Runtime.Serialization;         // DataContract, DataMember
using System.Runtime.Serialization.Json;    // DataContractJsonSerializer
using System.Threading;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Text;
using IGExtensions.Framework;

namespace IGExtensions.Common.Services
{
  
    /// <summary>
    /// Represents a service that retrieves weather data from Google 
    /// </summary>
    public class WeatherService
    {
        protected const string WeatherGoogleWebsite = "http://www.google.com/ig/api?weather={0}";
        protected const string WeatherNoaaWebsite = "http://weather.noaa.gov/pub/data/observations/metar/decoded/{0}.TXT";
        public WeatherService()
        {
            //TODO load ICAO codes for using them with city names
        }

        #region Google
        /// <summary>
        /// Requests weather data from 
        /// </summary>
        [Obsolete("This method is obsolete as of November 1st, 2013; use GetWeatherNOAA instead")]
        public static WeatherService GetWeatherGoogle(string location)
        {
            var wgs = new WeatherService();
            var request = WebRequest.Create(new Uri(string.Format(WeatherGoogleWebsite, location))) as HttpWebRequest;
            if (request != null) request.Method = "GET";

            var state = new WeatherGoogleRequestState { Request = request };
            if (request != null) request.BeginGetResponse(new AsyncCallback(RequestCallbackWeatherGoogle), state);
            return wgs;
        }
        /// <summary>
        /// Request's callback for weather from Google
        /// </summary>
        private static void RequestCallbackWeatherGoogle(IAsyncResult result)
        {
            var requestState = result.AsyncState as WeatherGoogleRequestState;
            if (requestState != null)
            {
                requestState.Response = (HttpWebResponse)requestState.Request.EndGetResponse(result);
                requestState.StreamResponse = requestState.Response.GetResponseStream();

                var reader = new StreamReader(requestState.StreamResponse);
                string webResult = reader.ReadToEnd();
                Debug.WriteLine(webResult);
            }
        }
        #endregion
        #region NOAA
        /// <summary>
        /// Requests weather data from Google
        /// </summary>
        private void GetWeatherNOAA(string codeICAO) //, Action<string, IEnumerable<string>> callback
        {
            //var ws = new WeatherService();
            //var request = WebRequest.Create(new Uri(string.Format(WeatherNoaaWebsite, codeICAO))) as HttpWebRequest;
            //if (request != null) request.Method = "GET";

            //var state = new WeatherNoaaRequestState { Request = request };
            //if (request != null) request.BeginGetResponse(new AsyncCallback(RequestCallbackWeatherNOAA), state);
            //return ws;
            var link = string.Format(WeatherNoaaWebsite, codeICAO);
            var uri = new Uri(link);
            //var wc = new WebClient();
            //wc.OpenReadCompleted += OnRefreshDataComplete;
           // wc.OpenReadAsync(uri);

            //   wc.OpenReadAsync(new uri, new Tuple<string, Action<string, IEnumerable<string>>>(codeICAO, callback));
            var webReq = WebRequest.Create(uri) as HttpWebRequest;
            webReq.Method = "GET";
           // IAsyncResult asyncResult = webReq.BeginGetResponse(new AsyncCallback(ResponseCallback_http), webReq);


            var req = HttpWebRequest.Create(link);
            req.BeginGetResponse(GetResponseCompleted, req);
       

            //var request = (HttpWebRequest)HttpWebRequest.Create(uri);
            //var state = new WeatherNoaaRequestState { Request = request };
            //var response = (HttpWebResponse)request.BeginGetResponse(new AsyncCallback(RequestCallbackWeatherNOAA), state);
            
            //var response = (HttpWebResponse)request.GetResponse();
            //Stream st = response.GetResponseStream();
            //using (var sr = new StreamReader(st))
            //{
            //    fileline = sr.ReadLine();
            //}
          }
        public void GetResponseCompleted(IAsyncResult result)
        {
            var request = (HttpWebRequest)result.AsyncState;
            var response = request.EndGetResponse(result);
         
        }

        private void ResponseCallback_http(IAsyncResult result)
        {
            var request = (HttpWebRequest)result.AsyncState;
            var response = (HttpWebResponse)request.EndGetResponse(result);
            Stream stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            Console.WriteLine(reader.ReadToEnd());

        }
        private void OnRefreshDataComplete(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                DebugManager.LogWarning("WeatherService->Failed to refresh stock data: " + e.Error);
                return;
            }

            var result = ProcessWeatherData(e.Result);
            var objectState = (Tuple<string, Action<string, IEnumerable<string>>>)e.UserState;

            var symbol = objectState.Item1;
            var callback = objectState.Item2;

            //_cache.UpdateCache(symbol, result);

            //SynchronizationContext.Current.Post(x => callback(symbol, (IEnumerable<string>)x), result);
        }
        private string ProcessWeatherData(Stream weatherData)
        {
            return string.Empty;

        }
        /// <summary>
        /// Request's callback for weather from Google
        /// </summary>
        private static void RequestCallbackWeatherNOAA(IAsyncResult result)
        {
            var requestState = result.AsyncState as WeatherNoaaRequestState;
            if (requestState != null)
            {
                requestState.Response = (HttpWebResponse)requestState.Request.EndGetResponse(result);
                requestState.StreamResponse = requestState.Response.GetResponseStream();

                var reader = new StreamReader(requestState.StreamResponse);
                string webResult = reader.ReadToEnd();
                Debug.WriteLine(webResult);
            }
        }
        #endregion
    }
    /// <summary>
    /// Represents a state for weather request from Google.
    /// </summary>
    public class WeatherNoaaRequestState
    {
        public WeatherNoaaRequestState()
        {
            BufferRead = new byte[BUFFER_SIZE];
            requestData = new StringBuilder("");
        }

        // This class stores the State of the request.
        const int BUFFER_SIZE = 1024;
        public StringBuilder requestData;
        public byte[] BufferRead;

        public HttpWebRequest Request { get; set; }
        public HttpWebResponse Response { get; set; }
        public Stream StreamResponse { get; set; }
    }

    /// <summary>
    /// Represents a state for weather request from Google.
    /// </summary>
    public class WeatherGoogleRequestState
    {
        public WeatherGoogleRequestState()
        {
            BufferRead = new byte[BUFFER_SIZE];
            requestData = new StringBuilder("");
        }

        // This class stores the State of the request.
        const int BUFFER_SIZE = 1024;
        public StringBuilder requestData;
        public byte[] BufferRead;
        
        public HttpWebRequest Request { get; set; } 
        public HttpWebResponse Response { get; set; }
        public Stream StreamResponse { get; set;}
    }


}