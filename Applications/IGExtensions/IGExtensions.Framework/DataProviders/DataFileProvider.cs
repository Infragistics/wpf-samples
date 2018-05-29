using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace IGExtensions.Framework
{
    public abstract class DataFileProvider
    {
        protected readonly WebClient WebClient;
        public string DataFileName { get; protected set; }

        protected DataFileProvider()
        {
            WebClient = new WebClient();

        }
    }
    public abstract class DataCsvProviderBase : DataFileProvider
    {
        public event EventHandler<LoadCsvDataCompletedEventArgs> LoadDataCompleted;
        public abstract void LoadDataAsync(string fileName);
        protected void OnLoadDataCompleted(LoadCsvDataCompletedEventArgs eventArgs)
        {
            if (this.LoadDataCompleted != null)
            {
                this.LoadDataCompleted(this, eventArgs);
            }
        }
    }

    #region Events 
    public class LoadDataCompletedEventArgs : EventArgs
    {
        public LoadDataCompletedEventArgs()
            : this(null)
        { }
        public LoadDataCompletedEventArgs(Exception error)
        {
            this.Error = error;
            this.UserState = null;
        }
        /// <summary>
        /// Gets an Exception associated with loading the CSV file
        /// </summary>
        public Exception Error { get; protected set; }
        /// <summary>
        /// Gets if there were error when loading the CSV file
        /// </summary>
        public bool HasError { get { return Error != null; } }
        public object UserState { get; set; }
    }
    public class LoadCsvDataCompletedEventArgs : LoadDataCompletedEventArgs
    {
        public LoadCsvDataCompletedEventArgs(Exception error)
            : base(error)
        { }
        public LoadCsvDataCompletedEventArgs(List<string> result)
            : this(result, null)
        { }
        public LoadCsvDataCompletedEventArgs(List<string> result, object userState)
        {
            this.Result = result;
            this.UserState = userState;
        }
        /// <summary>
        /// Gets the loaded data from CSV file as a list of strings
        /// </summary>
        public List<string> Result { get; private set; }
    }
    public class LoadXmlDataCompletedEventArgs : LoadDataCompletedEventArgs
    {
        public LoadXmlDataCompletedEventArgs(Exception error)
            : base(error)
        { }
        public LoadXmlDataCompletedEventArgs(XDocument xmlDocument)
            : this(xmlDocument, null)
        { }
        public LoadXmlDataCompletedEventArgs(XDocument xmlDocument, object userState)
        {
            this.XmlDocument = xmlDocument;
            this.UserState = userState;
        }
        public LoadXmlDataCompletedEventArgs(XmlReader xmlReader)
        {
            this.XmlDocument = null;
            this.XmlReader = xmlReader;
            this.Error = null;
            this.UserState = null;
        }
        /// <summary>
        /// Gets the loaded data from XML file as a XDocument object
        /// </summary>
        public XDocument XmlDocument { get; private set; }
        /// <summary>
        /// Gets the loaded xml file as a XmlReader object
        /// </summary>
        public XmlReader XmlReader { get; private set; }
    }
    #endregion
}