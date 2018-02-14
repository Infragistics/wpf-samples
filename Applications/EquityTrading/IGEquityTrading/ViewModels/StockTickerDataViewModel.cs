using System;

namespace IGTrading.ViewModels
{
    public class StockTickerDataViewModel
    {
        public double Open { get; set; }
        public double Low { get; set; }
        public long Volume { get; set; }
        public double High { get; set; }
        public double Close { get; set; }
        public DateTime Date { get; set; }
    }
}
