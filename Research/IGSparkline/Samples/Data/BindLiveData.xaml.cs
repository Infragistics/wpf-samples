using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using IGSparkline.Model;
using Infragistics.Samples.Framework;

namespace IGSparkline.Samples.Data
{
    public partial class BindLiveData : SampleContainer
    {
        DispatcherTimer _timer;
        public ObservableCollection<TestDataItem> randomData { get; set; }

        public BindLiveData()
        {
            var r = new Random();
            randomData = new ObservableCollection<TestDataItem>()
            {
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) },
                new TestDataItem { Value = r.Next(1, 50) }
            };

            InitializeComponent();
            this.DataContext = randomData;
        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            // Only one timer can be running
            if (_timer != null && _timer.IsEnabled) return;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            var r = new Random();
            randomData.Add(new TestDataItem { Value = r.Next(-5, 45) });
            randomData.RemoveAt(0);
        }

        private void OnStop(object sender, RoutedEventArgs e)
        {
            if (_timer != null && _timer.IsEnabled)
                _timer.Stop();
        }
    }
}
