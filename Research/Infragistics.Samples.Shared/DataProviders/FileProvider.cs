using System;
using System.IO;
using System.Net;

namespace Infragistics.Samples.Shared.DataProviders
{
    public class FileProvider
    {
        #region Private Fields
        private WebClient _client;
        private string filePath;
        #endregion // Private Fields

        #region Constructor
        public FileProvider()
        {
            _client = new WebClient();
            _client.OpenReadCompleted += OnClientReadCompleted;
        }
        #endregion // Constructor

        #region Static Methods
        /// <summary>
        /// Return Uri to a file
        /// </summary>
        /// <returns></returns>
        public static Uri GetFileLocalUri(string fileName)
        {
            return new Uri(GetFileLocalPath(fileName));
        }

        /// <summary>
        /// Returns a string containing the path to a file.
        /// </summary>
        /// <returns></returns>
        public static string GetFileLocalPath(string fileName)
        {
            fileName = fileName.TrimStart(@" \/".ToCharArray()).Replace(@"\", "/");
            return StorageDataProvider.GetLocalizedStoragePath() + "/" + fileName;
        }

        /// <summary>
        /// Returns the path to the storage directory.
        /// </summary>
        /// <returns></returns>
        public static string GetStorageLocalPath()
        {
            return StorageDataProvider.GetLocalizedStoragePath() + "/";
        }
        #endregion // Static Methods


        public event EventHandler<GetFileCompletedEventArgs> GetFileCompleted;

        /// <summary>
        /// Gets file from SamplesCommon/storage/[Local] path
        /// </summary>
        /// <param name="xmlFileName">File name with file path: "zip/Pictures.zip" </param>
        public void GetFileAsync(string filePath)
        {
            this.filePath = GetFileLocalPath(filePath);
            _client.OpenReadAsync(new Uri(this.filePath));
        }

        public void OnClientReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Exception error = new Exception("Failed to download: " + filePath + " file due to error: ", e.Error);
                OnGetFileCompleted(new GetFileCompletedEventArgs(error));
            }
            else
            {
                OnGetFileCompleted(new GetFileCompletedEventArgs(e.Result));
            }
        }

        private void OnGetFileCompleted(GetFileCompletedEventArgs eventArgs)
        {
            if (this.GetFileCompleted != null)
            {
                GetFileCompleted(this, eventArgs);
            }
        }
    }

    public class GetFileCompletedEventArgs : EventArgs
    {
        public GetFileCompletedEventArgs(Stream stream)
        {
            this.Result = stream;
            this.Error = null;
            this.HasError = false;
        }
        public GetFileCompletedEventArgs(Exception error)
        {
            this.Result = null;
            this.Error = error;
            this.HasError = true;
        }

        /// <summary>
        /// Gets the loaded file as a Stream object
        /// </summary>
        public Stream Result { get; private set; }
        /// <summary>
        /// Gets an Exception assosicated with loading the file
        /// </summary>
        public Exception Error { get; private set; }
        /// <summary>
        /// Gets if there were errors when loading the file
        /// </summary>
        public bool HasError { get; private set; }

    }
}
