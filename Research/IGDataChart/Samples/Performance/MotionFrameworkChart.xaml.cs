using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using IGDataChart.Custom.Frameworks;
using IGDataChart.Resources;

namespace IGDataChart.Samples.Performance
{
    public partial class MotionFrameworkChart : Infragistics.Samples.Framework.SampleContainer
    {
        public MotionFrameworkChart()
        {
            InitializeComponent();
            
            // get data sources that will be animated over time
            this.DataSources = DataSourceGenerator.GetDataSources(8, 160);

            this.MotionFramework = new MotionFramework();
            // set the interval between data updates 
            this.MotionFramework.DataUpdateInterval = TimeSpan.FromMilliseconds(300);
            
            // set the chart Transition Function and duration 
            this.MotionFramework.TransitionDuration = TimeSpan.FromMilliseconds(300);
            //this.MotionFramework.TransitionFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut };

            // automatically size the chart axes to contain all data in the data source 
            this.MotionFramework.AutosetAxisRange = true;
            this.MotionFramework.AutosetAxisMargin = new Thickness(0, 0.5, 0.1, 0.5);
            // set the chart to be used
            this.MotionFramework.Chart = this.MotionDataChart;
            // set the slider to control chart motion over time
            this.MotionFramework.DateTimeSlider = this.MotionSlider;
            // set whether trails would be shown
            this.MotionFramework.ShowTrails = true;
            this.MotionFramework.MarkerTemplate = this.Resources["MotionMarkerTemplate"] as DataTemplate;

            // set data sources
            this.MotionFramework.DataSources = this.DataSources;
            // set the properties of bound objects to be used in the chart
            this.MotionFramework.SeriesRadiusMemberPath = MotionDataPoint.PropertyNameValueR;
            this.MotionFramework.SeriesXMemberPath = MotionDataPoint.PropertyNameValueX;
            this.MotionFramework.SeriesYMemberPath = MotionDataPoint.PropertyNameValueY;
            this.MotionFramework.SeriesTimeMemberPath = MotionDataPoint.PropertyNameValueDateTime;

            // set the chart axes
            this.MotionFramework.XAxisName = "axisX";
            this.MotionFramework.YAxisName = "axisY";
            this.MotionFramework.Chart.LayoutUpdated += OnChartLayoutUpdated;
            this.MotionFramework.PlaybackStopped += MotionFramework_PlaybackStopped;
            
            this.DataContext = this.MotionFramework;

            this.MotionPlayToggleButton.Click += OnMotionPlayButtonClick;
            //this.DataUpdateIntervalSlider.ValueChanged += OnDataUpdateDurationSliderValueChanged;
            this.TransitionDurationSlider.ValueChanged += OnTransitionDurationSliderValueChanged;

            this.TransitionFunctionsComboBox.SelectionChanged += OnTransitionFunctionsComboBoxSelectionChanged;
            this.TransitionFunctionsComboBox.DisplayMemberPath = "DisplayName";
            this.TransitionFunctionsComboBox.ItemsSource = new TransitionFunctions();
            this.TransitionFunctionsComboBox.SelectedIndex = 0;
           
        }
        protected MotionFramework MotionFramework;
        protected Dictionary<int, IList> DataSources;

        #region Event Handlers
        //private void OnDataUpdateDurationSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    this.MotionFramework.DataUpdateInterval = TimeSpan.FromMilliseconds(e.NewValue);
        //}
        private void OnTransitionFunctionsComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;

            var trans = e.AddedItems[0] as TransitionFunction;
            if (trans != null)
            {
                this.MotionFramework.TransitionFunction = trans.Function;
            }
        }

