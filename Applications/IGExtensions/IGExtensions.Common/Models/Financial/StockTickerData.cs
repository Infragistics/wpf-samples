using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Globalization;
using IGExtensions.Framework;

namespace IGExtensions.Common.Models
{
    public class StockTickerList : List<StockTickerData>
    {
        
    }
	[DataContract]
	public class StockTickerData //: ObservableModel
	{
        public StockTickerData()
        { }
        public StockTickerData(StockTickerData stockData)
        {
            this.Open = stockData.Open;
            this.High = stockData.High;
            this.Low = stockData.Low;
            this.Close = stockData.Close;
            this.Volume = stockData.Volume;
            this.Date = stockData.Date;
        }
        public StockTickerData(double open, double low, double high, double close, long volume, DateTime date)
	    {
            this.Open = open;
            this.High = high;
            this.Low = low;
            this.Close = close;
            this.Volume = volume;
            this.Date = date;
	    }
        public new string ToString()
        {
            var result = string.Empty;
            result += " Open: " + Open + " Close: " + Close + " Volume: " + Volume + " Date: " + Date;
            return result;
        }
        public StockTickerData Copy()
        {
            return new StockTickerData(this);
        }

        //[DataMember(Name = "Volume")]
        //public double Volume { get { return _volume; } set { _volume = value; OnPropertyChanged("Volume"); } }
        //private double _volume;

        [DataMember(Name = "Volume")]
        public double Volume { get; set; }
		
		[DataMember(Name = "Open")]
		public double Open { get; set; }
		[DataMember(Name = "Low")]
		public double Low { get; set; }
        [DataMember(Name = "High")]
		public double High { get; set; }
		[DataMember(Name = "Close")]
		public double Close { get; set; }

        // TODO-MT implement % scale of OHLC values like Google finance chart does
        //public double ClosePercent { get { return ChangePercent; } }
        ////public double OpenPercent { get { return (OpenChange / Open) * 100; } }
        //public double HighPercent { get { return (HighChange / Open) * 100; } }
        //public double LowPercent { get { return (LowChange / Open) * 100; } }
        //public double OpenPercent { get; set; }
        //public double HighChange { get { return (High - Open); } }
        //public double LowChange { get { return (Low - Open); } }
        ////public double OpenChange { get { return (Open - Close); } }

        public double Change { get { return (Close - Open); } }
        public double ChangePercent { get { return (Change / Open) * 100; } }
        public double Range { get { return (High - Low); } }
     
		//Workaround date time de-serialization
		[DataMember(Name = "Date")]
		public string DateString { get; set; }
		public DateTime Date
		{
			get { return DateTime.ParseExact(DateString, "yyyy-MM-dd", CultureInfo.InvariantCulture); }
			set { DateString = value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture); }
		}
	}
}
