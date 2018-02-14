
namespace IGShowcase.FinanceDashboard.ViewModels
{
	public class StockSearchItemViewModel
	{
		#region Public Properties
		public string Symbol { get; set; }
		public string Name { get; set; }
        public string NameShort { get; set; }
        public string Exchange { get; set; }
		public StockViewModel Parent { get; set; }
		#endregion Public Properties

		#region Public Methods
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return Symbol;
		}
		#endregion Public Methods
	}
}
