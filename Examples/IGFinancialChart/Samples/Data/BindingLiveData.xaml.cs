using Infragistics.Samples.Framework;
using System; 
using System.Collections.ObjectModel; 
using System.Windows; 

namespace IGFinancialChart.Samples.Data
{ 
    public partial class BindingLiveData : SampleContainer
    { 
        public BindingLiveData()
        {
            InitializeComponent();
                     
           
            this.Loaded += OnSampleLoaded;
            this.Unloaded += OnSampleUnloaded;
        }

        protected LiveDataService DataService;
        protected ObservableCollection<StockItem> DataSource;

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            Chart.IsToolbarVisible = false;

            DataService = new LiveDataService();
            // configuring data service
            DataService.UpdateInterval = TimeSpan.FromSeconds(0.1);
            DataService.Settings.TimeInterval = TimeSpan.FromMinutes(1);
            // handling events of data service
            DataService.DataAdded += OnDataAdded;
            DataService.DataSetup += OnDataSetup;
            DataService.Start(); 
        }

        private void OnSampleUnloaded(object sender, RoutedEventArgs e)
        {
            DataService.Stop();
        }

        private void OnDataSetup(object sender, LiveDataServiceEventArgs e)
        { 
            // setting initial data binding
            this.DataSource = e.DataItems; 
            this.Chart.ItemsSource = DataSource;
        }

        private void OnDataAdded(object sender, LiveDataServiceEventArgs e)
        { 
            foreach (var item in e.DataItems)
            {
                // remove the first data item such that data source has 
                // the same number of data points
                if (DataSource.Count >= 100)
                    DataSource.RemoveAt(0);
                
                // append a new data item
                DataSource.Add(item);  
            }   
        } 
    }
    
 
}
