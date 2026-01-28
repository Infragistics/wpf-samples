using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGDataChart.Samples.Data
{
    public partial class BindingDynamicData : SampleContainer
    {
        public BindingDynamicData()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.BtnBindData.Click += OnBindDynamicDataButtonClick;
        }

        private void OnBindDynamicDataButtonClick(object sender, RoutedEventArgs e)
        {
            var dataPoints = (int)this.SldPoints.Value;
            var priceRange = (int)this.SldPriceRange.Value;
            var volumeRange = (int)this.SldVolumeRange.Value;
            double priceStart = (int)this.SldPriceStart.Value;
            double volumeStart = (int)this.SldVolumeStart.Value;

            var data = new DynamicDataCollection
            {
                DataSettings = new DynamicDataSettings
                {
                    DataPoints = dataPoints,
                    PriceStart = priceStart,
                    PriceRange = priceRange,
                    VolumeStart = volumeStart,
                    VolumeRange = volumeRange
                }
            };
            data.Generate();
            this.PriceChart.DataContext = data;
            this.VolumeChart.DataContext = data;
        }
    }
    #region DataModel
    public class DynamicDataCollection : ObservableCollection<DynamicDataPoint>
    {
        public DynamicDataCollection()
        {
            this.DataSettings = new DynamicDataSettings { DataPoints = 100 };
            this.Generate();
        }
        public DynamicDataSettings DataSettings { get; set; }
        public void Generate()
        {
            this.Clear();
            DynamicDataGenerator.DataSettings = this.DataSettings;
            DynamicDataGenerator.Generate();
            foreach (var dp in DynamicDataGenerator.Data)
            {
                this.Add(dp);
            }
        }
    }
    public static class DynamicDataGenerator
    {
        static DynamicDataGenerator()
        {
            Rnd = new Random();
            DataSettings = new DynamicDataSettings();
            Data = GenerateData();
        }
        #region Properties
        public static DynamicDataSettings DataSettings { get; set; }
        public static List<DynamicDataPoint> Data { get; set; }

        #endregion
        #region Fields
        public static Random Rnd;
        #endregion

        #region Methods
        public static DateTime GenerateStockDate(DateTime lastDate)
        {
            return lastDate.Add(DataSettings.DateInterval);
        }
        public static double GenerateStockVolume(double preVolume)
        {
            double sum = 0;
            var min = (int)preVolume - DataSettings.VolumeRange;
            var max = (int)preVolume + DataSettings.VolumeRange;
            for (int i = 0; i < DataSettings.VolumeSample; i++)
            {
                sum += Rnd.Next(min, max);
            }
            return Math.Abs(sum / DataSettings.VolumeSample);
        }
        public static double GenerateStockOpen(double preClose)
        {
            //open value always equals to previous close value
            return preClose;
        }
        public static double GenerateStockHigh(double open)
        {
            double sum = 0;
            var min = (int)open;
            var max = (int)open + DataSettings.PriceRange;
            for (int i = 0; i < DataSettings.PriceSample; i++)
            {
                sum += Rnd.Next(min, max);
            }
            return Math.Abs(sum / DataSettings.PriceSample);
        }
        public static double GenerateStockLow(double open)
        {
            double sum = 0;
            var min = (int)open - DataSettings.PriceRange;
            var max = (int)open;
            for (int i = 0; i < DataSettings.PriceSample; i++)
            {
                sum += Rnd.Next(min, max);
            }
            return Math.Abs(sum / DataSettings.PriceSample);
        }
        public static double GenerateStockClose(double low, double high)
        {
            var min = (int)Math.Floor(Math.Min(low, high));
            var max = (int)Math.Ceiling(Math.Max(low, high));
            return Math.Abs((double)Rnd.Next(min, max));
            // or return (low + high) / 2.0;
        }

        #endregion
        public static void Generate()
        {
            Data = GenerateData();
        }
        /// <summary>
        /// Generate new DynamicDataPoint based on the passed DynamicDataPoint
        /// </summary>
        /// <param name="lastDataPoint"></param>
        /// <returns></returns>
        public static DynamicDataPoint GenerateDataPoint(DynamicDataPoint lastDataPoint)
        {
            double open = GenerateStockOpen(lastDataPoint.Close);
            double high = GenerateStockHigh(open);
            double low = GenerateStockLow(open);
            double close = GenerateStockClose(low, high);

            double volume = GenerateStockVolume(lastDataPoint.Volume);
            DateTime date = lastDataPoint.Date.Add(DataSettings.DateInterval);

            return new DynamicDataPoint
            {
                Date = date,
                Open = open,
                Close = close,
                High = high,
                Low = low,
                Volume = volume
            };
        }
        /// <summary>
        /// Generate new DynamicDataPoint based on the last DynamicDataPoint in List of DynamicDataPoint 
        /// </summary>
        /// <returns></returns>
        public static DynamicDataPoint GenerateDataPoint()
        {
            DynamicDataPoint lastDataPoint = Data[Data.Count - 1];
            lastDataPoint.Item = Data.Count;
            return GenerateDataPoint(lastDataPoint);
        }
        /// <summary>
        /// Generate List of StockMarketDataPoint based on the StockSettings
        /// </summary>
        /// <returns></returns>
        public static List<DynamicDataPoint> GenerateData()
        {
            var data = new List<DynamicDataPoint>();

            var tmpDataPoint = new DynamicDataPoint
            {
                Close = DataSettings.PriceStart,
                Volume = DataSettings.VolumeStart,
                Date = DataSettings.DateStart
            };

            for (int i = 0; i < DataSettings.DataPoints; i++)
            {
                tmpDataPoint = GenerateDataPoint(tmpDataPoint);
                tmpDataPoint.Item = i;
                data.Add(tmpDataPoint);
            }
            return data;
        }

        /// <summary>
        /// Append new StockMarketDataPoint to existing StockMarketData
        /// </summary>
        public static void AppendDataPoint()
        {
            if (Data == null)
                Data = new List<DynamicDataPoint>();

            Data.Add(GenerateDataPoint());
        }

    }
    public class DynamicDataSettings
    {
        public DynamicDataSettings()
        {
            DataPoints = 100;

            VolumeStart = 2000;
            VolumeRange = 100;
            VolumeSample = 5;

            PriceStart = 1000;
            PriceRange = 20;
            PriceSample = 5;

            DateInterval = TimeSpan.FromDays(10);
            DateStart = new DateTime(2010, 1, 1);
        }
        public int DataPoints { get; set; }
        public int VolumeRange { get; set; }
        public int VolumeSample { get; set; }
        public int PriceRange { get; set; }
        public int PriceSample { get; set; }
        public double PriceStart { get; set; }
        public double VolumeStart { get; set; }
        public TimeSpan DateInterval { get; set; }
        public DateTime DateStart { get; set; }
    }
    public class DynamicDataPoint
    {
        public DynamicDataPoint() { }
        public DynamicDataPoint(string date, double open, double high, double low, double close, double volume)
        {
            this.Date = DateTime.Parse(date, CultureInfo.InvariantCulture);
            this.Open = open;
            this.High = high;
            this.Low = low;
            this.Close = close;
            this.Volume = volume;
        }
        #region Properties

        public int Item { get; set; }
        public double Index { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public DateTime Date { get; set; }

        #endregion
    }
    #endregion

}
