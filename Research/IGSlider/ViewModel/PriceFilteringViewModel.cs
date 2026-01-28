using System.Collections.Generic;
using System.Linq;
using Infragistics.Samples.Shared.Models;

namespace IGSlider.ViewModel
{
    public class PriceFilteringViewModel : ObservableModel
    {

        public PriceFilteringViewModel()
        {
        }

        private IEnumerable<OrderHistory> orderHistory;
        public IEnumerable<OrderHistory> OrderHistory
        {
            get
            {
                return this.FilterData();
            }
            set
            {
                if (this.orderHistory != value)
                {
                    this.orderHistory = value;
                    this.OnPropertyChanged("OrderHistory");
                }
            }
        }

        private double startingRangeValue = double.MinValue;
        private double endingRangeValue = double.MaxValue;

        public void FilterData(double startingRangeValue, double endingRangeValue)
        {
            this.startingRangeValue = startingRangeValue;
            this.endingRangeValue = endingRangeValue;

            this.OnPropertyChanged("OrderHistory");
        }

        private IEnumerable<OrderHistory> FilterData()
        {
            IEnumerable<OrderHistory> filteredResult = new List<OrderHistory>();

            if (this.orderHistory != null)
            {
                filteredResult = (from o in this.orderHistory
                                  where o.Price >= this.startingRangeValue && o.Price <= this.endingRangeValue
                                  select o).ToList();
            }

            return filteredResult;
        }
    }
}
