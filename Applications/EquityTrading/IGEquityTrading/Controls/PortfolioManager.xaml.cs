using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IGTrading.ViewModels;
using Infragistics.Windows.DataPresenter;
using IGTrading.Commands;

namespace IGTrading.Controls
{
    /// <summary>
    /// Interaction logic for PortfolioManager.xaml
    /// </summary>
    public partial class PortfolioManager : UserControl
    {
        #region Members
        private StockViewModel _vm;
        #endregion

        #region ctro
        public PortfolioManager()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(PortfolioManager_Loaded);
        }
        #endregion

        #region Properties
        #region BuySellCommand
        public ICommand BuySellCommand
        {
            get { return (ICommand)GetValue(BuySellCommandProperty); }
            set { SetValue(BuySellCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BuySellCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BuySellCommandProperty =
            DependencyProperty.Register("BuySellCommand", 
            typeof(ICommand), 
            typeof(PortfolioManager), 
            new UIPropertyMetadata(null));
        #endregion
        
        #endregion

        #region EventHandlers
        #region Loaded
        void PortfolioManager_Loaded(object sender, RoutedEventArgs e)
        {
            this._vm = this.DataContext as StockViewModel;
        }
        #endregion

        #region positionWatchGrid_DataValueChangedDirect
        private void positionWatchGrid_DataValueChangedDirect(object sender, Infragistics.Windows.DataPresenter.Events.DataValueChangedEventArgs e)
        {
            if (e.CellValuePresenter == null)
                return;
            if (e.ValueHistory == null)
                e.CellValuePresenter.ClearValue(Infragistics.Windows.DataPresenter.CellValuePresenter.BackgroundProperty);
            else
                if (e.CellValuePresenter != null && e.ValueHistory.Count == 1)
                {
                    Infragistics.Windows.DataPresenter.DataValueInfo latestDataValueInfo = e.ValueHistory[0];

                    if ((double)latestDataValueInfo.Value < 0)
                    {
                        e.CellValuePresenter.Background = Application.Current.Resources["RedBrush"] as Brush;
                        e.CellValuePresenter.Foreground = Brushes.White;
                    }
                    else
                    {
                        e.CellValuePresenter.Background = Application.Current.Resources["GreenBrush"] as Brush;
                        e.CellValuePresenter.Foreground = Brushes.White;
                    }
                }
        }
        #endregion

        #region positionWatchGrid_RecordFixedLocationChanged
        private void positionWatchGrid_RecordFixedLocationChanged(object sender, Infragistics.Windows.DataPresenter.Events.RecordFixedLocationChangedEventArgs e)
        {
            StockPositionViewModel sd = (e.Record as Infragistics.Windows.DataPresenter.DataRecord).DataItem as StockPositionViewModel;

            if (e.Record.IsFixed)
                _vm.AddStockCommand.Execute(sd.Symbol);
            else
                _vm.RemoveStockCommand.Execute(sd.Symbol);
        }
        #endregion

        #region positionWatchGrid_MouseDoubleClick
        private void positionWatchGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject source = e.OriginalSource as DependencyObject;

            if (source == null)
                return;

            if (source is FrameworkElement)
            {
                DataRecordPresenter drp = Infragistics.Windows.Utilities.GetAncestorFromType(source,
                    typeof(DataRecordPresenter), true) as DataRecordPresenter;
                if (drp == null)
                    return;

                if (drp.Record != null)
                {
                    drp.Record.IsSelected = true;
                    drp.IsActive = true;

                    DataRecord r = drp.DataRecord;
                    if (r != null)
                    {
                        StockPositionViewModel stockDetails = r.DataItem as StockPositionViewModel;
                        if (stockDetails != null)
                        {
                            StockTransactionViewModel stockTransaction = new StockTransactionViewModel();

                            stockTransaction.Account = _vm.SelectedAccount;
                            stockTransaction.StockTickerDetailsViewModel = stockDetails.StockTickerDetails;
                            stockTransaction.StockPosition = stockDetails;
                            stockTransaction.Quantity = stockDetails.Quantity;
                            stockTransaction.TransactionType = TransactionType.Sell;

                            if (this.BuySellCommand != null && this.BuySellCommand is RelayCommand<StockTransactionViewModel>)
                            {
                                this.BuySellCommand.Execute(stockTransaction);
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
