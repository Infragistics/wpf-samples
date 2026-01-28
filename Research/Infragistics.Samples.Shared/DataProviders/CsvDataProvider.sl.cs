using System;
using System.Linq;
using System.Net;

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a data provider for accessing CSV files.
    /// </summary>
    public class CsvDataProvider : CsvDataProviderBase
    {
        private readonly WebClient _client;
        private string _filePath;

        public CsvDataProvider()
        {
            _client = new WebClient();
            _client.DownloadStringCompleted += OnClientDownloadStringCompleted;
        }
        private void OnClientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                var error = new Exception("Failed to download: " + _filePath + " CSV file due to error: ", e.Error);
                OnGetCsvDataCompleted(new GetCsvDataCompletedEventArgs(error));
            }
            else
            {
                var splitLines = e.Result.Split('\n');
                var lines = splitLines.ToList();
                 
                OnGetCsvDataCompleted(new GetCsvDataCompletedEventArgs(lines));
            }
        }
        public override void GetCsvDataAsync(string xmlFileName)
        {
            _filePath = StorageDataProvider.GetStorageCsvPath() + xmlFileName;
            _client.DownloadStringAsync(new Uri(_filePath));
  
        }
    }
}