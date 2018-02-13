
namespace IGTrading.ViewModels
{
    public class StockTradingAccount
    {
        public string ID { get; set; }
        public double Balance { get; set; }
        public double DailyProfitLoss { get; private set; }
        public double GrossProfitLoss { get; private set;  }

        public override string ToString()
        {
            return ID;
        }
    }
}
