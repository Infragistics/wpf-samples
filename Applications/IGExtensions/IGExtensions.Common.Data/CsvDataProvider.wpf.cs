using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Resources;
using IGExtensions.Common.DataProviders;

namespace IGExtensions.Common.Data
{
    public class CsvDataProvider : CsvDataProviderBase
    {
         
        private string _filePath;
        //private GetCsvDataCompletedEventArgs _eventArg;
        protected string FileName;
        protected CsvDataCompletedEventArgs EventArg;

        public override void GetDataAsync(string xmlFileName)
        {
            try
            {
                _filePath = DataStorageProvider.GetStorageCsvPath(xmlFileName);
                //_filePath = xmlFileName; // StorageDataProvider.GetStorageCsvPath(xmlFileName);
                //_filePath = StorageDataProvider.GetStorageCsvPath(xmlFileName);
                if (_filePath == null)
                {
                    var error = new Exception("Storage Path Not Valid: " + _filePath);
                    throw error;
                }
                var streamInfo = Application.GetResourceStream(new Uri(_filePath));
                if (streamInfo == null)
                {
                    var error = new Exception("Storage Resource Not Found: " + _filePath);
                    throw error;
                }
                var stream = streamInfo.Stream;
                var bw = new BackgroundWorker();
                bw.DoWork += OnBackgroundWorkerDoWork;
                bw.RunWorkerCompleted += OnRunWorkerCompleted;
                bw.RunWorkerAsync(stream);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // create an event argument with failed attempt to read the CSV file 
                EventArg = new CsvDataCompletedEventArgs(ex);
                // notify when an error occurred while reading the CSV file 
                OnGetDataCompleted(EventArg);
            }
        }
        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var stream = e.Argument as Stream;
                if (stream != null)
                {
                    var lines = new List<string>();
                    using (var sr = new StreamReader(stream))
                    {
                        var line = sr.ReadLine();
                        while (line != null)
                        {
                            lines.Add(line);
                            line = sr.ReadLine();
                        }
                    }
                    // create an event argument with successful attempt to read the CSV file
                    EventArg = new CsvDataCompletedEventArgs(lines);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                // create an event argument with failed attempt to read the CSV file
                EventArg = new CsvDataCompletedEventArgs(ex);
            }
        }
        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // notify when the CSV file was successful read or failed
            OnGetDataCompleted(EventArg);
        }
    }
}