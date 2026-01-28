using IGSurfaceChart.Samples.ViewModels;
using Infragistics.Samples.Framework;
using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace IGSurfaceChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for BindingLiveData.xaml
    /// </summary>
    public partial class BindingLiveData : SampleContainer, INotifyPropertyChanged
    {
        private double _seed = 0.0;
        private const int PointCount = 150;

        public BindingLiveData()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 

            DataContext = this;

            var updateTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.025) };
            updateTimer.Tick += updateTimer_Tick;

            DataCollection = new LiveDataCollection(PointCount, _seed);
            updateTimer.Start();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            _seed += 0.01;
            DataCollection.Update(_seed);
        }

        private LiveDataCollection _dataCollection;
        public LiveDataCollection DataCollection
        {
            get { return _dataCollection; }
            private set
            {
                _dataCollection = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DataCollection"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, ea);
            }
        }
    }
}
