using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Infragistics.Documents.Excel;

namespace IGExtensions.Common.Data
{
    public class ExcelDataProvider : ExcelDataProviderBase
    {
        protected ExcelDataCompletedEventArgs EventArg;
        protected string FileName;
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
                EventArg = new ExcelDataCompletedEventArgs(ex);
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
                    var excelWorkbook = Workbook.Load(stream);
                    // create an event argument with successful attempt to read the CSV file
                    EventArg = new ExcelDataCompletedEventArgs(excelWorkbook);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // create an event argument with failed attempt to read the CSV file
                EventArg = new ExcelDataCompletedEventArgs(ex);
            }
        }
        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // notify when the CSV file was successful read or failed
            OnGetDataCompleted(EventArg);
        }

    }

}