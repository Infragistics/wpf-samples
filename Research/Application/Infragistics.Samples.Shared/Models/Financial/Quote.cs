using Infragistics.Samples.Shared.Tools;

namespace Infragistics.Samples.Shared.Models
{
    public class Quote : ObservableModel
    {

        public Quote()
        {
        }

        public QuoteType QuoteType { get; set; }

        private double _price = 0d;
        public string FormatedPrice
        {
            get
            {
                return CurrencyHelper.FormatToUsd(_price);
            }
        }

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

        private int _shares = 0;
        public int Shares
        {
            get
            {
                return _shares;
            }
            set
            {
                if (_shares != value)
                {
                    _shares = value;
                    this.OnPropertyChanged("Shares");
                }
            }
        }

        /// <summary>
        /// Calculate the volume for the quote being bought.
        /// </summary>
        /// <returns></returns>
        public double CalculateVolume()
        {
            return _price * _shares;
        }

        /// <summary>
        /// Compare the price between two quotes.
        /// </summary>
        public bool ComparePrice(Quote quote)
        {
            return _price >= quote.Price - 10d;
        }

        /// <summary>
        /// Add shares to the quote
        /// </summary>
        public void AddShares(int numberOfShares)
        {
            this.Shares += numberOfShares;
        }

        /// <summary>
        /// Remove shares from the quote. This happens when a sell occurs.
        /// </summary>
        public void RemoveShares(int numberOfShares)
        {
            if (this.Shares > numberOfShares)
            {
                this.Shares -= numberOfShares;
            }
            else
            {
                this.ResetShares();
            }
        }

        /// <summary>
        /// Reset shares to make simulation more realistic.
        /// </summary>
        public void ResetShares()
        {
            this.Shares = 100;
        }

    }
    public enum QuoteType
    {
        Buy = 0,
        Sell = 1
    }
    
}