using System;
using System.Collections.Generic;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Models
{
    /// <summary>
    /// Observable Collection of High Volume Data Points
    /// </summary>
    public class HighVolumeDataCollection : List<HighVolumeDataPoint>
    { }
    /// <summary>
    /// Represents High Volume Data Point without implementation of INotifyPropertyChanged
    /// </summary>
    public class HighVolumeDataPoint
    {
        #region Constructors
        public HighVolumeDataPoint(DateTime date, double value)
        {
            Date = date;
            Value = value;
        }

        public HighVolumeDataPoint()
            : this(new DateTime(2010, 1, 1), 1000)
        { }

        public HighVolumeDataPoint(string date, double value)
            : this(DateTime.Parse(date, System.Globalization.CultureInfo.InvariantCulture), value)
        { }

        public HighVolumeDataPoint(HighVolumeDataPoint dataPoint)
        {
            Date = dataPoint.Date;
            Value = dataPoint.Value;
            Index = dataPoint.Index;
        }
        public HighVolumeDataPoint Clone()
        {
            return new HighVolumeDataPoint(this);
        }
        #endregion

        #region Properties

        public int Index { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }

        #endregion

    }

    /// <summary>
    /// Represents settings for generating High Volume Data
    /// </summary>
    public class HighVolumeDataSettings : ObservableModel
    {
        public HighVolumeDataSettings()
        {
            DataPoints = 50;

            ValueStart = 2000;
            ValueRange = 50;
            ValueSample = 4;

            DateInterval = TimeSpan.FromSeconds(1);
            DateStart = new DateTime(2010, 1, 1);
        }

        #region Properties
        private int _dataPoints;
        public int DataPoints
        {
            get { return _dataPoints; }
            set { _dataPoints = value; OnPropertyChanged("DataPoints"); }
        }

        /// <summary>
        /// Determines range for used to generate random value 
        /// </summary>
        public int ValueRange { get; set; }
        /// <summary>
        /// Determines number of samples used to generate random value
        /// </summary>
        public int ValueSample { get; set; }
        /// <summary>
        /// Determines starting Value value for the first High Volume Data Point 
        /// </summary>
        public double ValueStart { get; set; }

        /// <summary>
        /// Determines time intervals between High Volume Data Point s
        /// </summary>
        public TimeSpan DateInterval { get; set; }
        /// <summary>
        /// Determines starting date value for the first High Volume Data Point 
        /// </summary>
        public DateTime DateStart { get; set; }
        #endregion
    }
    /// <summary>
    /// Generates data points for High Volume Data
    /// </summary>
    public static class HighVolumeDataGenerator
    {
        static HighVolumeDataGenerator()
        {
            Generator = new Random();
            Settings = new HighVolumeDataSettings();
            Data = GenerateData();
        }
        public static readonly Random Generator;
        #region Properties
        public static HighVolumeDataSettings Settings { get; set; }
        public static List<HighVolumeDataPoint> Data { get; set; }
        public static HighVolumeDataPoint LastDataPoint { get; set; }

        #endregion
        public static void Generate()
        {
            Data = GenerateData();
        }
        public static List<HighVolumeDataPoint> GenerateData(HighVolumeDataSettings newSettings)
        {
            Settings = newSettings;
            return GenerateData();
        }

        public static List<HighVolumeDataPoint> GenerateData()
        {
            // first data point
            HighVolumeDataPoint dataPoint = new HighVolumeDataPoint
            {
                Index = 0,
                Value = Settings.ValueStart,
                Date = Settings.DateStart
            };
            List<HighVolumeDataPoint> data = new List<HighVolumeDataPoint>();
            for (int i = 0; i < Settings.DataPoints; i++)
            {
                data.Add(dataPoint);
                dataPoint = GenerateDataPoint(dataPoint);
            }
            return data;
        }
        public static List<HighVolumeDataPoint> GenerateData(HighVolumeDataPoint dataPoint)
        {
            List<HighVolumeDataPoint> data = new List<HighVolumeDataPoint>();
            for (int i = 0; i < Settings.DataPoints; i++)
            {
                data.Add(dataPoint);
                dataPoint = GenerateDataPoint(dataPoint);
            }
            return data;
        }
        public static HighVolumeDataPoint GenerateDataPoint()
        {
            HighVolumeDataPoint newDataPoint = GenerateDataPoint(LastDataPoint);
            return newDataPoint;
        }

        public static HighVolumeDataPoint GenerateDataPoint(HighVolumeDataPoint dataPoint)
        {

            DateTime date = dataPoint.Date.Add(Settings.DateInterval);
            int index = dataPoint.Index + 1;

            double value = dataPoint.Value;
            if (Generator.NextDouble() > .5)
            {
                value += Generator.NextDouble();
            }
            else
            {
                value -= Generator.NextDouble();
            }

            LastDataPoint = new HighVolumeDataPoint
            {
                Index = index,
                Date = date,
                Value = value
            };
            return LastDataPoint;
        }

    }

}