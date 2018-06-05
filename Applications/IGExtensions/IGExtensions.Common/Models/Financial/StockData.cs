using System.Runtime.Serialization;

namespace IGExtensions.Common.Models
{
    [DataContract]
    public class StockData
    {
        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "Change")]
        public string Change { get; set; }
        [DataMember(Name = "MarketCap")]
        public string MarketCap { get; set; }
        [DataMember(Name = "PE")]
        public string PE { get; set; }
        [DataMember(Name = "ROE")]
        public string ROE { get; set; }
        [DataMember(Name = "DivYield")]
        public string DivYield { get; set; }
        [DataMember(Name = "PriceToBook")]
        public string PriceToBook { get; set; }
        [DataMember(Name = "PriceToFreeCashFlow")]
        public string PriceToFreeCashFlow { get; set; }
        [DataMember(Name = "NetProfitMargin")]
        public string NetProfitMargin { get; set; }

        [IgnoreDataMember]
        public double ChangeValue { get; set; }
        [IgnoreDataMember]
        public double MarketCapValue { get; set; }
        [IgnoreDataMember]
        public double PEValue { get; set; }
        [IgnoreDataMember]
        public double ROEValue { get; set; }
        [IgnoreDataMember]
        public double DivYieldValue { get; set; }
        [IgnoreDataMember]
        public double PriceToBookValue { get; set; }
        [IgnoreDataMember]
        public double PriceToFreeCashFlowValue { get; set; }
        [IgnoreDataMember]
        public double NetProfitMarginValue { get; set; }

        [IgnoreDataMember]
        public int Index { get; set; }
    }
}
