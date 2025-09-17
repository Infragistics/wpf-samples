using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IGDataChart.Models;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Samples.Display.Series
{
    public partial class GalleryFinancialSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GalleryFinancialSeries()
        {
            InitializeComponent();
            InitializeViewModel();

            this.Loaded += OnLoaded;
        }

        private StockMarketDataCollection _data;
        private FinancialSeriesViewModel _vm;
        protected FinancialSeriesModel SelectedFinancialIndicator;
        protected FinancialSeriesModel SelectedFinancialOverlay;

        private void InitializeViewModel()
        {
            _data = new StockMarketDataCollection();
            _vm = new FinancialSeriesViewModel();
            _vm.Data = _data;

            foreach (var model in FinancialSeriesCollection.GetKnownFinancialSeries())
            {
                _vm.Series.Add(model);
            }
        }

        #region EventHandlers

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.FinancialPriceChart.Series[0].ReplayTransitionIn();
            this.FinancialPriceChart.DataContext = _vm.Data;
            this.FinancialIndicatorChart.DataContext = _vm.Data;
            foreach (var model in _vm.Series)
            {
                if (model.IsIndicatorSeries())
                {
                    this.IndicatorsComboBox.Items.Add(model);
                }
                else if (model.IsOverlaySeries())
                {
                    this.OverlaysComboBox.Items.Add(model);
                }
            }

            OverlaysComboBox.SelectionChanged += OverlaysComboBox_SelectionChanged;
            OverlaysComboBox.SelectedIndex = 0;

            IndicatorModesComboBox.SelectionChanged += IndicatorModesComboBox_SelectionChanged;
            IndicatorModesComboBox.SelectedIndex = 0;
        
            IndicatorsComboBox.SelectionChanged += FinancialIndicatorsComboBox_SelectionChanged;
            IndicatorsComboBox.SelectedIndex = 0;
          
            FinancialPriceModesComboBox.SelectionChanged += FinancialPriceModesComboBox_SelectionChanged;
            FinancialPriceModesComboBox.SelectedIndex = 0;
            PrevFinancialPriceModeButton.Click += OnPrevFinancialPriceModeButtonClick;
            NextFinancialPriceModeButton.Click += OnNextFinancialPriceModeButtonClick;
            
            PrevIndicatorsButton.Click += OnPrevIndicatorsButtonClick;
            NextIndicatorsButton.Click += OnNextIndicatorsButtonClick;

            PrevOverlaysButton.Click += OnPrevOverlaysButtonClick;
            NextOverlaysButton.Click += OnNextOverlaysButtonClick;
        }

        void IndicatorModesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.FinancialIndicatorChart.Series.Count > 0)
            {
                //this.FinancialPriceChart.Series[0].ReplayTransitionIn();
                this.FinancialIndicatorChart.Series[0].ReplayTransitionIn();
                var indicatorSeries = (StrategyBasedIndicator)this.FinancialIndicatorChart.Series[0];
                indicatorSeries.DisplayType = GetFinancialIndicatorDisplayType();
                
            }
        }

        #region Financial Overlays
        void OverlaysComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // clear FinancialPriceChart for the previous selection
            this.FinancialOverlayPeriodPanel.Visibility = Visibility.Collapsed;
            this.FinancialOverlayMultiplierPanel.Visibility = Visibility.Collapsed;
            var xAxis = (CategoryXAxis)this.FinancialOverlayChart.Axes.First(axis => axis.Name == "overlayCategoryXAxis");
            var yAxis = (NumericYAxis)this.FinancialOverlayChart.Axes.First(axis => axis.Name == "overlayNumericYAxis");
            yAxis.ClearValue(NumericAxisBase.MinimumValueProperty);
            yAxis.ClearValue(NumericAxisBase.MaximumValueProperty);

            // remove previous financial overlay from the Financial Overlay chart
            if (this.FinancialOverlayChart.Series.Count > 0) this.FinancialOverlayChart.Series.RemoveAt(0);

            //SelectedFinancialIndicator = null;
            SelectedFinancialOverlay = (FinancialSeriesModel)e.AddedItems[0];

            #region PriceChannelOverlay
            // NOTE: PriceChannelOverlay can be plotted with FinancialPriceSeries in the FinancialPrice chart because they share range of values on y-axis 
            if (SelectedFinancialOverlay.IsOverlaySeries() && SelectedFinancialOverlay.Type.Equals(typeof(PriceChannelOverlay)))
            {
                this.FinancialOverlayPeriodPanel.Visibility = Visibility.Visible;
                int period = (int)this.FinancialOverlayPeriodSlider.Value;

                var overlay = (PriceChannelOverlay)Activator.CreateInstance(SelectedFinancialOverlay.Type, null);
                overlay.OpenMemberPath = "Open";
                overlay.CloseMemberPath = "Close";
                overlay.HighMemberPath = "High";
                overlay.LowMemberPath = "Low";
                overlay.VolumeMemberPath = "Volume";
                overlay.XAxis = xAxis;
                overlay.YAxis = yAxis;
                overlay.Period = period;
                overlay.ItemsSource = _data;
                overlay.IsHighlightingEnabled = true;
                overlay.IsTransitionInEnabled = true;
                this.FinancialOverlayChart.Series.Add(overlay);
            }
            #endregion
            #region BollingerBandsOverlay
            // NOTE: BollingerBandsOverlay also can be plotted with FinancialPriceSeries in the FinancialPrice chart 
            else if (SelectedFinancialOverlay.IsOverlaySeries() && SelectedFinancialOverlay.Type.Equals(typeof(BollingerBandsOverlay)))
            {
                this.FinancialOverlayMultiplierPanel.Visibility = Visibility.Visible;
                this.FinancialOverlayPeriodPanel.Visibility = Visibility.Visible;
                int period = (int)this.FinancialOverlayPeriodSlider.Value;
                double multiplier = this.FinancialOverlayMultiplierSlider.Value;

                var overlay = (BollingerBandsOverlay)Activator.CreateInstance(SelectedFinancialOverlay.Type, null);
                overlay.OpenMemberPath = "Open";
                overlay.CloseMemberPath = "Close";
                overlay.HighMemberPath = "High";
                overlay.LowMemberPath = "Low";
                overlay.VolumeMemberPath = "Volume";
                overlay.XAxis = xAxis;
                overlay.YAxis = yAxis;
                overlay.Period = period;
                overlay.Multiplier = multiplier;
                overlay.ItemsSource = _data;
                overlay.IsHighlightingEnabled = true;
                overlay.IsTransitionInEnabled = true;
                this.FinancialOverlayChart.Series.Add(overlay);
            }
            #endregion

        }
        private void OnNextOverlaysButtonClick(object sender, RoutedEventArgs e)
        {
            var index = (OverlaysComboBox.SelectedIndex + 1) % OverlaysComboBox.Items.Count;
            OverlaysComboBox.SelectedIndex = index;
        }
        private void OnPrevOverlaysButtonClick(object sender, RoutedEventArgs e)
        {
            var index = OverlaysComboBox.SelectedIndex == 0 ?
                OverlaysComboBox.Items.Count - 1 :
                OverlaysComboBox.SelectedIndex - 1;
            OverlaysComboBox.SelectedIndex = index;
        }

        private void FinancialOverlayPeriodSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SelectedFinancialOverlay == null) return;
            if (SelectedFinancialOverlay.Type == typeof(PriceChannelOverlay))
            {
                var indicatorSeries = (PriceChannelOverlay)this.FinancialOverlayChart.Series[0];
                indicatorSeries.Period = (int)e.NewValue;
            }
            if (SelectedFinancialOverlay.Type == typeof(BollingerBandsOverlay))
            {
                var indicatorSeries = (BollingerBandsOverlay)this.FinancialOverlayChart.Series[0];
                indicatorSeries.Period = (int)e.NewValue;
            }
        }

        private void FinancialOverlayMultiplierSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SelectedFinancialOverlay == null) return;
            if (SelectedFinancialOverlay.Type == typeof(BollingerBandsOverlay))
            {
                var indicatorSeries = (BollingerBandsOverlay)this.FinancialOverlayChart.Series[0];
                indicatorSeries.Multiplier = e.NewValue;
            }
        }

        #endregion
        #region Financial Price
        void FinancialPriceModesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var priceSeries = (FinancialPriceSeries)this.FinancialPriceChart.Series[0];
            priceSeries.IsTransitionInEnabled = true;
            priceSeries.ReplayTransitionIn();
            var item = FinancialPriceModesComboBox.SelectedItem as ComboBoxItem;
            if (item != null)
            {
                var tag = item.Tag as string;
                priceSeries.DisplayType = tag == "OHLC" ? PriceDisplayType.OHLC : PriceDisplayType.Candlestick;
            }
        }
        private void OnNextFinancialPriceModeButtonClick(object sender, RoutedEventArgs e)
        {
            var priceSeries = (FinancialPriceSeries)this.FinancialPriceChart.Series[0];
            priceSeries.IsTransitionInEnabled = true;
            priceSeries.ReplayTransitionIn();

            var index = (FinancialPriceModesComboBox.SelectedIndex + 1) % FinancialPriceModesComboBox.Items.Count;
            FinancialPriceModesComboBox.SelectedIndex = index;
        }
        private void OnPrevFinancialPriceModeButtonClick(object sender, RoutedEventArgs e)
        {
            var priceSeries = (FinancialPriceSeries)this.FinancialPriceChart.Series[0];
            priceSeries.IsTransitionInEnabled = true;
            priceSeries.ReplayTransitionIn();

            var index = FinancialPriceModesComboBox.SelectedIndex == 0 ?
                FinancialPriceModesComboBox.Items.Count - 1 :
                FinancialPriceModesComboBox.SelectedIndex - 1;
            FinancialPriceModesComboBox.SelectedIndex = index;
        }

        #endregion
        #region Financial Indicators
        private void OnNextIndicatorsButtonClick(object sender, RoutedEventArgs e)
        {
            var index = (IndicatorsComboBox.SelectedIndex + 1) % IndicatorsComboBox.Items.Count;
            IndicatorsComboBox.SelectedIndex = index;
        }
        private void OnPrevIndicatorsButtonClick(object sender, RoutedEventArgs e)
        {
            var index = IndicatorsComboBox.SelectedIndex == 0 ?
                IndicatorsComboBox.Items.Count - 1 :
                IndicatorsComboBox.SelectedIndex - 1;
            IndicatorsComboBox.SelectedIndex = index;
        }
        void FinancialIndicatorsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // clear FinancialIndicatorChart for the previous selection
            var xAxis = (CategoryXAxis)this.FinancialIndicatorChart.Axes.First(axis => axis.Name == "indicatorCategoryXAxis");
            var yAxis = (NumericYAxis)this.FinancialIndicatorChart.Axes.First(axis => axis.Name == "indicatorNumericYAxis");
            yAxis.ClearValue(NumericAxisBase.MinimumValueProperty);
            yAxis.ClearValue(NumericAxisBase.MaximumValueProperty);

            // remove previous financial indicator from the Financial Indicator data chart
            if (this.FinancialIndicatorChart.Series.Count > 0) this.FinancialIndicatorChart.Series.RemoveAt(0);

            //SelectedFinancialOverlay = null;
            SelectedFinancialIndicator = (FinancialSeriesModel)e.AddedItems[0];

            // NOTE: all financial indicator series does not share range of values on y-axis with FinancialPriceSeries 
            if (SelectedFinancialIndicator.IsIndicatorSeries())
            {
                var indicator = (StrategyBasedIndicator)Activator.CreateInstance(SelectedFinancialIndicator.Type, null);
                indicator.OpenMemberPath = "Open";
                indicator.CloseMemberPath = "Close";
                indicator.HighMemberPath = "High";
                indicator.LowMemberPath = "Low";
                indicator.VolumeMemberPath = "Volume";
                indicator.XAxis = xAxis;
                indicator.YAxis = yAxis;
                indicator.YAxis.ClearValue(NumericAxisBase.MinimumValueProperty);
                indicator.YAxis.ClearValue(NumericAxisBase.MaximumValueProperty);
                indicator.TrendLineType = TrendLineType.None;
                indicator.Thickness = 3;
                indicator.DisplayType = GetFinancialIndicatorDisplayType();
                indicator.ItemsSource = _data;
                indicator.IsHighlightingEnabled = true;
                indicator.IsTransitionInEnabled = true;
                this.FinancialIndicatorChart.Series.Add(indicator);
            }
        }

        private IndicatorDisplayType GetFinancialIndicatorDisplayType()
        {
            var value = IndicatorDisplayType.Area;
            var item = IndicatorModesComboBox.SelectedItem as ComboBoxItem;
            if (item != null)
            {
                var tag = item.Tag as string;
                if (tag == "Area")
                    value = IndicatorDisplayType.Area;
                else if (tag == "Column")
                    value = IndicatorDisplayType.Column;
                else if (tag == "Line")
                    value = IndicatorDisplayType.Line;

            }
            return value;
        }

        #endregion
      
       

       
        #endregion


    }

}
