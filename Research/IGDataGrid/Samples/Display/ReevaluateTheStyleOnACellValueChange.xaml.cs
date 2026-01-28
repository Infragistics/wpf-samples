using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using IGDataGrid.Models;
using Infragistics.Samples.Framework;
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

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for ReevaluateTheStyleOnACellValueChange.xaml
    /// </summary>
    public partial class ReevaluateTheStyleOnACellValueChange : SampleContainer
    {
        private DispatcherTimer _timer;
        private RealTimeViewModel _viewModel;

        public ReevaluateTheStyleOnACellValueChange()
        {
            InitializeComponent();

            this._viewModel = new RealTimeViewModel();
            this.DataContext = this._viewModel;

            this._timer = new DispatcherTimer();
            this._timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            this._timer.Tick += new EventHandler(timer_Tick);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this._timer.Start();

            this.dataGridBuy.DefaultFieldLayout.Fields["Shares"].Settings.CellValuePresenterStyleSelector = new CVPStyleSelector_SharesField(this);
            this.dataGridSell.DefaultFieldLayout.Fields["Shares"].Settings.CellValuePresenterStyleSelector = new CVPStyleSelector_SharesField(this);

            this.dataGridBuy.DefaultFieldLayout.Fields["Price"].Settings.CellValuePresenterStyleSelector = new CVPStyleSelector_PriceField(this);
            this.dataGridSell.DefaultFieldLayout.Fields["Price"].Settings.CellValuePresenterStyleSelector = new CVPStyleSelector_PriceField(this);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this._viewModel.UpdateDataSource();
        }
    }

    internal class StyleSelectorBase : StyleSelector
    {
        protected Page _page;
        public StyleSelectorBase(Page page)
        {
            _page = page;
        }
    }

    internal class CVPStyleSelector_SharesField : StyleSelectorBase
    {
        internal CVPStyleSelector_SharesField(Page page) : base(page) { }
        public override System.Windows.Style SelectStyle(object item, DependencyObject container)
        {
            int shares = (int)item;

            if (shares >= 1000)
            {
                return _page.Resources["CVP_Field_Green"] as System.Windows.Style;
            }
            else if (shares < 300)
            {
                return _page.Resources["CVP_Field_Red"] as System.Windows.Style;
            }
            else
            {
                return _page.Resources["CVP_Field_Normal"] as System.Windows.Style;
            }
        }
    }
    internal class CVPStyleSelector_PriceField : StyleSelectorBase
    {
        internal CVPStyleSelector_PriceField(Page page) : base(page) { }
        public override System.Windows.Style SelectStyle(object item, DependencyObject container)
        {
            double price = (double)item;

            if (price > 26.00)
            {
                return _page.Resources["CVP_Field_Green"] as System.Windows.Style;
            }
            else if (price < 25.00)
            {
                return _page.Resources["CVP_Field_Red"] as System.Windows.Style;
            }
            else
            {
                return _page.Resources["CVP_Field_Normal"] as System.Windows.Style;
            }
        }
    }
}
