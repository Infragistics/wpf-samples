using System;
using System.Xml.Linq;

namespace Infragistics.Samples.Shared.DataProviders
{
    public interface IXmlDataProvider
    {
        /// <summary>
        /// Occurs when finished loading a xml file.
        /// </summary>
        event EventHandler<GetXmlDataCompletedEventArgs> GetXmlDataCompleted;

        void GetXmlDataAsync(string xmlFileName);  
       
    }
    public class GetXmlDataCompletedEventArgs : EventArgs
    {

        public GetXmlDataCompletedEventArgs(XDocument xDocument)
        {
            this.Result = xDocument;
            this.Error = null;
            this.HasError = false;
            this.UserState = null;
        }
        public GetXmlDataCompletedEventArgs(XDocument xDocument, object userState)
        {
            this.Result = xDocument;
            this.Error = null;
            this.HasError = false;
            this.UserState = userState;
        }
        public GetXmlDataCompletedEventArgs(Exception error)
        {
            this.Result = null;
            this.Error = error;
            this.HasError = true;
            this.UserState = null;
        }

        /// <summary>
        /// Gets the loaded xml file as a XDocument object
        /// </summary>
        public XDocument Result { get; private set; }
        /// <summary>
        /// Gets an Exception associated with loading the xml file
        /// </summary>
        public Exception Error { get; private set; }
        /// <summary>
        /// Gets if there were error when loading the xml file
        /// </summary>
        public bool HasError { get; private set; }
        public object UserState { get; set; }

    }
}