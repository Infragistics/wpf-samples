using System;
using System.Collections.Generic; 
using System.Linq; 
using System.Windows.Media; 
using System.ComponentModel;
  
namespace IGShowcase.FinanceDashboard
{ 
    public class StockTradeData : List<StockDataItem>, INotifyPropertyChanged
    {
        #region Properties
        public string Symbol
        {
            get { return Details != null ? Details.Symbol : string.Empty; }
            set { if (Details != null) Details.Symbol = value; }
        }

        public string Title
        {
            get { return Details != null ? Details.Name + " (" + Details.Symbol + ")" : string.Empty; }
        }

        private StockTradeDetails _Details;
        public StockTradeDetails Details
        {
            get { return _Details; }
            set { if (value == _Details) return; _Details = value; OnPropertyChanged("Details"); OnPropertyChanged("Symbol"); }
        }

        private bool _IsSelected;
        /// <summary>  Gets or sets the selection state.  </summary>
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (value == _IsSelected) return;
                _IsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private Brush _Brush;
        /// <summary> Gets or sets Brush </summary>
        public Brush Brush
        {
            get { return _Brush; }
            set { if (_Brush == value) return; _Brush = value; OnPropertyChanged("Brush"); }
        }

        public StockViewModel Parent { get; internal set; }

        #endregion
        public StockTradeData()
        {
            _Brush = new SolidColorBrush(Colors.Yellow);
            _Details = new StockTradeDetails();
        }
        public StockTradeData(StockViewModel parent, string symbol, Brush brush)
        {
            Parent = parent;

            _Brush = brush;  
            _Details = new StockTradeDetails();
            _Details.Symbol = symbol;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        { 
            if (PropertyChanged != null)            
                PropertyChanged(sender, e);           
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        } 

        protected Random Random = Randomizer.Instance;
        public void SimmulateChanges(bool updateStockData = true)
        {
            var currentDetails = this.Details;
            //buy = ask and sell = bid, will be just a bit smaller than the buy price
            currentDetails.Ask = Math.Round(currentDetails.Open * (Random.NextDouble() / 20 + 0.975), 2);
            currentDetails.Bid = Math.Round(currentDetails.Ask - ((Random.NextDouble() / 20) * currentDetails.Ask), 2);
            var delta = currentDetails.Ask - currentDetails.Bid;
            currentDetails.LastTradeAmount = Math.Round(currentDetails.Bid + ((Random.NextDouble() / 20) * delta), 2);

            if (currentDetails.DailyLow == 0 || 
                currentDetails.DailyLow > currentDetails.LastTradeAmount)
                currentDetails.DailyLow = currentDetails.LastTradeAmount;
            if (currentDetails.DailyHigh == 0 ||
                currentDetails.DailyHigh < currentDetails.LastTradeAmount)
                currentDetails.DailyHigh = currentDetails.LastTradeAmount;

            currentDetails.UpdateTradeChange();
            currentDetails.Volume = currentDetails.Volume * (Random.NextDouble() / 5 + 0.90);

            // update stock data with latest values
            if (updateStockData)
                UpdateLastStockData(currentDetails.ToStockData());
        }

        public void UpdateLastStockData(StockTradeDetails stockDetails)
	    {
	        UpdateLastStockData(stockDetails.ToStockData());
	    }

        public void UpdateLastStockData(StockDataItem stockData)
	    {
            if (this.Any())// && FilteredData.Any())
            {
                var lastStockData = this.Last();
                if (lastStockData.Date.DayOfYear == stockData.Date.DayOfYear)
                {
                    this[this.Count - 1] = stockData;
                    //this.FilteredData[this.FilteredData.Count - 1] = stockData;
                }
                else
                {
                    this.Add(stockData);
                    //this.FilteredData.Add(stockData);
                }
            }
	    }
    }

     
}
