using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Controls.Charts;
using System.Diagnostics;

namespace IGDataChart.Custom.Behaviors
{
    public class ChartStripBehaviors : DependencyObject
    {
        #region Chart Info Strips Behavior

        internal const string ChartInfoStripsPropertyName = "ChartInfoStrips";
        public static readonly DependencyProperty ChartInfoStripsProperty =
            DependencyProperty.RegisterAttached(ChartInfoStripsPropertyName,
            typeof(ChartInfoStripsBehavior), typeof(ChartStripBehaviors),
            new PropertyMetadata(null, (o, e) => OnInfoStripsChanged(o as XamDataChart,
                    e.OldValue as ChartInfoStripsBehavior,
                    e.NewValue as ChartInfoStripsBehavior)));

        public static ChartInfoStripsBehavior GetChartInfoStrips(DependencyObject target)
        {
            return target.GetValue(ChartInfoStripsProperty) as ChartInfoStripsBehavior;
        }
        public static void SetChartInfoStrips(DependencyObject target, ChartInfoStripsBehavior behavior)
        {
            target.SetValue(ChartInfoStripsProperty, behavior);
        }

        private static void OnInfoStripsChanged(XamDataChart chart, ChartInfoStripsBehavior oldValue, ChartInfoStripsBehavior newValue)
        {
            Debug.WriteLine("ChartInfoStrips ChartInfoStrips ");
            if (chart == null)
            {
                return;
            }

            if (oldValue != null)
            {
                oldValue.OnDetach(chart);
            }
            if (newValue != null)
            {
                newValue.OnAttach(chart);
            }
        }
        #endregion
    }
    public class ChartInfoStripsBehavior : DependencyObject
    {
        public ChartInfoStripsBehavior()
        {
            InfoStrips = new InfoStripCollection();
        }
        private XamDataChart _owner;

        #region Properties
        internal const string InfoStripsPropertyName = "InfoStrips";
        public static readonly DependencyProperty InfoStripsProperty = 
            DependencyProperty.Register(InfoStripsPropertyName, 
            typeof(InfoStripCollection), typeof(ChartInfoStripsBehavior),
            new PropertyMetadata(null, (o, e) => (o as ChartInfoStripsBehavior).OnStripsChanged(
                e.OldValue as InfoStripCollection,
                e.NewValue as InfoStripCollection)));

        public InfoStripCollection InfoStrips
        {
            get { return (InfoStripCollection)GetValue(InfoStripsProperty); }
            set { SetValue(InfoStripsProperty, value); }
        }

        public DataTemplate InfoStripTemplate { get; set; } 
        #endregion

        #region Event Handlers
        private void OnStripsChanged(InfoStripCollection oldValue, InfoStripCollection newValue)
        {
            Debug.WriteLine("ChartInfoStrips OnStripsChanged ");
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= OnStripsCollectionChanged;
            }

