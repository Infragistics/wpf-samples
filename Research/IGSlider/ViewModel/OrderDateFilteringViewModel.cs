using System;
using System.Collections.Generic;
using System.Linq;
using Infragistics.Samples.Shared.Models;

namespace IGSlider.ViewModel
{
    public class OrderDateFilteringViewModel : ObservableModel
    {

        public OrderDateFilteringViewModel()
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

        private DateTime startingRangeValue = DateTime.MinValue;
        private DateTime endingRangeValue = DateTime.MaxValue;

        public void FilterData(DateTime startingRangeValue, DateTime endingRangeValue)
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
                                  where o.OrderDate >= this.startingRangeValue && o.OrderDate <= this.endingRangeValue
                                  select o).ToList();
            }

            return filteredResult;
        }
    }
}
