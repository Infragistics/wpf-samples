using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace IGExtensions.Common.Data
{
    /// <summary>
    /// Represents a data provider for accessing CSV files.
    /// </summary>
    public class CsvDataProvider : CsvDataProviderBase
    {
        private readonly WebClient _client;
        protected string FileName;
        protected CsvDataCompletedEventArgs EventArg;
     
        public CsvDataProvider()
        {
            _client = new WebClient();
            _client.DownloadStringCompleted += OnClientDownloadStringCompleted;
        }
        private void OnClientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                var error = new Exception("Failed to download: " + FileName + " CSV file due to error: ", e.Error);
                OnGetDataCompleted(new CsvDataCompletedEventArgs(error));
            }
            else
            {
                var splitLines = e.Result.Split('\n');
                var lines = splitLines.ToList();

                OnGetDataCompleted(new CsvDataCompletedEventArgs(lines));
            }
        }
        //public override void GetCsvDataAsync(string filePath)
        //{
        //    //var path = DataPathProvider + "csv/";
        //    //_filePath = StorageDataProvider.GetStorageCsvPath() + fileName;
        //    _filePath = filePath; // StorageDataProvider.GetStorageCsvPath() + fileName;
        //    _client.DownloadStringAsync(new Uri(_filePath, UriKind.RelativeOrAbsolute));
        //}
        //TODO changed to LoadDataAsync and provide EventArgs with common interface for data loading
        public override void GetDataAsync(string fileName)
        {
            try
            {
                FileName = fileName; // StorageDataProvider.GetStorageCsvPath(xmlFileName);
                if (FileName == null)
                {
                    throw new Exception("Storage Path Not Valid: " + FileName);
                }
                Assembly assembly = Assembly.GetExecutingAssembly();

                var resources = (from n in assembly.GetManifestResourceNames()
                             where (n.Contains(fileName))
                             select n).ToList();
                if (resources.Count == 0)
                {
                    throw new Exception("Storage Resource Not Found: " + FileName);
                }
                Stream stream = assembly.GetManifestResourceStream(resources[0]);
                if (stream == null)
                {
                    throw new Exception("Storage Stream Not Found: " + resources[0]);
                }
                //StreamResourceInfo streamInfo = Application.GetResourceStream(new Uri(_filePath));
                //if (streamInfo == null)
                //{
                //    throw new Exception("Storage Resource Not Found: " + _filePath);
                //}
                //Stream stream = streamInfo.Stream;
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
                Debug.WriteLine(ex.Message);
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