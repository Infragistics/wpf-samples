using System;
using Infragistics.Documents.Excel;

namespace IGExtensions.Common.Data
{
    public abstract class ExcelDataProviderBase
    {
        /// <summary>
        /// Occurs when finished loading a Excel file.
        /// </summary>
        public event EventHandler<ExcelDataCompletedEventArgs> GetDataCompleted;

        public abstract void GetDataAsync(string xmlFileName);

        public void OnGetDataCompleted(ExcelDataCompletedEventArgs eventArgs)
        {
            if (this.GetDataCompleted != null)
            {
                this.GetDataCompleted(this, eventArgs);
            }
        }
    }
    public class ExcelDataCompletedEventArgs : EventArgs
    {
        public ExcelDataCompletedEventArgs(Workbook result)
        {
            this.Result = result;
            this.Error = null;
            this.HasError = false;
            this.UserState = null;
        }
        public ExcelDataCompletedEventArgs(Workbook result, object userState)
        {
            this.Result = result;
            this.Error = null;
            this.HasError = false;
            this.UserState = userState;
        }
        public ExcelDataCompletedEventArgs(Exception error)
        {
            this.Result = null;
            this.Error = error;
            this.HasError = true;
            this.UserState = null;
        }

        /// <summary>
        /// Gets the loaded CSV file as a list of strings
        /// </summary>
        public Workbook Result { get; private set; }
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