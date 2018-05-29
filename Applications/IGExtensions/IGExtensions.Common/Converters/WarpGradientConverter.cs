using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace IGExtensions.Common.Converters
{
	public class WarpGradientConverter : IValueConverter
	{
		#region IValueConverter Members
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null || parameter == null ||
				!(value is WarpGradientParameters) ||
				!(parameter is LinearGradientBrush))
			{
				return null;
			}

			var warpParams = (WarpGradientParameters)value;
			double offset = warpParams.Offset;
			double scale = warpParams.Scale;
			var originalBrush = (LinearGradientBrush)parameter;

			if (originalBrush.GradientStops.Count < 2) return originalBrush;
			if (offset == 0.0 && scale == 1.0) return originalBrush;
			if (!(scale > 0)) throw new ArgumentException("Scale > 0.0");

			//OFFSET
			// Arbitrary if GradientSpreadMethod == Pad. The size of the translation vector
			// 0.0 <= offset < 1.0 - if GradientSpreadMethod != Pad, the offset as a percent of the gradient period
			//SCALE
			// scale > 0.0
			// scale > 1.0 - zoomin
			// scale < 1.0 - zoomout

			Point startPoint;
			Point endPoint;

			double dx = (originalBrush.EndPoint.X - originalBrush.StartPoint.X) * scale;
			double dy = (originalBrush.EndPoint.Y - originalBrush.StartPoint.Y) * scale;

			if (originalBrush.SpreadMethod == GradientSpreadMethod.Pad)
			{
				double angle = Math.Atan2(dy, dx);
				startPoint = new Point(
						originalBrush.StartPoint.X - offset * Math.Cos(angle),
						originalBrush.StartPoint.Y - offset * Math.Sin(angle)
					);

				endPoint = new Point(
						startPoint.X + dx,
						startPoint.Y + dy
					);

				//TODO: Check if the gradient is outside the 0,0,1,1 rectangle and replace with a single color
			}
			else
			{
				if (offset < 0.0 || !(offset < 1.0)) throw new ArgumentException("0.0 <= Offset < 1.0");

				if (originalBrush.SpreadMethod == GradientSpreadMethod.Reflect)
				{
					offset *= 2.0;
				}

				startPoint = new Point(
						originalBrush.StartPoint.X - (dx * offset),
						originalBrush.StartPoint.Y - (dy * offset)
					);
				endPoint = new Point(
						startPoint.X + dx,
						startPoint.Y + dy
					);
			}

			var stops = new GradientStopCollection();
			foreach (GradientStop stop in originalBrush.GradientStops)
			{
				stops.Add(new GradientStop { Offset = stop.Offset, Color = stop.Color });
			}

			return new LinearGradientBrush
     			{
     				StartPoint = startPoint,
     				EndPoint = endPoint,
     				GradientStops = stops,
     				ColorInterpolationMode = originalBrush.ColorInterpolationMode,
     				MappingMode = originalBrush.MappingMode,
     				Opacity = originalBrush.Opacity,
     				SpreadMethod = originalBrush.SpreadMethod,
     				RelativeTransform = originalBrush.RelativeTransform,
     				Transform = originalBrush.Transform
     			};
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		#endregion IValueConverter Members
	}
    public class WarpGradientParameters
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WarpGradientParameters"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="scale">The scale.</param>
        public WarpGradientParameters(double offset, double scale)
        {
            Offset = offset;
            Scale = scale;
        }
        #endregion Constructors

        #region Public Properties
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public double Offset { get; private set; }
        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>The scale.</value>
        public double Scale { get; private set; }
        #endregion Public Properties
    }
}
