using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;                           // HttpWebRequest
using System.Runtime.Serialization;         // DataContract, DataMember
using System.Runtime.Serialization.Json;    // DataContractJsonSerializer
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace Infragistics.Samples.Services
{
  
    public class WeatherService
    {
        protected const string WeatherGoogleWebsite = "http://www.google.com/ig/api?weather={0}";
        private WeatherService() { }

        public static WeatherService GetGoogleWeather(string location)
        {
            var wgs = new WeatherService();

            var request = WebRequest.Create(new Uri(string.Format(WeatherGoogleWebsite, location))) as HttpWebRequest;

            request.Method = "GET";

            var state = new WeatherGoogleRequestState();
            state.Request = request;
            request.BeginGetResponse(new AsyncCallback(wgs.RequestCallback), state);

            return wgs;
        }

        private void RequestCallback(IAsyncResult result)
        {
            var requestState = result.AsyncState as WeatherGoogleRequestState;
            requestState.Response = (HttpWebResponse)requestState.Request.EndGetResponse(result);
            requestState.StreamResponse = requestState.Response.GetResponseStream();

            var reader = new StreamReader(requestState.StreamResponse);
            string webResult = reader.ReadToEnd();
            Debug.WriteLine(webResult);
        }

    }

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