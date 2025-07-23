using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Infragistics.Samples.Shared.Models
{
    public class QuoteCollection : ObservableCollection<Quote>
    {
        public QuoteCollection()
        {
        }

        public QuoteCollection(IEnumerable<Quote> source)
        {
            this.Append(source);
        }

        public void Append(IEnumerable<Quote> source)
        {
            foreach (Quote item in source)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Get the minium price from the collection of quotes.
        /// </summary>
        public double GetMinPrice()
        {
            return this.Min<Quote>(x => x.Price);
        }

        /// <summary>
        /// Get the maxium price from the collection of quotes.
        /// </summary>
        public double GetMaxPrice()
        {
            return this.Max<Quote>(x => x.Price);
        }
    }
}