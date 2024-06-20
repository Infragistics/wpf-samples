using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace IGDataGrid.Datasources
{
    public class StockPortfolioData : DependencyObject
    {
        private ObservableCollection<StockPortfolio> _collection;
        private DispatcherTimer _timer;
        private List<StockPortfolio> _list;
        private Hashtable _hiLoValues = new Hashtable(25);

        public StockPortfolioData(double changeIntervalMilliseconds)
        {
            this._list = new List<StockPortfolio>(25);
            this._collection = new ObservableCollection<StockPortfolio>(this._list);

            // Setup list of HiLo values for each stock so we can change their values within
            // a specific range.
            this._hiLoValues.Add("WFC", new HiLoValues(18, 30));
            this._hiLoValues.Add("BAC", new HiLoValues(8, 20));
            this._hiLoValues.Add("KEY", new HiLoValues(8, 20));
            this._hiLoValues.Add("FITB", new HiLoValues(2, 11));
            this._hiLoValues.Add("JPM", new HiLoValues(3, 10));

            this._hiLoValues.Add("BA", new HiLoValues(35, 55));
            this._hiLoValues.Add("NOC", new HiLoValues(38, 49));
            this._hiLoValues.Add("GD", new HiLoValues(42, 59));
            this._hiLoValues.Add("LMT", new HiLoValues(73, 91));
            this._hiLoValues.Add("FRPT", new HiLoValues(3, 10));

            this._hiLoValues.Add("KO", new HiLoValues(35, 55));
            this._hiLoValues.Add("PEP", new HiLoValues(38, 49));
            this._hiLoValues.Add("CCE", new HiLoValues(42, 59));
            this._hiLoValues.Add("DPS", new HiLoValues(73, 91));
            this._hiLoValues.Add("STZ", new HiLoValues(3, 10));

            this._hiLoValues.Add("MSFT", new HiLoValues(18, 31));
            this._hiLoValues.Add("ORCL", new HiLoValues(13, 26));
            this._hiLoValues.Add("CSCO", new HiLoValues(17, 28));
            this._hiLoValues.Add("DELL", new HiLoValues(8, 15));
            this._hiLoValues.Add("NVDA", new HiLoValues(7, 13));

            this._hiLoValues.Add("NWL", new HiLoValues(6, 13));
            this._hiLoValues.Add("KMB", new HiLoValues(48, 59));
            this._hiLoValues.Add("AM", new HiLoValues(4, 11));
            this._hiLoValues.Add("ENR", new HiLoValues(47, 57));
            this._hiLoValues.Add("JAH", new HiLoValues(11, 21));

            // 'Banks' Porfolio
            this._collection.Add(new StockPortfolio("Banks",
                                                    "WFC", this.GetNextPrice("WFC"),
                                                    "BAC", this.GetNextPrice("BAC"),
                                                    "KEY", this.GetNextPrice("KEY"),
                                                    "FITB", this.GetNextPrice("FITB"),
                                                    "JPM", this.GetNextPrice("JPM")));

            // 'Aerospace' Porfolio
            this._collection.Add(new StockPortfolio("Aerospace",
                                                    "BA", this.GetNextPrice("BA"),
                                                    "NOC", this.GetNextPrice("NOC"),
                                                    "GD", this.GetNextPrice("GD"),
                                                    "LMT", this.GetNextPrice("LMT"),
                                                    "FRPT", this.GetNextPrice("FRPT")));

            // 'Beverages' Porfolio
            this._collection.Add(new StockPortfolio("Beverages",
                                                    "KO", this.GetNextPrice("KO"),
                                                    "PEP", this.GetNextPrice("PEP"),
                                                    "CCE", this.GetNextPrice("CCE"),
                                                    "DPS", this.GetNextPrice("DPS"),
                                                    "STZ", this.GetNextPrice("STZ")));

            // 'Computer' Porfolio
            this._collection.Add(new StockPortfolio("Computer",
                                                    "MSFT", this.GetNextPrice("MSFT"),
                                                    "ORCL", this.GetNextPrice("ORCL"),
                                                    "CSCO", this.GetNextPrice("CSCO"),
                                                    "DELL", this.GetNextPrice("DELL"),
                                                    "NVDA", this.GetNextPrice("NVDA")));

            // 'ComsumerProducts' Porfolio
            this._collection.Add(new StockPortfolio("Consumer products",
                                                    "NWL", this.GetNextPrice("NWL"),
                                                    "KMB", this.GetNextPrice("KMB"),
                                                    "AM", this.GetNextPrice("AM"),
                                                    "ENR", this.GetNextPrice("ENR"),
                                                    "JAH", this.GetNextPrice("JAH")));

            this._timer = new DispatcherTimer(DispatcherPriority.Background, this.Dispatcher);
            this._timer.Tick += new EventHandler(_timer_Tick);
            this._timer.Interval = TimeSpan.FromMilliseconds(changeIntervalMilliseconds);
        }

        #region Properties

        public ObservableCollection<StockPortfolio> Data
        {
            get { return this._collection; }
        }

        public double UpdateInterval
        {
            get { return this._timer.Interval.Milliseconds; }
            set { this._timer.Interval = TimeSpan.FromMilliseconds(value); }
        }

        public void StartUpdatingData()
        {
            if (this._timer.IsEnabled == false)
                this._timer.IsEnabled = true;
        }

        public void StopUpdatingData()
        {
            if (this._timer.IsEnabled == true)
                this._timer.IsEnabled = false;
        }

        #endregion //Properties

        void _timer_Tick(object sender, EventArgs e)
        {
            foreach (StockPortfolio stockPortfolio in this._collection)
            {
                stockPortfolio.Stock0Price = this.GetNextPrice(stockPortfolio.Stock0);
                stockPortfolio.Stock1Price = this.GetNextPrice(stockPortfolio.Stock1);
                stockPortfolio.Stock2Price = this.GetNextPrice(stockPortfolio.Stock2);
                stockPortfolio.Stock3Price = this.GetNextPrice(stockPortfolio.Stock3);
                stockPortfolio.Stock4Price = this.GetNextPrice(stockPortfolio.Stock4);
            }
        }

        #region GetNextPrice

        private Random _intRnd;
        private Random IntRnd
        {
            get
            {
                if (this._intRnd == null)
                    this._intRnd = new Random(Environment.TickCount);

                return this._intRnd;
            }
        }
        private double GetNextPrice(string stock)
        {
            HiLoValues hiLoValues = this._hiLoValues[stock] as HiLoValues;

            int min = hiLoValues.LoValue * 100;
            int max = hiLoValues.HiValue * 100;

            int nextInt = this.IntRnd.Next(min, max);
            return (double)nextInt / 100.0d;
        }

        #endregion //GetNextPrice
    }

    internal class HiLoValues
    {
        internal HiLoValues(int loValue, int hiValue)
        {
            this.LoValue = loValue;
            this.HiValue = hiValue;
        }

        internal int LoValue { get; set; }
        internal int HiValue { get; set; }
    }

    public class StockPortfolio : INotifyPropertyChanged
    {
        #region Member Variables

        private double _stock0Price;
        private double _stock1Price;
        private double _stock2Price;
        private double _stock3Price;
        private double _stock4Price;

        #endregion //Member Variables

        #region Constructor

        public StockPortfolio(string portfolioName,
                              string stock0, double stock0Price,
                              string stock1, double stock1Price,
                              string stock2, double stock2Price,
                              string stock3, double stock3Price,
                              string stock4, double stock4Price)
        {
            this.PortfolioName = portfolioName;

            this.Stock0 = stock0;
            this.Stock0Price = stock0Price;
            this.Stock1 = stock1;
            this.Stock1Price = stock1Price;
            this.Stock2 = stock2;
            this.Stock2Price = stock2Price;
            this.Stock3 = stock3;
            this.Stock3Price = stock3Price;
            this.Stock4 = stock4;
            this.Stock4Price = stock4Price;
        }

        #endregion //Constructor

        #region Properties

        public string PortfolioName { get; set; }
        public string Stock0 { get; set; }
        public string Stock1 { get; set; }
        public string Stock2 { get; set; }
        public string Stock3 { get; set; }
        public string Stock4 { get; set; }

        public double Stock0Price
        {
            get { return this._stock0Price; }
            set
            {
                this._stock0Price = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Stock0Price"));
            }
        }

        public double Stock1Price
        {
            get { return this._stock1Price; }
            set
            {
                this._stock1Price = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Stock1Price"));
            }
        }

        public double Stock2Price
        {
            get { return this._stock2Price; }
            set
            {
                this._stock2Price = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Stock2Price"));
            }
        }

        public double Stock3Price
        {
            get { return this._stock3Price; }
            set
            {
                this._stock3Price = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Stock3Price"));
            }
        }

        public double Stock4Price
        {
            get { return this._stock4Price; }
            set
            {
                this._stock4Price = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Stock4Price"));
            }
        }

        #endregion //Properties

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
