using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.DataProviders;
using IGDataChart.Resources;

namespace IGDataChart.Samples.Data
{
    public partial class BindingDataTypes : SampleContainer
    {
        public BindingDataTypes()
        {
            // Initialize the sample after all the data has been loaded.
            InitializeComponent();

            this.Loaded += BindingDataTypesSampleLoaded;
        }

        void BindingDataTypesSampleLoaded(object sender, EventArgs e)
        {
            ViewModel = DataChart.DataContext as DataViewModel;
            
            if (ViewModel == null) return;
            
            ViewModel.DataLoaded += ViewModelDataLoaded;
            ViewModel.LoadData();
        }

        void ViewModelDataLoaded(object sender, EventArgs e)
        {
            CbDataContext.SelectedIndex = 0; 
        }

      
         
        protected DataViewModel ViewModel;
         
        private void OnDataContextSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (this.IsSampleLoaded && ViewModel != null) // This flag is provided by the SampleContainer.
            {
                var cmb = (sender as ComboBox);
                if (cmb == null) return;
                var tag = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag.ToString();

                switch (tag)
                {
                    case "CollectionData":
                        ViewModel.DataType = DataTypes.CollectionData; break;
                    case "EnumerableData":
                         ViewModel.DataType = DataTypes.EnumerableData; break;
                    case "ListData":
                        ViewModel.DataType = DataTypes.ListData; break;
                     case "LinkedListData":
                        ViewModel.DataType = DataTypes.LinkedListData; break;
                    case "QueueData":
                         ViewModel.DataType = DataTypes.QueueData; break;
                    case "StackData":
                        ViewModel.DataType = DataTypes.StackData; break;
                    default:
                         ViewModel.DataType = DataTypes.CollectionData; break;
                    // Break necessary on default.
                }
            }
        }

