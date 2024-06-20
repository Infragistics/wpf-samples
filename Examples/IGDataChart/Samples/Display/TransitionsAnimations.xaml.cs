using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using IGDataChart.Resources;                    // DataChartStrings
using Infragistics.Controls.Charts;             // XamDataChart 
using Infragistics.Samples.Shared.Controls;
using Infragistics.Samples.Shared.Models;       // GallerySampleViewModel 

namespace IGDataChart.Samples.Display.Series
{
    public partial class TransitionsAnimations : Infragistics.Samples.Framework.SampleContainer
    {
        public TransitionsAnimations()
        {
            InitializeComponent();
            InitializeSample();
        }

        private GallerySampleViewModel _samplesViewModel;
        protected TimeSpan TransDuration = TimeSpan.FromSeconds(2);
        protected TransitionInSpeedType TransSpeed = TransitionInSpeedType.Auto;
        protected CategoryTransitionInMode TransMode = CategoryTransitionInMode.Auto;
        protected EasingMode TransEasingMode = EasingMode.EaseInOut;
        protected EasingFunctionBase TransEasingFunction = new BackEase();
      
        private void InitializeSample()
        {
            _samplesViewModel = new GallerySampleViewModel();
            
            _samplesViewModel.AddSample(this.ColumnChart, DataChartStrings.XWDC_CategorySeries_ColumnSeries);
            _samplesViewModel.AddSample(this.AreaChart, DataChartStrings.XWDC_CategorySeries_AreaSeries);
            _samplesViewModel.AddSample(this.LineChart, DataChartStrings.XWDC_CategorySeries_LineSeries);
            _samplesViewModel.AddSample(this.SplineChart, DataChartStrings.XWDC_CategorySeries_SplineSeries);
            _samplesViewModel.AddSample(this.SplineAreaChart, DataChartStrings.XWDC_CategorySeries_SplineAreaSeries);
            _samplesViewModel.AddSample(this.PointChart, DataChartStrings.XWDC_CategorySeries_PointSeries);
            _samplesViewModel.AddSample(this.StepLineChart, DataChartStrings.XWDC_CategorySeries_StepLineSeries);
            _samplesViewModel.AddSample(this.StepAreaChart, DataChartStrings.XWDC_CategorySeries_StepAreaSeries);
            _samplesViewModel.AddSample(this.RangeColumnChart, DataChartStrings.XWDC_CategorySeries_RangeColumnSeries);
            _samplesViewModel.AddSample(this.RangeAreaChart, DataChartStrings.XWDC_CategorySeries_RangeAreaSeries);
            _samplesViewModel.AddSample(this.WaterfallChart, DataChartStrings.XWDC_CategorySeries_WaterfallSeries);
            _samplesViewModel.AddSample(this.BarChart, DataChartStrings.XWDC_CategorySeries_BarSeries);
            _samplesViewModel.AddSample(this.FinancialPriceChart, DataChartStrings.XWDC_FinancialPriceSeries);
            _samplesViewModel.AddSample(this.FinancialIndicatorChart, DataChartStrings.XWDC_FinancialIndicators);
            _samplesViewModel.AddSample(this.FinancialOverlayChart, DataChartStrings.XWDC_FinancialOverlays);
        
            InitializeCharts();

            this.ComboBoxTransitionType.ItemsSource = EnumValuesProvider.GetEnumValues<CategoryTransitionInMode>();
            this.ComboBoxTransitionType.SelectedIndex = 0;

            this.ComboBoxTransitionSpeedType.ItemsSource = EnumValuesProvider.GetEnumValues<TransitionInSpeedType>();
            this.ComboBoxTransitionSpeedType.SelectedIndex = 0;

            this.ComboBoxTransitionEasingFunction.SelectedIndex = 0;
            this.ComboBoxTransitionEasingMode.SelectedIndex = 0;
          
            this.PrevItemButton.Click += OnPrevItemButtonClick;
            this.NextItemButton.Click += OnNextItemButtonClick;

            this.ItemSelectionComboBox.ItemsSource = _samplesViewModel.SamplesNames;
            this.ItemSelectionComboBox.SelectionChanged += OnItemSelectionChanged;
            this.ItemSelectionComboBox.SelectedIndex = 0;
        }
        private void InitializeCharts()
        {
            foreach (XamDataChart chart in _samplesViewModel.Samples)
            {
                UpdateChartSeries(chart, Visibility.Collapsed);
            }
        }
        private void UpdateChartSeries(XamDataChart chart, Visibility isVisibile)
        {
            //chart.Visibility = isVisibile;
            if (chart.IsFinancialChart() && isVisibile == Visibility.Visible)
            {
                this.FinancialLegend.Visibility = Visibility.Visible;
                this.Legend.Visibility = Visibility.Collapsed;
            }
            if (chart.IsCategoryChart() && isVisibile == Visibility.Visible)
            {
                this.Legend.Visibility = Visibility.Visible;
                this.FinancialLegend.Visibility = Visibility.Collapsed;
            }
            foreach (var series in chart.Series)
            {
                series.Visibility = isVisibile;
                series.TransitionInSpeedType = TransSpeed;
                series.TransitionInEasingFunction = TransEasingFunction;
                series.TransitionInDuration = TransDuration;
                 
                if (series is CategorySeries)
                {
                    ((CategorySeries)series).TransitionInMode = TransMode;
                    ((CategorySeries)series).IsTransitionInEnabled = true;
                }
                if (series is FinancialSeries)
                {
                    ((FinancialSeries)series).TransitionInMode = TransMode;
                    ((FinancialSeries)series).IsTransitionInEnabled = true;
                }

                if (series is StackedSeriesBase)
                {
                    foreach (var stack in ((StackedSeriesBase)series).Series)
                    {
                        stack.Visibility = isVisibile;
                    }
                }
                series.ReplayTransitionIn();
            }
        }
   
