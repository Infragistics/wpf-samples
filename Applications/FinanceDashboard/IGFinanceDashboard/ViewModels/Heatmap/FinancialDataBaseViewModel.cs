using System.ComponentModel;
using IGExtensions.Common.Models;

namespace IGShowcase.FinanceDashboard.ViewModels
{
    public class StockDataViewModel : StockData, INotifyPropertyChanged
    {
        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public StockDataViewModel()
        { }

        /// <summary>
        /// Creates a FinancialDataBaseViewModel instance from a FinancialData instance.
        /// </summary>
        /// <param name="data">The FinancialData instance.</param>
        public StockDataViewModel(StockData data)
        {
            SetFinancialData(data);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the financial data fields.
        /// </summary>
        public void SetFinancialData(StockData data)
        {
            Description = data.Description;

            Change = data.Change;
            MarketCap = data.MarketCap;
            PE = data.PE;
            ROE = data.ROE;
            DivYield = data.DivYield;
            PriceToBook = data.PriceToBook;
            PriceToFreeCashFlow = data.PriceToFreeCashFlow;
            NetProfitMargin = data.NetProfitMargin;

            string marketCap = MarketCap;

            marketCap = marketCap.Replace(".", string.Empty);
            marketCap = marketCap.Replace("M", "0000");
            marketCap = marketCap.Replace("B", "0000000");

            ChangeValue = Parse(Change);
            MarketCapValue = Parse(marketCap);
            PEValue = Parse(PE);
            ROEValue = Parse(ROE);
            DivYieldValue = Parse(DivYield);
            PriceToBookValue = Parse(PriceToBook);
            PriceToFreeCashFlowValue = Parse(PriceToFreeCashFlow);
            NetProfitMarginValue = Parse(NetProfitMargin);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Parses a string to a double without crashing.
        /// </summary>
        /// <param name="stringValue">The string.</param>
        /// <returns>The parsed double.</returns>
        private double Parse(string stringValue)
        {
            double value = 0;
            double.TryParse(stringValue, out value);
            return value;
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