        private void OnPrevDataContextButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.CbDataContext.SelectedIndex == 0)
                this.CbDataContext.SelectedIndex = this.CbDataContext.Items.Count - 1;
            else
                this.CbDataContext.SelectedIndex -= 1;
        }

        private void OnNextDataContextButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.CbDataContext.SelectedIndex = (this.CbDataContext.SelectedIndex + 1) % this.CbDataContext.Items.Count;
        }
    }

    #region ViewModel
    public class DataViewModel : INotifyPropertyChanged
    {
        public DataViewModel()
        {
            CollectionData = new CollectionData();
            EnumerableData = new EnumerableData();
            ListData = new ListData();
            LinkedListData = new LinkedListData();
            QueueData = new QueueData();
            StackData = new StackData();

            _xmlProvider = new XmlDataProvider();
            _xmlProvider.GetXmlDataCompleted += OnGetXmlDataCompleted;


            _dataType = DataTypes.CollectionData;
          
        }
        
        public void LoadData()
        {
            _xmlProvider.GetXmlDataAsync("DJIA.xml");
           
        }
        
        public event EventHandler DataLoaded = delegate { };
    
        private readonly XmlDataProvider _xmlProvider;
        
        private void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            var doc = e.Result;

            var data = (from value in doc.Descendants("StockQuote")
                     select new SkinnyStockMarketDataPoint
                     {
                         Date = value.Element("Date").GetDateTime(),
                         Value = value.Element("Value").GetDouble()
                     }).ToList<SkinnyStockMarketDataPoint>();

            CollectionData.GenerateData(data);
            EnumerableData.GenerateData(data);
            ListData.GenerateData(data);
            LinkedListData.GenerateData(data);
            QueueData.GenerateData(data);
            StackData.GenerateData(data);

            DataType = DataTypes.CollectionData;

            if (this.DataLoaded != null)
            {
                this.DataLoaded(this, EventArgs.Empty);
            }
        }

        public CollectionData CollectionData { get; set; }
        public EnumerableData EnumerableData { get; set; }
        public ListData ListData { get; set; }
        public LinkedListData LinkedListData { get; set; }
        public QueueData QueueData { get; set; }
        public StackData StackData { get; set; }

        private IEnumerable<SkinnyStockMarketDataPoint> _dataItems;
        public IEnumerable<SkinnyStockMarketDataPoint> DataItems
        {
            get { return _dataItems; }
            set
            {
                _dataItems = value;
                this.OnPropertyChanged("DataItems");
            }
        }

        private DataTypes _dataType;
        public DataTypes DataType
        {
            get { return _dataType; }
            set
            {
                _dataType = value;
                OnDataTypeChanged();
                this.OnPropertyChanged("DataType");
            }
        }

        private string _dataSubtitle;
        public string DataSubtitle
        {
            get { return _dataSubtitle; }
            set
            {
                _dataSubtitle = value;
                this.OnPropertyChanged("DataSubtitle");
            }
        }
        
        private void OnDataTypeChanged()
        {
            _dataItems = null;
            switch (DataType)
            {
                case DataTypes.CollectionData:
                    DataItems = CollectionData;
                    DataSubtitle = DataChartStrings.XWDC_BindingDataTypes_ProgressiveEra + CollectionData.DataRange;
                    break;
                case DataTypes.EnumerableData:
                    DataItems = EnumerableData;
                    DataSubtitle = DataChartStrings.XWDC_BindingDataTypes_RoaringTwenties + EnumerableData.DataRange;
                   break;
                case DataTypes.ListData:
                    DataItems = ListData;
                    DataSubtitle = DataChartStrings.XWDC_BindingDataTypes_GreatDepression + ListData.DataRange;
                   break;
                case DataTypes.LinkedListData:
                    DataItems = LinkedListData;
                    DataSubtitle = DataChartStrings.XWDC_BindingDataTypes_WW2 + LinkedListData.DataRange;
                   break;
                case DataTypes.QueueData:
                    DataItems = QueueData;
                    DataSubtitle = DataChartStrings.XWDC_BindingDataTypes_PostwarProsperity + QueueData.DataRange;
                   break;
                case DataTypes.StackData:
                    DataItems = StackData;
                    DataSubtitle = DataChartStrings.XWDC_BindingDataTypes_Deregulation + StackData.DataRange;
                   break;
            }
        }
       
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    #endregion
   
    #region DataModels
    // ----------------------------------------------------------------------
    public enum DataTypes
    {
        EnumerableData,
        CollectionData,
        ListData,
        LinkedListData,
        QueueData,
        StackData,
    }
    // ----------------------------------------------------------------------
    public class CollectionData : ObservableCollection<SkinnyStockMarketDataPoint>
    {
        public string DataRange { get; private set; }
        
        private readonly DateTime _start;
        private readonly DateTime _end;

        public CollectionData()
        {
            // Progressive Era
            _start = new DateTime(1896, 1, 31);
            _end = new DateTime(1919, 12, 31);

           DataRange = ": " + _start.Year + " - " + _end.Year;
        }
        public void GenerateData(List<SkinnyStockMarketDataPoint> dataPoints)
        {
            //var dataPoints = DJIADataProvider.GetDJIAData(Start, End);
            foreach (SkinnyStockMarketDataPoint dp in dataPoints)
            {
                if (dp.Date >= _start && dp.Date <= _end)
                {
                    this.Add(dp);
                }  
           
            }
        }
    }
    // ----------------------------------------------------------------------
    public class EnumerableData : IEnumerable<SkinnyStockMarketDataPoint>, INotifyCollectionChanged
    {
        public string DataRange { get; private set; }

        private readonly DateTime _start;
        private readonly DateTime _end;

        public EnumerableData()
        {
            _items = new ObservableCollection<SkinnyStockMarketDataPoint>();
            _items.CollectionChanged += OnItemsCollectionChanged;
            
            // Roaring Twenties
            _start = new DateTime(1920, 1, 31);
            _end  = new DateTime(1929, 9, 30);

            DataRange = ": " + _start.Year + " - " + _end.Year;
        }

        public void GenerateData(List<SkinnyStockMarketDataPoint> dataPoints)
        {
            foreach (SkinnyStockMarketDataPoint dp in dataPoints)
            {
                if (dp.Date >= _start && dp.Date <= _end)
                {
                    this.Add(dp);
                }  
            }
        }
        private readonly ObservableCollection<SkinnyStockMarketDataPoint> _items;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        #region Methods
        public void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(sender, e);
            }
        }
        public void Add(SkinnyStockMarketDataPoint item)
        {
            _items.Add(item);
        }
        public void Remove(SkinnyStockMarketDataPoint item)
        {
            _items.Remove(item);
        }
        public void Clear()
        {
            _items.Clear();
        }
        public IEnumerator<SkinnyStockMarketDataPoint> GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        #endregion

    }
    // ----------------------------------------------------------------------
    public class ListData : List<SkinnyStockMarketDataPoint>
    {
        public string DataRange { get; private set; }

        private readonly DateTime _start;
        private readonly DateTime _end;
        
        public ListData()
        {
            // Great Depression
            _start = new DateTime(1929, 10, 31);
            _end = new DateTime(1940, 12, 31);

            DataRange = ": " + _start.Year + " - " + _end.Year;
        }

        public void GenerateData(List<SkinnyStockMarketDataPoint> dataPoints)
        {
            foreach (SkinnyStockMarketDataPoint dp in dataPoints)
            {
                if (dp.Date >= _start && dp.Date <= _end)
                {
                    this.Add(dp);
                }  
            }
        }
    }
    // ----------------------------------------------------------------------
    public class LinkedListData : LinkedList<SkinnyStockMarketDataPoint>
    {
        public string DataRange { get; private set; }

        private readonly DateTime _start;
        private readonly DateTime _end;
        
        public LinkedListData()
        {
            _start = new DateTime(1941, 1, 31);
            _end = new DateTime(1945, 5, 31);

            DataRange = ": " + _start.Year + " - " + _end.Year;
        }
        public void GenerateData(List<SkinnyStockMarketDataPoint> dataPoints)
        {
            foreach (SkinnyStockMarketDataPoint dp in dataPoints)
            {
                if (dp.Date >= _start && dp.Date <= _end)
                {
                    this.AddLast(dp);
                }  
            }
        }
    }
    // ----------------------------------------------------------------------
    public class QueueData : Queue<SkinnyStockMarketDataPoint>
    {
        public string DataRange { get; private set; }

        private readonly DateTime _start;
        private readonly DateTime _end;
        
        public QueueData()
        {
            // Postwar Prosperity
            _start = new DateTime(1945, 6, 30);
            _end = new DateTime(1975, 12, 31);

            DataRange = ": " + _start.Year + " - " + _end.Year;
        }

        public void GenerateData(List<SkinnyStockMarketDataPoint> dataPoints)
        {
            foreach (SkinnyStockMarketDataPoint dp in dataPoints)
            {
                if (dp.Date >= _start && dp.Date <= _end)
                {
                    this.Enqueue(dp);
                }
            }
        }
    }
    // ----------------------------------------------------------------------
    public class StackData : Stack<SkinnyStockMarketDataPoint>
    {
        public string DataRange { get; set; }

        private readonly DateTime _start;
        private readonly DateTime _end;

        public StackData()
        {
            // Deregulation and Reaganomics
            _start = new DateTime(1976, 1, 31);
            _end = new DateTime(1992, 12, 31);

            DataRange = ": " + _start.Year + " - " + _end.Year;
        }

        public void GenerateData(List<SkinnyStockMarketDataPoint> dataPoints)
        {
            //var dataPoints = DJIADataProvider.GetDJIAData(Start, End);
            foreach (SkinnyStockMarketDataPoint dp in dataPoints)
            {
                if (dp.Date >= _start && dp.Date <= _end)
                {
                    this.Push(dp);
                }  
            }
        }
    }
    // ----------------------------------------------------------------------
    public class SkinnyStockMarketDataPoint
    {
        public SkinnyStockMarketDataPoint()
        {
            Date = new DateTime();
            Value = -1;
        }

        public DateTime Date { get; set; }
        public double Value { get; set; }
    }   
    #endregion
}