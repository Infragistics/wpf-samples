using System;
using System.Collections.Generic;
using System.Runtime.Serialization; 

namespace Infragistics.Framework
{
    public class StockPriceData : List<StockPrice>
    { 
        public StockPriceData()
        {
        }

        #region Ignored DataMembers
        [IgnoreDataMember]
        protected internal Random Rand = new Random();
        /// <summary> Gets or sets Price Range </summary>
        [IgnoreDataMember]
        public double PriceRange { get; set; }
        /// <summary> Gets or sets Volume Range </summary>
        [IgnoreDataMember]
        public double VolumeRange { get; set; }
        /// <summary> Gets or sets DateSpan between items </summary>
        [IgnoreDataMember]
        public TimeSpan DateSpan { get; set; }
        
        [IgnoreDataMember]
        public string Titile { get; set; }

        public StockPriceData(int itemsCount)
        {
            DateSpan = TimeSpan.FromDays(1);
            PriceRange = 5;
            VolumeRange = 1000;

            Generate(itemsCount);
        }
        public void Generate(int total = 100)
        {
            this.Clear();

            var close = 500.0;
            var volume = 10000.0;

            var date = DateTime.Now.AddDays(-total);

            for (var i = 0; i < total; i++)
            {
                var item = GenerateItem(close, volume, date);
                this.Add(item);

                close = item.Close;
                volume = item.Volume;
                date = item.Date;
            }
        }

        public StockPrice GenerateItem(double close, double volume, DateTime date)
        {
            double low, high, open;
            var mod = Rand.NextDouble() - 0.45;
            volume = volume + (mod * VolumeRange * 100);
            date = date.Add(DateSpan);

            low = close - (Rand.NextDouble() * PriceRange * 3);
            high = close + (Rand.NextDouble() * PriceRange * 3);
            open = close + (mod * PriceRange);
            close = open + (mod * PriceRange * 2);

            var item = new StockPrice();
            item.Volume = volume;
            item.Open = open;
            item.Close = close;
            item.High = high;
            item.Low = low;
            item.Volume = volume;
            item.Date = date;

            return item;
        }

        public StockPrice GenerateItem(StockPrice previous)
        {
            return GenerateItem(previous.Close, previous.Volume, previous.Date);
        } 
        #endregion
    }
    //public class StockPriceData : List<StockPrice>
    //{
    //}
    [DataContract]
    public class StockPrice
    {
        public StockPrice()
        {
            Split = double.NaN;
            Dividend = double.NaN;
            Volume = double.NaN;
            Close = double.NaN;
            Open = double.NaN;
            High = double.NaN;
            Low = double.NaN;
           
        }
        public StockPrice(StockPrice stock)
        {
            Symbol = stock.Symbol;
            Split = stock.Split;
            Dividend = stock.Dividend;
            Volume = stock.Volume;
            Close = stock.Close;
            Open = stock.Open;
            High = stock.High;
            Low = stock.Low;
        }

        [DataMember]
        public string Symbol { get; set; }
        [DataMember]
        public double Volume { get; set; }
        [DataMember]
        public double Open { get; set; }
        [DataMember]
        public double Close { get; set; }
        [DataMember]
        public double High { get; set; }
        [DataMember]
        public double Low { get; set; }
        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public double Dividend { get; set; }
        [DataMember]
        public double Split { get; set; }

        public override string ToString()
        {
            var str = string.Format("{0:yyyy-MM-dd}", Date);

            if (!double.IsNaN(Open)) str += " " + Open;
            if (!double.IsNaN(Close)) str += " " + Close;
            if (!double.IsNaN(High)) str += " " + High;
            if (!double.IsNaN(Low)) str += " " + Low;
            if (!double.IsNaN(Volume)) str += " " + Volume;

            return str;

        }
    }
}