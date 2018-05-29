using System;  
using System.Runtime.Serialization;  
using IGExtensions.Framework; 

namespace IGShowcase.FinanceDashboard
{ 
	[DataContract]
    public class StockTradeDetails : ObservableModel
	{
	    public StockTradeDetails()
	    {
            this.Symbol = "";
            this.Name = "";

            this.Ask = 0.0;
            this.Bid = 0.0;
            this.Change = 0.0;
            this.Open = 0.0;
            this.PreviousClose = 0.0;
            this.Volume = 0.0;

            this.EarningsPerShare = 0.0;
            this.PERatio = 0.0;
            this.EBITDA = "0.00";
            this.MarketCapitalization = "0.00";
            this.StockExchange = "0.00";
            this.PercentAndChange = "0.00";
            this.ChangePercentage = "0.00%";
       
            this.LastTradeAmount = 0.0;
            this.LastTradeTime = "--:--";
            this.LastTradeDate = "--/--/----";
            
            this.YearlyRange = "0.00 - 0.00";
            this.DailyRange = "0.00 - 0.00";
            this.DailyHigh = 0.0;
            this.DailyLow = 0.0;
     
	    }
		 
		#region Public Properties

        [IgnoreDataMember]
        public bool IsStockTrading { get { return IsStockOnMarket(); } }
	
        private bool IsStockOnMarket()
        {
            var daysFromLastTrade = DateTime.Now.Subtract(LastUpdate).TotalDays;
            return daysFromLastTrade <= 10;
        }

        private DateTime _lastUpdate = DateTime.Now.AddYears(-1);
        [IgnoreDataMember]
        public DateTime LastUpdate
        {
            get { return _lastUpdate; }
            set { _lastUpdate = value; OnPropertyChanged("LastUpdate"); }
        }
      
