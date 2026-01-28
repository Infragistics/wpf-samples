using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

namespace IGBusyIndicator.Samples.DataProviders
{
    public class OrdersDataProvider : ObservableModel
    {
        #region Members

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get
            {
                return this._orders;
            }
            set
            {
                if (this._orders != value)
                {
                    this._orders = value;
                    this.OnPropertyChanged("Orders");
                }
            }
        }

        private bool _isInProgress;
        public bool IsInProgress
        {
            get
            {
                return this._isInProgress;
            }
            set
            {
                if (this._isInProgress != value)
                {
                    this._isInProgress = value;
                    this.OnPropertyChanged("IsInProgress");
                }
            }
        }

        private double _currentProgress;
        public double CurrentProgress
        {
            get
            {
                return this._currentProgress;
            }
            set
            {
                if (this._currentProgress != value)
                {
                    this._currentProgress = value;
                    this.OnPropertyChanged("CurrentProgress");
                }
            }
        }

        private BackgroundWorker _worker = null;
        
        #endregion // Members

        #region Constructor

        public OrdersDataProvider()
        {
            GetOrdersData();
        }

        #endregion // Constructor

        private void GetOrdersData()
        {
            _worker = new BackgroundWorker { WorkerReportsProgress = true };

            _worker.DoWork += WorkerDoWork;
            _worker.ProgressChanged += WorkerProgressChanged;
            _worker.RunWorkerCompleted += WorkerCompleted;

            if (!_worker.IsBusy)
            {
                IsInProgress = true;
                _worker.RunWorkerAsync();
            }      
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            const int totalRecords = 1000;
            var orders = new List<Order>();
            e.Result = "";

            for (int i = 1; i <= totalRecords; i++)
            {
                Thread.Sleep(10);
                orders.Add(new Order()
                               {
                                   OrderNumber = string.Format("{0:185000#}", i),
                                   SalesPrice = RandomizeUtility.GenerateRandomNumber(10, 900),
                                   Quantity = RandomizeUtility.GenerateRandomNumber(1, 10),
                               });

                _worker.ReportProgress(Convert.ToInt32(((decimal)i / (decimal)totalRecords) * 100));
            }

            e.Result = orders;
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentProgress = (double)e.ProgressPercentage / 100;
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.IsInProgress = false;
            this.Orders = new ObservableCollection<Order>((List<Order>) e.Result);
        }
    }
}
