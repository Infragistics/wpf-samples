using System;
using System.Windows.Threading;
using Infragistics.Controls.Charts;

namespace IGDataChart.Tools
{
    public class ChartPerformanceMonitor
    {
        public ChartPerformanceMonitor()
        {
            this.ChartStatsUpdateTimer = new DispatcherTimer();
            this.ChartStatsUpdateTimer.Tick += OnChartStatsUpdateTimerTick;
            this.ChartStatsUpdateTimer.Interval = TimeSpan.FromSeconds(1);

        }
        #region Properties
        protected double ChartLayoutUpdatesPerSecond;
        protected long ChartLayoutUpdatesCount;
        protected DispatcherTimer ChartStatsUpdateTimer;

        private XamDataChart _dataChart;
        public XamDataChart DataChart
        {
            get { return _dataChart; }
            set
            {
                if (_dataChart != null)
                {
                    _dataChart.LayoutUpdated -= OnChartLayoutUpdated;
                }
                _dataChart = value;

                if (_dataChart != null)
                {
                    _dataChart.LayoutUpdated += OnChartLayoutUpdated;
                }
            }
        }

        #endregion
        public void StartMonitor()
        {
            if (DataChart != null)
                this.ChartStatsUpdateTimer.Start();
        }
        public void StopMonitor()
        {
            this.ChartStatsUpdateTimer.Stop();
        }

        #region Event Handlers
        private void OnChartLayoutUpdated(object sender, EventArgs e)
        {
            this.ChartLayoutUpdatesCount++;
        }
        private void OnChartStatsUpdateTimerTick(object sender, EventArgs e)
        {
            this.ChartLayoutUpdatesPerSecond = ChartLayoutUpdatesCount;
            this.ChartLayoutUpdatesCount = 0;

            OnCurrentDateChanged(new ChartStatsChangedEventArgs { ChartLayoutUpdatesPerSecond = this.ChartLayoutUpdatesPerSecond });
        }
        #endregion
        #region Events
        public delegate void ChartStatsChangedEventHandler(object sender, ChartStatsChangedEventArgs e);
        public event ChartStatsChangedEventHandler ChartStatsChanged;
        protected virtual void OnCurrentDateChanged(ChartStatsChangedEventArgs e)
        {
            if (ChartStatsChanged != null)
                ChartStatsChanged(this, e);
        }
        public class ChartStatsChangedEventArgs : EventArgs
        {
            public double ChartLayoutUpdatesPerSecond { get; set; }
        }
        #endregion

    }
    
}