using System;
using System.ComponentModel;
using System.Windows.Threading;
using IGExtensions.Common.Models;
using IGExtensions.Framework;

namespace IGExtensions.Common.Frameworks
{
    /// <summary>
    /// Represents the Motion Framework for animating data over time 
    /// </summary>
    public class MotionTimeFramework : ObservableModel
    {
        public MotionTimeFramework()
        {
            MotionSlider = new MotionTimeSlider();
            //MotionSliderInterval = TimeSpan.FromMinutes(1);
            MotionSlider.PropertyChanged += OnMotionSliderChanged;
            this.MotionUpdateTimer = new DispatcherTimer(); // { Interval = MotionUpdateInterval };
            this.MotionUpdateInterval = TimeSpan.FromMilliseconds(20);
            this.MotionUpdateTimer.Tick += OnMotionUpdateTimerTick;
        }

        #region Properties
        protected DispatcherTimer MotionUpdateTimer = new DispatcherTimer();
        public TimeSpan MotionUpdateInterval { get { return MotionUpdateTimer.Interval; } set { MotionUpdateTimer.Interval = value; } }
       
        // public bool IsMotionEnabled { get { return MotionUpdateTimer.IsEnabled; } }

        private bool _isMotionStarted = false;
        /// <summary>
        /// Gets or sets weather the Motion Framework is started 
        /// </summary>
        public bool IsMotionStarted
        {
            get { return _isMotionStarted; }
            set { if (_isMotionStarted == value) return; _isMotionStarted = value; UpdateMotionTimer(); OnPropertyChanged("IsMotionStarted"); }
        }
        private MotionTimeSlider _motionSlider = new MotionTimeSlider();
        /// <summary>
        /// Gets or sets the slider of the Motion Framework
        /// </summary>
        public MotionTimeSlider MotionSlider
        {
            get { return _motionSlider; }
            set { if (_motionSlider == value) return; _motionSlider = value; OnPropertyChanged("MotionSlider"); }
        }
        #endregion
        private void OnMotionSliderChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("MotionSlider");
        }
        private void OnMotionUpdateTimerTick(object sender, EventArgs e)
        {
            MotionSlider.Value = MotionSlider.Value.AddMinutes(MotionSlider.Interval.TotalMinutes);
            if (MotionSlider.Value >= MotionSlider.MaxValue)
            {
                IsMotionStarted = false;
                OnPropertyChanged("IsMotionStarted");
                //ToggleMotionTimer();
                //MotionTimeSlider.Value = MotionTimeSlider.MinValue;
                //this.MotionToggleButton.IsChecked = false;
            }
        }
        private void UpdateMotionTimer()
        {
            if (this.IsMotionStarted)
            {
                if (MotionSlider.Value >= MotionSlider.MaxValue)
                {
                    MotionSlider.Value = MotionSlider.MinValue;
                }
                this.MotionSlider.IsEnabled = false;
                MotionUpdateTimer.Start();
            }
            else
            {
                if (MotionUpdateTimer.IsEnabled)
                {
                    this.MotionSlider.IsEnabled = true;
                    //IsMotionStarted = false;
                    MotionUpdateTimer.Stop();
                }
            }
        }
        public void ToggleMotionTimer()
        {
            IsMotionStarted = !IsMotionStarted;

            //if (MotionUpdateTimer.IsEnabled)
            //{
            //    //IsMotionStarted = false;
            //    MotionUpdateTimer.Stop();
            //}
            //else
            //{
            //    if (MotionSlider.Value >= MotionSlider.MaxValue)
            //    {
            //        MotionSlider.Value = MotionSlider.MinValue;
            //    }
            //    //IsMotionStarted = true;
            //    MotionUpdateTimer.Start();
            //}
            //OnPropertyChanged("IsMotionEnabled");
            //OnPropertyChanged("IsMotionSliderEnabled");
            // OnPropertyChanged("IsMotionSliderEnabled");
        }
    }

    public class MotionTimeSlider : ObservableModel
    {
        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { if (_isEnabled == value) return; _isEnabled = value; OnPropertyChanged("IsEnabled"); }
        }

        private DateTime _minValue;
        public DateTime MinValue
        {
            get { return _minValue; }
            set
            { //if (_minValue == value) return;
                _minValue = value; OnPropertyChanged("MinValue");
            }
        }
        private DateTime _maxValue;
        public DateTime MaxValue
        {
            get { return _maxValue; }
            set
            { //if (_maxValue == value) return; 
                _maxValue = value; OnPropertyChanged("MaxValue");
            }
        }
        private DateTime _value;
        public DateTime Value
        {
            get { return _value; }
            set { if (_value == value) return; _value = value; OnPropertyChanged("Value"); }
        }
        private TimeSpan _valueeChange = TimeSpan.FromMinutes(1);
        public TimeSpan Interval
        {
            get { return _valueeChange; }
            set { if (_valueeChange == value) return; _valueeChange = value; OnPropertyChanged("Interval"); }
        }
    }

}