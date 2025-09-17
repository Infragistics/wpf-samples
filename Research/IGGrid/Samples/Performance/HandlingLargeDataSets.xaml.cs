using IGGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace IGGrid.Samples.Performance
{
    public partial class HandlingLargeDataSets : SampleContainer
    {
        public HandlingLargeDataSets()
        {
            InitializeComponent();
        }

        private void GenerateData_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.ItemsSource = null;
            this.dataGrid.UpdateLayout();

            GenerateData.IsEnabled = false;
            lblStatus.Visibility = Visibility.Visible;
            lblStatus.Text = GridStrings.XWG_DataGeneration_Message;

            var workerThread = new BackgroundWorker();
            workerThread.DoWork += new DoWorkEventHandler(workerThread_DoWork);
            workerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerThread_RunWorkerCompleted);
            workerThread.RunWorkerAsync();
        }

        void workerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            var result = (from d in Enumerable.Range(0, 1500000)
                          select new Order
                          {
                              OrderNumber = string.Format("{0:185000#}", d),
                              SalesPrice = RandomizeUtility.GenerateRandomNumber(10, 900),
                              Quantity = RandomizeUtility.GenerateRandomNumber(1, 10)
                          });
            e.Result = result.ToList();
        }

        void workerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.dataGrid.ItemsSource = e.Result as List<Order>;
            lblStatus.Visibility = Visibility.Collapsed;
            GenerateData.IsEnabled = true;
        }
    }
}
