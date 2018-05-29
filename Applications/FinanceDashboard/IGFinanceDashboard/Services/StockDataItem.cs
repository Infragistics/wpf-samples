using System; 
using System.Runtime.Serialization; 

namespace IGShowcase.FinanceDashboard
{ 
    [DataContract]
    public class StockDataItem
    {
        public StockDataItem()
        {
            Split = double.NaN;
            Dividend = double.NaN;
            Volume = double.NaN;
            Close = double.NaN;
            Open = double.NaN;
            High = double.NaN;
            Low = double.NaN; 
        }
        public StockDataItem(StockDataItem stock)
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