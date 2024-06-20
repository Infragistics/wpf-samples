using System;
using System.Windows;
using System.Windows.Threading;
using IGDataChart.Models;
using IGDataChart.Resources;
using IGDataChart.Tools;

namespace IGDataChart.Samples.Performance
{
    public partial class BindingRealTimeData : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingRealTimeData()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        protected RealTimeDataViewModel DataViewModel;
        protected ChartPerformanceMonitor ChartMonitor;

        #region Event Handlers
        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.ChartMonitor = new ChartPerformanceMonitor();
            this.ChartMonitor.ChartStatsChanged += OnChartMonitorStatsChanged;
            this.ChartMonitor.DataChart = this.DataChart;

            this.DataViewModel = new RealTimeDataViewModel();
            this.DataContext = this.DataViewModel.Data;

            this.DataPointsCountSlider.Value = this.DataViewModel.DataSettings.DataPoints;
            this.DataPointsCountSlider.ValueChanged += OnDataPointsCountSliderChanged;
            this.DataUpdatesIntervalSlider.ValueChanged += OnDataFeedIntervalSliderChanged;
            this.ToggleDataFeedButton.Click += OnToggleVolumeChartFeedButtonClick;
            this.AddDataPointsButton.Click += OnAddDataPointsButtonClick;

            this.Loaded -= OnSampleLoaded;
        }

        private void OnChartMonitorStatsChanged(object sender, ChartPerformanceMonitor.ChartStatsChangedEventArgs e)
        {
            this.DataChartUpdatesPerSecondTextBlock.Text = String.Format("{0:0.00}", e.ChartLayoutUpdatesPerSecond);
        }
        private void OnDataPointsCountSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.DataViewModel != null)
            {
                this.DataViewModel.DataSettings.DataPoints = (int)e.NewValue;
            }
        }
        private void OnDataFeedIntervalSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.DataViewModel != null)
            {
                this.DataViewModel.DataUpdateInterval = TimeSpan.FromMilliseconds((int)e.NewValue);
            }
        }
        private void OnAddDataPointsButtonClick(object sender, RoutedEventArgs e)
        {
            this.DataViewModel.StopDataFeeds();
            this.DataViewModel.GenerateData();
        }
        private void OnToggleVolumeChartFeedButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.DataViewModel.IsEnabledDataFeed)
            {
                this.ToggleDataFeedButton.Content = DataChartStrings.XWDC_BindingRealTimeData_StartDataFeed;
                this.AddDataPointsButton.IsEnabled = true;
                this.DataPointsCountSlider.IsEnabled = true;
                this.DataViewModel.StopDataFeeds();
                this.ChartMonitor.StopMonitor();
            }
            else
            {
                this.ToggleDataFeedButton.Content = DataChartStrings.XWDC_BindingRealTimeData_StopDataFeed;
                this.AddDataPointsButton.IsEnabled = false;
                this.DataPointsCountSlider.IsEnabled = false;
                if (this.DataViewModel.Data.Count != this.DataViewModel.DataSettings.DataPoints)
                {
                    this.DataViewModel.GenerateData();
                }
                this.DataViewModel.StartDataFeeds();
                this.ChartMonitor.StartMonitor();
            }
        }
        #endregion
    }

    /// <summary>
    /// Represents view model for Real-Time Data
    /// </summary>
    public class RealTimeDataViewModel : ObservableModel
    {
        public RealTimeDataViewModel()
        {

            this.DataUpdateInterval = TimeSpan.FromMilliseconds(50);
            this.DataUpdateTimer = new DispatcherTimer { Interval = this.DataUpdateInterval };
            this.DataUpdateTimer.Tick += OnDataUpdateTimerTick;

            _dataSettings = new RealTimeDataSettings();
            _dataSettings.DataPoints = 5000;
            _dataSettings.DateInterval = TimeSpan.FromMilliseconds(50);
            this.Data = new RealTimeDataCollection();
            this.GenerateData();

            this.LastDataPoint = RealTimeDataGenerator.LastDataPoint;
        }

        public bool IsEnabledDataFeed { get { return this.DataUpdateTimer.IsEnabled; } }
        protected DispatcherTimer DataUpdateTimer;

        protected RealTimeDataPoint LastDataPoint;
        private RealTimeDataCollection _data;
        public RealTimeDataCollection Data
        {
            get { return _data; }
            set
            {
                if (_data == value) return;
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private RealTimeDataSettings _dataSettings;
        public RealTimeDataSettings DataSettings
        {
            get { return _dataSettings; }
            set
            {
                if (_dataSettings == value) return;
                _dataSettings = value;
                OnPropertyChanged("DataSettings");
            }
        }

        private TimeSpan _dataUpdateInterval;
        public TimeSpan DataUpdateInterval
        {
            get { return _dataUpdateInterval; }
            set
            {
                if (_dataUpdateInterval == value) return;
                _dataUpdateInterval = value;
                if (this.DataUpdateTimer == null) return;
                this.DataUpdateTimer.Interval = _dataUpdateInterval;
            }
        }

        #region Event Handlers

        private void OnDataUpdateTimerTick(object sender, EventArgs e)
        {
            // generate new data point using RealTimeDataGenerator
            this.LastDataPoint = RealTimeDataGenerator.GenerateDataPoint(LastDataPoint);
            // remove the first data point
            this.Data.RemoveAt(0);
            // add the new data point at the end
            this.Data.Add(LastDataPoint);
        }

        #endregion

        #region Methods
        public void GenerateData()
        {
            RealTimeDataGenerator.Settings = this.DataSettings;
            RealTimeDataGenerator.Generate();
            this.Data.Clear();
            foreach (RealTimeDataPoint dp in RealTimeDataGenerator.Data)
            {
                this.Data.Add(dp);
            }
        }
        public void StartDataFeeds()
        {
            if (!this.DataUpdateTimer.IsEnabled)
            {
                this.DataUpdateTimer.Start();
            }
        }
        public void StopDataFeeds()
        {
            if (this.DataUpdateTimer.IsEnabled)
            {
                this.DataUpdateTimer.Stop();
            }
        }
        #endregion
    }


}
