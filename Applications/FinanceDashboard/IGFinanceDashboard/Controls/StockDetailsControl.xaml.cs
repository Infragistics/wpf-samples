using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using IGShowcase.FinanceDashboard;

namespace IGShowcase.FinanceDashboard
{
    /// <summary>
    /// Interaction logic for StockTickerControl.xaml
    /// </summary>
    public partial class StockDetailsControl : UserControl, INotifyPropertyChanged
    {
        public StockDetailsControl()
        {
            InitializeComponent();
            this.SelectedStock = new StockTradeData();
        }

        public static readonly DependencyProperty SelectedStockProperty =
        DependencyProperty.Register("SelectedStock", typeof(StockTradeData), typeof(StockDetailsControl),
          new PropertyMetadata(new PropertyChangedCallback(SelectedStock_Changed)));

        public StockTradeData SelectedStock
        {
            get { return (StockTradeData)GetValue(SelectedStockProperty); }
            set { SetValue(SelectedStockProperty, value); }
        }
        private static void SelectedStock_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var owner = (StockDetailsControl)obj;
            //owner.UpdateColorSamplerBitmap(true);
            owner.OnPropertyChanged("SelectedStock");
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
