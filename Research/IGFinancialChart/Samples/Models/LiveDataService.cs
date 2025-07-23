using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq; 
using System.Windows;
using System.Windows.Threading;

namespace IGFinancialChart.Samples 
{    
      /// <summary>
    /// Represents a data service for receiving stock items
    /// </summary>
    public class LiveDataService
    {
        public LiveDataService()
        { 
            Settings = new LiveDataSettings();
            _UpdateInterval = TimeSpan.FromSeconds(0.1);  
        }
          
        private DispatcherTimer _UpdateTimer;
        private TimeSpan _UpdateInterval;
        public TimeSpan UpdateInterval
        {
            get { return _UpdateInterval; }
            set
            {
                if (_UpdateInterval != value)  
                    _UpdateInterval = value;
                if (_UpdateTimer != null)  
                    _UpdateTimer.Interval = _UpdateInterval; 
            }
        }
                
        public LiveDataSettings Settings { get; set; }
        public StockItem LastDataItem { get; private set; }  

        internal bool IsEnabled { get { return IsInitalized && _UpdateTimer.IsEnabled; } }
        internal bool IsInitalized { get { return _UpdateTimer != null; } }
        public void Start()
        {
            //this.IsEnabled = true;
            if (IsInitalized)
            {
                _UpdateTimer.Stop();
            }
            else
            {
                _UpdateTimer = new DispatcherTimer();
                _UpdateTimer.Tick += OnTimerTick; 
                LiveDataGenerator.Settings = this.Settings;
                var data = LiveDataGenerator.GenerateDataItems();
                LastDataItem = data.Last();
                OnDataSetup(data);
            }
            
            _UpdateTimer.Interval = this.UpdateInterval;
            _UpdateTimer.Start();
        }
        
        public void Stop()
        {
            if (_UpdateTimer == null) return;
            _UpdateTimer.Stop();
        }
        
        private void OnTimerTick(object sender, EventArgs e)
        { 
            // Generate new item point using last data item
            LastDataItem = LiveDataGenerator.GenerateDataPoint(LastDataItem);
            OnDataAdded(LastDataItem);
        }

        public delegate void LiveDataServiceEventHandler(object sender, LiveDataServiceEventArgs e);
        public event LiveDataServiceEventHandler DataAdded;
        public event LiveDataServiceEventHandler DataSetup;
        
        public void OnDataAdded(StockItem dataItem)
        {
            var data = new List<StockItem> { dataItem };
            if (DataAdded != null)            
                DataAdded(this, new LiveDataServiceEventArgs(data));            
        }
        public void OnDataSetup(List<StockItem> data)
        {
            if (DataSetup != null)            
                DataSetup(this, new LiveDataServiceEventArgs(data));            
        }

    }
    
    /// <summary>
    /// Represents event arguments for events of <see cref="LiveDataService"/>
    /// </summary>
    public class LiveDataServiceEventArgs : EventArgs
    { 
        public ObservableCollection<StockItem> DataItems { get; set; }
        
        public LiveDataServiceEventArgs(List<StockItem> dataItems)
        {
            this.DataItems = new ObservableCollection<StockItem>();
            foreach (var item in dataItems)
            { 
                this.DataItems.Add(item);
            }
        }
        public LiveDataServiceEventArgs(StockItem dataItem)
        {
            this.DataItems = new ObservableCollection<StockItem>();
            this.DataItems.Add(dataItem);
        }
    }
    
    /// <summary>
    /// Represents a class for generating stock items based on <see cref="LiveDataSettings"/>
    /// </summary>
    [System.Runtime.InteropServices.Guid("C373613C-C601-434D-B039-B2E45349CF23")]
    public static class LiveDataGenerator
    {
        static LiveDataGenerator()
        {
            Generator = new Random();
            Settings = new LiveDataSettings();
            Data = GenerateDataItems();
        }
        #region Properties
        public static LiveDataSettings Settings { get; set; }
        public static List<StockItem> Data { get; set; }
        public static StockItem LastDataPoint { get; set; }
         
        private static readonly Random Generator;
        #endregion
        #region Methods
        public static void Generate()
        {
            Data = GenerateDataItems();
        }
        public static DateTime GetStockDate(DateTime lastDate)
        {
            return lastDate.Add(Settings.TimeInterval);
        }

        #region Methods for Random GeneratorMode
          
