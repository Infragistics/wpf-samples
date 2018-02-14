using System;
using System.ComponentModel;

namespace IGTrading.ViewModels
{
    public class StockPositionViewModel : INotifyPropertyChanged
    {
        private static int _sequenceNum = 0;
        private readonly int _sequenceNumber = StockPositionViewModel._sequenceNum++;
        private StockTickerDetailsViewModel _stockTickerDetailsViewModel;
        private double _pricePaid;
        private int _quantity;
        private readonly DateTime _acquiredTimeStamp = DateTime.Now;

        public StockPositionViewModel()
        {
            //var td = new StockTickerDetailsViewModel();
            //td.Name = "Microsoft";
            //td.Symbol = "MSFT";
            //td.Open = 50;
            //td.DailyLow = 50;
            //td.Bid = 49;
            //td.Ask = 50;
            //td.Change = 10;
            //td.LastTradeAmount = 46;
            _pricePaid = 50;
            _quantity = 1;

            _stockTickerDetailsViewModel = new StockTickerDetailsViewModel();
            _stockTickerDetailsViewModel.Name = "Microsoft";
            _stockTickerDetailsViewModel.Symbol = "MSFT";
            _stockTickerDetailsViewModel.Open = 50;
            _stockTickerDetailsViewModel.DailyLow = 50;
            _stockTickerDetailsViewModel.Bid = 49;
            _stockTickerDetailsViewModel.Ask = 50;
            _stockTickerDetailsViewModel.Change = 10;
            _stockTickerDetailsViewModel.LastTradeAmount = 46;
        }

        void _stockTickerDetails_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("LastTradeAmount");
            RaisePropertyChanged("PercentChange");
            RaisePropertyChanged("MKTValue");
            RaisePropertyChanged("ProfitLoss");
            RaisePropertyChanged("ProfitLossPercent");
        }

        /// <summary>
        /// Gets the ID for this position
        /// </summary>
        public int ID
        {
            get { return _sequenceNumber; }
        }

        /// <summary>
        /// Gets the symbol for this position
        /// </summary>
        public string Symbol
        {
            get { return _stockTickerDetailsViewModel.Symbol; }
        }

        /// <summary>
        /// Gets the amount of the stock in this position
        /// </summary>
        public int Quantity
        {
            get { return _quantity; }
            set 
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    RaisePropertyChanged("Quantity");
                }
            }
        }

        /// <summary>
        /// Gets the price paid per share for this stock at acquisition
        /// </summary>
        public double PricePaid
        {
            get { return _pricePaid; }
            set { _pricePaid = value; }
        }

        public double LastTradeAmount
        {
            get { return _stockTickerDetailsViewModel.LastTradeAmount; }
        }

        public double MKTValue
        {
            get { return _stockTickerDetailsViewModel.LastTradeAmount * this.Quantity; }
        }

        public double ProfitLoss
        {
            get
            {
                return (_stockTickerDetailsViewModel.LastTradeAmount - this.PricePaid) * this.Quantity;
            }
        }

        public string ProfitLossPercent
        {
            get
            {
                return Math.Round((_stockTickerDetailsViewModel.LastTradeAmount - this.PricePaid) / this.PricePaid, 2).ToString() + "%";
            }
        }

        /// <summary>
        /// Gets the time the position was entered into
        /// </summary>
        public DateTime AcquiredTimeStamp
        {
            get { return _acquiredTimeStamp; }
        }

        
        /// <summary>
        /// Gets or sets the details for the stock in this position
        /// </summary>
        public StockTickerDetailsViewModel StockTickerDetails
        {
            get { return _stockTickerDetailsViewModel; }
            set 
            { 
                _stockTickerDetailsViewModel = value;
                _stockTickerDetailsViewModel.PropertyChanged += new PropertyChangedEventHandler(_stockTickerDetails_PropertyChanged);
                RaisePropertyChanged("StockTickerDetails");
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
