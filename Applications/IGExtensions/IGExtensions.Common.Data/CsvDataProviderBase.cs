using System;
using System.Collections.Generic;

namespace IGExtensions.Common.Data
{
    public abstract class CsvDataProviderBase
    {
        /// <summary>
        /// Occurs when finished loading a CSV file.
        /// </summary>
        public event EventHandler<CsvDataCompletedEventArgs> GetDataCompleted;

        public abstract void GetDataAsync(string xmlFileName);

        public void OnGetDataCompleted(CsvDataCompletedEventArgs eventArgs)
        {
            if (this.GetDataCompleted != null)
            {
                this.GetDataCompleted(this, eventArgs);
            }
        }
    }
    public class CsvDataCompletedEventArgs : EventArgs
    {
        public CsvDataCompletedEventArgs(List<string> result)
        {
            this.Result = result;
            this.Error = null;
            this.HasError = false;
            this.UserState = null;
        }
        public CsvDataCompletedEventArgs(List<string> result, object userState)
        {
            this.Result = result;
            this.Error = null;
            this.HasError = false;
            this.UserState = userState;
        }
        public CsvDataCompletedEventArgs(Exception error)
        {
            this.Result = null;
            this.Error = error;
            this.HasError = true;
            this.UserState = null;
        }

        /// <summary>
        /// Gets the loaded CSV file as a list of strings
        /// </summary>
        public List<string> Result { get; private set; }
        /// <summary>
        /// Gets an Exception associated with loading the CSV file
        /// </summary>
        public Exception Error { get; private set; }
        /// <summary>
        /// Gets if there were error when loading the CSV file
        /// </summary>
        public bool HasError { get; private set; }
        public object UserState { get; set; }

    }

}