using System;
using System.Windows;
using Infragistics.Samples.Shared.Tools;

namespace Infragistics.Samples.Shared.Models
{
    public class Stock : ObservableModel
    {
        private Quote _currentBuyQuote = null;
        private Quote _currentSellQuote = null;

        public Stock()
        {

        }

        public string Symbol { get; set; }

        private DateTime _lastMatchTime = default(DateTime);
        public string Time
        {
            get
            {
                string time = string.Empty;

                if (!_lastMatchTime.Equals(default(DateTime)))
                {
                    time = String.Format("{0:T}", _lastMatchTime);
                }

                return time;
            }
        }

        private QuoteCollection _buyOrders = new QuoteCollection();
        public QuoteCollection BuyOrders
        {
            get
            {
                return _buyOrders;
            }
            set
            {
                _buyOrders = value;
            }
        }

        private QuoteCollection _sellOrders = new QuoteCollection();
        public QuoteCollection SellOrders
        {
            get
            {
                return _sellOrders;
            }
            set
            {
                _sellOrders = value;
            }
        }

        public string FormatedPrice
        {
            get
            {
                return CurrencyHelper.FormatToUsd(_price);
            }
        }

        private double _price = 0d;
        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    this.OnPropertyChanged("Price");
                    this.OnPropertyChanged("FormatedPrice");
                }
            }
        }

        private double _closedPrice = 0d;
        public double ClosedPrice
        {
            get
            {
                return _closedPrice;
            }
            set
            {
                if (_closedPrice != value)
                {
                    _closedPrice = value;
                    this.OnPropertyChanged("ClosedPrice");
                }
            }
        }

        public string PercentChange
        {
            get
            {
                return String.Format("{0:P2}",
                    Math.Abs(this.CalculatePercent(_currentBuyQuote.Price)));
            }
        }

        public Visibility PositiveChangeVisibility
        {
            get
            {
                if (this.IsPositivePercentChange())
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility NegativeChangeVisibility
        {
            get
            {
                if (!this.IsPositivePercentChange())
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        private bool IsPositivePercentChange()
        {
            return _currentBuyQuote.Price >= _closedPrice;
        }

        private long _orders = 0;
        public long Orders
        {
            get
            {
                return _orders;
            }
            set
            {
                if (_orders != value)
                {
                    _orders = value;
                    this.OnPropertyChanged("Orders");
                }
            }
        }

        public string FormatedVolume
        {
            get
            {
                return CurrencyHelper.FormatToUsd(_volume);
            }
        }

        private double _volume = 0;
        public double Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    this.OnPropertyChanged("Volume");
                }
            }
        }

        /// <summary>
        /// Initialize the stock
        /// </summary>
        public void Initialize()
        {
            if (_buyOrders != null && _buyOrders.Count > 0)
            {
                _currentBuyQuote = _buyOrders[0];
                _currentSellQuote = _sellOrders[0];
            }
        }

        public void AddQuote(Quote newQuote)
        {
            if (this.VerifyPriceRange(newQuote))
            {
                this.UpdateQuoteCache(newQuote);
                this.SetCurrentQuote(newQuote);

                if (_currentBuyQuote.ComparePrice(_currentSellQuote))
                {
                    this.PurchaseStock(newQuote);
                }

                this.OnPropertyChanged("BuyOrders");
                this.OnPropertyChanged("SellOrders");
            }
        }

        /// <summary>
        /// Check to see if the price matches the last matched price.
        /// </summary>
        /// <remarks>This is used for condtional formating</remarks>
        public bool MatchesLastBuyPrice(double price)
        {
            return price >= this.Price && price <= (this.Price + .10d);
        }

        /// <summary>
        /// Veirfy the generate quote does not extend the simulaiton percent changed range.
        /// </summary>
        private bool VerifyPriceRange(Quote newQuote)
        {
            bool isValidValue = false;

            double percentChanged = this.CalculatePercent(newQuote.Price);
            isValidValue = (percentChanged < .051d) && (percentChanged > -.051d);
            return isValidValue;
        }

        /// <summary>
        /// Updated the Quote Cache with the generated quote value
        /// </summary>
        /// <param name="newQuote"></param>
        /// <remarks>Will update # of shares for existing quotes or create a new cached quote.</remarks>
        private void UpdateQuoteCache(Quote newQuote)
        {
            bool isNewQuote = true;

            // Check to see if new quote is already cached.
            QuoteCollection currentQuotes = this.GetQuoteCollection(newQuote.QuoteType);
            foreach (Quote item in currentQuotes)
            {
                if (Math.Round(item.Price, 2) == Math.Round(newQuote.Price, 2))
                {
                    item.AddShares(newQuote.Shares);
                    isNewQuote = false;
                }
                else
                {
                    // This is used to force the grid to rebound all the quotes
                    item.Shares = item.Shares + 10;
                    item.Price = item.Price + .00001d;
                }
            }

            // Add new quote to cache
            if (isNewQuote)
            {
                currentQuotes.Insert(0, newQuote);

                // Always keep only 5 current quotes in the cache
                if (currentQuotes.Count > 5)
                {
                    currentQuotes.RemoveAt(5);
                }
            }
        }

        /// <summary>
        /// Set the current buy or sell stock qoute
        /// </summary>
        private void SetCurrentQuote(Quote newQuote)
        {
            if (newQuote.QuoteType == QuoteType.Buy)
            {
                _currentBuyQuote = newQuote;
            }
            else
            {
                _currentSellQuote = newQuote;
            }
        }

        /// <summary>
        /// Purchase a stock. An update the quote cache accordingly.
        /// </summary>
        private void PurchaseStock(Quote newQuote)
        {
            _lastMatchTime = DateTime.Now;

            this.Price = newQuote.Price;
            this.Orders += _currentBuyQuote.Shares;
            this.Volume += _currentBuyQuote.CalculateVolume();

            _currentSellQuote.RemoveShares(_currentBuyQuote.Shares);
            _currentBuyQuote.ResetShares();

            this.SendNotification();
        }

        /// <summary>
        /// Send Data Binding Notifications
        /// </summary>
        private void SendNotification()
        {
            this.OnPropertyChanged("LastMatchedPrice");
            this.OnPropertyChanged("Price");
            this.OnPropertyChanged("Time");
            this.OnPropertyChanged("NegativeChangeVisibility");
            this.OnPropertyChanged("PositiveChangeVisibility");
            this.OnPropertyChanged("PercentChange");
        }

        /// <summary>
        /// Calculate the Percent of change
        /// </summary>
        private double CalculatePercent(double price)
        {
            double priceDifferent = price - _closedPrice;
            return ((priceDifferent * 100) / _closedPrice) * .01;
        }

        /// <summary>
        /// Get the Quote Collection to update
        /// </summary>
        private QuoteCollection GetQuoteCollection(QuoteType quoteType)
        {
            QuoteCollection result;

            if (quoteType == QuoteType.Buy)
            {
                result = _buyOrders;
            }
            else
            {
                result = _sellOrders;
            }

            return result;
        }

    }
}