using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace IGExtensions.Framework.Transitions
{
    public abstract class Transition : DependencyObject
    {
        #region EasingFunction Dependency Property
        /// <summary>
        /// Sets or gets the EasingFunction for this layer.
        /// <para>This is a dependency property.</para>
        /// </summary>
        public IEasingFunction EasingFunction
        {
            get
            {
                return (IEasingFunction)GetValue(EasingFunctionProperty);
            }
            set
            {
                SetValue(EasingFunctionProperty, value);
            }
        }

        internal const string EasingFunctionPropertyName = "EasingFunction";

        /// <summary>
        /// Identifies the EasingFunction dependency property.
        /// </summary>
        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(EasingFunctionPropertyName, typeof(IEasingFunction), typeof(Transition),
            new PropertyMetadata(null, (sender, e) =>
            {
                //(sender as Transition).OnPropertyChanged(new PropertyChangedEventArgs<IEasingFunction>(EasingFunctionPropertyName, (IEasingFunction)e.OldValue, (IEasingFunction)e.NewValue));
            }));
        #endregion

        #region Duration Dependency Property
        /// <summary>
        /// Sets or gets the Duration for this layer.
        /// <para>This is a dependency property.</para>
        /// </summary>
        public Duration Duration
        {
            get
            {
                return (Duration)GetValue(DurationProperty);
            }
            set
            {
                SetValue(DurationProperty, value);
            }
        }

        internal const string DurationPropertyName = "Duration";

        /// <summary>
        /// Identifies the Duration dependency property.
        /// </summary>
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(DurationPropertyName, typeof(Duration), typeof(Transition),
            new PropertyMetadata(new Duration(TimeSpan.FromSeconds(1.0)), (sender, e) =>
            {
                //(sender as Transition).OnPropertyChanged(new PropertyChangedEventArgs<Duration>(DurationPropertyName, (Duration)e.OldValue, (Duration)e.NewValue));
            }));
        #endregion

        public abstract TransitionState CreateTransitionState();
    }

    public abstract class TransitionState
    {
        public Transition Transition { get; set; }
        public UIElement UIElement { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Action<UIElement> Done { get; set; }

        public abstract double Progress { get; set; }
    }

}
