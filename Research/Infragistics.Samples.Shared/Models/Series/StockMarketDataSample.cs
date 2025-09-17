using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

 
namespace Infragistics.Samples.Shared.Models
{
    public class StockMarketDataSample : StockMarketDataCollection
    {
        
    }
    public class StockMarketDataCollection : ObservableCollection<StockMarketDataPoint>
    {
        public StockMarketDataCollection()
        {
            _stockSettings = new StockMarketSettings();
            this.Generate();
        }
        public void Generate()
        {
            this.Clear();
            StockMarketGenerator.Settings = this.Settings;
            StockMarketGenerator.Generate();
            foreach (var dp in StockMarketGenerator.Data)
            {
                this.Add(dp);
            }

            LastItem = this[this.Count - 1];
        }

        private StockMarketDataPoint _lastItem;
        public StockMarketDataPoint LastItem
        {
            get { return _lastItem; }
            set { _lastItem = value; OnPropertyChanged(new PropertyChangedEventArgs("LastItem")); }
        }

        private StockMarketSettings _stockSettings;
       

        /// <summary>
        /// Settings used to generate StockMarketDataPoint objects
        /// </summary>
        public StockMarketSettings Settings
        {
            get { return _stockSettings; }
            set
            {
                _stockSettings = value;
                this.Generate();
                OnPropertyChanged(new PropertyChangedEventArgs("Settings"));
            }
        }

    }
    public class StockMarketSettings : DataSettings
    {
        public StockMarketSettings()
        {
            DataPoints = 100;

            VolumeStart = 2000;
            VolumeRange = 500;
            VolumeSample = 4;

            PriceStart = 500;
            PriceRange = 50;
            PriceSample = 4;
             
            DateStart = new DateTime(2010, 1, 1);
            DateInterval = TimeSpan.FromDays(1);

            IncludeWeekands = false;
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

        /// <summary>
        /// Determines time intervals between StockMarketDataPoint objects
        /// </summary>
        public TimeSpan DateInterval { get; set; }
        /// <summary>
        /// Determines starting date value for the first StockMarketDataPoint object
        /// </summary>
        public DateTime DateStart { get; set; }

        public bool IncludeWeekands { get; set; }
        #endregion
    }
    public class StockMarketDataPoint : ObservableModel
    {
        #region Constructors
        public StockMarketDataPoint()
            : this(new DateTime(2010, 1, 1), 100, 120, 90, 110, 1000)
        { }

        public StockMarketDataPoint(DateTime date, double open, double high, double low, double close, double volume)
        {
            _date = date;
            _open = open;
            _high = high;
            _low = low;
            _close = close;
            _volume = volume;
        }
        public StockMarketDataPoint(string date, double open, double high, double low, double close, double volume)
            : this(DateTime.Parse(date, System.Globalization.CultureInfo.InvariantCulture),
                open, high, low, close, volume)
        { }

        public StockMarketDataPoint(StockMarketDataPoint dataPoint)
        {
            _date = dataPoint.Date;
            _open = dataPoint.Open;
            _high = dataPoint.High;
            _low = dataPoint.Low;
            _close = dataPoint.Close;
            _volume = dataPoint.Volume;
            _index = dataPoint.Index;
        }
        #endregion

        #region Properties
        private double _open;
        private double _high;
        private double _low;
        private double _close;
        private double _volume;
        private DateTime _date;
        private int _index;
        private string _label;

        public int Index { get { return _index; } set { if (_index == value) return; _index = value; OnPropertyChanged("Index"); } }
        public double Open { get { return _open; } set { if (_open == value) return; _open = value; OnPropertyChanged("Open"); } }
        public double High { get { return _high; } set { if (_high == value) return; _high = value; OnPropertyChanged("High"); } }
        public double Low { get { return _low; } set { if (_low == value) return; _low = value; OnPropertyChanged("Low"); } }
        public double Close { get { return _close; } set { if (_close == value) return; _close = value; OnPropertyChanged("Close"); } }
        public double Volume { get { return _volume; } set { if (_volume == value) return; _volume = value; OnPropertyChanged("Volume"); } }
        public DateTime Date { get { return _date; } set { if (_date == value) return; _date = value; OnPropertyChanged("Date"); } }

        public string Category { get { return _label; } set { if (_label == value) return; _label = value; OnPropertyChanged("Category"); } }




        /// <summary>
        /// returns the difference between Close and Open values
        /// </summary>
        public double Change
        {
            get { return Close - Open; }
        }
        public double ChangePerCent
        {
            get { return (Change / Open) * 100; }
        }
        #endregion

