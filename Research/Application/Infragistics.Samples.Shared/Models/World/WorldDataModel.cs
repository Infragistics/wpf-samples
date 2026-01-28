using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;
using Infragistics.Samples.Shared.DataProviders;
using XmlDataProvider = Infragistics.Samples.Shared.DataProviders.XmlDataProvider;

namespace Infragistics.Samples.Shared.Models
{

    public class WorldData : ObservableModel
    {
        public WorldData()
        {
            _source = new List<CountryDataModel>();
            DataProvider = new WorldDataProvider();
            DataProvider.GetWorldDataCompleted += OnGetWorldDataCompleted;
            DataProvider.GetWorldDataAsync();
        }
        private List<CountryDataModel> _source = new List<CountryDataModel>();
        public List<CountryDataModel> DataSource
        {
            get { return _source; }
            protected set { _source = value; OnPropertyChanged("DataSource"); OnDataLoaded(); }
        }
        

        protected readonly WorldDataProvider DataProvider;
        private void OnGetWorldDataCompleted(object sender, GetWorldDataCompletedEventArgs e)
        {
            var data = e.Result;
            // sort country data points based on GdpPerCapita value 
            data.Sort(delegate(CountryDataModel n1, CountryDataModel n2)
            {
                if (n1.GdpPerCapita == n2.GdpPerCapita) return 0;
                if (n1.GdpPerCapita > n2.GdpPerCapita) return -1;
                return 1;
            });
            DataSource = data;
        }
        public event EventHandler<EventArgs> DataLoaded;
        public void OnDataLoaded()
        {
            if (this.DataLoaded != null)
            {
                this.DataLoaded(this, EventArgs.Empty);
            }
        }
    }
    public class SortableCollectionViewSource : CollectionViewSource
    {
        private string _sortByColumnName = string.Empty;
        public string SortingProperty
        {
            get { return _sortByColumnName; }
            set { if (_sortByColumnName == value) return; _sortByColumnName = value; OnSortChanged(); }
        }
        private ListSortDirection _sortingDirection = ListSortDirection.Descending;
        public ListSortDirection SortingDirection
        {
            get { return _sortingDirection; }
            set { if (_sortingDirection == value) return; _sortingDirection = value; OnSortChanged(); }
        }
        protected void OnSortChanged()
        {
            Sort();
        }
        public void Sort()
        {
            if (Source != null && SortingProperty != string.Empty)
            {
                this.SortDescriptions.Clear();
                this.SortDescriptions.Add(new SortDescription(SortingProperty, SortingDirection));
            }
        }
    }
    public class WorldDataViewSource : SortableCollectionViewSource 
    {
        public WorldDataViewSource()
        {
            SortingProperty = "Population";
            SortingDirection = ListSortDirection.Ascending;
            DataProvider = new WorldDataProvider();
            DataProvider.GetWorldDataCompleted += OnGetWorldDataCompleted;
            DataProvider.GetWorldDataAsync();
        }
       
        protected readonly WorldDataProvider DataProvider;
        private void OnGetWorldDataCompleted(object sender, GetWorldDataCompletedEventArgs e)
        {
            var data = e.Result;
            // sort country data points based on GdpPerCapita value 

            this.Source = data;
            this.Sort();
            OnDataLoaded();
        }
     
        public event EventHandler<EventArgs> DataLoaded;
        public void OnDataLoaded()
        {
            if (this.DataLoaded != null)
                this.DataLoaded(this, EventArgs.Empty);
        }
        protected WorldData WorldData;
        //private object _source;
        //public new object Source
        //{
        //    get { return _source; }
        //    set { if (_source == value) return; _source = value; OnSourceChanged(); }
        //}
        //public void OnSourceChanged()
        //{
        //    base.Source = Source;
        //    View.Refresh();
        //    //OnPropertyChanged("Source");
        //}
       
    }

    public class ObservableCollectionViewSource : ObservableModel
    {
        public ObservableCollectionViewSource()
        {
            ViewSource = new CollectionViewSource();
            ViewSource.Filter += this.Filter;
        }
        protected CollectionViewSource ViewSource = new CollectionViewSource();

        private object _source;
        public object Source
        {
            get { return _source; }
            set { if (_source == value) return; _source = value; OnSourceChanged(); }
        }

        public ICollectionView View { get { return ViewSource.View; } }

        public void OnSourceChanged()
        {
            ViewSource.Source = Source;
            ViewSource.View.Refresh();
            OnPropertyChanged("Source");
        }

        public event FilterEventHandler Filter;


    }
   
    //TODO check usage in samples and remove
    public class WorldDataModel : ObservableModel
    {
        /// <summary>
        /// The default constructor for the WorldDataModel class.
        /// </summary>
        public WorldDataModel()
        {
            CountryDataRecords = new List<CountryDataModel>();
            CountryDataRecords.Add(new CountryDataModel() { CountryCode = "JA", BirthRate = 10 });
            var worldInfoDataProvider = new XmlDataProvider();
            worldInfoDataProvider.GetXmlDataCompleted += OnGetXmlDataCompleted;
            worldInfoDataProvider.GetXmlDataAsync("CountryData.xml");
        }

        /// <summary>
        /// Handles the GetXmlDataCompletedEventArgs and parses the country data.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">A GetXmlDataCompletedEventArgs object.</param>
        private void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) MessageBox.Show(e.Error.Message);

            XDocument document = e.Result; 

            CountryDataRecords = document.Element("NewDataSet")
                .Elements("Table")
                .Select(element => new CountryDataModel()
                {
                    CountryCode = element.Element("CountryCode").Value,
                    CountryName = element.Element("CountryName").Value,
                    BirthRate = ConvertToDouble(element.Element("BirthRate")),
                    ElectricityProduction = ConvertToDouble(element.Element("ElectricityProduction")),
                    InternetUsers = ConvertToDouble(element.Element("InternetUsers")),
                    MedianAge = ConvertToDouble(element.Element("MedianAge")),
                    OilProduction = ConvertToDouble(element.Element("OilProduction")),
                    Population = ConvertToDouble(element.Element("Population")),
                    PublicDebt = ConvertToDouble(element.Element("PublicDebt")),
                    Televisions = ConvertToDouble(element.Element("Televisions")),
                    UnemploymentRate = ConvertToDouble(element.Element("UnemploymentRate"))
                }).ToList();

            OnPropertyChanged("CountryDataRecords");
        }

        /// <summary>
        /// Converts an XElement's value to a double.
        /// </summary>
        /// <param name="element">The XElement.</param>
        /// <returns>The value of the XElement as double.</returns>
        private double ConvertToDouble(XElement element)
        {
            return element == null ? 0 : Convert.ToDouble(element.Value);
        }

        /// <summary>
        /// A collection with the Country Records.
        /// </summary>
        public List<CountryDataModel> CountryDataRecords { get; set; }

        /// <summary>
        /// A collection with the Filter names
        /// </summary>
        public Dictionary<string, string> Filters { get; set; }
    }  
}
