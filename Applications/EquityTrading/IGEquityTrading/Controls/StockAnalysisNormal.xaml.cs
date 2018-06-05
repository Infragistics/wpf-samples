using System.Windows;
using System.Windows.Controls;

namespace IGTrading.Controls
{
    /// <summary>
    /// Interaction logic for StockAnalysisNormal.xaml
    /// </summary>
    public partial class StockAnalysisNormal : UserControl
    {
        public StockAnalysisNormal()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(StockAnalysisNormal_Loaded);
        }

        void StockAnalysisNormal_Loaded(object sender, RoutedEventArgs e)
        {
            // Unlink the MiniChart from its previous owner, if any
            if (((DataChartEx)Application.Current.Resources["MiniStocksChart"]).Parent != null )
                ((ContentControl)((DataChartEx)Application.Current.Resources["MiniStocksChart"]).Parent).Content = null;
            this.MiniChart.Content = Application.Current.Resources["MiniStocksChart"];
        }
    }
}
