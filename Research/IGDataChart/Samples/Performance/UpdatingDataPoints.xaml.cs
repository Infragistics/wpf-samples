using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace IGDataChart.Samples.Performance
{
    public partial class UpdatingDataPoints : Infragistics.Samples.Framework.SampleContainer
    {
        public UpdatingDataPoints()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        protected DispatcherTimer UpdateTimer;
        protected DispatcherTimer PerformanceTimer;
        protected int ChartUpdateCount;
        protected List<DataViewModel> DataViewModels;

        private void OnUpdateTimerTick(object sender, EventArgs e)
        {
            // update values of all data points in each data view model
            foreach (DataViewModel vm in this.DataViewModels)
            {
                vm.Update();
            }
        }
        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DataViewModels = new List<DataViewModel>
            {
                this.Resources["DataSource1"] as DataViewModel,
                this.Resources["DataSource2"] as DataViewModel,
                this.Resources["DataSource3"] as DataViewModel,
                this.Resources["DataSource4"] as DataViewModel,
                this.Resources["DataSource5"] as DataViewModel
            };
            this.ToggleUpdateTimerButton.IsChecked = false;
            this.ToggleUpdateTimerButton.Click += OnToggleUpdateTimerButtonClick;

            this.DataChart.LayoutUpdated += OnChartLayoutUpdated;

            this.PerformanceTimer = new DispatcherTimer();
            this.PerformanceTimer.Interval = TimeSpan.FromSeconds(1.0);
            this.PerformanceTimer.Tick += OnPerformanceTimerTick;
            this.PerformanceTimer.Start();

            // setup update timer to trigger updates of data points 
            this.UpdateTimer = new DispatcherTimer();
            this.UpdateTimer.Interval = TimeSpan.FromSeconds(1 / 50.0);
            this.UpdateTimer.Tick += OnUpdateTimerTick;
            this.UpdateTimer.Start();

            this.UpdateIntervalSlider.ValueChanged += OnUpdateIntervalSliderChanged;
            this.UpdateIntervalSlider.Value = this.UpdateTimer.Interval.TotalMilliseconds;
        }

        private void OnToggleUpdateTimerButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.UpdateTimer.IsEnabled)
                this.UpdateTimer.Stop();
            else
                this.UpdateTimer.Start();
        }

        private void OnUpdateIntervalSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.UpdateTimer != null)
                this.UpdateTimer.Interval = TimeSpan.FromMilliseconds(e.NewValue);
        }
        private void OnPerformanceTimerTick(object sender, EventArgs e)
        {
            this.ChartUpdatesPerSecondTextBlock.Text = String.Format("{0:0.00}", this.ChartUpdateCount);
            this.ChartUpdateCount = 0;
        }
        private void OnChartLayoutUpdated(object sender, EventArgs e)
        {
            this.ChartUpdateCount++;
        }

    }
    public class DataViewModel : ObservableModel
    {
        public DataViewModel()
        {
            _data = new DataCollection();
            _dataDataSettings = new DataSettings
            {
                DataPoints = 200,
                ValueXOffset = 0,
                ValueXInterval = 5,
                ValueYMultiplier = 1,
                ValueYChange = -0.05,
                ValueYShift = 0
            };
            this.Generate();
        }

        #region Methods

        private double _modifier;
        /// <summary>
        /// Updates values of all data points
        /// </summary>
        public void Update()
        {
            _modifier += this.Settings.ValueYChange;

            if (_modifier < -this.Settings.ValueYMultiplier || _modifier > this.Settings.ValueYMultiplier)
                this.Settings.ValueYChange *= -1;

            foreach (ObservableDataPoint dataPoint in Data)
            {
                double x = dataPoint.X;
                double y = _modifier * System.Math.Sin(x * System.Math.PI / 180);
                dataPoint.Value = y;
            }
        }
        protected void Generate()
        {
            _modifier = this.Settings.ValueYMultiplier;
            double x = this.Settings.ValueXOffset;
            this.Data.Clear();
            for (int i = 0; i <= this.Settings.DataPoints; i++)
            {
                double y = this.Settings.ValueYMultiplier * System.Math.Sin(x * System.Math.PI / 180) +
                           this.Settings.ValueYShift;
                this.Data.Add(new ObservableDataPoint(x, y, i));
                x += this.Settings.ValueXInterval;
            }
        }

        #endregion
        #region Properties
        private DataCollection _data;
        public DataCollection Data
        {
            get { return _data; }
            set
            {
                if (_data == value) return;
                _data = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Data"));
            }
        }
        private DataSettings _dataDataSettings;
        public DataSettings Settings
        {
            get { return _dataDataSettings; }
            set
            {
                if (_dataDataSettings == value) return;
                _dataDataSettings = value;
                this.Generate();
                OnPropertyChanged(new PropertyChangedEventArgs("Settings"));
            }
        }
        #endregion

    }
    public class DataSettings
    {
        public DataSettings()
        {
            this.DataPoints = 200;
            this.ValueXOffset = 0;
            this.ValueXInterval = 5;
            this.ValueYMultiplier = 1;
            this.ValueYChange = -0.05;
            this.ValueYShift = 0;
        }

        public double ValueYChange { get; set; }
        public double ValueYShift { get; set; }
        public double ValueYMultiplier { get; set; }
        public double ValueXInterval { get; set; }
        public double ValueXOffset { get; set; }

        public int DataPoints { get; set; }
    }
    public class DataCollection : List<ObservableDataPoint>
    {
        public DataCollection()
        {
            _title = "";
        }
        #region Properties
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title.Equals(value)) return;
                _title = value;
            }
        }
        #endregion
    }
    public class ObservableDataPoint : ObservableModel
    {
        #region Constructors
        public ObservableDataPoint() { }
        public ObservableDataPoint(double value, string label)
        {
            this.Value = value;
            this.Category = label;
        }
        public ObservableDataPoint(double x, double y, int index)
        {
            this.X = x;
            this.Y = y;
            this.Index = index;
            this.Value = y;
        }
        public ObservableDataPoint(ObservableDataPoint dataPoint)
        {
            this.Index = dataPoint.Index;
            this.X = dataPoint.X;
            this.Y = dataPoint.Y;
            this.Category = dataPoint.Category;
            this.Value = dataPoint.Value;
        }
        #endregion

        #region Properties

        private double _index;
        public double Index
        {
            get { return _index; }
            set { if (_index == value) return; _index = value; OnPropertyChanged("Index"); }
        }

        private double _value;
        public double Value
        {
            get { return _value; }
            set { if (_value == value) return; _value = value; OnPropertyChanged("Value"); }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set { if (_category == value) return; _category = value; OnPropertyChanged("Category"); }
        }

        private double _x;
        public double X
        {
            get { return _x; }
            set { if (_x == value) return; _x = value; OnPropertyChanged("X"); }
        }
        private double _y;
        public double Y
        {
            get { return _y; }
            set { if (_y == value) return; _y = value; OnPropertyChanged("Y"); }
        }

        #endregion

        #region Methods

        public ObservableDataPoint Clone()
        {
            return new ObservableDataPoint(this);
        }
        #endregion
    }
    public abstract class ObservableModel : INotifyPropertyChanged
    {
        #region Event Handlers
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged(PropertyChangedEventArgs propertyChangedEventArgs)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(this, propertyChangedEventArgs);
        }
        #endregion




    }
}
