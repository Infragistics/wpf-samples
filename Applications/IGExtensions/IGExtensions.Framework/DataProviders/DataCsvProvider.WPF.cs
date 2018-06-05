using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace IGExtensions.Framework
{
    public class DataCsvProvider : DataCsvProviderBase 
    {
        protected LoadCsvDataCompletedEventArgs EventArg;

        public override void LoadDataAsync(string fileName)
        {
            DataFileName = fileName;
            var fileNamePath = fileName;
            //if (fileNamePath.StartsWith("/")) fileNamePath = "@" + fileNamePath;
            if (fileNamePath.StartsWith("/")) fileNamePath = fileNamePath.Remove(0, 1);

            //TODO add code for loading from resources
            var stream = File.OpenText(fileNamePath);

            var bw = new BackgroundWorker();
            bw.DoWork += OnBackgroundWorkerDoWork;
            bw.RunWorkerCompleted += OnRunWorkerCompleted;
            bw.RunWorkerAsync(stream);
        }

        public void LoadData(StreamReader stream)
        {
            var results = new List<string>();
             
        }
        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var stream = e.Argument as StreamReader;
                if (stream != null)
                {
                    var results = new List<string>();
                    using (var sr = stream) //new StreamReader(stream))
                    {
                        var line = sr.ReadLine();
                        while (line != null)
                        {
                            results.Add(line);
                            line = sr.ReadLine();
                        }
                    }
                    // create an event argument with successful attempt to read the CSV file
                    EventArg = new LoadCsvDataCompletedEventArgs(results);
                }
            }
            catch (Exception ex)
            {
                // create an event argument with failed attempt to read the CSV file
                var error = new Exception("Failed to download: " + DataFileName + " file due to error: ", ex);
                System.Diagnostics.Debug.WriteLine(error.Message);
                EventArg = new LoadCsvDataCompletedEventArgs(error);
            }
        }

        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // notify when the CSV file was successful read or failed
            OnLoadDataCompleted(EventArg);
        }
    }

}