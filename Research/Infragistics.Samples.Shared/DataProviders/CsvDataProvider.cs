using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a data provider for accessing CSV files.
    /// </summary>
    public class CsvDataProvider : CsvDataProviderBase
    {
        private string _filePath;
        private GetCsvDataCompletedEventArgs _eventArg;
        
        public override void GetCsvDataAsync(string xmlFileName)
        {
            try
            {
                _filePath = StorageDataProvider.GetStorageCsvPath(xmlFileName);
                if (_filePath == null)
                {
                    var error = new Exception("Storage Path Not Valid: " + _filePath);
                    throw error;
                }
                StreamResourceInfo streamInfo = Application.GetResourceStream(new Uri(_filePath));
                if (streamInfo == null)
                {
                    var error = new Exception("Storage Resource Not Found: " + _filePath);
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
                Debug.WriteLine(ex.Message);
                // create an event argument with failed attempt to read the CSV file 
                _eventArg = new GetCsvDataCompletedEventArgs(ex);
                // notify when an error occurred while reading the CSV file 
                OnGetCsvDataCompleted(_eventArg);
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
                    _eventArg = new GetCsvDataCompletedEventArgs(lines);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                // create an event argument with failed attempt to read the CSV file
                _eventArg = new GetCsvDataCompletedEventArgs(ex);
            }
        }
        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // notify when the CSV file was successful read or failed
            OnGetCsvDataCompleted(_eventArg);
        }

        
    }
}