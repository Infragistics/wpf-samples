
using System.Collections.Generic;

namespace IGShowcase.FinanceDashboard
{
	public class StockSearchItem
	{
        public StockSearchItem()
        {
            IPO = 1900;
        }
		#region Public Properties
		public string Symbol { get; set; }
		public string Name { get; set; }
        public string NameShort { get; set; }
        public string Exchange { get; set; }
        public int IPO { get; set; }
        public string MarketCap { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }

		#endregion Public Properties

		#region Public Methods 
		public override string ToString()
		{
			return Symbol;
//			return IPO + " " + Exchange + " " + Symbol + " " + Sector + " " + Industry;
		}
		#endregion Public Methods
	}

    public class StockSearchDictionary : Dictionary<string, StockSearchItem>
    {

    }
}
