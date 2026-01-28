using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using IGPivotGrid.Resources;
using IGPivotGrid.Samples.Controls;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Data
{
    public partial class LiveUpdate : SampleContainer
    {
        private SalesCollection _dataItems;
        private ObservableCollection<string> _latestSales;
        BackgroundWorker worker;

        public LiveUpdate()
        {
            InitializeComponent();
            InitializeDataSource();
            if (worker != null)
            {
                this.Unloaded += (s, e) => worker.CancelAsync();
            }           
        }

        private void InitializeDataSource()
        {
            SampleFlatDataSourceForLiveUpdate dataSource = this.Resources["FlatDataSource"] as SampleFlatDataSourceForLiveUpdate;
            if (dataSource != null)
            {
                _dataItems = dataSource.ItemsSource as SalesCollection;
                dataSource.ResultChanged += DataSource_ResultChanged;

                // add another collection for new sales' history
                _latestSales = new ObservableCollection<string>();
                this.newSalesList.ItemsSource = _latestSales;
            }
        }

        private void DataSource_ResultChanged(object sender, AsyncCompletedEventArgs e)
        {
            this.pivotGrid.DataSource.ResultChanged -= DataSource_ResultChanged;

            //Start the updating of the data source in a new thread
            //as it would otherwise block the UI thread
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Generate new sales and add them to the data source every second
            int i = 0;
            while (i < 15000 && worker.CancellationPending != true)
            {
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    if (!this.pivotGrid.DataSource.IsBusy)
                    {
                        ObservableCollection<Infragistics.Samples.Shared.Models.Sale> dataToAdd =
                            Infragistics.Samples.Shared.Models.SalesDataGenerator.GenerateSales(1);
                        foreach (Infragistics.Samples.Shared.Models.Sale sale in dataToAdd)
                        {
                            string str = String.Format(PivotGridStrings.XPG_LiveUpdate_UpdateString,
                                                       sale.Seller.Name,
                                                       sale.Seller.City,
                                                       sale.NumberOfUnits,
                                                       sale.Product.Name,
                                                       sale.AmountOfSale.ToString());
                            _latestSales.Insert(0, str);
                            _dataItems.Add(new Sale()
                            {
                                Seller = new Seller() { City = sale.Seller.City, Name = sale.Seller.Name },
                                Product = new Product() { Name = sale.Product.Name, UnitPrice = sale.Product.UnitPrice },
                                Date = sale.Date,
                                NumberOfUnits = sale.NumberOfUnits,
                            });
                        }
                    }
                }));

                Thread.Sleep(1000);
                i++;
            }
        }
    }
}
