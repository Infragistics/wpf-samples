namespace FinancialDataGrid.Services.Finance.Business
{
    public class FinancialDataItem
    {
        public string Category { get; set; }
        public string Type { get; set; }
        public double Spread { get; set; }
        public double OpenPrice { get; set; }
        public double Price { get; set; }
        public double Buy { get; set; }
        public double Sell { get; set; }
        public double Change { get; set; }
        public double ChangePercent { get; set; }
        public int Volume { get; set; }
        public double HighD { get; set; }
        public double LowD { get; set; }
        public double HighY { get; set; }
        public double LowY { get; set; }
        public double StartY { get; set; }
        public double ChangeOnYearPerecent { get; set; }
    }
}
