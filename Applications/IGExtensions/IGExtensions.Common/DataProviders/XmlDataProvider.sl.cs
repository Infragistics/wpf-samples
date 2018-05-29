using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using System.Xml.Linq;
using IGExtensions.Framework;

namespace IGExtensions.Common.DataProviders
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
        public event EventHandler<LoadXmlDataCompletedEventArgs> LoadXmlDataCompleted;

        /// <summary>
        /// Gets xml file as XDocument from specified path, e.g. Assets/Data/[Local]/FileName.xml
        /// </summary>
        public void LoadXmlDataResource(string xmlFilePath, string xmlFileName)
        {
            _xmlFilePath = xmlFilePath; 
            var streamInfo = GetStreamResourceInfo(xmlFilePath, xmlFileName);
            if (streamInfo == null || streamInfo.Stream == null) return;

            var xmlDoc = XDocument.Load(streamInfo.Stream);
            OnGetXmlDataCompleted(new LoadXmlDataCompletedEventArgs(xmlDoc));
        }
        /// <summary>
        /// Gets xml file as XmlReader from specified path, e.g. Assets/Data/[Local]/FileName.xml
        /// </summary>
        public void LoadXmlDataReader(string xmlFilePath, string xmlFileName)
        {
            _xmlFilePath = xmlFilePath;
            var streamInfo = GetStreamResourceInfo(xmlFilePath, xmlFileName);
            if (streamInfo == null || streamInfo.Stream == null) return;

            var reader = XmlReader.Create(streamInfo.Stream);
            OnGetXmlDataCompleted(new LoadXmlDataCompletedEventArgs(reader));
        }

        private StreamResourceInfo GetStreamResourceInfo(string xmlFilePath, string xmlFileName)
        {
            if (!xmlFilePath.EndsWith("/")) xmlFilePath += "/";

            var xmlFileUriString = "";
            if (Thread.CurrentThread.CurrentCulture.Name.ToLower().Contains("us"))
            {
                xmlFileUriString = xmlFilePath + xmlFileName;
            }
            else
            {
                xmlFileUriString = xmlFilePath + Thread.CurrentThread.CurrentCulture.Name + "/" + xmlFileName;
            }

            //StreamResourceInfo streamInfo = Application.GetResourceStream(new Uri(xmlFilePath + Thread.CurrentThread.CurrentCulture.Name + "/" + xmlFileName, UriKind.Relative));
            var streamInfo = Application.GetResourceStream(new Uri(xmlFileUriString, UriKind.Relative));
            if (streamInfo == null)
            {
                DebugManager.LogWarning("XmlDataProvider Missing Localized XML File: " + xmlFilePath + xmlFileName + " - will default to English local.");
                streamInfo = Application.GetResourceStream(new Uri(xmlFilePath + xmlFileName, UriKind.Relative));
            }
            if (streamInfo == null || streamInfo.Stream == null)
            {
                var error = new Exception("XmlDataProvider Failed to open XML File: " + xmlFilePath + xmlFileName);
                DebugManager.LogError(error);
                OnGetXmlDataCompleted(new LoadXmlDataCompletedEventArgs(error));
            }
            return streamInfo;
        }

        /// <summary>
        /// Gets xml file from ~/SamplesCommon/sl/[Local]/xml location
        /// </summary>
        /// <param name="xmlFileName">File name without file path: "Customer.xml" </param>
        public void LoadXmlDataAsync(string xmlFileName)
        {
            //_xmlFilePath = StorageDataProvider.GetStorageXmlPath() + xmlFileName;
            //_client.DownloadStringAsync(new Uri(xmlFileName, UriKind.RelativeOrAbsolute));
#if WPF
            LoadXmlDataLocal(xmlFileName);
#else
            LoadXmlDataAsync(new Uri(xmlFileName, UriKind.RelativeOrAbsolute));
#endif
        }
        public void LoadXmlDataAsync(Uri xmlFileUri)
        {
            _client.DownloadStringAsync(xmlFileUri);
        }
        public void LoadXmlDataLocal(string xmlFileName)
        {
            _xmlFilePath = xmlFileName;
            try
            {
                //TODO use BackgroundWorker
                //var bw = new BackgroundWorker();
                //bw.DoWork += OnBackgroundWorkerDoWork;
                //bw.RunWorkerCompleted += OnRunWorkerCompleted;
                //bw.RunWorkerAsync(stream);

                var xmlDoc = XDocument.Load(xmlFileName);
                OnGetXmlDataCompleted(new LoadXmlDataCompletedEventArgs(xmlDoc));
            }
            catch (Exception ex)
            {
                var error = new Exception("XmlDataProvider Failed to open: " + _xmlFilePath + " file due to error: \n", ex);
                DebugManager.LogError(error);
                OnGetXmlDataCompleted(new LoadXmlDataCompletedEventArgs(error));
            }
        }
        /// <summary>
        /// Gets xml file from specified path, e.g. ~/SamplesCommon/sl/[Local]/xml path
        /// </summary>
        /// <param name="xmlFileName">File name without file path: "Customer.xml" </param>
        /// <param name="userState"></param>
        public void GetXmlDataAsync(string xmlFileName, object userState)
        {
            //_xmlFilePath = StorageDataProvider.GetStorageXmlPath() + xmlFileName;

            //_client.OpenReadAsync(new Uri(_xmlFilePath));

            _client.DownloadStringAsync(new Uri(xmlFileName), userState);
        }
        
        private void OnClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                var error = new Exception("XmlDataProvider Failed to open: " + _xmlFilePath + " file due to error: \n", e.Error);
                DebugManager.LogError(error);
                OnGetXmlDataCompleted(new LoadXmlDataCompletedEventArgs(error));
            }
            else
            {
                using (var stream = e.Result)
                {
                    var xmlDoc = XDocument.Load(stream);
                    OnGetXmlDataCompleted(new LoadXmlDataCompletedEventArgs(xmlDoc));
                }
            }
        }
        private void OnClientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                var error = new Exception("XmlDataProvider Failed to download: " + _xmlFilePath + " file due to error: \n", e.Error);
                DebugManager.LogError(error);
                OnGetXmlDataCompleted(new LoadXmlDataCompletedEventArgs(error));
            }
            else
            {
                var xmlDoc = XDocument.Parse(e.Result);
                OnGetXmlDataCompleted(new LoadXmlDataCompletedEventArgs(xmlDoc, e.UserState));
            }
        }
        private void OnGetXmlDataCompleted(LoadXmlDataCompletedEventArgs eventArgs)
        {
            if (this.LoadXmlDataCompleted != null)
            {
                this.LoadXmlDataCompleted(this, eventArgs);
            }
        }
    }

    public interface IXmlDataProvider
    {
        /// <summary>
        /// Occurs when finished loading a xml file.
        /// </summary>
        event EventHandler<LoadXmlDataCompletedEventArgs> LoadXmlDataCompleted;

        void LoadXmlDataAsync(string xmlFileName);

    }


    //public class LoadXmlDataCompletedEventArgs : EventArgs
    //{

    //    public LoadXmlDataCompletedEventArgs(XDocument xDocument)
    //    {
    //        this.XmlDocument = xDocument;
    //        this.XmlReader = null;
    //        this.Error = null;
    //        this.HasError = false;
    //        this.UserState = null;
    //    }
    //    public LoadXmlDataCompletedEventArgs(XmlReader xmlReader)
    //    {
    //        this.XmlDocument = null;
    //        this.XmlReader = xmlReader;
    //        this.Error = null;
    //        this.HasError = false;
    //        this.UserState = null;
    //    }
    //    public LoadXmlDataCompletedEventArgs(XDocument xDocument, object userState)
    //    {
    //        this.XmlDocument = xDocument;
    //        this.XmlReader = null;
    //        this.Error = null;
    //        this.HasError = false;
    //        this.UserState = userState;
    //    }
    //    public LoadXmlDataCompletedEventArgs(Exception error)
    //    {
    //        this.XmlDocument = null;
    //        this.XmlReader = null;
    //        this.Error = error;
    //        this.HasError = true;
    //        this.UserState = null;
    //    }
    //    /// <summary>
    //    /// Gets the loaded xml file as a XmlReader object
    //    /// </summary>
    //    public XmlReader XmlReader { get; private set; }

    //    /// <summary>
    //    /// Gets the loaded xml file as a XDocument object
    //    /// </summary>
    //    public XDocument XmlDocument { get; private set; }
    //    /// <summary>
    //    /// Gets an Exception associated with loading the xml file
    //    /// </summary>
    //    public Exception Error { get; private set; }
    //    /// <summary>
    //    /// Gets if there were error when loading the xml file
    //    /// </summary>
    //    public bool HasError { get; private set; }
    //    public object UserState { get; set; }

    //}
  
}