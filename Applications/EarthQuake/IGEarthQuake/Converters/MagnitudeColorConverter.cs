using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using IGExtensions.Common.Models;
using IGExtensions.Framework;

namespace IGShowcase.EarthQuake.Converters
{
	public sealed class MagnitudeColorConverter : IValueConverter
	{

		#region IValueConverter Members

		/// <summary>
		/// Modifies the source data before passing it to the target for display in the UI.
		/// 
		/// This ValueConverter will take the "value" parameter and use it to find a solid color from the gradient
		/// This is done by treat the "value" as a scale/index to use as a lookup inside the gradient
		/// </summary>
		/// <param name="value">The source data being passed to the target.</param>
		/// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
		/// <param name="parameter">An optional parameter to be used in the converter logic.</param>
		/// <param name="culture">The culture of the conversion.</param>
		/// <returns>
		/// The value to be passed to the target dependency property.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null || parameter == null || !(parameter is GradientBrush)) 
				return null;

			GradientBrush brush = (GradientBrush)parameter;

			if (brush.GradientStops.Count == 0)
			{
				return null;
			}

			if (brush.GradientStops.Count == 1)
			{
				return brush.GradientStops[0].Color;
			}

			// Magnitude is from 0 to 10. Bring it into the 0..1 interval
			double v;
			double.TryParse(value.ToString(), out v);
			v = v / 10.0;

			var stops = brush.GradientStops.OrderBy(x=>x.Offset);

			GradientStop s1 = stops.First();
			SolidColorBrush result;

			//Find such gradient stops s1 and s2 that s1.Offset<v<s2.Offset and interpolate the color
			//If v < s.Offset for every stop s, then return the color of the first stop
			//If v > s.Offset for every stop s, then return the color of the last stop
			if(!(v > s1.Offset))
			{
				result = new SolidColorBrush(s1.Color);
			}
			else
			{
				GradientStop s2 = s1;

				foreach (GradientStop stop in stops.Skip(1))
				{
					s1 = s2;
					s2 = stop;
					if (!(v > stop.Offset)) break;
				}

				if (v < s2.Offset)
				{
					Color color = InterpolateColor(s1.Color, s2.Color, (v - s1.Offset)/(s2.Offset - s1.Offset));
					result = new SolidColorBrush(color);
				}
				else
				{
					result = new SolidColorBrush(s2.Color);
				}
			}
			return result;
		}

		/// <summary>
		/// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
		/// </summary>
		/// <param name="value">The target data being passed to the source.</param>
		/// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
		/// <param name="parameter">An optional parameter to be used in the converter logic.</param>
		/// <param name="culture">The culture of the conversion.</param>
		/// <returns>
		/// The value to be passed to the source object.
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion

		/// <summary>
		/// Interpolates the color.
		/// </summary>
		/// <param name="c1">The c1.</param>
		/// <param name="c2">The c2.</param>
		/// <param name="t">The t.</param>
		/// <returns></returns>
		private static Color InterpolateColor(Color c1, Color c2, double t)
		{
			int aDt = c2.A - c1.A;
			int rDt = c2.R - c1.R;
			int gDt = c2.G - c1.G;
			int bDt = c2.B - c1.B;

			return Color.FromArgb(
				(byte)(c1.A + Math.Round(aDt * t)),
				(byte)(c1.R + Math.Round(rDt * t)),
				(byte)(c1.G + Math.Round(gDt * t)),
				(byte)(c1.B + Math.Round(bDt * t))
			); 
		}
	}

    public sealed class MagnitudeColorScaleConverter : ObservableObject, IValueConverter
    {
        public static DependencyProperty ScaleBrushProperty = DependencyProperty.Register("ScaleBrush",
            typeof(GradientBrush), typeof(MagnitudeColorScaleConverter),
            new PropertyMetadata(GradientGrayScaleBrush));
        /// <summary>
        /// Gets or sets a value for the ScaleBrush property
        /// </summary>
        public GradientBrush ScaleBrush { get { return (GradientBrush)GetValue(ScaleBrushProperty); } set { SetValue(ScaleBrushProperty, value); } }
        
        public static GradientBrush GradientGrayScaleBrush 
        { 
            get 
            {
                var brush = new LinearGradientBrush();
                brush.StartPoint = new Point(0, 0.5);
                brush.EndPoint = new Point(1, 0.5);
                brush.GradientStops.Add(new GradientStop { Color = Colors.Yellow, Offset = 0.0 });
                brush.GradientStops.Add(new GradientStop { Color = Colors.Orange, Offset = 0.0 });
                brush.GradientStops.Add(new GradientStop { Color = Colors.Red, Offset = 0.0 });
                return brush;
            }
        }
       

        #region IValueConverter Members

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// 
        /// This ValueConverter will take the "value" parameter and use it to find a solid color from the gradient
        /// This is done by treat the "value" as a scale/index to use as a lookup inside the gradient
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || this.ScaleBrush == null)
                return null;

            var brush = this.ScaleBrush; // (GradientBrush)parameter;

            if (brush.GradientStops.Count == 0)
            {
                return null;
            }

            if (brush.GradientStops.Count == 1)
            {
                return brush.GradientStops[0].Color;
            }

            // Magnitude is from 0 to 10. Bring it into the 0..1 interval
            double v;
            double.TryParse(value.ToString(), out v);
            v = v / 10.0;

            var stops = brush.GradientStops.OrderBy(x => x.Offset);

            var s1 = stops.First();
            SolidColorBrush result;

            //Find such gradient stops s1 and s2 that s1.Offset<v<s2.Offset and interpolate the color
            //If v < s.Offset for every stop s, then return the color of the first stop
            //If v > s.Offset for every stop s, then return the color of the last stop
            if (!(v > s1.Offset))
            {
                result = new SolidColorBrush(s1.Color);
            }
            else
            {
                var s2 = s1;
                foreach (GradientStop stop in stops.Skip(1))
                {
                    s1 = s2;
                    s2 = stop;
                    if (!(v > stop.Offset)) break;
                }

                if (v < s2.Offset)
                {
                    var color = InterpolateColor(s1.Color, s2.Color, (v - s1.Offset) / (s2.Offset - s1.Offset));
                    result = new SolidColorBrush(color);
                }
                else
                {
                    result = new SolidColorBrush(s2.Color);
                }
            }
            return result;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Interpolates the color.
        /// </summary>
        /// <param name="c1">The c1.</param>
        /// <param name="c2">The c2.</param>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        private static Color InterpolateColor(Color c1, Color c2, double t)
        {
            int aDt = c2.A - c1.A;
            int rDt = c2.R - c1.R;
            int gDt = c2.G - c1.G;
            int bDt = c2.B - c1.B;

            return Color.FromArgb(
                (byte)(c1.A + Math.Round(aDt * t)),
                (byte)(c1.R + Math.Round(rDt * t)),
                (byte)(c1.G + Math.Round(gDt * t)),
                (byte)(c1.B + Math.Round(bDt * t))
            );
        }
    }

}
