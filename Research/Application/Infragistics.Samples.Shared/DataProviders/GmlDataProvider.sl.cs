using System;
using System.Net;

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a data provider for accessing GML files.
    /// </summary>
    public class GmlDataProvider : IGmlDataProvider
    {
        public GmlDataProvider()
        {
            _client = new WebClient();
            _client.DownloadStringCompleted += OnClientDownloadStringCompleted;
        }
        private string _gmlFilePath;
        private readonly WebClient _client;
        
    
        #region IGmlDataProvider Members

        /// <summary>
        /// Occurs when finished loading a GML file.
        /// </summary>
        public event EventHandler<GetGmlDataCompletedEventArgs> GetGmlDataCompleted;

        /// <summary>
        /// Gets a GML file from ~/SamplesCommon/sl/[Local]/GML location
        /// </summary>
        /// <param name="gmlFileName">File name without file path: "Customer.Gml" </param>
        public void GetGmlDataAsync(string gmlFileName)
        {
            _gmlFilePath = StorageDataProvider.GetStorageGmlPath() + gmlFileName;
            _client.DownloadStringAsync(new Uri(_gmlFilePath));
        }
        private void OnClientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                var error = new Exception("Failed to download: " + _gmlFilePath + " GML file due to error: ", e.Error);
                // notify when an error occurred while reading the GML file 
                OnGetGmlDataCompleted(new GetGmlDataCompletedEventArgs(error));
            }
            else
            {
                var str = e.Result;
                // notify when the GML file was successful read or failed
                OnGetGmlDataCompleted(new GetGmlDataCompletedEventArgs(str, e.UserState));
            }

        }
        private void OnGetGmlDataCompleted(GetGmlDataCompletedEventArgs eventArgs)
        {
            if (this.GetGmlDataCompleted != null)
            {
                this.GetGmlDataCompleted(this, eventArgs);
            }
        }
        #endregion
    }
}
