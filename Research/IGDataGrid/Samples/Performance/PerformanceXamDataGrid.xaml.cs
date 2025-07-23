using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;

namespace IGDataGrid.Samples.Performance
{
    /// <summary>
    /// Interaction logic for PerformanceXamDataGrid.xaml
    /// </summary>
    public partial class PerformanceXamDataGrid : SampleContainer
    {
        public PerformanceXamDataGrid()
        {
            InitializeComponent();
        }

        private void GenerateData_Click(object sender, RoutedEventArgs e)
        {
            this.ShowDialog();
            this.dataGrid.DataSource = null;
            this.dataGrid.UpdateLayout();
            BackgroundWorker workerThread = new BackgroundWorker();
            workerThread.DoWork += new DoWorkEventHandler(workerThread_DoWork);
            workerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerThread_RunWorkerCompleted);
            workerThread.RunWorkerAsync();
        }

        void workerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            // obtaining an IEnumerable with "OrderViewModel" in it
            var dataSource = (from d in Enumerable.Range(0, 500000)
                              select new OrderViewModel
                              {
                                  OrderNumber = string.Format("{0:185000#}", d),
                                  SalesPrice = RandomizeUtility.GenerateRandomNumber(10, 900),
                                  Quantity = RandomizeUtility.GenerateRandomNumber(1, 10)
                              });
            // create a list with the OrderViewModel items, which is a time consuming process
            e.Result = dataSource.ToList();
        }

        void workerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.dataGrid.DataSource = e.Result as List<OrderViewModel>;
            this.MessageDisplay.Visibility = Visibility.Hidden;
        }

        private void ShowDialog()
        {
            this.MessageDisplay.Text = DataGridStrings.XWG_DataGeneration_Message;
            this.MessageDisplay.Visibility = Visibility.Visible;
        }
    }
}