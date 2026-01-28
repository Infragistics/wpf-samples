using System;
using System.Windows;
using System.Windows.Threading;
using IGDataChart.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Samples.Data
{
    public partial class BindingLiveData : SampleContainer
    {
        private StockMarketServiceClient _client;
        private StockMarketDataCollection _data;
        
        public BindingLiveData()
        {
            InitializeComponent();
            InitializeSample();
        }

        private void InitializeSample()
        {
            _data = new StockMarketDataCollection();
           
            // Bind StockMarketData to the volume and price data charts.
            this.VolumeChart.DataContext = _data;
            this.PriceChart.DataContext = _data;

            // Create a StockMarketServiceClient for receiving StockMarketDataPoint objects.
            _client = new StockMarketServiceClient();
            _client.StockMarketDataReceived += OnStockMarketDataReceived;

            this.ToggleLiveDataFeedButton.Click += OnToggleLiveDataFeedButtonClick;
            this.DataFeedIntervalSlider.ValueChanged += OnDataFeedIntervalSliderValueChanged;
        }
        
        private void OnDataFeedIntervalSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_client != null)
            {
                _client.StockRequestInterval = TimeSpan.FromMilliseconds((int)DataFeedIntervalSlider.Value);
            }
        }

        private void OnToggleLiveDataFeedButtonClick(object sender, RoutedEventArgs e)
        {
            if (_client.IsEnabled)
            {
                this.ToggleLiveDataFeedButton.Content = DataChartStrings.XWDC_BindingLiveData_StartLiveDataFeed;
                _client.Stop();
            }

            else
            {
                this.ToggleLiveDataFeedButton.Content = DataChartStrings.XWDC_BindingLiveData_StopLiveDataFeed;
                _client.Start();
            }
        }

        private void OnStockMarketDataReceived(object sender, StockMarketDataReceivedEventArgs e)
        {
            // Replace the old StockMarketDataPoint with the new one in StockMarketData.
            _data.RemoveAt(0);
            _data.Add(e.NewDataPoint);

            _data.LastItem = _data[_data.Count - 1];
        }
    }
    
    /// <summary>
    /// StockMarketServiceClient simulates receiving StockMarketDataPoint objects 
    /// in intervals equal to the value of the StockRequestInterval property.
    /// </summary>
    public class StockMarketServiceClient
    {
        public StockMarketServiceClient()
        {
            this.IsEnabled = false;
            StockMarketGenerator.Settings.DateInterval = TimeSpan.FromDays(10);
            this.StockRequestInterval = TimeSpan.FromMilliseconds(10); // StockMarketGenerator.Settings.DateInterval;
            _lastDataPoint = StockMarketGenerator.LastDataPoint;
        }
        
        private DispatcherTimer _timer;
        private StockMarketDataPoint _lastDataPoint;
        private TimeSpan _stockRequestInterval;
        
        public bool IsEnabled { get; set; }
        
        public TimeSpan StockRequestInterval
        {
            get { return _stockRequestInterval; }
            set
            {
                if (_stockRequestInterval == value) return;
                _stockRequestInterval = value;
                if (_timer == null) return;
                _timer.Interval = _stockRequestInterval;

                StockMarketGenerator.Settings.DateInterval = _stockRequestInterval;
            }
        }

        public void Start()
        {
            this.IsEnabled = true;
            _timer = new DispatcherTimer { Interval = this.StockRequestInterval };
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }
        
        public void Stop()
        {
            this.IsEnabled = false;
            if (_timer == null) return;
            _timer.Stop();
            _timer = null;
        }
        
        private void OnTimerTick(object sender, EventArgs e)
        {
            // Generate new StockMarketData using StockMarketGenerator.
            _lastDataPoint = StockMarketGenerator.GenerateDataPoint(_lastDataPoint);
            OnStockMarketDataReceived(_lastDataPoint);
        }

        public delegate void StockMarketDataReceivedEventHandler(object sender, StockMarketDataReceivedEventArgs e);
        public event StockMarketDataReceivedEventHandler StockMarketDataReceived;
        
        public void OnStockMarketDataReceived(StockMarketDataPoint data)
        {
            if (StockMarketDataReceived != null)
            {
                StockMarketDataReceived(this, new StockMarketDataReceivedEventArgs(data));
            }
        }
    }
    
    public class StockMarketDataReceivedEventArgs : EventArgs
    {
        public StockMarketDataPoint NewDataPoint { get; set; }
        
        public StockMarketDataReceivedEventArgs(StockMarketDataPoint data)
        {
            this.NewDataPoint = data;
        }
    }
}
