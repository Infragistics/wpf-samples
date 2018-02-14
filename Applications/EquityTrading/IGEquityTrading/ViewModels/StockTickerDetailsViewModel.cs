using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IGTrading.ViewModels
{
    public class StockTickerDetailsViewModel : INotifyPropertyChanged
    {
        #region Private Member Variables
        private double _oldLastTradeAmount;
        private DateTime _lastUpdate = DateTime.Now;
        private double _bid;
        private string _percentAndChange;
        private string _percentChange;
        private double _change;
        private string _stockExchange;
        private double _open;
        private double _previousClose;
        private string _volume;
        private double _sharesOwned;
        private string _symbol;
        private string _range52Week;
        private double _ask;
        private double _earningsPerShare;
        private double _peRatio;
        private string _lastTradeDate;
        private string _lastTradeTime;
        private double _lastTradeAmount;
        public double _dailyHigh;
        public string _name;
        public string _EBITDA;
        public string _dailyRange;
        public double _dailyLow;
        public string _marketCapitalization;

        private ObservableCollection<StockTickerDataViewModel> _sellTickerData = new ObservableCollection<StockTickerDataViewModel>();
        private ObservableCollection<StockTickerDataViewModel> _buyTickerData = new ObservableCollection<StockTickerDataViewModel>();
        int oldTwoSecIndex = -1;
        private StockTickerDataViewModel currentTickerDataSell;
        private StockTickerDataViewModel currentTickerDataBuy;
        #endregion Private Member Variables

        #region Public Properties
        public ObservableCollection<StockTickerDataViewModel> SellTickerData
        {
            get { return _sellTickerData; }
        }

        public ObservableCollection<StockTickerDataViewModel> BuyTickerData
        {
            get { return _buyTickerData; }
        }
        public DateTime LastUpdate { get { return _lastUpdate; } set { _lastUpdate = value; } }

        public double Bid 
        {
            get
            {
                return _bid;
            }
            set
            {
                if (_bid != value)
                {
                    _bid = value;
                    RaisePropertyChanged("Bid");
                }
            }
        }
        public string PercentAndChange 
        { 
            get
            {
                return _percentAndChange;
            }
            set
            {
                if (_percentAndChange != value)
                {
                    _percentAndChange = value;
                    RaisePropertyChanged("PercentAndChange");
                }
            }
        }
        public string PercentChange 
        { 
            get
            {
                return _percentChange;
            }
            set
            {
                if (_percentChange != value)
                {
                    _percentChange = value;
                    RaisePropertyChanged("PercentChange");
                }
            }
        }
        public double Change 
        { 
            get
            {
                return _change;
            }
            set
            {
                if (_change != value)
                {
                    _change = value;
                    RaisePropertyChanged("Change");
                }
            }
        }
        public string StockExchange
        {
            get
            {
                return _stockExchange;
            }
            set
            {
                if (_stockExchange != value)
                {
                    _stockExchange = value;
                    RaisePropertyChanged("StockExchange");
                }
            }
        }
        public double Open 
        { 
            get
            {
                return _open;
            }
            set
            {
                if (_open != value)
                {
                    _open = value;
                    RaisePropertyChanged("Open");
                }
            }
        }
        public double PreviousClose 
        { 
            get
            {
                return _previousClose;
            }
            set
            {
                if (_previousClose != value)
                {
                    _previousClose = value;
                    RaisePropertyChanged("PreviousClose");
                }
            }
        }
        public string Volume 
        { 
            get
            {
                return _volume;
            }
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    RaisePropertyChanged("Volume");
                }
            }
        }
        public double SharesOwned 
        {
            get
            {
                return _sharesOwned;
            }
            set
            {
                if (_sharesOwned != value)
                {
                    _sharesOwned = value;
                    RaisePropertyChanged("SharesOwned");
                }
            }
        }
        public string Symbol 
        { 
            get
            {
                return _symbol;
            }
            set
            {
                if (_symbol != value)
                {
                    _symbol = value;
                    RaisePropertyChanged("Symbol");
                }
            }
        }
        public string Range52Week
        {
            get
            {
                return _range52Week;
            }
            set
            {
                if (_range52Week != value)
                {
                    _range52Week = value;
                    RaisePropertyChanged("Range52Week");
                }
            }
        }
        public double Ask 
        { 
            get
            {
                return _ask;
            }
            set
            {
                if (_ask != value)
                {
                    _ask = value;
                    RaisePropertyChanged("Ask");
                }
            }
        }
        public double EarningsPerShare 
        { 
            get
            {
                return _earningsPerShare;
            }
            set
            {
                if (_earningsPerShare != value)
                {
                    _earningsPerShare = value;
                    RaisePropertyChanged("EarningsPerShare");
                }
            }
        }
        public double PERatio
        {
            get
            {
                return _peRatio;
            }
            set
            {
                if (_peRatio != value)
                {
                    _peRatio = value;
                    RaisePropertyChanged("PERatio");
                }
            }
        }
        public string LastTradeDate
        {
            get
            {
                return _lastTradeDate;
            }
            set
            {
                if (_lastTradeDate != value)
                {
                    _lastTradeDate = value;
                    RaisePropertyChanged("LastTradeDate");
                }
            }
        }
        public string LastTradeTime
        {
            get
            {
                return _lastTradeTime;
            }
            set
            {
                if (_lastTradeTime != value)
                {
                    _lastTradeTime = value;
                    RaisePropertyChanged("LastTradeTime");
                }
            }
        }
        public double LastTradeAmount
        {
            get
            {
                return _lastTradeAmount;
            }
            set
            {
                if (_lastTradeAmount != value)
                {
                    _oldLastTradeAmount = _lastTradeAmount;
                    _lastTradeAmount = value;

                    if (this.DailyLow > this.LastTradeAmount)
                        this.DailyLow = this.LastTradeAmount;

                    if (this.DailyHigh < this.LastTradeAmount)
                        this.DailyHigh = this.LastTradeAmount;

                    this.Change = Math.Round(_lastTradeAmount - _oldLastTradeAmount,2);
                    this.PercentChange = String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.00}", (_lastTradeAmount - _oldLastTradeAmount) * 100 / _oldLastTradeAmount) + "%";
                    RaisePropertyChanged("LastTradeAmount");
                }
            }
        }
        public double DailyHigh
        {
            get
            {
                return _dailyHigh;
            }
            set
            {
                if (_dailyHigh != value)
                {
                    _dailyHigh = value;
                    RaisePropertyChanged("DailyHigh");
                }
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        public string EBITDA
        {
            get
            {
                return _EBITDA;
            }
            set
            {
                if (_EBITDA != value)
                {
                    _EBITDA = value;
                    RaisePropertyChanged("EBITDA");
                }
            }
        }
        public string DailyRange
        {
            get
            {
                return _dailyRange;
            }
            set
            {
                if (_dailyRange != value)
                {
                    _dailyRange = value;
                    RaisePropertyChanged("DailyRange");
                }
            }
        }
        public double DailyLow
        {
            get
            {
                return _dailyLow;
            }
            set
            {
                if (_dailyLow != value)
                {
                    _dailyLow = value;
                    RaisePropertyChanged("DailyLow");
                }
            }
        }
        public string MarketCapitalization
        {
            get
            {
                return _marketCapitalization;
            }
            set
            {
                if (_marketCapitalization != value)
                {
                    _marketCapitalization = value;
                    RaisePropertyChanged("MarketCapitalization");
                }
            }
        }
        #endregion Public Properties

        public StockTickerDetailsViewModel()
        {

        }

        public StockTickerDetailsViewModel(IGTrading.Models.StockTickerDetails st)
        {
            Update(st);
        }

        public void UpdateBuySellPrices()
        {
            int twoSecIndex = DateTime.Now.Second % 2;
            //we're in the same 2-sec interval, just update values
            if (oldTwoSecIndex == twoSecIndex)
            {
                currentTickerDataSell.Close = this.Bid;
                if (currentTickerDataSell.High < this.Bid)
                    currentTickerDataSell.High = this.Bid;
                if (currentTickerDataSell.Low > this.Bid)
                    currentTickerDataSell.Low = this.Bid;

                currentTickerDataBuy.Close = this.Ask;
                if (currentTickerDataBuy.High < this.Ask)
                    currentTickerDataBuy.High = this.Ask;
                if (currentTickerDataBuy.Low > this.Ask)
                    currentTickerDataBuy.Low = this.Ask;
            }
            //change of interval, add current data to buffers, initialize new ticker data
            else 
            {
                //oldTwoSec is -1 the first time a position is updated, so insert the ticker data if it's different from -1
                if (oldTwoSecIndex != -1)
                {
                    _buyTickerData.Add(currentTickerDataBuy);
                    _sellTickerData.Add(currentTickerDataSell);

                    //keep track of the last minute of real-time data
                    //30 2-sec intervals = one minutes worth of data
                    //remove the oldest data point once data for more than a minute is accumulated
                    if (_buyTickerData.Count == 31)
                        _buyTickerData.RemoveAt(0);
                    if (_sellTickerData.Count == 31)
                        _sellTickerData.RemoveAt(0);
                }

                oldTwoSecIndex = twoSecIndex;
                currentTickerDataSell = new StockTickerDataViewModel();
                currentTickerDataSell.Date = DateTime.Now;
                currentTickerDataSell.Open = this.Bid;
                currentTickerDataSell.Close = this.Bid;
                currentTickerDataSell.High = this.Bid;
                currentTickerDataSell.Low = this.Bid;

                currentTickerDataBuy = new StockTickerDataViewModel();
                currentTickerDataBuy.Date = DateTime.Now;
                currentTickerDataBuy.Open = this.Ask;
                currentTickerDataBuy.Close = this.Ask;
                currentTickerDataBuy.High = this.Ask;
                currentTickerDataBuy.Low = this.Ask;
            }
        }

        public void Update(IGTrading.Models.StockTickerDetails st)
        {
            this.Ask = st.Ask;
            this.Bid = st.Bid;
            this.Change = st.Change;
            this.DailyHigh = st.DailyHigh;
            this.DailyLow = st.DailyLow;
            this.DailyRange = st.DailyRange;
            this.EarningsPerShare = st.EarningsPerShare;
            this.EBITDA = st.EBITDA;
            this.LastTradeAmount = st.LastTradeAmount;
            this.LastTradeDate = st.LastTradeDate;
            this.LastTradeTime = st.LastTradeTime;
            this.LastUpdate = DateTime.Now;
            this.MarketCapitalization = st.MarketCapitalization;
            this.Name = st.Name;
            this.Open = st.Open;
            this.PERatio = st.PERatio;
            this.PercentAndChange = st.PercentAndChange;
            this.PercentChange = st.PercentChange;
            this.PreviousClose = st.PreviousClose;
            this.Range52Week = st.Range52Week;
            this.SharesOwned = st.SharesOwned;
            this.StockExchange = st.StockExchange;
            this.Symbol = st.Symbol;
            this.Volume = st.Volume;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        internal void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
