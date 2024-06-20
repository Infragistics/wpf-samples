using System;
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
    public partial class ExternalFiltering : SampleContainer
    {
        public ExternalFiltering()
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

        private void dataGridCommon_RecordFilterChanged(object sender, RecordFilterChangedEventArgs e)
        {
            DateTime _startCommonFiltering = DateTime.Now;
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() =>
                {
                    this.Label_CommonFilterTime.Text = DataGridStrings.XDG_ExternalFiltering_FilteringTime + " " + (DateTime.Now - _startCommonFiltering).TotalSeconds;
                })
            );
        }

        private void dataGridExt_RecordFilterChanged(object sender, RecordFilterChangedEventArgs e)
        {
            DateTime _startExtFiltering = DateTime.Now;
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() =>
                {
                    this.Label_ExternalFilterTime.Text = DataGridStrings.XDG_ExternalFiltering_FilteringTime + " " + (DateTime.Now - _startExtFiltering).TotalSeconds;
                })
            );
        }
    }
}
