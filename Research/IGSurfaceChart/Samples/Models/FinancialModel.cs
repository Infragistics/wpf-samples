namespace IGSurfaceChart.Samples.Models
{
    public class FinancialModel
    {
        public FinancialModel() { }

        public FinancialModel(double days, double strike, double volatility)
        {
            this.Days = days;
            this.Strike = strike;
            this.Volatility = Volatility;
        }

        public double Days { get; set; }
        public double Strike { get; set; }
        public double Volatility { get; set; }
    }
}
