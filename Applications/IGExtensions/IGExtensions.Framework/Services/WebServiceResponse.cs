using System;
using System.IO;
using System.Net;

namespace Infragistics.Framework.Services
{
    /// <summary>
    /// Represents a response for downloading data from <see cref="WebService"/>
    /// </summary>
    public class WebServiceResponse
    {
        public WebServiceResponse()
        {
            
        }

        public WebServiceResponse(HttpWebResponse httpResponse)
        {
            this.Method = httpResponse.Method;
            this.StatusCode = httpResponse.StatusCode;
            this.StatusInfo = httpResponse.StatusDescription;
            this.ResultType = httpResponse.ContentType;
            this.ResultLength = httpResponse.ContentLength;
            //this.ResultStream = httpResponse.GetResponseStream();
        }

        public HttpStatusCode StatusCode { get; set; }
        public int StatusNumber { get { return (int)StatusCode; } }
        public string StatusInfo { get; set; }

        public string RequestUri { get; set; }
       
        public bool   ErrorOccured { get; set; }
        public string ErrorStack { get; set; }
        public string ErrorMessage { get; set; }

        public string ResultData { get; set; }
        public Stream ResultStream { get; set; }
        public long   ResultLength { get; set; }
        public string ResultType { get; set; }
        public Uri Uri { get; set; }
     
        public string Method { get; set; }
        public object UserState { get; set; }
    }

    public class WebServiceRequestCompletedArgs : EventArgs
    {
        /// <summary> Gets or sets Response </summary>
        public WebServiceResponse Response { get; set; }

    }
}