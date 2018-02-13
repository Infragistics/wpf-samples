using System;
using System.ComponentModel;

namespace IGTrading.ViewModels
{
    /// <summary>
    /// Stores the types of transactions available
    /// </summary>
    public enum TransactionType
    {
        Buy = 0,
        Sell = 1
    }

    /// <summary>
    /// Stores a single stock transaction record
    /// </summary>
    public class StockTransactionViewModel : INotifyPropertyChanged
    {
        public StockPositionViewModel StockPosition { get; set; }

        private readonly DateTime _timestamp = DateTime.Now;
        public DateTime Timestamp
        {
            get { return _timestamp; }
        }

        #region Account
        /// <summary>
        /// local variable _Account
        /// </summary>
        private StockTradingAccount _account;

        /// <summary>
        /// Identifies the Account property.
        /// </summary>		
        public StockTradingAccount Account
        {
            get { return _account; }
            set
            {
                _account = value;
                OnPropertyChanged("Account");
            }
        }
        #endregion  //Account
        
        private StockTickerDetailsViewModel _stockTickerDetailsViewModel;
        public StockTickerDetailsViewModel StockTickerDetailsViewModel
        {
            get { return _stockTickerDetailsViewModel; }
            set 
            { 
                _stockTickerDetailsViewModel = value;
                _stockTickerDetailsViewModel.PropertyChanged += new PropertyChangedEventHandler(_stockTickerDetailsViewModel_PropertyChanged);
            }
        }

        void _stockTickerDetailsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("CurrentPricePerShare");
        }
        
        #region Symbol
        ///// <summary>
        ///// local variable _Symbol
        ///// </summary>
        //private string _symbol;

        /// <summary>
        /// Identifies the Symbol property.
        /// </summary>		
        public string Symbol
        {
            get { return this.StockTickerDetailsViewModel.Symbol; }
        }
        #endregion  //Symbol
        
        #region Quantity
        /// <summary>
        /// local variable _Quantity
        /// </summary>
        private int _quantity;

        /// <summary>
        /// Identifies the Quantity property.
        /// </summary>		
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        #endregion  //Quantity
        
        #region PricePerShare
        /// <summary>
        /// local variable _PricePerShare
        /// </summary>
        private double _pricePerShare;

        /// <summary>
        /// The final price at which the transaction was executed - filled in once the transaction is committed
        /// </summary>		
        public double PricePerShare
        {
            get { return _pricePerShare; }
            set { _pricePerShare = value; }
        }
        
        public double CurrentPricePerShare
        {
            get 
            {
                if (this.TransactionType == ViewModels.TransactionType.Sell)
                    return this.StockTickerDetailsViewModel.Bid; 
                else
                    return this.StockTickerDetailsViewModel.Ask;
            }
        }

        #endregion  //PricePerShare



        #region TransactionType
        /// <summary>
        /// local variable _TransactionType
        /// </summary>
        private TransactionType _transactionType;

        /// <summary>
        /// Identifies the TransactionType property.
        /// </summary>		
        public TransactionType TransactionType
        {
            get { return _transactionType; }
            set
            {
                _transactionType = value;
                OnPropertyChanged("TransactionType");
            }
        }
        #endregion  //TransactionType
        

        #region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Raises the property changed.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
        internal void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null) return;
			
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
    }
}
