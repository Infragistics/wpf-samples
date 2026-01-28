using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using IGDataChart.Models;
using Infragistics.Samples.Shared.Tools;

namespace IGDataChart.Samples.Performance
{
    public partial class BindingHighVolumeData : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingHighVolumeData()
        {
            InitializeComponent();

            this.Loaded += OnSampleLoaded;
        }

        private HighVolumeDataViewModel _viewModel;

        #region Event Handlers
        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = new HighVolumeDataViewModel();
            this.DataChart.DataContext = _viewModel.Data;
            this.PerformanceHistoryChart.DataContext = _viewModel.PerformanceHistory;

            this.DataPointsCountSlider.ValueChanged += OnDataPointsCountSliderChanged;
            this.DataPointsCountSlider.Value = _viewModel.DataSettings.DataPoints;

            BindHighVolumeData();
        }
        private void OnDataPointsCountSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // update data view model with the new selection of data points
            var roundDataPoints = (int)(e.NewValue - (e.NewValue % 50000));
            _viewModel.DataSettings.DataPoints = roundDataPoints;
            this.DataPointsCountTextBlock.Text = String.Format("{0:0,0}", roundDataPoints);
        }
        private void OnDataChartLayoutUpdated(object sender, EventArgs e)
        {
            // stop PerformanceMonitor and update UI with new performance values
            _viewModel.PerformanceMonitor.StopMonitor();
            var timeSpan = _viewModel.PerformanceMonitor.TimeSpan;
            this.DataBindingTimeSpanTextBlock.Text = String.Format("{0:0.000}", timeSpan.TotalSeconds);
            this.DataChart.LayoutUpdated -= OnDataChartLayoutUpdated;
        }
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            BindHighVolumeData();
        }
        #endregion

        private void BindHighVolumeData()
        {
            this.DataBindingTimeSpanTextBlock.Text = "-.---";
            this.DataChart.LayoutUpdated += OnDataChartLayoutUpdated;

            _viewModel.LoadData();
            _viewModel.PerformanceMonitor.StartMonitor();
            this.DataChart.DataContext = _viewModel.Data;
        }
    }

    /// <summary>
    /// Represents view model for High Volume Data and monitors performance of data binding
    /// </summary>
    public class HighVolumeDataViewModel : ObservableModel
    {
        public HighVolumeDataViewModel()
        {
            this.PerformanceMonitor = new PerformanceMonitor();
            this.PerformanceMonitor.MonitorStopped += OnPerformanceMonitorStopped;

            this.DataMonitor = new PerformanceMonitor();
            this.DataMonitor.MonitorStopped += OnDataMonitorStopped;

            _dataSettings = new HighVolumeDataSettings();
            _dataSettings.DataPoints = 500000;
            _dataSettings.DateInterval = TimeSpan.FromMilliseconds(50);
            this.Data = new HighVolumeDataCollection();

            InitializePerformanceHistory();
            InitializeData();
        }

        #region Properties
        protected TimeSpan DataTimeSpan;

        public PerformanceMonitor PerformanceMonitor { get; set; }
        public PerformanceMonitor DataMonitor { get; set; }
        public ObservableCollection<PerformanceDataPoint> PerformanceHistory { get; set; }

        protected List<HighVolumeDataPoint> RandomData { get; set; }

        private List<HighVolumeDataPoint> _data;
        public List<HighVolumeDataPoint> Data
        {
            get { return _data; }
            set
            {
                if (_data == value) return;
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private HighVolumeDataSettings _dataSettings;
        public HighVolumeDataSettings DataSettings
        {
            get { return _dataSettings; }
            set
            {
                if (_dataSettings == value) return;
                _dataSettings = value;
                OnPropertyChanged("DataSettings");
            }
        }

        #endregion

        public void LoadData()
        {
            this.DataMonitor.StartMonitor();
            List<HighVolumeDataPoint> newData = new List<HighVolumeDataPoint>();

            if (this.DataSettings.DataPoints <= this.RandomData.Count)
            {
                newData = this.RandomData.GetRange(0, this.DataSettings.DataPoints);
            }
            this.DataMonitor.StopMonitor();
            this.Data = newData;
        }

        public void LoadData(int dataPoints)
        {
            this.DataMonitor.StartMonitor();
            List<HighVolumeDataPoint> newData = new List<HighVolumeDataPoint>();

            if (dataPoints <= this.RandomData.Count)
            {
                newData = this.RandomData.GetRange(0, dataPoints);
            }

            this.DataMonitor.StopMonitor();
            this.Data = newData;
        }
        /// <summary>
        /// Initializes Performance History with 20 data points
        /// </summary>
        private void InitializePerformanceHistory()
        {
            this.PerformanceHistory = new ObservableCollection<PerformanceDataPoint>();
            for (int i = 0; i < 20; i++)
            {
                this.PerformanceHistory.Add(new PerformanceDataPoint());
            }
        }
        private void InitializeData()
        {
            PerformanceMonitor pm = new PerformanceMonitor();
            pm.StartMonitor();
            HighVolumeDataGenerator.Settings.DataPoints = 1000000;
            this.RandomData = new List<HighVolumeDataPoint>();
            this.RandomData = HighVolumeDataGenerator.GenerateData();

            pm.StopMonitor();
        }

        #region Event Handlers
        private void OnPerformanceMonitorStopped(object sender, EventArgs e)
        {
            // add new Performance data point
            PerformanceDataPoint dataPoint = new PerformanceDataPoint
            {
                Label = String.Format("{0:0,0}", this.Data.Count),
                TimeSpan = this.PerformanceMonitor.TimeSpan,
            };
            this.PerformanceHistory.Add(dataPoint);
            this.PerformanceHistory.RemoveAt(0);
        }
        private void OnDataMonitorStopped(object sender, EventArgs e)
        {
            DataTimeSpan = this.DataMonitor.TimeSpan;
        }
        #endregion

    }


}