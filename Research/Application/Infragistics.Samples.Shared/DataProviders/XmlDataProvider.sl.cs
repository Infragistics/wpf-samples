using System;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a data provider for accessing XML files.
    /// </summary>
    public class XmlDataProvider : IXmlDataProvider
    {
        public XmlDataProvider()
        {
            _client = new WebClient();
            //_client.OpenReadCompleted += OnClientOpenReadCompleted;
            _client.DownloadStringCompleted += OnClientDownloadStringCompleted;
        }
        
        private string _xmlFilePath;
        private readonly WebClient _client;
        /// <summary>
        /// Occurs when finished downloading a xml file.
        /// </summary>
        public event EventHandler<GetXmlDataCompletedEventArgs> GetXmlDataCompleted;
         
        /// <summary>
        /// Gets xml file from ~/SamplesCommon/sl/[Local]/xml location
        /// </summary>
        /// <param name="xmlFileName">File name without file path: "Customer.xml" </param>
        public void GetXmlDataAsync(string xmlFileName)
        {
            _xmlFilePath = StorageDataProvider.GetStorageXmlPath() + xmlFileName;

            //_client.OpenReadAsync(new Uri(_xmlFilePath));
            _client.DownloadStringAsync(new Uri(_xmlFilePath));
        }

        /// <summary>
        /// Gets xml file from ~/SamplesCommon/sl/[Local]/xml path
        /// </summary>
        /// <param name="xmlFileName">File name without file path: "Customer.xml" </param>
        /// <param name="userState"></param>
        public void GetXmlDataAsync(string xmlFileName, object userState)
        {
            _xmlFilePath = StorageDataProvider.GetStorageXmlPath() + xmlFileName;

            //_client.OpenReadAsync(new Uri(_xmlFilePath));
            _client.DownloadStringAsync(new Uri(_xmlFilePath), userState);
        }
        
        private void OnClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Exception error = new Exception("Failed to open: " + _xmlFilePath + " xml file due to error: ", e.Error);
                OnGetXmlDataCompleted(new GetXmlDataCompletedEventArgs(error));
            }
            else
            {
                using (Stream stream = e.Result)
                {
                    XDocument xmlDoc = XDocument.Load(stream);
                    OnGetXmlDataCompleted(new GetXmlDataCompletedEventArgs(xmlDoc));
                }
            }
        }
        private void OnClientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Exception error = new Exception("Failed to download: " + _xmlFilePath + " xml file due to error: ", e.Error);
                OnGetXmlDataCompleted(new GetXmlDataCompletedEventArgs(error));
            }
            else
            {
                XDocument xmlDoc = XDocument.Parse(e.Result);
                OnGetXmlDataCompleted(new GetXmlDataCompletedEventArgs(xmlDoc, e.UserState));
            }
        }
        private void OnGetXmlDataCompleted(GetXmlDataCompletedEventArgs eventArgs)
        {
            if (this.GetXmlDataCompleted != null)
            {
                this.GetXmlDataCompleted(this, eventArgs);
            }
        }
    }

    
}