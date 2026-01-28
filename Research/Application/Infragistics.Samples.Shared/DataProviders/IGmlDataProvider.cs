using System;
using System.IO;

namespace Infragistics.Samples.Shared.DataProviders
{
    public interface IGmlDataProvider
    {
        /// <summary>
        /// Occurs when finished loading a Gml file.
        /// </summary>
        event EventHandler<GetGmlDataCompletedEventArgs> GetGmlDataCompleted;

        void GetGmlDataAsync(string gmlFileName);

    }
    public class GetGmlDataCompletedEventArgs : EventArgs
    {
        public GetGmlDataCompletedEventArgs(string result)
            : this(result, null)
        { }
        public GetGmlDataCompletedEventArgs(string result, object userState)
        {
            this.Error = null;
            this.Result = result;
            this.UserState = userState;
        }
 
        public GetGmlDataCompletedEventArgs(Exception error)
            : this(null, null)
        {
            this.Error = error;
        }

        /// <summary>
        /// Gets the loaded Gml file as a Stream object
        /// </summary>
        //public StreamReader Result { get; private set; }
        public string Result { get; private set; }
        
        /// <summary>
        /// Gets an Exception associated with loading the Gml file
        /// </summary>
        public Exception Error { get; private set; }
        /// <summary>
        /// Gets if there were error when loading the Gml file
        /// </summary>
        public bool HasError
        {
            get { return this.Error != null; }
        }
        public object UserState { get; set; }

    }
}