        public static double RandomValue;
        public static double GetStockRandom()
        {
            double nextRandomValue = Generator.NextDouble();
            if (nextRandomValue > .5)
                RandomValue += nextRandomValue * Settings.PriceRange;
            else
                RandomValue -= nextRandomValue * Settings.PriceRange;
            return RandomValue;
        }
        public static double GetStockOpen(double preClose)
        {
            //open value always equals to previous close value
            return preClose;
        }
        public static double GetStockOpen(double low, double high)
        {
            low = Math.Max(low, Settings.PriceStart - Settings.PriceRange);
            high = Math.Min(high, Settings.PriceStart + Settings.PriceRange);
            
            var min = (int)Math.Floor(Math.Min(low, high));
            var max = (int)Math.Ceiling(Math.Max(low, high));
            return Math.Abs(Generator.Next(min, max)); 
        }
        public static double GetStockHigh(double open)
        {
            var sum = 0.0;
            var min = (int)open;
            var max = (int)open + Settings.PriceRange;
            for (int i = 0; i < Settings.PriceSample; i++)
            {
                sum += Generator.Next((int)min, (int)max);
            }
            return Math.Abs(sum / Settings.PriceSample);
        }
        public static double GetStockLow(double open)
        {
            var sum = 0.0;
            var min = (int)open - Settings.PriceRange;
            var max = (int)open;
            for (var i = 0; i < Settings.PriceSample; i++)
            {
                sum += Generator.Next((int)min, (int)max);
            }
            return Math.Abs(sum / Settings.PriceSample);
        }
        public static double GetStockClose(double low, double high)
        {
            var min = (int)Math.Floor(Math.Min(low, high));
            var max = (int)Math.Ceiling(Math.Max(low, high));
            return Math.Abs((double)Generator.Next(min, max)); 
        }
        public static double GetStockVolume(double preVolume)
        {
            var sum = 0.0;
            //var min = (int)preVolume - Settings.VolumeRange;
            //var max = (int)preVolume + Settings.VolumeRange;
            var min = Settings.VolumeStart - Settings.VolumeRange;
            var max = Settings.VolumeStart + Settings.VolumeRange;

            for (var i = 0; i < Settings.VolumeSample; i++)
            {
                sum += Generator.Next((int)min, (int)max);
            }
            var volume = sum / Settings.VolumeSample;
            return Math.Abs(volume);
        }
         
        #endregion

        /// <summary>
        /// Generate new StockItem based on the passed StockItem
        /// </summary> 
        public static StockItem GenerateDataPoint(StockItem dataPoint)
        {
            var open = GetStockOpen(dataPoint.Close);
            //double open = GetStockOpen(dataPoint.Low, dataPoint.High);
            //double open = GetStockRandom();
            var high = GetStockHigh(open);
            var low = GetStockLow(open);
            var close = GetStockClose(low, high);

            var volume = GetStockVolume(dataPoint.Volume);
            var date = dataPoint.Time.Add(Settings.TimeInterval);
            var index = dataPoint.Index + 1;

            LastDataPoint = new StockItem
            {
                Index = index,
                Time = date,
                Open = open,
                Close = close,
                High = high,
                Low = low,
                Volume = volume
            };
            return LastDataPoint;
        }

        /// <summary>
        /// Generate new StockItem based on the last StockItem in List of StockItem 
        /// </summary> 
        public static StockItem GenerateDataPoint()
        { 
            return GenerateDataPoint(LastDataPoint); 
        }

        /// <summary>
        /// Generate List of StockItem based on the Settings
        /// </summary> 
        public static List<StockItem> GenerateDataItems()
        {
            var data = new List<StockItem>();

            var dataPoint = new StockItem
            {
                Index = -1,
                Close = Settings.PriceStart,
                Volume = Settings.VolumeStart,
                Time = Settings.TimeStart
            };
            RandomValue = Settings.PriceStart;

            for (int i = 0; i < Settings.DataPoints; i++)
            {
                dataPoint = GenerateDataPoint(dataPoint);
                data.Add(dataPoint);
            }
            LastDataPoint = data.Last();
            return data;
        }
         
        #endregion

    }
    
    /// <summary>
    /// Represents a class store settings that will be used to generate stock items
    /// </summary>
    public class LiveDataSettings  
    {
        public LiveDataSettings()
        {
            DataPoints = 100;

            VolumeStart = 15000;
            VolumeRange = 100;
            VolumeSample = 1;

            PriceStart = 1000;
            PriceRange = 20;
            PriceSample = 1;
             
            TimeStart = new DateTime(DateTime.Now.Year, 1, 1);
            TimeInterval = TimeSpan.FromMinutes(1);
            //TimeInterval = TimeSpan.FromDays(1);
        }

        #region Properties
        /// <summary>
        /// Determines range for used to generate random Volume value 
        /// </summary>
        public double VolumeRange { get; set; }
        /// <summary>
        /// Determines number of samples used to generate random Volume values
        /// </summary>
        public int VolumeSample { get; set; }
        /// <summary>
        /// Determines starting Volume value for the first StockMarketDataPoint object
        /// </summary>
        public double VolumeStart { get; set; }

        /// <summary>
        /// Determines range used to generate random Open, High, Low, and Close values
        /// </summary>
        public double PriceRange { get; set; }
        /// <summary>
        /// Determines number of samples used value used to generate random Price values
        /// </summary>
        public int PriceSample { get; set; }
        /// <summary>
        /// Determines starting Price value for the first StockMarketDataPoint object
        /// </summary>
        public double PriceStart { get; set; }

        public int DataPoints { get; set; }
     
        /// <summary>
        /// Determines time intervals between StockMarketDataPoint objects
        /// </summary>
        public TimeSpan TimeInterval { get; set; }
        /// <summary>
        /// Determines starting date value for the first StockMarketDataPoint object
        /// </summary>
        public DateTime TimeStart { get; set; }
        #endregion
    }
   
    
}
