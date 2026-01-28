using System;
using System.Windows;
using System.Windows.Threading;
using IGGrid.Models;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using System.Windows.Media;
using System.Xaml;

namespace IGGrid.Samples.Performance
{
    public partial class RealTimeUpdating : SampleContainer
    {
        #region Fields
        private DispatcherTimer timer;
        private RealTimeViewModel viewModel;
        #endregion Fields

        public RealTimeUpdating()
        {
            InitializeComponent();

            this.Loaded += OnSampleLoaded;

            this.viewModel = new RealTimeViewModel();
            this.DataContext = this.viewModel;

            this.BuyOrdersDisplay.CellControlAttached += new EventHandler<CellControlAttachedEventArgs>(BuyOrdersDisplay_CellControlAttached);
            this.SellOrdersDisplay.CellControlAttached += new EventHandler<CellControlAttachedEventArgs>(SellOrdersDisplay_CellControlAttached);

            this.timer = new DispatcherTimer();
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            this.timer.Tick += new EventHandler(timer_Tick);
        }

        #region Event Handlers
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {

            Column buyPriceColumn = this.BuyOrdersDisplay.Columns["FormatedPrice"] as Column;
            buyPriceColumn.IsSorted = SortDirection.Ascending;
            this.BuyOrdersDisplay.Columns.SortedColumns.Add(buyPriceColumn);

            Column sellPriceColumn = this.SellOrdersDisplay.Columns["FormatedPrice"] as Column;
            sellPriceColumn.IsSorted = SortDirection.Descending;
            this.SellOrdersDisplay.Columns.SortedColumns.Add(sellPriceColumn);

            this.timer.Start();
        }

        void SellOrdersDisplay_CellControlAttached(object sender, CellControlAttachedEventArgs e)
        {
            this.ToggleLastMatchStyle(e.Cell);
        }

        void BuyOrdersDisplay_CellControlAttached(object sender, CellControlAttachedEventArgs e)
        {
            this.ToggleLastMatchStyle(e.Cell);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.viewModel.UpdateDataSource();
        }
        #endregion Event Handlers

        #region Private Methods
        private void ToggleLastMatchStyle(Cell currentCell)
        {
            Quote dataContext = currentCell.Row.Data as Quote;

            if (dataContext != null)
            {
                if (this.viewModel.SelectedStock.MatchesLastBuyPrice(dataContext.Price))
                {
                    currentCell.Style = this.Resources["LastMatchStyle"] as System.Windows.Style;
                }
                else
                {
                    currentCell.Style = this.Resources["CustomCellControlStyle"] as System.Windows.Style;
                }
            }
        }
        #endregion Private Methods
    }
}
