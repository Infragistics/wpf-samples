using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a data provider for accessing GML files.
    /// </summary>
    public class GmlDataProvider : IGmlDataProvider
    {
        public GmlDataProvider()
        {

        }

        private string _gmlFilePath;
        private GetGmlDataCompletedEventArgs _eventArg;

        /// <summary>
        /// Occurs when finished loading a GML file.
        /// </summary>
        public event EventHandler<GetGmlDataCompletedEventArgs> GetGmlDataCompleted;

        /// <summary>
        /// Gets a GML file from the Infragistics.Samples.Assets assembly (/storage/[Local]/GML)
        /// </summary>
        /// <param name="gmlFileName">File name without file path: "Customer.Gml" </param>
        public void GetGmlDataAsync(string gmlFileName)
        {
            try
            {
                _gmlFilePath = StorageDataProvider.GetStorageGmlPath(gmlFileName);
                if (_gmlFilePath == null)
                {
                    var error = new Exception("Storage Path Not Valid: " + _gmlFilePath);
                    throw error;
                }
                StreamResourceInfo streamInfo = Application.GetResourceStream(new Uri(_gmlFilePath));
                if (streamInfo == null)
                {
                    var error = new Exception("Storage Resource Not Found: " + _gmlFilePath);
                    throw error;
                }

                Stream stream = streamInfo.Stream;
                var bw = new BackgroundWorker();
                bw.DoWork += OnBackgroundWorkerDoWork;
                bw.RunWorkerCompleted += OnRunWorkerCompleted;
                bw.RunWorkerAsync(stream);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                // create an event argument with failed attempt to read the GML file 
                _eventArg = new GetGmlDataCompletedEventArgs(ex);
                // notify when an error occurred while reading the GML file 
                OnGetGmlDataCompleted(_eventArg);
            }
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var stream = e.Argument as Stream;
                if (stream != null)
                {
                    // convert stream to string
                    var reader = new StreamReader(stream);
                    var result = reader.ReadToEnd();
                    _eventArg = new GetGmlDataCompletedEventArgs(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                // create an event argument with failed attempt to read the GML file
                _eventArg = new GetGmlDataCompletedEventArgs(ex);
            }
        }
        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // notify when the GML file was successful read or failed
            OnGetGmlDataCompleted(_eventArg);
        }

        private void OnGetGmlDataCompleted(GetGmlDataCompletedEventArgs eventArgs)
        {
            if (this.GetGmlDataCompleted != null)
            {
                this.GetGmlDataCompleted(this, eventArgs);
            }
        }

     

    }

}
