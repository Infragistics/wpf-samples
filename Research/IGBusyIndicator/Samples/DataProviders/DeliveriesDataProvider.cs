using System;
using IGBusyIndicator.Resources;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IGBusyIndicator.Samples.DataProviders
{
    public class DeliveriesDataProvider : ObservableModel
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

        private string _operationStatus;
        public string OperationStatus
        {
            get
            {
                return this._operationStatus;
            }
            set
            {
                if (this._operationStatus != value)
                {
                    this._operationStatus = value;
                    this.OnPropertyChanged("OperationStatus");
                }
            }
        }

        private int _rowsLoaded;
        public int RowsLoaded
        {
            get
            {
                return this._rowsLoaded;
            }
            set
            {
                if (this._rowsLoaded != value)
                {
                    this._rowsLoaded = value;
                    this.OnPropertyChanged("RowsLoaded");
                }
            }
        }

        private List<Order> _partialOrders;
        private CancellationTokenSource _cancellationTokenSource;
        private DelegateCommand _cancelLoadingCommand;
        private DelegateCommand _loadingCommand;

        #endregion // Members

        #region Commands

        private bool CanDoAction(object obj)
        {
            return true;
        }

        public ICommand LoadingCommand
        {
            get { return _loadingCommand; }
        }

        private void Loading(object param)
        {
            GetOrdersData();
        }

        public ICommand CancelLoadingCommand
        {
            get { return _cancelLoadingCommand; }
        }

        private void CancelLoading(object param)
        {
            _cancellationTokenSource.Cancel();
        }

        #endregion // Commands

        #region Constructor

        public DeliveriesDataProvider()
        {
            // Create command to cancel loading operation
            _loadingCommand = new DelegateCommand(Loading, CanDoAction);

            // Create command to cancel loading operation
            _cancelLoadingCommand = new DelegateCommand(CancelLoading, CanDoAction);
        }

        #endregion // Constructor

        private void GetOrdersData()
        {
            this.Orders = new ObservableCollection<Order>();
            this.IsInProgress = true;
            this.OperationStatus = BusyIndicatorStrings.Loading;

            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            var loadingTask = Task<List<Order>>.Factory.StartNew(() => DoWork(token), token);
            var finalizingTask = loadingTask.ContinueWith(task => Finalize(task.Result, token));
        }

        private void Finalize(List<Order> result, CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
            {
                this.Orders = new ObservableCollection<Order>(_partialOrders);
                this.OperationStatus = BusyIndicatorStrings.LoadingInterrupted;
            }
            else
            {
                this.Orders = new ObservableCollection<Order>(result);
                this.OperationStatus = BusyIndicatorStrings.LoadingCompleted;
            }

            IsInProgress = false;
        }

        private List<Order> DoWork(CancellationToken ct)
        {
            const int totalRecords = 1000;
            var orders = new List<Order>();

            for (int i = 1; i <= totalRecords; i++)
            {
                if (ct.IsCancellationRequested)
                {
                    _partialOrders = orders;
                    break;
                }

                Thread.Sleep(10);
                orders.Add(new Order()
                {
                    OrderNumber = string.Format("{0:185000#}", i),
                    SalesPrice = RandomizeUtility.GenerateRandomNumber(10, 900),
                    Quantity = RandomizeUtility.GenerateRandomNumber(1, 10),
                });

                this.RowsLoaded = i;
            }

            return orders;
        }
    }
}
