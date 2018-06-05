using System.Runtime.Serialization;

namespace IGTrading.Models
{
    public class FinancialData
    {
        [DataMember(Name = "Description")]
        public string Description { get; set; }

        public string Change { get; set; }
        public string MarketCap { get; set; }
        public string PE { get; set; }
        public string ROE { get; set; }
        public string DivYield { get; set; }
        public string PriceToBook { get; set; }
        public string PriceToFreeCashFlow { get; set; }
        public string NetProfitMargin { get; set; }
    }
}
