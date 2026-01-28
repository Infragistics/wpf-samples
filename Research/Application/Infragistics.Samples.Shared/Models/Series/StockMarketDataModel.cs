using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace Infragistics.Samples.Shared.Models
{
    public class StockMarketDataModel : ObservableModel
    {
        public StockMarketDataModel()
        {
            _stockSettings = new StockMarketSettings();
            _data = new StockMarketDataCollection();
        }
        public void Generate()
        {
            _data.Settings = this.Settings;
            _data.Generate();
        }
        #region Properties
        private StockMarketDataCollection _data;
        public StockMarketDataCollection Data
        {
            get { return _data; }
            set
            {
                if (_data == value) return;
                _data = value;
                OnPropertyChanged("Data");
            }
        }
   
        /// <summary>
        /// Settings used to generate StockMarketDataPoint objects
        /// </summary>
        public StockMarketSettings Settings { get { return _stockSettings; } set { _stockSettings = value; OnPropertyChanged("Settings"); } }
        private StockMarketSettings _stockSettings;
     

        /// <summary>
        /// Determines number of StockMarketDataPoint objects that will be generated 
        /// </summary>
        public int DataPoints
        {
            get { return _stockSettings.DataPoints; }
            set { _stockSettings.DataPoints = value; OnPropertyChanged("DataPoints"); }
        }

        public double PriceStart
        {
            get { return _stockSettings.PriceStart; }
            set { _stockSettings.PriceStart = value; OnPropertyChanged("PriceStart"); }
        }
        /// <summary>
        /// Determines range for Open, High, Low, Close price values
        /// </summary>
        public double PriceRange
        {
            get { return _stockSettings.PriceRange; }
            set { _stockSettings.PriceRange = value; OnPropertyChanged("PriceRange"); }
        }
        /// <summary>
        /// Determines sample used to generate random Price values
        /// </summary>
        public int PriceSample
        {
            get { return _stockSettings.PriceSample; }
            set { _stockSettings.PriceSample = value; OnPropertyChanged("PriceSample"); }
        }


        /// <summary>
        /// Determines range for used to generate random Volume value 
        /// </summary>
        public double VolumeRange
        {
            get { return _stockSettings.VolumeRange; }
            set { _stockSettings.VolumeRange = value; OnPropertyChanged("VolumeRange"); }
        }
        /// <summary>
        /// Determines number of samples used to generate random Volume values
        /// </summary>
        public int VolumeSample
        {
            get { return _stockSettings.VolumeSample; }
            set { _stockSettings.VolumeSample = value; OnPropertyChanged("VolumeSample"); }
        }
        /// <summary>
        /// Determines starting Volume value for the first StockMarketDataPoint object
        /// </summary>
        public double VolumeStart
        {
            get { return _stockSettings.VolumeStart; }
            set { _stockSettings.VolumeStart = value; OnPropertyChanged("VolumeStart"); }
        }

        /// <summary>
        /// Determines time intervals between StockMarketDataPoint objects
        /// </summary>
        public TimeSpan DateInterval
        {
            get { return _stockSettings.DateInterval; }
            set { _stockSettings.DateInterval = value; OnPropertyChanged("DateInterval"); }
        }
        /// <summary>
        /// Determines starting date value for the first StockMarketDataPoint object
        /// </summary>
        public DateTime DateStart
        {
            get { return _stockSettings.DateStart; }
            set { _stockSettings.DateStart = value; OnPropertyChanged("DateStart"); }
        }

        #endregion

    }
  
    public class StockMarketDataObservableCollection : ObservableModel, IEnumerable<StockMarketDataPoint>
    {
        public StockMarketDataObservableCollection()
        {
            _stockSettings = new StockMarketSettings();
            _items = new ObservableCollection<StockMarketDataPoint>();
            _items.CollectionChanged += OnItemsCollectionChanged;
           
            this.Generate();
        }
        private readonly ObservableCollection<StockMarketDataPoint> _items;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public void Generate()
        {
            this.Clear();
            StockMarketGenerator.Settings = this.Settings;
            StockMarketGenerator.Generate();
            foreach (StockMarketDataPoint dp in StockMarketGenerator.Data)
            {
                this.Add(dp);
            }
        }
        #region Methods
       
        public void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(sender, e);
            }
        }
        public void Add(StockMarketDataPoint item)
        {
            _items.Add(item);
            OnItemsCollectionChanged(this, new NotifyCollectionChangedEventArgs(new NotifyCollectionChangedAction()));
            OnPropertyChanged("Settings");
        }
        public void Remove(StockMarketDataPoint item)
        {
            _items.Remove(item);
        }
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }
        public void Clear()
        {
            _items.Clear();
        }
        public IEnumerator<StockMarketDataPoint> GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        #endregion

        private StockMarketSettings _stockSettings;
        /// <summary>
        /// Settings used to generate StockMarketDataPoint objects
        /// </summary>
        public StockMarketSettings Settings 
        { 
            get { return _stockSettings; } 
            set { _stockSettings = value; OnPropertyChanged("Settings"); } 
        }

   
    }


}
