//-------------------------------------------------------------------------
// <copyright file="StockTickerDetails.cs" company="Infragistics">
//
// Copyright (c) 2010 Infragistics, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace IGTrading.Models
{
    [Serializable]
	[DataContract]
	public class StockTickerDetails : INotifyPropertyChanged
	{
		#region Private Member Variables
		private DateTime _lastUpdate = DateTime.Now;
        //private double _bid;
        //private string _percentAndChange;
        //private string _percentChange;
        //private double _change;
        //private string _stockExchange;
        //private double _open;
        //private double _previousClose;
        //private string _volume;
        //private double _sharedOwned;
        //private string _symbol;
        //private string _range52Week;
        //private double _ask;
        //private double _earningsPerShare;
        //private double _peRatio;
        //private string _lastTradeDate;
        //private string _lastTradeTime;
        private double _lastTradeAmount;
        public double _dailyHigh;
        public string _name;
        public string _EBITDA;
        public string _dailyRange;
        public double _dailyLow;
        public string _marketCapitalization;
		#endregion Private Member Variables

		#region Public Properties
		[IgnoreDataMember]
		public DateTime LastUpdate { get { return _lastUpdate; } set { _lastUpdate = value; } }

        [DataMember(Name = "Bid")]
        public double Bid { get; set; }
		[DataMember(Name = "PercentAndChange")]
		public string PercentAndChange { get; set; }
		[DataMember(Name = "PercentChange")]
		public string PercentChange { get; set; }
		[DataMember(Name = "Change")]
		public double Change { get; set; }
		[DataMember(Name = "StockExchange")]
		public string StockExchange { get; set; }
		[DataMember(Name = "Open")]
		public double Open { get; set; }
		[DataMember(Name = "PreviousClose")]
		public double PreviousClose { get; set; }
		[DataMember(Name = "Volume")]
		public string Volume { get; set; }
		[DataMember(Name = "SharesOwned")]
		public double SharesOwned { get; set; }
		[DataMember(Name = "Symbol")]
		public string Symbol { get; set; }
		[DataMember(Name = "Range52Week")]
		public string Range52Week { get; set; }
		[DataMember(Name = "Ask")]
		public double Ask { get; set; }
		[DataMember(Name = "EarningsPerShare")]
		public double EarningsPerShare { get; set; }
		[DataMember(Name = "PERatio")]
		public double PERatio { get; set; }
		[DataMember(Name = "LastTradeDate")]
		public string LastTradeDate { get; set; }
		[DataMember(Name = "LastTradeTime")]
		public string LastTradeTime { get; set; }
		[DataMember(Name = "LastTradeAmount")]
		public double LastTradeAmount 
        { 
            get
            {
                return _lastTradeAmount;
            }
            set
            {
                _lastTradeAmount = value;
                RaisePropertyChanged("LastTradeAmount");
            }
        }
		[DataMember(Name = "DailyHigh")]
		public double DailyHigh { get; set; }
		[DataMember(Name = "Name")]
		public string Name { get; set; }
		[DataMember(Name = "EBITDA")]
		public string EBITDA { get; set; }
		[DataMember(Name = "DailyRange")]
		public string DailyRange { get; set; }
		[DataMember(Name = "DailyLow")]
		public double DailyLow { get; set; }
		[DataMember(Name = "MarketCapitalization")]
		public string MarketCapitalization { get; set; }
		#endregion Public Properties

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

/*
{
	"count":4,
	"value":
	{
		"title":"Current Financial Stock Data",
		"description":"Get the latest Financial quotes (delayed 15 minutes) from Yahoo! Finance by entering the ticker symbol. Duplicate entries removed by title",
		"link":"http:\/\/pipes.yahooapis.com\/pipes\/pipe.info?_id=3e1b7fc9a1a63ea0772d20ce4573d792",
		"pubDate":"Tue, 09 Mar 2010 18:42:57 -0800",
		"generator":"http:\/\/pipes.yahooapis.com\/pipes\/",
		"callback":"",
		"items":
		[
			{
				"Bid":"557.73",
				"Change_PercentChange":"-2.29 - -0.41%",
				"StockExchange":"NasdaqNM",
				"Open":"559.46",
				"Previous_Close":"562.48",
				"Volume":"3177588",
				"Shares_Owned":"-",
				"Symbol":"GOOG",
				"52_WeekRange":"294.25 - 629.51",
				"y:row":"1",
				"Ask":"559.87",
				"EPS":"20.414",
				"PE_Ratio":"27.55",
				"Last_Trade_Date":"3\/9\/2010",
				"Last_Trade_Time":"4:00pm",
				"description":"560.19",
				"Last_Trade_Amount":"560.19",
				"Day_High":"564.66",
				"title":"GOOG",
				"Name":"Google Inc.",
				"EBITDA":"9.836B",
				"Day_Range":"556.50 - 564.66",
				"Day_Low":"556.50",
				"Market_Cap":"178.1B"
			},
			{
				"Bid":"28.80",
				"Change_PercentChange":"+0.17 - +0.59%",
				"StockExchange":"NasdaqNM",
				"Open":"28.56",
				"Previous_Close":"28.63",
				"Volume":"50278456",
				"Shares_Owned":"-",
				"Symbol":"MSFT",
				"52_WeekRange":"15.25 - 31.50",
				"y:row":"2",
				"Ask":"28.87",
				"EPS":"1.815",
				"PE_Ratio":"15.77",
				"Last_Trade_Date":"3\/9\/2010",
				"Last_Trade_Time":"4:00pm",
				"description":"28.80",
				"Last_Trade_Amount":"28.80",
				"Day_High":"29.11",
				"title":"MSFT",
				"Name":"Microsoft Corpora",
				"EBITDA":"24.427B",
				"Day_Range":"28.55 - 29.11",
				"Day_Low":"28.55",
				"Market_Cap":"252.6B"
			}
		]
	}
}
*/