            if (newValue != null)
            {
                newValue.CollectionChanged += OnStripsCollectionChanged;
            }
            RefreshStrips();
        }
        private void OnStripsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine("ChartInfoStrips OnStripsCollectionChanged ");
            RefreshStrips();
        }
        public void OnAttach(XamDataChart chart)
        {
            if (_owner != null)
            {
                OnDetach(_owner);
            }
            _owner = chart;

            _owner.WindowRectChanged += Owner_WindowRectChanged;
            _owner.SizeChanged += Owner_SizeChanged;
            _owner.Loaded += OnChartLoaded;
            _owner.Axes.CollectionChanged += Axes_CollectionChanged;
            _owner.Axes.CollectionResetting += Axes_CollectionResetting;
            
            Debug.WriteLine("ChartInfoStrips OnAttach ");
            RefreshStrips();
        }

        private void OnChartLoaded(object sender, RoutedEventArgs e)
        { 
            Debug.WriteLine("ChartInfoStrips OnChartLoaded ");
            RefreshStrips();
        }

        public void OnDetach(XamDataChart chart)
        {
            if (_owner != chart)
            {
                return;
            }

            _owner.WindowRectChanged -= Owner_WindowRectChanged;
            _owner.SizeChanged -= Owner_SizeChanged;
            _owner.Loaded -= OnChartLoaded;
            _owner.Axes.CollectionChanged -= Axes_CollectionChanged;
            _owner.Axes.CollectionResetting -= Axes_CollectionResetting;

            Axis xAxis;
            Axis yAxis;
            GetAxes(out xAxis, out yAxis);

            if (xAxis == null || yAxis == null)
            {
                return;
            }
            
            Debug.WriteLine("ChartInfoStrips OnDetach ");
            RemoveStrips(xAxis);

            _owner = null;
        }
        private void Owner_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Debug.WriteLine("ChartInfoStrips Owner_SizeChanged");
            RefreshStrips();
        }
        private void Owner_WindowRectChanged(object sender, Infragistics.RectChangedEventArgs e)
        {
            Debug.WriteLine("ChartInfoStrips Owner_WindowRectChanged");
            RefreshStrips();
        }
        private void Axis_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Debug.WriteLine("ChartInfoStrips Axis_SizeChanged");
            RefreshStrips();
        }
        private void Axes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Axis axis in e.OldItems)
                {
                    axis.SizeChanged -= Axis_SizeChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (Axis axis in e.NewItems)
                {
                    axis.SizeChanged += Axis_SizeChanged;
                }
            }
            
            Debug.WriteLine("ChartInfoStrips Axes_CollectionChanged ");
            RefreshStrips();
        }
        private void Axes_CollectionResetting(object sender, EventArgs e)
        {
            Debug.WriteLine("ChartInfoStrips Axes_CollectionResetting");
            foreach (Axis axis in _owner.Axes)
            {
                axis.SizeChanged -= Axis_SizeChanged;
            }
            RefreshStrips();
        }

        #endregion

        #region Methods
        private void GetAxes(out Axis xAxis, out Axis yAxis)
        {
            if (_owner == null)
            {
                xAxis = null;
                yAxis = null;
                return;
            }

            xAxis =
               (from axis in _owner.Axes
                where axis is NumericXAxis ||
                axis is CategoryXAxis ||
                axis is CategoryDateTimeXAxis
                select axis).FirstOrDefault();

            yAxis =
                (from axis in _owner.Axes
                 where axis is NumericYAxis
                 select axis).FirstOrDefault();
        }
        private void RemoveStrips(Axis xAxis)
        {
            if (xAxis.RootCanvas == null)
            {
                return;
            }
            Debug.WriteLine("ChartInfoStrips RemoveStrips xAxis=" + xAxis.RootCanvas.Children.Count + "...");
            
            var existing =
               from child in xAxis.RootCanvas.Children.OfType<UIElement>()
               where child is ContentControl &&
               (child as ContentControl).Content != null &&
               (child as ContentControl).Content is InfoStrip
               select child;
            existing.ToList().ForEach(ele => xAxis.RootCanvas.Children.Remove(ele));

            Debug.WriteLine("ChartInfoStrips RemoveStrips xAxis=" + xAxis.RootCanvas.Children.Count);
        }
        private void RefreshStrips()
        {
            Axis xAxis;
            Axis yAxis;
            GetAxes(out xAxis, out yAxis);

            if (xAxis == null || yAxis == null || xAxis.RootCanvas == null)
            {
                return;
            }

            RemoveStrips(xAxis);
            
            Rect viewport = GetViewportRect(xAxis);
            Rect window = _owner.WindowRect;
            
            Debug.WriteLine("ChartInfoStrips RefreshStrips viewport=" + viewport);
            Debug.WriteLine("ChartInfoStrips RefreshStrips window=" + window);

            foreach (InfoStrip strip in this.InfoStrips)
            {
                InfoStrip toAdd = strip.Clone();

                bool isInverted = xAxis.IsInverted;
                ScalerParams param = new ScalerParams(window, viewport, isInverted);
                
                if (!toAdd.UseDates)
                {
                    if (double.IsNaN(toAdd.StartX))
                    {
                        toAdd.StartX = viewport.Left;
                    }
                    else
                    {
                        toAdd.StartX = xAxis.GetScaledValue(toAdd.StartX, param);
                        //NOTE: prior to 12.1 release: 
                        //toAdd.StartX = xAxis.GetScaledValue(toAdd.StartX, window, viewport);
                    }
                    if (double.IsNaN(toAdd.EndX))
                    {
                        toAdd.EndX = viewport.Right;
                    }
                    else
                    {
                        toAdd.EndX = xAxis.GetScaledValue(toAdd.EndX, param);
                        //NOTE: prior to 12.1 release: 
                        //toAdd.EndX = xAxis.GetScaledValue(toAdd.EndX, window, viewport);
                    }
                }
                else
                {
                    toAdd.StartX = xAxis.GetScaledValue(toAdd.StartDate.Ticks, param);
                    toAdd.EndX = xAxis.GetScaledValue(toAdd.EndDate.Ticks, param);
                    //NOTE: prior to 12.1 release: 
                    //toAdd.StartX = xAxis.GetScaledValue(toAdd.StartDate.Ticks, window, viewport);
                    //toAdd.EndX = xAxis.GetScaledValue(toAdd.EndDate.Ticks, window, viewport);
                }
                // check if y-axis is inverted
                isInverted = yAxis.IsInverted;
                param = new ScalerParams(window, viewport, isInverted);
              
                if (double.IsNaN(toAdd.StartY))
                {
                    toAdd.StartY = viewport.Top;
                }
                else
                {
                    toAdd.StartY = yAxis.GetScaledValue(toAdd.StartY, param);
                    //NOTE: prior to 12.1 release: 
                    //toAdd.StartY = yAxis.GetScaledValue(toAdd.StartY, window, viewport);
                }
                if (double.IsNaN(toAdd.EndY))
                {
                    toAdd.EndY = viewport.Bottom;
                }
                else
                {
                    toAdd.EndY = yAxis.GetScaledValue(toAdd.EndY, param);
                    //NOTE: prior to 12.1 release: 
                    //toAdd.EndY = yAxis.GetScaledValue(toAdd.EndY, window, viewport);
                }
                if (toAdd.StripTemplate == null)
                {
                    toAdd.StripTemplate = this.InfoStripTemplate;
                }

                if (toAdd.StripTemplate != null &&
                    !double.IsNaN(toAdd.StartY) &&
                    !double.IsInfinity(toAdd.StartY) &&
                    !double.IsNaN(toAdd.StartX) &&
                    !double.IsInfinity(toAdd.StartX))
                {
                    ContentControl stripControl = new ContentControl();
                    stripControl.ContentTemplate = toAdd.StripTemplate;
                    stripControl.Content = toAdd;
                    Canvas.SetLeft(stripControl, toAdd.StartX);
                    Canvas.SetTop(stripControl, toAdd.StartY);

                    
                    Debug.WriteLine("ChartInfoStrips RefreshStrips add=" + toAdd);
                    xAxis.RootCanvas.Children.Add(stripControl);
                }
            }
            
            Debug.WriteLine("ChartInfoStrips RefreshStrips xAxis=" + xAxis.RootCanvas.Children.Count);
            Debug.WriteLine("ChartInfoStrips RefreshStrips yAxis=" + yAxis.RootCanvas.Children.Count);
        }
        private Rect GetViewportRect(Axis axis)
        {
            double top = 0;
            double bottom = axis.ActualHeight;
            double left = 0;
            double right = axis.ActualWidth;

            double width = right - left;
            double height = bottom - top;

            if (width > 0.0 && height > 0.0)
            {
                return new Rect(left, top, width, height);
            }
            return Rect.Empty;
        } 
        #endregion

    }

    public class InfoStrip : DependencyObject, INotifyPropertyChanged
    {
        #region Constructors
        public InfoStrip()
        {
            _id = Guid.NewGuid();
            StartX = double.NaN;
            EndX = double.NaN;
            StartY = double.NaN;
            EndY = double.NaN;
        }
        private InfoStrip(Guid id)
        {
            _id = id;
            StartX = double.NaN;
            EndX = double.NaN;
            StartY = double.NaN;
            EndY = double.NaN;
        }
        #endregion

        #region Properties
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public double StartX { get; set; }
        public double EndX { get; set; }
        public double StartY { get; set; }
        public double EndY { get; set; }

        private string _startDateString;
        public string StartDateString
        {
            get
            {
                return _startDateString;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                _startDateString = value;
                StartDate = DateTime.Parse(_startDateString);
            }
        }
        private string _endDateString;
        public string EndDateString
        {
            get
            {
                return _endDateString;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                _endDateString = value;
                EndDate = DateTime.Parse(_endDateString);
            }
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool UseDates { get; set; }

        public Brush Fill { get; set; }
        public DataTemplate StripTemplate { get; set; }

        #region Dependency Properties
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
         "Label", typeof(string), typeof(InfoStrip), new PropertyMetadata(new PropertyChangedCallback(OnLabelChanged)));

        private static void OnLabelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            InfoStrip owner = (InfoStrip)obj;
            owner.OnPropertyChanged("Label");
        }
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        #endregion 
        public double Width
        {
            get { return System.Math.Abs(EndX - StartX); }
        }
        public double Height
        {
            get { return System.Math.Abs(EndY - StartY); }
        }

        #endregion

        #region Event Handlers
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Methods
        public InfoStrip Clone()
        {
            InfoStrip newStrip = new InfoStrip(_id);
            newStrip.StartX = StartX;
            newStrip.StartY = StartY;
            newStrip.EndX = EndX;
            newStrip.EndY = EndY;
            newStrip.Label = Label;
            newStrip.StripTemplate = StripTemplate;
            newStrip.Fill = Fill;
            newStrip.UseDates = UseDates;
            newStrip.StartDate = StartDate;
            newStrip.EndDate = EndDate;

            return newStrip;
        }

        public override string ToString()
        {
            return "X " + StartX + " ... " + EndX + ", W=" + Width +
                " Y " + StartY + " ... " + EndY + ", H=" + Width + ", " + Label;
                 
        }
        #endregion
    }
    public class InfoStripCollection : ObservableCollection<InfoStrip>
    { }
}