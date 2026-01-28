using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace Infragistics.Framework
{
    /// <summary>
    /// Represents a provider with Stock Market data  
    /// </summary>
    public static class StockMarketData
    {
        internal static Random Rand = new Random();
        static StockMarketData()
        {
            Stocks = new List<StockItem>();
            var csv = DataProvider.GetCsvTable("stock-market.csv");

            //var year = DateTime.Now.Year - 1;
            var date = DateTime.Now; // new DateTime(year, 1, 1);
            date = date.AddDays(-csv.Rows.Count);

            for (var i = 0; i < csv.Rows.Count; i++)
            {
                var row = csv.Rows[i];
                if (i == 0)
                    continue; // skip csv header

                var prec = Rand.NextDouble();
                date = date.AddDays(1);
                //var date = new DateTime(year, month, day);
                var open = double.Parse(row[1]) + prec;
                var high = double.Parse(row[2]) + prec;
                var low = double.Parse(row[3]) + prec;
                var close = double.Parse(row[4]) + prec;
                var volume = long.Parse(row[5]);

                var item = new StockItem(open, low, high, close, volume, date);
                item.Index = i;
                Stocks.Add(item); 
            } 
            Stocks = Stocks.OrderBy(i => i.DateTime.Ticks).ToList();

        } 
        /// <summary> Gets or sets PropertyName </summary>
        public static List<StockItem> Stocks { get; set; }
    }

    public class StockRandomData : List<StockItem>
    {
        public StockRandomData(int total = 20, string name = "", bool skipWeekends = false)
        { 
            this.AddRange(StockDataGenerator.Populate(total, name, skipWeekends));
        }
    }

    public static class StockDataGenerator  
    {
        public static List<StockItem> Populate(int total = 20, string name = "", bool skipWeekends = false)
        {
            var data = new List<StockItem>();
            var now = DateTime.Now;  
            var start = new DateTime(now.Year, 1, 1, 10, 0, 0);
            var end = start.AddDays(total);
             
            var price = (double)Random.Next(100, 200);
            var volume = (long)Random.Next(10000, 20000);

            var i = 0;
            while (start.Ticks < end.Ticks)
            { 
                var item = new StockItem();
                item.Name = name;
                item.Index = i++;
                item.Volume = volume;
                item.DateTime = start; 
                item.Open = price;
                item.High = price + (Random.NextDouble() * 5);
                item.Low = price - (Random.NextDouble() * 5);
                item.Close = Random.Next((int)item.Low, (int)item.High);

                if (Random.NextDouble() < 0.4)
                {
                    price += (Random.NextDouble() * 5);
                    volume += (long)(Random.NextDouble() * 250);
                }
                else
                {
                    price -= (Random.NextDouble() * 5);
                    volume -= (long)(Random.NextDouble() * 250);
                }
                // skip non-trading hours (9:30 am - 4:00 pm) and weekends
                if (skipWeekends && 
                    start.DayOfWeek == DayOfWeek.Saturday &&
                    start.DayOfWeek == DayOfWeek.Sunday)
                {
                    // data.Add(item);
                    start = start.AddDays(1);
                }
                else //if (start.TimeOfDay < TimeSpan.FromHours(9.5) &&
                     //    start.TimeOfDay < TimeSpan.FromHours(16.0))
                { 
                    data.Add(item);

                    start = start.AddDays(1);
                } 
            }

            var items = data.OrderByDescending(item => item.Index).ToList();
            foreach (var item in items)
            {
                item.Index = i++;

                item.DateTime = start;
                start = start.AddDays(1);
            }

            data.AddRange(items);

            return data;

        }
        public static Random Random = new Random();
        public static double PriceMin = 100;
        public static double PriceMax = 500; 
    }

    [DataContract]
    public class StockItem
    {
        public StockItem()
        {
            Index = -1;
        }
        public StockItem(StockItem stockData)
        {
            this.Open = stockData.Open;
            this.High = stockData.High;
            this.Low = stockData.Low;
            this.Close = stockData.Close;
            this.Volume = stockData.Volume;
            this.DateTime = stockData.DateTime;

            Update();
        }
        public StockItem(double open, double low, double high, double close,
            long volume, DateTime date)
        {
            this.Open = open;
            this.High = high;
            this.Low = low;
            this.Close = close;
            this.Volume = volume;
            this.DateTime = date;

            Update();
        }

        #region Properties
        ///// <summary> Gets or sets Value </summary>
        //public double Value { get; set; }

        [DataMember(Name = "High")]
        public double High { get; set; }
        [DataMember(Name = "Low")]
        public double Low { get; set; }
        [DataMember(Name = "Close")]
        public double Close { get; set; }
        [DataMember(Name = "Open")]
        public double Open { get; set; }

        [DataMember(Name = "Dividend")]
        public double Dividend { get; set; }
        [DataMember(Name = "Split")]

        public double Split { get; set; }
        [DataMember(Name = "Volume")]
        public long Volume { get; set; }

        [DataMember(Name = "Date")]
        public string DateString { get; set; }

        public DateTime DateTime { get; set; }

        [DataMember(Name = "Symbol")]
        public string Symbol { get; set; }

        [IgnoreDataMember]
        public string Name { get; set; }
        [IgnoreDataMember]
        public int Index { get; set; }

        // TODO-MT implement % scale of OHLC values like Google finance chart does
        //public double ClosePercent { get { return ChangePercent; } }
        //public double OpenPercent { get { return (OpenChange / Open) * 100; } }
        //public double HighPercent { get { return (HighChange / Open) * 100; } }
        //public double LowPercent { get { return (LowChange / Open) * 100; } }
        //public double OpenPercent { get; set; }
        //public double HighChange { get { return (High - Open); } }
        //public double LowChange { get { return (Low - Open); } }
        //public double OpenChange { get { return (Open - Close); } }
        public double Change
        {
            get { return Close - Open; }
        }
        public double ChangePerCent
        {
            get { return (Change / Open) * 100; }
        }
        #endregion

        public void Update()
        {
            //this.Change = (Close - Open);
            //this.ChangePercent = (Change / Open) * 100;
            //this.Range = (High - Low);

            this.DateString = DateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public new string ToString()
        {
            var str = string.Format("{0:yyyy-MM-dd}", DateTime);

            if (Index != -1) str += " " + Index;
            if (!double.IsNaN(Open)) str += " " + Open;
            if (!double.IsNaN(Close)) str += " " + Close;
            if (!double.IsNaN(High)) str += " " + High;
            if (!double.IsNaN(Low)) str += " " + Low;
            if (!double.IsNaN(Volume)) str += " " + Volume;

            return str;
        }
        public StockItem Copy()
        {
            return new StockItem(this);
        }

    }
}