using System.Collections.Specialized;
using System.Windows.Media;
using Infragistics;
using Infragistics.Controls.Charts;

namespace IGGeographicMap.Extensions
{
    public class GeoMarkerBrushScale : GeoMarkerSizeScale
    {
        public GeoMarkerBrushScale()
        {
            Brushes = new BrushCollection();
            Brushes.CollectionChanged += Brushes_CollectionChanged;

        }
        #region Event Handlers

        /// <summary>
        /// Called when the members of the brushes collection change.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        protected virtual void Brushes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }
        #endregion

        private BrushCollection _brushes = null;
        /// <summary>
        /// Gets the brushes collection used by this scale.
        /// </summary>
        public BrushCollection Brushes
        {
            get { return _brushes; }
            set
            {
                if (_brushes != null) _brushes.CollectionChanged -= Brushes_CollectionChanged;
                _brushes = value;
                if (_brushes != null) _brushes.CollectionChanged += Brushes_CollectionChanged;
                OnPropertyChanged("Brushes");
            }
        }

        public virtual Brush GetBrushByValue(double value)
        {
            double scaledValue = GetScaledValue(value);
            double scaledBrushIndex = scaledValue * (Brushes.Count - 1);

            return GetInterpolatedBrush(scaledBrushIndex);

            //double min = double.IsNaN(MinimumValue) || double.IsInfinity(MinimumValue) ? 0 : this.MinimumValue;
            //double max = double.IsNaN(MaximumValue) || double.IsInfinity(MaximumValue) ? 100 : this.MaximumValue;

            //if (value < min)
            //{
            //    value = min;
            //}

            //if (value > max)
            //{
            //    value = max;
            //}

            //return GetInterpolatedBrushLogarithmic(min, max, value);
        }
        //private Brush GetInterpolatedBrushLogarithmic(double min, double max, double value)
        //{
        //    if (IsLogarithmic && LogarithmBase > 0)
        //    {
        //        double newMin = System.Math.Log(min, LogarithmBase);
        //        double newMax = System.Math.Log(max, LogarithmBase);
        //        double newValue = System.Math.Log(value, LogarithmBase);

        //        return GetInterpolatedBrushLinear(newMin, newMax, newValue);
        //    }

        //    return GetInterpolatedBrushLinear(min, max, value);
        //}
        //private Brush GetInterpolatedBrushLinear(double min, double max, double value)
        //{
        //    //if the value is outside the range, return a null brush
        //    if (value < min || value > max) return null;

        //    double scaledValue = (value - min) / (max - min);
        //    double scaledBrushIndex = scaledValue * (Brushes.Count - 1);

        //    return GetInterpolatedBrush(scaledBrushIndex);
        //}

        /// <summary>
        /// Gets a brush from the brushes collection by index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Brush for a given index.</returns>
        public virtual Brush GetBrush(int index)
        {
            if (Brushes == null || index < 0 || index >= Brushes.Count)
            {
                return null;
            }

            return Brushes[index];
        }

        /// <summary>
        /// Returns an interpolated brush value based on index.
        /// </summary>
        /// <param name="index">The index to use.</param>
        /// <returns>The interpolated brush.</returns>
        protected internal Brush GetInterpolatedBrush(double index)
        {
            if (Brushes == null || Brushes.Count == 0 || index < 0)
            {
                return null;
            }
            return Brushes[index];
        }

    }


    public class GeoMarkerBrushScales
    {
        public static BrushCollection GetRedBrushes()
        {
            var brushes = new BrushCollection { new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Red) };
            return brushes;
        }
        public static GeoMarkerBrushScale RedBrushScale = new GeoMarkerBrushScale
        {
            MinimumValue = 0.0,
            MaximumValue = 10.0,
            Brushes = new BrushCollection { new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Red) }
        };
        public static GeoMarkerBrushScale GreenBrushScale = new GeoMarkerBrushScale
        {
            MinimumValue = 0.0,
            MaximumValue = 10.0,
            Brushes = new BrushCollection { new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Green) }
        };
        public static GeoMarkerBrushScale BlueBrushScale = new GeoMarkerBrushScale
        {
            MinimumValue = 0.0,
            MaximumValue = 10.0,
            Brushes = new BrushCollection { new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Blue) }
        };


    }
    public class GeoMarkerBrushScale22 : ValueBrushScale
    {
        public virtual Brush GetBrushByValue(double value)
        {
            double min = double.IsNaN(MinimumValue) || double.IsInfinity(MinimumValue) ? 0 : this.MinimumValue;
            double max = double.IsNaN(MaximumValue) || double.IsInfinity(MaximumValue) ? 100 : this.MaximumValue;

            if (value < min)
            {
                value = min;
            }

            if (value > max)
            {
                value = max;
            }

            return GetInterpolatedBrushLogarithmic(min, max, value);
        }
        private Brush GetInterpolatedBrushLogarithmic(double min, double max, double value)
        {
            if (IsLogarithmic && LogarithmBase > 0)
            {
                double newMin = System.Math.Log(min, LogarithmBase);
                double newMax = System.Math.Log(max, LogarithmBase);
                double newValue = System.Math.Log(value, LogarithmBase);

                return GetInterpolatedBrushLinear(newMin, newMax, newValue);
            }

            return GetInterpolatedBrushLinear(min, max, value);
        }
        private Brush GetInterpolatedBrushLinear(double min, double max, double value)
        {
            //if the value is outside the range, return a null brush
            if (value < min || value > max) return null;

            double scaledValue = (value - min) / (max - min);
            double scaledBrushIndex = scaledValue * (Brushes.Count - 1);

            return GetInterpolatedBrush(scaledBrushIndex);
        }

    }
   

}