        private void UpdateChartSamples()
        {
            if (_samplesViewModel == null) return;
            var i = 0;
            foreach (XamDataChart chart in _samplesViewModel.Samples)
            {
                var isVisibile = (i == this.ItemSelectionComboBox.SelectedIndex) ? Visibility.Visible : Visibility.Collapsed;
                UpdateChartSeries(chart, isVisibile);
                i++;
            }
        }
        #region Event Handlers
        private void OnNextItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.ItemSelectionComboBox.SelectedIndex = _samplesViewModel.GetNextSampleIndex();
        }
        private void OnPrevItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.ItemSelectionComboBox.SelectedIndex = _samplesViewModel.GetPreviousSampleIndex();
        }
        private void OnItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _samplesViewModel.ShowSample(this.ItemSelectionComboBox.SelectedIndex);
            UpdateChartSamples();
        }

        private void ComboBoxTransitionSpeedType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxTransitionSpeedType == null) return;
            TransSpeed = (TransitionInSpeedType)e.AddedItems[0];
            UpdateChartSamples();
        }
        private void ComboBoxTransitionEasingFunction_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxTransitionEasingFunction == null) return;
            if (ComboBoxTransitionEasingFunction.SelectedIndex == -1)
            {
                TransEasingFunction = EasingFunctions[0];
            }
            else
            {
                TransEasingFunction = EasingFunctions[ComboBoxTransitionEasingFunction.SelectedIndex];
            }
            TransEasingFunction.EasingMode = TransEasingMode;
            UpdateChartSamples();
        }
        private void ComboBoxTransitionEasingMode_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxTransitionEasingMode == null) return;
            if (ComboBoxTransitionEasingMode.SelectedIndex == -1)
            {
                TransEasingMode = EasingModes[0];
            }
            else
            {
                TransEasingMode = EasingModes[ComboBoxTransitionEasingMode.SelectedIndex];
            }
            TransEasingFunction.EasingMode = TransEasingMode;
            UpdateChartSamples();
        }
        private void ComboBoxTransitionType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TransMode = (CategoryTransitionInMode)e.AddedItems[0];
            UpdateChartSamples();
        }
        private void TransitionDurationSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TransitionDurationSlider == null) return;
            TransDuration = TimeSpan.FromSeconds(TransitionDurationSlider.Value);
        }
        private void TransitionReplayButton_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateChartSamples();
        }
        #endregion

        static readonly EasingFunctionBase[] EasingFunctions = 
        {
            new PowerEase(),
            new BackEase(),
            new BounceEase(),
            new CircleEase(),
            new CubicEase(),
            new ElasticEase(),
            new ExponentialEase(),
            new QuadraticEase(),
            new QuarticEase(),
            new QuinticEase(),
            new SineEase(),
        };
        static readonly EasingMode[] EasingModes = 
        {
            EasingMode.EaseOut,
            EasingMode.EaseInOut,
            EasingMode.EaseIn,
        };


        
    }

}
