using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IGTrading.Commands;
using IGTrading.Controls;
using IGTrading.ViewModels;
using Infragistics.Controls.Layouts;

namespace IGTrading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region fields

        string layoutString = String.Empty;
        private StockViewModel _vm;
        private ICommand _sellCommand;
        private ICommand _buyCommand;

        #endregion fields

        #region ctor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion ctor

        #region EventHandlers

        #region Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lets grab the data context
            _vm = DataContext as StockViewModel;

            if (_vm == null)
            {
                throw new NullReferenceException("DataContext must be of type StockViewModel");
            }

            // Add some default data
            _vm.AddSymbolToWatchList("MSFT");
            _vm.SelectStockCommand.Execute("MSFT");
        }

        #endregion Loaded

        #region saveLayoutBtn_Click

        private void saveLayoutBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var storageFileStream = new IsolatedStorageFileStream(
             "IGTradingLayout.xml",
             FileMode.Create,
             IsolatedStorageFile.GetStore(
                 IsolatedStorageScope.Assembly
                 | IsolatedStorageScope.Domain
                 | IsolatedStorageScope.User,
                 null,
                 null)))
            {
              //TODO:  this.TileManager.SaveLayout(storageFileStream);
            }
        }

        #endregion saveLayoutBtn_Click

        #region xamTilesControl1_Loaded

        private void xamTilesControl1_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //using (var storageFileStream = new IsolatedStorageFileStream(
                //            "IGTradingLayout.xml",
                //            FileMode.Open,
                //            IsolatedStorageFile.GetStore(
                //                IsolatedStorageScope.Assembly
                //                | IsolatedStorageScope.Domain
                //                | IsolatedStorageScope.User,
                //                null,
                //                null)))
                //{
                //    //TODO:  this.TileManager.LoadLayout(storageFileStream);
                //}
            }
            catch (FileNotFoundException)
            {
            }
        }

        #endregion xamTilesControl1_Loaded

        #region ContentStockTransactionCompleted

        void ContentStockTransactionCompleted(object sender, EventArgs e)
        {
            var stockTransaction = BuySellDialogWindow.DataContext as StockTransactionViewModel;
            if (stockTransaction == null)
                return;

            if
            (
                stockTransaction.Quantity == 0
                || stockTransaction.CurrentPricePerShare == 0
                //prevent the user from selling more units than they currently have
                || (stockTransaction.TransactionType == TransactionType.Sell && stockTransaction.Quantity > stockTransaction.StockPosition.Quantity)
            )
                return;

            var content = BuySellDialogWindow.Content as BuySellDialogContent;
            if (content == null)
                return;

            if (stockTransaction.TransactionType == TransactionType.Buy)
                this._vm.BuyStock(stockTransaction);
            else this._vm.SellStock(stockTransaction);

            BuySellDialogWindow.Close();
        }

        #endregion ContentStockTransactionCompleted

        #endregion EventHandlers

        #region Commands

        #region SellCommand

        /// <summary>
        /// Gets the Sell command.
        /// </summary>
        /// <value>The Sell command.</value>
        public ICommand SellCommand
        {
            get
            {
                if (_sellCommand == null)
                {
                    _sellCommand = new RelayCommand<StockTransactionViewModel>(
                      p => { ShowBuySellDialog(p); },
                      p => { return true; });
                }
                return (_sellCommand);
            }
        }

        #endregion SellCommand

        #region BuyCommand

        /// <summary>
        /// Gets the Sell command.
        /// </summary>
        /// <value>The Sell command.</value>
        public ICommand BuyCommand
        {
            get
            {
                if (_buyCommand == null)
                {
                    _buyCommand = new RelayCommand<StockTransactionViewModel>(
                      p => { ShowBuySellDialog(p, true); },
                      p => { return true; });
                }
                return (_buyCommand);
            }
        }

        #endregion BuyCommand

        #region TileMaximizeCommand

        ICommand _tileMaximizeCommand;

        public ICommand TileMaximizeCommand
        {
            get
            {
                if (_tileMaximizeCommand == null)
                {
                    _tileMaximizeCommand = new RelayCommand<XamTile>(
                            p => { p.IsMaximized = true ; }, 
                            p => { return true; });
                }
                return (_tileMaximizeCommand);
            }
        }

        #endregion TileMaximizeCommand

        #endregion Commands

        #region Methods

        #region ShowBuySellDialog

        internal void ShowBuySellDialog(StockTransactionViewModel transaction, bool isBuying = false)
        {
            BuySellDialogWindow.DataContext = transaction;
            var content = BuySellDialogWindow.Content as BuySellDialogContent;
            if (isBuying)
                content.MaxSellableUnits = Int16.MaxValue;
            else
                content.MaxSellableUnits = transaction.StockPosition.Quantity;
            content.StockTransactionCompleted -= ContentStockTransactionCompleted;
            content.StockTransactionCompleted += new EventHandler<EventArgs>(ContentStockTransactionCompleted);
            BuySellDialogWindow.Show();
        }

        #endregion ShowBuySellDialog

        #endregion Methods

        private void ShowAbout_Click(object sender, RoutedEventArgs e)
        {
            var content = AboutDialogWindow.Content as AboutDialog;
            AboutDialogWindow.Show();
        }

		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (AboutDialogWindow.IsVisible)
			{
				AboutDialogWindow.CenterDialogWindow();
			}
			if (BuySellDialogWindow.IsVisible)
			{
				BuySellDialogWindow.CenterDialogWindow();
			}
		}

        private void OnTileManager_TileDragging(object sender, TileDraggingEventArgs e)
        {
            if (_vm != null)
                _vm.SuspendUpdates();
        }

        private void OnTileManager_TileStateChanging(object sender, TileStateChangingEventArgs e)
        {
            if (_vm != null)
                _vm.SuspendUpdates();
        }

        private void OnTileManager_AnimationStarted(object sender, EventArgs e)
        {
            if (_vm != null)
                _vm.SuspendUpdates();
        }

        private void OnTileManager_AnimationEnded(object sender, EventArgs e)
        {
            if (_vm != null)
                _vm.ResumeUpdates();
        }

        
    }
}