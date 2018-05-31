using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using IGTrading.Commands;
using IGTrading.ViewModels;
using Infragistics.Windows.DataPresenter;

namespace IGTrading.Controls
{
    /// <summary>
    /// Interaction logic for RealTimeStocks.xaml
    /// </summary>
    public partial class RealTimeStocks : UserControl
    {
        #region Members
        private StockViewModel _vm;
        #endregion

        #region ctor
        public RealTimeStocks()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(RealTimeStocks_Loaded);
        }
        #endregion

        #region Commands
        #region BuyCommand
        public ICommand BuyCommand
        {
            get { return (ICommand)GetValue(BuyCommandProperty); }
            set { SetValue(BuyCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BuyCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BuyCommandProperty =
            DependencyProperty.Register("BuyCommand", 
            typeof(ICommand), 
            typeof(RealTimeStocks), 
            new UIPropertyMetadata(null));
        #endregion
        #endregion

        #region EventHandlers
        #region Loaded
        void RealTimeStocks_Loaded(object sender, RoutedEventArgs e)
        {
            this._vm = this.DataContext as StockViewModel;
        }
        #endregion

        #region tickingStocksGrid_DataValueChangedDirect
        private void tickingStocksGrid_DataValueChangedDirect(object sender, Infragistics.Windows.DataPresenter.Events.DataValueChangedEventArgs e)
        {
            if (e.CellValuePresenter == null)
                return;

            if (e.ValueHistory == null)
                e.CellValuePresenter.ClearValue(Infragistics.Windows.DataPresenter.CellValuePresenter.BackgroundProperty);
            else
                if (e.CellValuePresenter != null && e.ValueHistory.Count > 1)
                {
                    Infragistics.Windows.DataPresenter.DataValueInfo latestDataValueInfo = e.ValueHistory[0];
                    Infragistics.Windows.DataPresenter.DataValueInfo previousDataValueInfo = e.ValueHistory[1];

                    if ((double)latestDataValueInfo.Value < (double)previousDataValueInfo.Value)
                    {
                        e.CellValuePresenter.Background = Application.Current.Resources["RedBrush"] as Brush;
                        e.CellValuePresenter.Foreground = Brushes.White;
                    }
                    else
                        if ((double)latestDataValueInfo.Value > (double)previousDataValueInfo.Value)
                        {
                            e.CellValuePresenter.Background = Application.Current.Resources["GreenBrush"] as Brush;
                            e.CellValuePresenter.Foreground = Brushes.White;
                        }
                        else
                            e.CellValuePresenter.Background = Application.Current.Resources["LightBlueBrush"] as Brush;
                }
                else
                    e.CellValuePresenter.ClearValue(Infragistics.Windows.DataPresenter.CellValuePresenter.BackgroundProperty);
        }
        #endregion

        #region tickingStocksGrid_RecordFixedLocationChanged
        private void tickingStocksGrid_RecordFixedLocationChanged(object sender, Infragistics.Windows.DataPresenter.Events.RecordFixedLocationChangedEventArgs e)
        {
            StockTickerDetailsViewModel sd = (e.Record as Infragistics.Windows.DataPresenter.DataRecord).DataItem as StockTickerDetailsViewModel;

            if (e.Record.IsFixed)
                _vm.AddStockCommand.Execute(sd.Symbol);
            else
                _vm.RemoveStockCommand.Execute(sd.Symbol);
        }
        #endregion

        #region TickingStocksGridMouseDoubleClick
        private void TickingStocksGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject source = e.OriginalSource as DependencyObject;

            if (source == null)
            {
                return;
            }

            if (source is FrameworkElement)
            {
                DataRecordPresenter drp = Infragistics.Windows.Utilities.GetAncestorFromType(source,
                typeof(DataRecordPresenter), true) as DataRecordPresenter;
                if (drp == null)
                {
                    return;
                }
                if (drp.Record != null)
                {
                    drp.Record.IsSelected = true;

                    drp.IsActive = true;

                    DataRecord r = drp.DataRecord;

                    if (r != null)
                    {
                        StockTickerDetailsViewModel stockDetails = r.DataItem as StockTickerDetailsViewModel;
                        if (stockDetails != null)
                        {
                            StockTransactionViewModel stockTransaction = new StockTransactionViewModel();
                            stockTransaction.Account = _vm.SelectedAccount;
                            stockTransaction.StockTickerDetailsViewModel = stockDetails;
                            stockTransaction.Quantity = 10;
                            stockTransaction.TransactionType = TransactionType.Buy;
                            if (this.BuyCommand != null && this.BuyCommand is RelayCommand<StockTransactionViewModel>)
                            {
                                this.BuyCommand.Execute(stockTransaction);
                            }
                        }
                    }
                }
            }
        }
        #endregion


        #endregion
    }
}