        public new string ToString()
        {
            return String.Format(
                "Index {0}, Open {1}, High {2}, Low {3}, Close {4}, Change {5}, Volume {6}, Date {7}",
                Index, Open, High, Low, Close, Change, Volume, Date);
        }

        internal StockMarketDataPoint Clone()
        {
            var item = new StockMarketDataPoint();
            item.Date = this.Date;
            item.Open = this.Open;
            item.High = this.High;
            item.Low = this.Low;
            item.Close  = this.Close;
            item.Volume = this.Volume;
            return item;
        }
    }
    public static class StockMarketGenerator
    {
        static StockMarketGenerator()
        { 
            Settings = new StockMarketSettings();
            Data = GenerateData();
        }
        #region Properties
        public static StockMarketSettings Settings { get; set; }
        public static List<StockMarketDataPoint> Data { get; set; }
        public static StockMarketDataPoint LastDataPoint { get; set; }

        #endregion
       
        #region Methods
        public static void Generate()
        {
            Data = GenerateData();
        }
         
        /// <summary>
        /// Generate new StockMarketDataPoint based on the passed StockMarketDataPoint
        /// </summary>  
        public static StockMarketDataPoint GenerateDataPoint(StockMarketDataPoint dataPoint)
        {
            DateTime date;
            if (dataPoint == null)            
                date = Settings.DateStart;             
            else
                date = dataPoint.Date.Add(Settings.DateInterval);

            if (Settings.IncludeWeekands &&
                (date.DayOfWeek == DayOfWeek.Sunday ||
                 date.DayOfWeek == DayOfWeek.Saturday))
            {
                LastDataPoint = dataPoint.Clone();
                LastDataPoint.Date = date;
            }
            else
            {                
                double o = 0;
                double h = 0;
                double l = 0;
                double c = 0;
                double v = 0;

                if (dataPoint == null)
                {
                     o = Settings.PriceStart; 
                     c = Settings.PriceStart; 
                     v = Settings.VolumeStart;                       
                }
                else
                { 
                    o = dataPoint.Open; 
                    c = dataPoint.Close;  
                    v = dataPoint.Volume;
                }                    
                   
                o = c + ((rand.NextDouble() - 0.5) * Settings.PriceRange);
                h = o + (rand.NextDouble() * 1.001 * Settings.PriceRange);
                l = o - (rand.NextDouble() * Settings.PriceRange);
                c = l + (rand.NextDouble() * (h - l));
                v = v + ((rand.NextDouble() - 0.5) * Settings.VolumeRange);
                        
                LastDataPoint = new StockMarketDataPoint();
                LastDataPoint.Date = date;
                LastDataPoint.Open = o;
                LastDataPoint.High = h;
                LastDataPoint.Low = l;
                LastDataPoint.Close = c;
                LastDataPoint.Volume = v;
            }             

            return LastDataPoint; 
        } 

        public static Random rand = new Random();
        /// <summary>
        /// Generate List of StockMarketDataPoint based on the Settings
        /// </summary>
        /// <returns></returns>
        public static List<StockMarketDataPoint> GenerateData()
        {
            var data = new List<StockMarketDataPoint>(); 

            var date =  Settings.DateStart;
            var end = Settings.DateStart;
            for (int i = 0; i < Settings.DataPoints; i++)
            {
                end = end.Add(Settings.DateInterval);              
            }
             
            var v = Settings.VolumeStart;
            var o = Settings.PriceStart;
            var h = o + (rand.NextDouble() * 10);
            var l = o - (rand.NextDouble() * 10);
            var c = l + (rand.NextDouble() * (h - l));
             
            while (date.Ticks < end.Ticks)
            { 
                if (Settings.IncludeWeekands && data.Count > 0 &&
                   (date.DayOfWeek == DayOfWeek.Sunday ||
                    date.DayOfWeek == DayOfWeek.Saturday))
                {
                    var previous = data[data.Count - 1];
                    var stock = previous.Clone();                    
                    stock.Date = date;
                    
                    data.Add(stock);  
                }
                else                
                {
                    var stock = new StockMarketDataPoint();
                    stock.Date = date;
                    stock.Open = o;
                    stock.High = h;
                    stock.Low = l;
                    stock.Close = c;
                    stock.Volume = v;
                    
                    data.Add(stock);
                
                    o = c + ((rand.NextDouble() - 0.5) * Settings.PriceRange);
                    h = o + (rand.NextDouble() * 1.001 * Settings.PriceRange);
                    l = o - (rand.NextDouble() * Settings.PriceRange);
                    c = l + (rand.NextDouble() * (h - l));
                    v = v + ((rand.NextDouble() - 0.5) * 300);
                } 
                date = date.Add(Settings.DateInterval);
            }
            return data;
        }
         

        #endregion

    }
}