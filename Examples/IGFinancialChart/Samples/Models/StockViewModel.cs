using Infragistics.Samples.Shared.Models; 
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IGFinancialChart.Samples 
{    
    /// <summary>
    /// Represents a view model for comparing values of multiple stocks
    /// </summary>
    public class StockViewModel : ObservableModel
    {  
        public StockViewModel()
        { 
            AllStocks = new List<StockSelection>();
            AllStocks.Add( new StockSelection { StockProvider.AMZN, StockProvider.TSLA });
            AllStocks.Add( new StockSelection { StockProvider.AMZN, StockProvider.GOOG });
            AllStocks.Add( new StockSelection { StockProvider.AMZN, StockProvider.MSFT });
            AllStocks.Add( new StockSelection { StockProvider.AMZN });
            AllStocks.Add( new StockSelection { StockProvider.GOOG, StockProvider.TSLA });
            AllStocks.Add( new StockSelection { StockProvider.GOOG, StockProvider.MSFT });
            AllStocks.Add( new StockSelection { StockProvider.GOOG });
            AllStocks.Add( new StockSelection { StockProvider.TSLA, StockProvider.MSFT });
            AllStocks.Add( new StockSelection { StockProvider.TSLA });
            AllStocks.Add( new StockSelection { StockProvider.MSFT }); 
                    
            SelectedStock = AllStocks[1];
        } 
         
        private StockSelection _SelectedStock;
        /// <summary> Gets or sets selected single stock or multiple stocks </summary>
        public StockSelection SelectedStock
        {
            get { return _SelectedStock; }
            set { if (_SelectedStock == value) return; _SelectedStock = value; OnPropertyChanged("SelectedStock"); }
        }

        private List<StockSelection> _AllStocks;
        /// <summary> Gets or sets a list of multiple stocks </summary>
        public List<StockSelection> AllStocks
        {
            get { return _AllStocks; }
            set { if (_AllStocks == value) return; _AllStocks = value; OnPropertyChanged("AllStocks"); }
        } 
    }

    public class StockSelection : List<StockData>
    {
        public string Symbols
        {
            get { return string.Join(" + ", this.Select(s => s.Title)); }
        }
    }
     
    
    public class StockDataTSLA : StockData
    {
        public StockDataTSLA()
        {
            foreach (var item in StockProvider.TSLA)
            {
                this.Add(item);
            }
        } 
    }
}
