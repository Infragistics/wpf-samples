using System;
using Infragistics.Samples.Shared.Tools;

namespace Infragistics.Samples.Shared.Models
{
    public class QuoteSimulator
    {
        private readonly double[] _offSetValues = { 0, 0, 0, .10, .10, .10, -.10, -.10, -.10, .20, .20, .20, -.20, -.20, -.20 };

        public QuoteSimulator()
        {
        }

        /// <summary>
        /// Create a stock with default quote values.
        /// </summary>
        public Stock CreateInitialStockState(string symbol, double closePrice)
        {
            Stock simStock = new Stock { Symbol = symbol, Price = closePrice, ClosedPrice = closePrice };

            simStock.BuyOrders = this.GenerateDefaultValues(closePrice, 100, 5, false);
            simStock.SellOrders = this.GenerateDefaultValues(closePrice, 100, 5, true);
            simStock.Initialize();

            return simStock;
        }

        /// <summary>
        /// Get a new quote.
        /// </summary>
        public Quote GenerateNewQuote(double price, double minSellPrice, double maxBuyPrice)
        {

            int buyOrSellQuote = RandomizeUtility.GenerateRandomNumber(0, 2);
            QuoteType quoteType = (QuoteType)Enum.ToObject(typeof(QuoteType), buyOrSellQuote);

            Quote newQuote = null;
            if (quoteType == QuoteType.Buy)
            {
                newQuote = this.GetNewBuyQuote(price, maxBuyPrice, minSellPrice);
            }
            else
            {
                newQuote = this.GetNewSellQuote(price, maxBuyPrice, minSellPrice);
            }

            return newQuote;
        }

        /// <summary>
        /// Get a new Buy Quote
        /// </summary>
        private Quote GetNewBuyQuote(double currentPrice, double minPriceRange, double MaxPriceRange)
        {
            double price = this.GenerateNewPrice(currentPrice);

            if (price > MaxPriceRange)
            {
                price = minPriceRange - .10d;
            }

            return this.CreateQuote(price, QuoteType.Buy);
        }

        /// <summary>
        /// Get a new Sell Quote
        /// </summary>
        private Quote GetNewSellQuote(double currentPrice, double minPriceRange, double maxPriceRange)
        {
            double price = this.GenerateNewPrice(currentPrice);

            if (price < minPriceRange)
            {
                price = maxPriceRange + .10d;
            }

            return this.CreateQuote(price, QuoteType.Sell);
        }

        /// <summary>
        /// Generate a new quote price.
        /// </summary>
        private double GenerateNewPrice(double currentPrice)
        {
            int offSetkey = RandomizeUtility.GenerateRandomNumber(0, 14);
            double offSetValue = _offSetValues[offSetkey];
            return currentPrice + offSetValue;
        }

        /// <summary>
        /// Create a new quote based on the generated price.
        /// </summary>
        private Quote CreateQuote(double price, QuoteType quoteType)
        {
            return new Quote
            {
                QuoteType = quoteType,
                Price = System.Math.Round(price, 2),
                Shares = RandomizeUtility.GenerateRandomNumber(100, 250)
            };
        }

        /// <summary>
        /// Get offset value (used for default quote generation)
        /// </summary>
        private double GetOffSetValue(bool increaseValue)
        {
            double priceOffSet = 0;

            if (increaseValue)
            {
                priceOffSet = .10;
            }
            else
            {
                priceOffSet = -.10;
            }

            return priceOffSet;
        }

        /// <summary>
        /// Generate a collection of default quotes
        /// </summary>
        /// <param name="price">Seed Price</param>
        /// <param name="numberOfShares"># of Shares</param>
        /// <param name="numberofDefaultItems"># of Items</param>
        /// <param name="increaseValue">Increase or decrease seed value</param>
        /// <returns>Default collection of quotes</returns>
        private QuoteCollection GenerateDefaultValues(double price, int numberOfShares, int numberofDefaultItems, bool increaseValue)
        {

            QuoteCollection simQuotes = new QuoteCollection();

            double currentOffSetValue = 0;
            double offSetPrice = this.GetOffSetValue(increaseValue);

            for (int i = 0; i < numberofDefaultItems; i++)
            {
                currentOffSetValue += offSetPrice;
                simQuotes.Add(new Quote { Price = price + currentOffSetValue, Shares = numberOfShares });
            }

            return simQuotes;
        }

    }

}