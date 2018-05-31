using System;
using System.Windows;
using System.Windows.Controls;

namespace IGTrading.Controls
{
    /// <summary>
    /// Interaction logic for BuySellDialogContent.xaml
    /// </summary>
    public partial class BuySellDialogContent : UserControl
    {
        public BuySellDialogContent()
        {
            InitializeComponent();
        }

        //public properties
        public int MaxSellableUnits { set { QuantityEditor.ValueConstraint.MaxInclusive = value; } }
        
        //public EventArgs 

        public event EventHandler<EventArgs> StockTransactionCompleted;

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            EventArgs args = new EventArgs();
            //set the current price as the price at which the transaction will be executed
            ((IGTrading.ViewModels.StockTransactionViewModel)this.DataContext).PricePerShare = 
                ((IGTrading.ViewModels.StockTransactionViewModel)this.DataContext).CurrentPricePerShare;
            this.StockTransactionCompleted(this, args);
        }
        
        private void QuantityEditor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            BtnBuy.IsEnabled = e.NewValue.ToString().Length > 0 ? true : false;
        }
    }
}
