using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace System.Windows.Media
{
    public static class ColorEx
    {
        public static Color Opacity(this Color color, double opacity)
        {
            var alpha = (byte)(255 * opacity);
            return Color.FromArgb(alpha, color.R, color.G, color.B);
        }

        public static List<Color> ToColors(this SolidColorBrush[] brushes)
        {
            return brushes.Select(b => b.Color).ToList();
        }
        public static List<Color> ToColors(this IList<SolidColorBrush> brushes)
        {
            return brushes.Select(b => b.Color).ToList();
        }

        public static SolidColorBrush ToBrush(this Color color)
        {
            return new SolidColorBrush(color);
        }

        public static LinearGradientBrush ToBrush(this SolidColorBrush[] brushes, Point? start = null, Point? end = null)
        {
            return brushes.ToColors().ToBrush(start, end);
        }
        public static LinearGradientBrush ToBrush(this IList<SolidColorBrush> brushes, Point? start = null, Point? end = null)
        {
            return brushes.ToColors().ToBrush(start, end);
        }

        public static LinearGradientBrush ToBrush(this Color[] colors, Point? start = null, Point? end = null)
        {
            return colors.ToList().ToBrush(start, end);
        }
        public static LinearGradientBrush ToBrush(this IList<Color> colors, Point? start = null, Point? end = null)
        {
            var gradient = new LinearGradientBrush();
            // default to vertical gradient
            gradient.StartPoint = start.HasValue ? start.Value : new Point(0, 0);
            gradient.EndPoint = end.HasValue ? end.Value : new Point(1, 0);

            var offset = 0.0;
            var step = 1.0 / colors.Count();
            foreach (var color in colors)
            {
                var stop = new GradientStop { Color = color, Offset = offset };
                gradient.GradientStops.Add(stop);

                offset += step;
            }
            return gradient;
        }

        public static LinearGradientBrush ToBrush(this SolidColorBrush[] brushes, GradientOrientation orientation)
        {
            return brushes.ToColors().ToBrush(orientation);
        }
        public static LinearGradientBrush ToBrush(this IList<SolidColorBrush> brushes, GradientOrientation orientation)
        {
            return brushes.ToColors().ToBrush(orientation);
        }

        public static LinearGradientBrush ToBrush(this Color[] colors, GradientOrientation orientation)
        {
            return colors.ToList().ToBrush(orientation);
        }
        public static LinearGradientBrush ToBrush(this IList<Color> colors, GradientOrientation orientation)
        {
            var start = new Point(0, 0);
            var end = new Point(1, 0);

            if (orientation == GradientOrientation.Vertical)
            {
                start = new Point(0, 0);
                end = new Point(1, 0);
            }
            else if (orientation == GradientOrientation.Horizontal)
            {
                start = new Point(0, 0);
                end = new Point(0, 1);
            }
            else if (orientation == GradientOrientation.Diagnal)
            {
                start = new Point(0, 0);
                end = new Point(1, 1);
            }
            return colors.ToList().ToBrush(start, end);
        }
    }

    public enum GradientOrientation
    {
        Default,
        Vertical,
        Horizontal,
        Diagnal,
    }
}