        private void OnTransitionDurationSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.MotionFramework.DataUpdateInterval = TimeSpan.FromMilliseconds(e.NewValue);
            this.MotionFramework.TransitionDuration = TimeSpan.FromMilliseconds(e.NewValue);
        }
        private void MotionFramework_PlaybackStopped(object sender, EventArgs e)
        {
            this.MotionPlayToggleButton.IsChecked = false;
            this.MotionPlayToggleButton.Content = DataChartStrings.XWDC_Motion_Framework_Play;
        }

        private void OnChartLayoutUpdated(object sender, EventArgs e)
        {
            // initialize MotionFramework when chart finished loading for the first time
            if (!this.MotionFramework.IsInitialized)
            {
                this.MotionFramework.Initialize();
            }
        }

        private void OnMotionPlayButtonClick(object sender, RoutedEventArgs e)
        {
            if (!this.MotionFramework.IsPlaying)
            {
                this.MotionFramework.Play();
                this.MotionPlayToggleButton.IsChecked = true;
                this.MotionPlayToggleButton.Content = DataChartStrings.XWDC_Motion_Framework_Pause;
            }
            else
            {
                this.MotionFramework.Pause();
                this.MotionPlayToggleButton.IsChecked = false;
                this.MotionPlayToggleButton.Content = DataChartStrings.XWDC_Motion_Framework_Play;
            }
        }
        #endregion

    }
    public class DataSourceGenerator
    {
        protected static Random Random;

        static DataSourceGenerator()
        {
            Random = new Random();
        }

        public static IList<DataPoint> GetRandomData(int maxValues, int order)
        {
            var result = new List<DataPoint>(maxValues);
            var valueDateTime = DateTime.Now.AddDays(maxValues * -1);
            var valueModifier = (order % 2 == 0) ? 1 : -1;
            var valueMultiplier = (order % 2 == 0) ? order : order - 1;

            for (int i = 0; i < maxValues; i++)
            {
                double x = Convert.ToDouble(i) / 100f;
                double y = valueModifier * System.Math.Round(System.Math.Sin(x * (valueMultiplier + 1)), 2);
                double r = 10 + (Convert.ToDouble(i) / 25f) * (order + 1);

                result.Add(new DataPoint
                {
                    ValueX = x,
                    ValueY = y,
                    ValueR = r,
                    ValueDateTime = valueDateTime
                });
                valueDateTime = valueDateTime.AddDays(1);
            }
            return result;
        }

        public static Dictionary<int, IList> GetDataSources(int maxSeries, int maxDataPerSeries)
        {
            var dict = new Dictionary<int, IList>();
            for (var i = 0; i < maxSeries; i++)
            {
                dict.Add(i, (IList)GetRandomData(maxDataPerSeries, i));
            }
            return dict;
        }
    }

    public class TransitionFunctions : List<TransitionFunction>
    {
        public TransitionFunctions()
        {
            this.Add(new TransitionFunction()); // Linear Transition Function
            this.Add(new TransitionFunction(new BackEase(), EasingMode.EaseIn));
            this.Add(new TransitionFunction(new BackEase(), EasingMode.EaseInOut));
            this.Add(new TransitionFunction(new BackEase(), EasingMode.EaseOut));
            this.Add(new TransitionFunction(new BounceEase(), EasingMode.EaseIn));
            this.Add(new TransitionFunction(new BounceEase(), EasingMode.EaseInOut));
            this.Add(new TransitionFunction(new BounceEase(), EasingMode.EaseOut));
            this.Add(new TransitionFunction(new CircleEase(), EasingMode.EaseIn));
            this.Add(new TransitionFunction(new CircleEase(), EasingMode.EaseInOut));
            this.Add(new TransitionFunction(new CircleEase(), EasingMode.EaseOut));
            this.Add(new TransitionFunction(new CubicEase(), EasingMode.EaseIn));
            this.Add(new TransitionFunction(new CubicEase(), EasingMode.EaseInOut));
            this.Add(new TransitionFunction(new CubicEase(), EasingMode.EaseOut));
            this.Add(new TransitionFunction(new ElasticEase(), EasingMode.EaseIn));
            this.Add(new TransitionFunction(new ElasticEase(), EasingMode.EaseInOut));
            this.Add(new TransitionFunction(new ElasticEase(), EasingMode.EaseOut));
            this.Add(new TransitionFunction(new ExponentialEase(), EasingMode.EaseIn));
            this.Add(new TransitionFunction(new ExponentialEase(), EasingMode.EaseInOut));
            this.Add(new TransitionFunction(new ExponentialEase(), EasingMode.EaseOut));
            this.Add(new TransitionFunction(new PowerEase(), EasingMode.EaseIn));
            this.Add(new TransitionFunction(new PowerEase(), EasingMode.EaseInOut));
            this.Add(new TransitionFunction(new PowerEase(), EasingMode.EaseOut));
            this.Add(new TransitionFunction(new QuadraticEase(), EasingMode.EaseIn));
            this.Add(new TransitionFunction(new QuadraticEase(), EasingMode.EaseInOut));
            this.Add(new TransitionFunction(new QuadraticEase(), EasingMode.EaseOut));
            this.Add(new TransitionFunction(new QuarticEase(), EasingMode.EaseIn));
            this.Add(new TransitionFunction(new QuarticEase(), EasingMode.EaseInOut));
            this.Add(new TransitionFunction(new QuarticEase(), EasingMode.EaseOut));
            this.Add(new TransitionFunction(new SineEase(), EasingMode.EaseIn));
            this.Add(new TransitionFunction(new SineEase(), EasingMode.EaseInOut));
            this.Add(new TransitionFunction(new SineEase(), EasingMode.EaseOut));

        }
    }
    public class TransitionFunction
    {
        /// <summary>
        /// Constructor for Transition Function, default is Linear Transition Function
        /// </summary>
        public TransitionFunction()
        {
        }
        public TransitionFunction(EasingFunctionBase function, EasingMode easingMode)
        {
            function.EasingMode = easingMode;
            this.Function = function;
            this.Mode = easingMode;
        }
        public EasingMode Mode { get; private set; }
        public EasingFunctionBase Function { get; private set; }

        private string GetFunctionName()
        {
            return this.Function.GetType().Name.Replace("Ease", " ");
        }
        private string GetModeName()
        {
            return this.Mode.ToString().Replace("Ease", "");
        }
        public string DisplayName
        {
            get
            {
                if (Function == null) return "Linear";
                return this.GetFunctionName() + this.GetModeName();
            }
        }
        public override string ToString()
        {
            return this.DisplayName;
        }

    }
}