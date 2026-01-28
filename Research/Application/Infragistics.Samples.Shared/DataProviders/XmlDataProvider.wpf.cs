using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Resources;
using System.Xml.Linq;
using System.Windows;
using Infragistics.Samples.Assets.Providers;     // XDocumentEx

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a data provider for accessing XML files.
    /// </summary>
    public class XmlDataProvider : IXmlDataProvider
    {
        public XmlDataProvider()
        {
        
        }

        private string _xmlFilePath;
        private GetXmlDataCompletedEventArgs _eventArg;

        /// <summary>
        /// Occurs when finished loading a xml file.
        /// </summary>
        public event EventHandler<GetXmlDataCompletedEventArgs> GetXmlDataCompleted;
         
        /// <summary>
        /// Gets xml file from the Infragistics.Samples.Assets assembly (/storage/[Local]/xml)
        /// </summary>
        /// <param name="xmlFileName">File name without file path: "Customer.xml" </param>
        public void GetXmlDataAsync(string xmlFileName)
        {
            try
            {
                _xmlFilePath = StorageDataProvider.GetStorageXmlPath(xmlFileName);
                if (_xmlFilePath == null)
                {
                    var error = new Exception("Storage Path Not Valid: " + _xmlFilePath);
                    throw error;
                }
                StreamResourceInfo streamInfo = Application.GetResourceStream(new Uri(_xmlFilePath));
                if (streamInfo == null)
                {
                    var error = new Exception("Storage Resource Not Found: " + _xmlFilePath);
                    throw error;
                }
                Stream xmlStream = streamInfo.Stream;
                var bw = new BackgroundWorker();
                bw.DoWork += OnBackgroundWorkerDoWork;
                bw.RunWorkerCompleted += OnRunWorkerCompleted;
                bw.RunWorkerAsync(xmlStream);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                // create an event argument with failed attempt to read the xml file 
                _eventArg = new GetXmlDataCompletedEventArgs(ex);
                // notify when an error occurred while reading the xml file 
                OnGetXmlDataCompleted(_eventArg);
            }
        }
      

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var xmlStream = e.Argument as Stream;
                if (xmlStream != null)
                {                 
                    XDocument xDocument = XDocument.Load(xmlStream);
                    // create an event argument with successful attempt to read the xml file
                    _eventArg = new GetXmlDataCompletedEventArgs(xDocument);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                // create an event arg with failed attempt to read the xml file
                _eventArg = new GetXmlDataCompletedEventArgs(ex);
            }
        }
        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // notify when the xml file was successful read or failed
            OnGetXmlDataCompleted(_eventArg);
        }
        
        private void OnGetXmlDataCompleted(GetXmlDataCompletedEventArgs eventArgs)
        {
            if (this.GetXmlDataCompleted != null)
            {
                this.GetXmlDataCompleted(this, eventArgs);
            }
        }

        /// <summary>
        /// Returns localized path to xml resources.
        /// </summary>
        public static string GetXmlLocalPath()
        {
            return StorageProvider.GetStorageXmlPath();
        }

        /// <summary>
        /// Returns the localized path to an xml file.
        /// </summary>
        /// <param name="xmlName">The name of the xml file in "Name.xml" format</param>
        /// <returns></returns>
        public static string GetXmlLocalPath(string xmlName)
        {
            xmlName = xmlName.TrimStart(@" \/".ToCharArray()).Replace(@"\", "/");
            return StorageProvider.GetStorageXmlPath(xmlName);
        }

        /// <summary>
        /// Returns a Uri to the specified xml file. 
        /// </summary>
        /// <param name="xmlName">The name of the xml file in "Name.xml" format</param>
        /// <returns></returns>
        public static Uri GetXmlUri(string xmlName)
        {
            return new Uri(GetXmlLocalPath(xmlName), UriKind.Absolute);
        }
    }

    
}