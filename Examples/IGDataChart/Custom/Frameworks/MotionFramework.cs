using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Infragistics.Controls.Charts;

namespace IGDataChart.Custom.Frameworks
{
    /// <summary>
    /// Provides animation of data points over time in xamDataChart control
    /// </summary>
    public class MotionFramework : ObservableModel
    {
        public MotionFramework()
        {
            _transitionDuration = TimeSpan.FromMilliseconds(1000);
            _transitionFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut };

            this.AutosetAxisMargin = new Thickness(0, 0.5, 0, 0.5);
            this.AutosetAxisRange = true;
            this.DataUpdateTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
            this.DataUpdateTimer.Tick += OnDataUpdateTimerTick;
        }
        #region Fields

        protected DispatcherTimer DataUpdateTimer;
        protected int ElementsMaxCount;

        #endregion

        #region Events
        public delegate void CurrentDateChangedEventHandler(object sender, DateTimeChangedEventArgs e);
        public event CurrentDateChangedEventHandler CurrentDateChanged;
        protected virtual void OnCurrentDateChanged(DateTimeChangedEventArgs e)
        {
            if (CurrentDateChanged != null)
                CurrentDateChanged(this, e);
        }

        public delegate void PlaybackStoppedEventHandler(object sender, EventArgs e);
        public event PlaybackStoppedEventHandler PlaybackStopped;
        protected virtual void OnPlaybackStopped(EventArgs e)
        {
            if (PlaybackStopped != null)
                PlaybackStopped(this, e);
        }

        public delegate void PlaybackStartedEventHandler(object sender, EventArgs e);
        public event PlaybackStartedEventHandler PlaybackStarted;
        protected virtual void OnPlaybackStarted(EventArgs e)
        {
            if (PlaybackStarted != null)
                PlaybackStarted(this, e);
        }
        public class DateTimeChangedEventArgs : EventArgs
        {
            public DateTime CurrentDate { get; set; }
        }
        #endregion

