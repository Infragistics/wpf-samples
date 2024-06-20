using System;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Models
{
    public class RealTimeViewModel : ObservableModel
    {
        private StockCollection _stocks;
        private QuoteSimulator _simulator = null;

        public RealTimeViewModel()
        {
            this.LoadStocks();
        }

        private void LoadStocks()
        {
            _simulator = new QuoteSimulator();

            _stocks = new StockCollection();
            _stocks.Add(_simulator.CreateInitialStockState("Msft", 25d));
            this.SelectedStock = _stocks[0];
        }

        private Stock _selectedStock;
        public Stock SelectedStock
        {
            get
            {
                return _selectedStock;
            }
            set
            {
                if (_selectedStock != value)
                {
                    _selectedStock = value;
                    this.OnPropertyChanged("SelectedStock");
                }
            }
        }

        public string Time
        {
            get
            {
                DateTime dt = DateTime.Now;
                return String.Format("{0:ddd, MMM d, yyyy  HH:mm:ss}", dt);
            }
        }

        public void UpdateDataSource()
        {

            if (_selectedStock != null)
            {
                _selectedStock.AddQuote(_simulator.GenerateNewQuote(
                    _selectedStock.Price,
                    _selectedStock.SellOrders.GetMinPrice(),
                    _selectedStock.BuyOrders.GetMaxPrice()));
            }

            // Trigger Databinding notifcation
            this.OnPropertyChanged("SelectedStock");
            this.OnPropertyChanged("Time");
        }

    }
}