        [DataMember(Name = "Symbol")]
        public string Symbol { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        public string Title { get; set; }
	
        [DataMember(Name = "Ask")]
        public double Ask { get { return _ask; } set { _ask = value; OnPropertyChanged("Ask"); } }
        private double _ask;
        [DataMember(Name = "Bid")]
        public double Bid { get { return _bid; } set { _bid = value; OnPropertyChanged("Bid"); } }
        private double _bid;
        [DataMember(Name = "Change")]
        public double Change { get { return _change; } set { _change = value; OnPropertyChanged("Change"); } }
        private double _change;
        
        [DataMember(Name = "PercentAndChange")]
        public string PercentAndChange { get { return _percentAndChange; } set { _percentAndChange = value; OnPropertyChanged("PercentAndChange"); } }
        private string _percentAndChange;
        
        [DataMember(Name = "PercentChange")]
        public string ChangePercentage { get { return _changePercentage; } set { _changePercentage = value; OnPropertyChanged("ChangePercentage"); } }
        private string _changePercentage;
     
        [DataMember(Name = "Volume")]
        public double Volume { get { return _volume; } set { _volume = value; OnPropertyChanged("Volume"); } }
        private double _volume;
        [DataMember(Name = "Open")]
        public double Open { get { return _open; } set { _open = value; OnPropertyChanged("Open"); } }
        private double _open;

        [DataMember(Name = "LastTradeDate")]
        public string LastTradeDate { get; set; }
        [DataMember(Name = "LastTradeTime")]
        public string LastTradeTime { get; set; }
        [DataMember(Name = "LastTradeAmount")]
	    public double LastTradeAmount
	    {
	        get { return _lastTradeAmount; }
            set
            {
                _oldTradeAmount = _lastTradeAmount;
                _lastTradeAmount = value; OnPropertyChanged("LastTradeAmount"); 
            }
	    }
        private double _oldTradeAmount = 0;
        private double _lastTradeAmount;

	    [DataMember(Name = "DailyHigh")]
	    public double DailyHigh
	    {
	        get { return _dailyHigh; }
            set { _dailyHigh = value; OnDailyRangeChanged(); OnPropertyChanged("DailyHigh"); }
	    }
        private double _dailyHigh;
        [DataMember(Name = "DailyLow")]
        public double DailyLow 
        { 
            get { return _dailyLow; }
            set { _dailyLow = value; OnDailyRangeChanged(); OnPropertyChanged("DailyLow"); }
        }
        private double _dailyLow;
        public double YearlyLow
        {
            get { return _yearlyLow; }
            set { _yearlyLow = value; OnYearlyRangeChanged(); OnPropertyChanged("YearlyLow"); }
        }
        private double _yearlyLow;
        public double YearlyHigh
        {
            get { return _yearlyHigh; }
            set { _yearlyHigh = value; OnYearlyRangeChanged(); OnPropertyChanged("YearlyHigh"); }
        }
        private double _yearlyHigh;

        [DataMember(Name = "DailyRange")]
        public string DailyRange { get { return _dailyRange; } set { _dailyRange = value; OnPropertyChanged("DailyRange"); } }
        private string _dailyRange;

        protected void OnDailyRangeChanged()
        {
            DailyRange = string.Format("{0:0.00} - {1:0.00}", DailyLow, DailyHigh);
        }
        protected void OnYearlyRangeChanged()
        {
            YearlyRange = string.Format("{0:0.00} - {1:0.00}", YearlyLow, YearlyHigh);
        }
	    
        [DataMember(Name = "StockExchange")]
		public string StockExchange { get; set; }
  
		[DataMember(Name = "PreviousClose")]
		public double PreviousClose { get; set; }
		
		[DataMember(Name = "Range52Week")]
		public string YearlyRange { get; set; }
		[DataMember(Name = "EarningsPerShare")]
		public double EarningsPerShare { get; set; }
		[DataMember(Name = "PERatio")]
		public double PERatio { get; set; }
		 
		[DataMember(Name = "EBITDA")]
		public string EBITDA { get; set; }
        [DataMember(Name = "MarketCapitalization")]
		public string MarketCapitalization { get; set; }
		#endregion Public Properties

        int oldTwoSecIndex = -1;
        //private StockTickerData TickerSellData;
        //private StockTickerData TickerBuyData;

        //private ObservableCollection<StockTickerData> TickerSellDataList = new ObservableCollection<StockTickerData>();
        //private ObservableCollection<StockTickerData> TickerBuyDataList = new ObservableCollection<StockTickerData>();

     //   public void UpdateStockPrices()
	    //{
     //       var twoSecIndex = DateTime.Now.Second % 2;
     //       //we're in the same 2-sec interval, just update values
     //       if (oldTwoSecIndex == twoSecIndex)
     //       {
     //           TickerSellData.Close = this.Bid;
     //           if (TickerSellData.High < this.Bid)
     //               TickerSellData.High = this.Bid;
     //           if (TickerSellData.Low > this.Bid)
     //               TickerSellData.Low = this.Bid;

     //           TickerBuyData.Close = this.Ask;
     //           if (TickerBuyData.High < this.Ask)
     //               TickerBuyData.High = this.Ask;
     //           if (TickerBuyData.Low > this.Ask)
     //               TickerBuyData.Low = this.Ask;
     //       }
     //       //change of interval, add current data to buffers, initialize new ticker data
     //       else
     //       {
     //           //oldTwoSec is -1 the first time a position is updated, so insert the ticker data if it's different from -1
     //           if (oldTwoSecIndex != -1)
     //           {
     //               TickerBuyDataList.Add(TickerBuyData);
     //               TickerSellDataList.Add(TickerSellData);

     //               //keep track of the last minute of real-time data
     //               //30 2-sec intervals = one minutes worth of data
     //               //remove the oldest data point once data for more than a minute is accumulated
     //               if (TickerBuyDataList.Count == 31)
     //                   TickerBuyDataList.RemoveAt(0);
     //               if (TickerSellDataList.Count == 31)
     //                   TickerSellDataList.RemoveAt(0);
     //           }

     //           oldTwoSecIndex = twoSecIndex;
     //           TickerSellData = new StockTickerData();
     //           TickerSellData.Date = DateTime.Now;
     //           TickerSellData.Open = this.Bid;
     //           TickerSellData.Close = this.Bid;
     //           TickerSellData.High = this.Bid;
     //           TickerSellData.Low = this.Bid;

     //           TickerBuyData = new StockTickerData();
     //           TickerBuyData.Date = DateTime.Now;
     //           TickerBuyData.Open = this.Ask;
     //           TickerBuyData.Close = this.Ask;
     //           TickerBuyData.High = this.Ask;
     //           TickerBuyData.Low = this.Ask;
     //       }
     //   }

        public StockDataItem ToStockData()
        {
            var stockData = new StockDataItem();
            stockData.Close = LastTradeAmount;
            stockData.Open = Open;
            stockData.High = DailyHigh;
            stockData.Low = DailyLow;
            stockData.Volume = Volume;
            stockData.Date = LastUpdate;
            return stockData;
        }
        
        public void UpdateTradeChange()
	    {
            this.Change = LastTradeAmount - this.Open;
            var percentage = this.Change * 100 / _oldTradeAmount;
            var percentageString = string.Format("{0:0.00}%", percentage);
            if (percentage > 0) percentageString = "+" + percentageString;
            this.ChangePercentage = percentageString;
	    }

	}
}