        #region Event Handlers
        private void OnDateTimeSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!this.IsPlaying)
            {
                if (!this.IsInitialized && this.RequiredConditionsCheck())
                {
                    this.InitializeDataChart();
                    this.IsInitialized = true;
                }

                this.CurrentElementShown = (int)e.NewValue;
                if (this.CurrentElementShown < this.ElementsMaxCount)
                    this.RefreshSeries();
            }
        }
        private void OnDataUpdateTimerTick(object sender, EventArgs e)
        {
            if (this.CurrentElementShown < this.ElementsMaxCount)
            {
                this.RefreshSeries();
                this.CurrentElementShown++;
                if (this.DateTimeSlider != null)
                {
                    this.DateTimeSlider.Value = this.CurrentElementShown;
                    this.DateTimeSlider.Tag = this.CurrentDateTime.ToShortDateString();
                }
            }
            else
            {
                this.Pause();
            }
        }
        #endregion

        #region Private Methods
        private bool RequiredConditionsCheck()
        {
            bool ret = this.Chart != null;
            ret = (ret && this.DataSources != null);
            ret = (ret && this.XAxisName != null);
            ret = (ret && this.YAxisName != null);
            ret = (ret && this.SeriesXMemberPath != null);
            ret = (ret && this.SeriesYMemberPath != null);
            ret = (ret && this.SeriesRadiusMemberPath != null);
            return ret;
        }
        private string GetSeriesName(int key, bool isUnique = false)
        {
            if (isUnique)
                return string.Format("{0}|Series {0}", key);
            return string.Format("Series{0}", key);
        }
        private int GetNumDigits(double max, double min)
        {
            double error = (max - min) / 100;
            int numDigits = 0;
            while (error < 1)
            {
                error *= 10;
                ++numDigits;
            }

            return numDigits;
        }

        private void InitializeDataChart()
        {
            if (this.RequiredConditionsCheck())
            {
                this.Chart.Series.Clear();
                this.ElementsMaxCount = this.DataSources[0].Count;
                this.CurrentElementShown = 0;

                double minX = Double.PositiveInfinity;
                double minY = Double.PositiveInfinity;
                double maxX = Double.NegativeInfinity;
                double maxY = Double.NegativeInfinity;
                DateTime minDateTime = DateTime.MaxValue;
                DateTime maxDateTime = DateTime.MinValue;

                foreach (var item in this.DataSources)
                {
                    this.InitializeSeries(this.GetSeriesName(item.Key), item.Key);
                    //if autoset enabled, traverse all data points for this series
                    //looking for the smallest/largest values along X and Y
                    #region Find Min/Max Values
                    if (this.AutosetAxisRange)
                    {
                        //make sure the item.value list is sorted along its datetime property
                        foreach (var seriesPoint in item.Value)
                        {
                            double xValue = (double)seriesPoint.GetType().GetProperty(this.SeriesXMemberPath).GetValue(seriesPoint, null);
                            double yValue = (double)seriesPoint.GetType().GetProperty(this.SeriesYMemberPath).GetValue(seriesPoint, null);
                            DateTime dateTimeValue = (DateTime)seriesPoint.GetType().GetProperty(this.SeriesTimeMemberPath).GetValue(seriesPoint, null);

                            if (xValue < minX) minX = xValue;
                            if (xValue > maxX) maxX = xValue;

                            if (yValue < minY) minY = yValue;
                            if (yValue > maxY) maxY = yValue;

                            if (dateTimeValue < minDateTime) minDateTime = dateTimeValue;
                            if (dateTimeValue > maxDateTime) maxDateTime = dateTimeValue;
                        }
                    }
                    #endregion
                }

                #region Set range of axes based on min/max values
                if (this.AutosetAxisRange)
                {
                    NumericXAxis xAxis = this.Chart.FindName(XAxisName) as NumericXAxis;
                    NumericYAxis yAxis = this.Chart.FindName(YAxisName) as NumericYAxis;

                    double xAxisMarginLeft = ((maxX - minX) * this.AutosetAxisMargin.Left) / 2;
                    double xAxisMarginRight = ((maxX - minX) * this.AutosetAxisMargin.Right) / 2;

                    if (xAxis != null)
                    {
                        xAxis.MinimumValue = System.Math.Round(((float)minX - (float)xAxisMarginLeft), GetNumDigits(maxX, minX));
                        xAxis.MaximumValue = System.Math.Round(((float)maxX + (float)xAxisMarginRight), GetNumDigits(maxX, minX));
                    }

                    double yAxisMarginTop = ((maxY - minY) * this.AutosetAxisMargin.Top) / 2;
                    double yAxisMarginBottom = ((maxY - minY) * this.AutosetAxisMargin.Bottom) / 2;

                    if (yAxis != null)
                    {
                        yAxis.MinimumValue = System.Math.Round(((float)minY - (float)yAxisMarginBottom), GetNumDigits(maxY, minY));
                        yAxis.MaximumValue = System.Math.Round(((float)maxY + (float)yAxisMarginTop), GetNumDigits(maxY, minY));
                    }
                }
                #endregion

                if (this.DateTimeSlider != null)
                {
                    this.MinDateTime = minDateTime;
                    this.MaxDateTime = maxDateTime;
                    this.DateTimeSlider.Minimum = 0;
                    this.DateTimeSlider.Maximum = this.ElementsMaxCount - 1;
                }
            }
        }
        private void InitializeSeries(string name, int dataOrder, IEnumerable dataSource = null)
        {
            BubbleSeries motionSeries = new BubbleSeries();
            motionSeries.Name = this.GetSeriesName(dataOrder);
            motionSeries.YMemberPath = this.SeriesYMemberPath;
            motionSeries.XMemberPath = this.SeriesXMemberPath;
            motionSeries.RadiusMemberPath = this.SeriesRadiusMemberPath;
            motionSeries.TransitionDuration = this.TransitionDuration;
            motionSeries.TransitionEasingFunction = this.TransitionFunction;
            motionSeries.Title = name;
            motionSeries.XAxis = this.Chart.FindName(XAxisName) as NumericXAxis;
            motionSeries.YAxis = this.Chart.FindName(YAxisName) as NumericYAxis;
            motionSeries.MarkerTemplate = this.MarkerTemplate;

            if (this.ChartLegend != null) motionSeries.Legend = this.ChartLegend;
            if (this.LegendItemTemplate != null) motionSeries.LegendItemTemplate = this.LegendItemTemplate;

            // adding the single data point that will be changed in order to be animated over time
            List<MotionDataPoint> newDataSource = new List<MotionDataPoint>();
            newDataSource.Add(new MotionDataPoint { ValueX = 0, ValueY = 0, ValueR = 0 });
            motionSeries.ItemsSource = newDataSource;

            if (dataSource != null)
                motionSeries.ItemsSource = dataSource;

            ScatterSplineSeries trailSeries = CopySeries(motionSeries);
            trailSeries.Thickness = 2;
            motionSeries.Tag = trailSeries;
            this.Chart.Series.Add(trailSeries);
            this.Chart.Series.Add(motionSeries);
        }
        private ScatterSplineSeries CopySeries(BubbleSeries series)
        {
            ScatterSplineSeries trailSeries = new ScatterSplineSeries();
            trailSeries.MarkerType = MarkerType.None;
            trailSeries.YMemberPath = series.YMemberPath;
            trailSeries.XMemberPath = series.XMemberPath;
            trailSeries.TransitionDuration = series.TransitionDuration;
            trailSeries.Title = series.Title;
            trailSeries.XAxis = series.XAxis;
            trailSeries.YAxis = series.YAxis;

            MotionDataSource<DataPoint> ds = new MotionDataSource<DataPoint>();
            trailSeries.ItemsSource = ds;
            return trailSeries;
        }
        private void UpdateMotionSeries(Series series, DataPoint dataPoint)
        {
            ((IList<MotionDataPoint>)series.ItemsSource)[0].ValueX = dataPoint.ValueX;
            ((IList<MotionDataPoint>)series.ItemsSource)[0].ValueY = dataPoint.ValueY;
            ((IList<MotionDataPoint>)series.ItemsSource)[0].ValueR = dataPoint.ValueR;
            ((IList<MotionDataPoint>)series.ItemsSource)[0].ToolTip = dataPoint.ToolTip;
        }
        private void UpdateTrailSeries(Series series)
        {
            ScatterLineSeries trailSeries = series.Tag as ScatterLineSeries;
            if (trailSeries != null)
            {
                trailSeries.Brush = series.ActualBrush;
                trailSeries.MarkerBrush = series.ActualBrush;
                MotionDataSource<DataPoint> dm = trailSeries.ItemsSource as MotionDataSource<DataPoint>;
                if (dm != null)
                {
                    dm.Add(new DataPoint
                    {
                        ValueX = ((IList<MotionDataPoint>)series.ItemsSource)[0].ValueX,
                        ValueY = ((IList<MotionDataPoint>)series.ItemsSource)[0].ValueY,
                        ToolTip = ((IList<MotionDataPoint>)series.ItemsSource)[0].ToolTip
                    });
                }
            }
        }
        private void UpdateTrailSeries(Series series, IEnumerable<DataPoint> dataSource)
        {
            ScatterSplineSeries trailSeries = series.Tag as ScatterSplineSeries;
            if (trailSeries != null)
            {
                trailSeries.Brush = series.ActualBrush;
                trailSeries.MarkerBrush = series.ActualBrush;
                trailSeries.ItemsSource = dataSource;
            }
        }
        private void UpdateSeriesTransitions()
        {
            if (this.IsInitialized)
            {
                this.Chart.Series.Clear();

                // update all series with the current Transition parameters
                foreach (var item in this.DataSources)
                {
                    string seriesName = this.GetSeriesName(item.Key);
                    this.InitializeSeries(seriesName, item.Key);

                    DataPoint dataPoint;
                    List<DataPoint> trailsDataSource;
                    if (this.CurrentElementShown < this.ElementsMaxCount)
                    {
                        dataPoint = (DataPoint)item.Value[this.CurrentElementShown];
                        trailsDataSource = ((List<DataPoint>)item.Value).GetRange(0, this.CurrentElementShown + 1);
                    }
                    else
                    {
                        dataPoint = (DataPoint)item.Value[this.ElementsMaxCount - 1];
                        trailsDataSource = ((List<DataPoint>)item.Value).GetRange(0, this.CurrentElementShown);
                    }

                    this.UpdateMotionSeries(this.Chart.Series[seriesName], dataPoint);
                    if (this.ShowTrails)
                    {
                        // update trails series with all data points up to the current element shown
                        this.UpdateTrailSeries(this.Chart.Series[seriesName], trailsDataSource);
                    }
                }
            }
        }
        private void RefreshSeries()
        {
            foreach (var item in this.DataSources)
            {
                // update the motion series with the current element shown
                string seriesName = this.GetSeriesName(item.Key);
                DataPoint dataPoint = (DataPoint)item.Value[this.CurrentElementShown];
                this.UpdateMotionSeries(this.Chart.Series[seriesName], dataPoint);

                if (this.ShowTrails)
                {
                    // update trails series with all data points up to the current element shown
                    List<DataPoint> trailsDataSource = ((List<DataPoint>)item.Value).GetRange(0, this.CurrentElementShown + 1);
                    this.UpdateTrailSeries(this.Chart.Series[seriesName], trailsDataSource);
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes elements of MotionFramework 
        /// </summary>
        public void Initialize()
        {
            if (this.RequiredConditionsCheck())
            {
                this.InitializeDataChart();
                this.DataUpdateTimer.Interval = this.DataUpdateInterval;
                this.CurrentElementShown = 0;
                this.IsInitialized = true;
            }
        }
        /// <summary>
        /// Starts animation of data points over time in xamDataChart control
        /// </summary>
        public void Play()
        {
            if (!this.IsInitialized) this.Initialize();

            //starting play when playback has finished resets the current element shown to 0
            if (this.CurrentElementShown >= this.ElementsMaxCount - 1)
            {
                this.CurrentElementShown = 0;
                if (this.DateTimeSlider != null) this.DateTimeSlider.Value = 0;
            }


            if (this.IsInitialized && this.CurrentElementShown == 0)
            {
                //clear scatter series when starting playback from the beginning
                foreach (Series series in this.Chart.Series)
                {
                    ScatterLineSeries trailSeries = series as ScatterLineSeries;
                    if (trailSeries != null)
                    {
                        if (trailSeries.ItemsSource != null) ((IList)trailSeries.ItemsSource).Clear();
                    }
                }
            }
            if (this.DateTimeSlider != null) this.DateTimeSlider.IsEnabled = false;

            this.DataUpdateTimer.Start();
            OnPlaybackStarted(new EventArgs());
        }

        /// <summary>
        /// Stops animation of data points in xamDataChart control
        /// </summary>
        public void Pause()
        {
            if (this.DataUpdateTimer.IsEnabled)
            {
                if (this.DateTimeSlider != null) this.DateTimeSlider.IsEnabled = true;
                this.DataUpdateTimer.Stop();
                OnPlaybackStopped(new EventArgs());
            }

        }
        #endregion

        #region Properties

        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Checks if MotionFramework is playing animation of data points over time
        /// </summary>
        public bool IsPlaying { get { return this.DataUpdateTimer.IsEnabled; } }

        /// <summary>
        /// Represents the Timer Interval between update in animation of data points  
        /// </summary>
        public TimeSpan DataUpdateInterval
        {
            get { return this.DataUpdateTimer.Interval; }
            set { this.DataUpdateTimer.Interval = value; }
        }

        private TimeSpan _transitionDuration;
        public TimeSpan TransitionDuration
        {
            get { return _transitionDuration; }
            set
            {
                if (_transitionDuration == value) return;
                _transitionDuration = value;
                this.UpdateSeriesTransitions();
                this.OnPropertyChanged("TransitionDuration");
            }
        }
        private EasingFunctionBase _transitionFunction;
        public EasingFunctionBase TransitionFunction
        {
            get { return _transitionFunction; }
            set
            {
                if (_transitionFunction == value) return;
                _transitionFunction = value;
                this.UpdateSeriesTransitions();
                this.OnPropertyChanged("TransitionFunction");
            }
        }

        private int _currentElementShown;
        /// <summary>
        /// Represents the current index of animation of data points
        /// </summary>
        public int CurrentElementShown
        {
            get { return _currentElementShown; }
            set
            {
                _currentElementShown = value;
                this.CurrentDateTime = this.CurrentDataPoint.ValueDateTime;
                this.CurrentDateTimeString = this.CurrentDateTime.ToShortDateString();
            }
        }
        private string _currentDateTimeString;
        /// <summary>
        /// Represents the current Date Time of animation of data points as string 
        /// </summary>
        public string CurrentDateTimeString
        {
            get
            {
                return _currentDateTimeString;
            }
            set
            {
                if (_currentDateTimeString == value) return;
                _currentDateTimeString = value;
                this.OnPropertyChanged("CurrentDateTimeString");
            }
        }
        private DateTime _currentDateTime;
        /// <summary>
        /// Represents the current Date Time of animation of data points  
        /// </summary>
        public DateTime CurrentDateTime
        {
            get
            {
                return _currentDateTime;
            }
            set
            {
                if (_currentDateTime == value) return;
                _currentDateTime = value;
                this.OnPropertyChanged("CurrentDateTime");
                OnCurrentDateChanged(new DateTimeChangedEventArgs { CurrentDate = this.CurrentDateTime });
            }
        }

        protected DataPoint CurrentDataPoint
        {
            get
            {
                if (this.CurrentElementShown < this.ElementsMaxCount)
                    return ((DataPoint)this.DataSources[0][this.CurrentElementShown]);

                return ((DataPoint)this.DataSources[0][this.ElementsMaxCount - 1]);
            }
        }

        public TimeSpan CurrentTimeSpan
        {
            get
            {
                return this.CurrentDateTime.Subtract(this.MinDateTime);
            }
        }

        public DateTime MinDateTime { get; private set; }
        public DateTime MaxDateTime { get; private set; }

        public Thickness AutosetAxisMargin { get; set; }

        public bool AutosetAxisRange { get; set; }
        public bool ShowTrails { get; set; }

        private Slider _dateTimeSlider;
        /// <summary>
        /// Represents the Slider that controls animation of data points over time
        /// </summary>
        public Slider DateTimeSlider
        {
            get
            {
                return _dateTimeSlider;
            }
            set
            {
                if (_dateTimeSlider != null)
                    _dateTimeSlider.ValueChanged -= OnDateTimeSliderValueChanged;

                _dateTimeSlider = value;

                if (_dateTimeSlider != null)
                    _dateTimeSlider.ValueChanged += OnDateTimeSliderValueChanged;
            }
        }


        public Legend ChartLegend { get; set; }
        public XamDataChart Chart { get; set; }

        private Dictionary<int, IList> _dataSource;
        /// <summary>
        ///  Represents Data Sources for all series in the xamDataChart
        /// </summary>
        public Dictionary<int, IList> DataSources
        {
            get { return _dataSource; }
            set
            {
                if (_dateTimeSlider == null) return;
                _dataSource = value;
            }
        }

        public string XAxisName { get; set; }
        public string YAxisName { get; set; }

        public string SeriesXMemberPath { get; set; }
        public string SeriesYMemberPath { get; set; }
        public string SeriesRadiusMemberPath { get; set; }
        public string SeriesTimeMemberPath { get; set; }

        public DataTemplate MarkerTemplate { get; set; }
        public DataTemplate LegendItemTemplate { get; set; }

        #endregion

    }

    public class DataPoint
    {
        public DateTime ValueDateTime { get; set; }
        public string ToolTip { get; set; }
        public double ValueX { get; set; }
        public double ValueY { get; set; }
        public double ValueR { get; set; }
    }

    public class MotionDataSource<T> : ObservableCollection<T>
    { }

    public class MotionDataPoint : ObservableModel
    {
        private double _valueX;
        private double _valueY;
        private double _valueR;


        public DateTime ValueDateTime { get; set; }
        public string ToolTip { get; set; }

        public static string PropertyNameValueX = "ValueX";
        public static string PropertyNameValueY = "ValueY";
        public static string PropertyNameValueR = "ValueR";
        public static string PropertyNameValueDateTime = "ValueDateTime";

        public double ValueX
        {
            get { return _valueX; }
            set { if (_valueX == value) return; _valueX = value; this.OnPropertyChanged(PropertyNameValueX); }
        }

        public double ValueY
        {
            get { return _valueY; }
            set { if (_valueY == value) return; _valueY = value; this.OnPropertyChanged(PropertyNameValueY); }
        }

        public double ValueR
        {
            get { return _valueR; }
            set { if (_valueR == value) return; _valueR = value; this.OnPropertyChanged(PropertyNameValueR); }
        }

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