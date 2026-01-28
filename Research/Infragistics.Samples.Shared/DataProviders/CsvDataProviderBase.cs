using System;
using System.Collections.Generic;

namespace Infragistics.Samples.Shared.DataProviders
{
    public abstract class CsvDataProviderBase
    {
        /// <summary>
        /// Occurs when finished loading a CSV file.
        /// </summary>
        public event EventHandler<GetCsvDataCompletedEventArgs> GetCsvDataCompleted;

        public abstract void GetCsvDataAsync(string xmlFileName);

        public void OnGetCsvDataCompleted(GetCsvDataCompletedEventArgs eventArgs)
        {
            if (this.GetCsvDataCompleted != null)
            {
                this.GetCsvDataCompleted(this, eventArgs);
            }
        }
    }
    public class GetCsvDataCompletedEventArgs : EventArgs
    {
        public GetCsvDataCompletedEventArgs(List<string> result)
        {
            this.Result = result;
            this.Error = null;
            this.HasError = false;
            this.UserState = null;
        }
        public GetCsvDataCompletedEventArgs(List<string> result, object userState)
        {
            this.Result = result;
            this.Error = null;
            this.HasError = false;
            this.UserState = userState;
        }
        public GetCsvDataCompletedEventArgs(Exception error)
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