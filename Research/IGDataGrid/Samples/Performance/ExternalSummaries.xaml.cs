using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Windows.DataPresenter.Events;

namespace IGDataGrid.Samples.Performance
{
    public partial class ExternalSummaries : SampleContainer
    {
        public ExternalSummaries()
        {
            InitializeComponent();
        }

        private void GenerateData_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker workerThread = new BackgroundWorker();
            workerThread.DoWork += new DoWorkEventHandler(workerThread_DoWork);
            workerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerThread_RunWorkerCompleted);
            workerThread.RunWorkerAsync((int)this.slDataItems.Value);
        }

        private void workerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            // obtaining an IEnumerable with "OrderViewModel" in it
            var dataSource = (from d in Enumerable.Range(0, (int)e.Argument)
                              select new OrderViewModel
                              {
                                  OrderNumber = string.Format("{0:185000#}", d),
                                  SalesPrice = RandomizeUtility.GenerateRandomNumber(10, 900),
                                  Quantity = RandomizeUtility.GenerateRandomNumber(10, 20)
                              });

            e.Result = new ObservableCollection<OrderViewModel>(dataSource);
        }

        private void workerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ObservableCollection<OrderViewModel> data = e.Result as ObservableCollection<OrderViewModel>;

            ListCollectionView listCollectionView = new ListCollectionView(data);

            this.dataGridExt.DataSource = listCollectionView;
            this.dataGridCommon.DataSource = data;

            this.panel.Visibility = Visibility.Hidden;
        }

        private void dataGridExt_SummaryResultChanged(object sender, SummaryResultChangedEventArgs e)
        {
            DateTime _startExtSummaries = DateTime.Now;
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() =>
                {
                    this.Label_ExternalSummariesTime.Text = DataGridStrings.XDG_ExternalSummaries_SummariesTime + " " + (DateTime.Now - _startExtSummaries).TotalSeconds;
                })
            );
        }

        private void dataGridCommon_SummaryResultChanged(object sender, SummaryResultChangedEventArgs e)
        {
            DateTime _startCommonSummaries = DateTime.Now;
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() =>
                {
                    this.Label_CommonSummariesTime.Text = DataGridStrings.XDG_ExternalSummaries_SummariesTime + " " + (DateTime.Now - _startCommonSummaries).TotalSeconds;
                })
            );
        }

        private IEnumerable<OrderViewModel> _data = null;
        private void dataGridCommon_QuerySummaryResult(object sender, QuerySummaryResultEventArgs e)
        {
            if (_data == null)
            {
                _data = this.dataGridCommon.DataSource as IEnumerable<OrderViewModel>;
            }

            string summaryType = e.Summary.SummaryDefinition.Calculator.Name;

            switch (summaryType)
            {
                case "Average":
                    e.SetSummaryValue(_data.Average(order => order.Total));
                    break;
                case "Sum":
                    e.SetSummaryValue(_data.Sum(order => order.Total));
                    break;
                case "Count":
                    e.SetSummaryValue(_data.Count());
                    break;
                case "Minimum":
                    e.SetSummaryValue(_data.Min(order => order.Total));
                    break;
                case "Maximum":
                    e.SetSummaryValue(_data.Max(order => order.Total));
                    break;
            }
        }
    }
}
