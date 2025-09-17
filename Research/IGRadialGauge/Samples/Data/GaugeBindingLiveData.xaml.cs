using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using System.Windows.Threading;

namespace IGRadialGauge.Samples.Data
{
    public partial class GaugeBindingLiveData : Infragistics.Samples.Framework.SampleContainer
    {
        public GaugeBindingLiveData()
        {
            InitializeComponent();
            this.InitTimer();
        }

        private DispatcherTimer Timer { get; set; }
        private readonly Random _rnd = new Random();

        #region Event Handlers

        #region Timer
        private void OnTimerTick(object sender, EventArgs e)
        {
            // Generate Value for Gauge
            this.radialGauge.Value = Convert.ToDouble(_rnd.Next(20, 70));
        }
        #endregion Timer

        #endregion Event Handlers

        #region Private Methods
        private void InitTimer()
        {
            this.Timer = new DispatcherTimer();
            this.Timer.Tick += OnTimerTick;
            this.Timer.Interval = new TimeSpan(0, 0, 5); // 5 second
            this.Timer.Start();
        }
        #endregion Private Methods
    }
}
