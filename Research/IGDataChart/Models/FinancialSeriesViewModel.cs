using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Models
{
    public class FinancialSeriesViewModel : ObservableModel
    {

        public FinancialSeriesViewModel()
        {
            _selecetedSeriesIndex = 0;
            _series = new FinancialSeriesCollection();
            _data = new StockMarketDataCollection();
        }
        /// <summary>
        /// Collection of stock market data points
        /// </summary>
        public StockMarketDataCollection Data
        {
            get { return _data; }
            set
            {
                _data = value;
                this.OnPropertyChanged("Data");
            }
        }
        private StockMarketDataCollection _data;

        /// <summary>
        /// Collection of FinancialSeriesModel objects
        /// </summary>
        public FinancialSeriesCollection Series
        {
            get { return _series; }
            set
            {
                _series = (FinancialSeriesCollection)value;
                this.OnPropertyChanged("Series");
            }
        }
        private int _selecetedSeriesIndex;
        /// <summary>
        /// Get the previous sample index in Samples collection
        /// </summary>
        /// <returns></returns>
        public int GetPreviousSeriesIndex()
        {
            if (_selecetedSeriesIndex == 0)
            {
                _selecetedSeriesIndex = this.Series.Count - 1;
            }
            else
            {
                _selecetedSeriesIndex -= 1;
            }
            return _selecetedSeriesIndex;
        }
        /// <summary>
        /// Get the next sample index in Samples collection
        /// </summary>
        /// <returns></returns>
        public int GetNextSampleIndex()
        {
            _selecetedSeriesIndex = (_selecetedSeriesIndex + 1) % this.Series.Count;
            return _selecetedSeriesIndex;
        }

        private FinancialSeriesCollection _series;
       
    }
}