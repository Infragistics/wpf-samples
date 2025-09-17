using System;
using System.Windows;

namespace Infragistics.Samples.Shared.Extensions
{
    public static class RectExtension
    {
        public static Point CenterPoint(this Rect rect)
        {
            double x = rect.X + (rect.Width / 2);
            double y = rect.Y + (rect.Height / 2);
            return new Point(x,y);
        }

        /// <summary>
        /// Scales the current Rect by a factor value in all directions
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="factor">scale factor in percents of original size of the Rect</param>
        public static Rect Scale(this Rect rect, double factor)
        {
            double scale = factor / 100.0;
            double scaleVertical = (rect.Height * scale);
            double scaleHorizontal = (rect.Width * scale);
            Rect newRect = rect.ScaleInflate(scaleHorizontal, scaleVertical);
            return newRect; 
        }
        /// <summary>
        /// Inflates the current Rect by a dimension value in all directions
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public static Rect Inflate(this Rect rect, double dimension)
        {
            Rect newRect;
            newRect = rect.ScaleInflate(dimension, dimension);
            return newRect; 
        }
        /// <summary>
        /// Inflates the current Rect by a dimension values in vertical and horizontal directions
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="dimensionX"></param>
        /// <param name="dimensionY"></param>
        /// <returns></returns>
        public static Rect ScaleInflate(this Rect rect, double dimensionX, double dimensionY)
        {
            double width = rect.Width + dimensionX;
            double height = rect.Height + dimensionY;
            double x = rect.X - dimensionX;
            double y = rect.Y - dimensionY;
            Rect newRect = new Rect(x, y, width, height);
            return newRect;
        }

        public static Rect ScaleDown(this Rect rect, double factor)
        {
            double scale = factor / 100.0;
            double scaleVertical = (rect.Height * scale);
            double scaleHorizontal = (rect.Width * scale);

            double width = rect.Width - scaleHorizontal;
            double height = rect.Height - scaleVertical;
            double x = rect.X + scaleHorizontal;
            double y = rect.Y + scaleVertical;
            Rect newRect = new Rect(x, y, width, height);
            return newRect;
        }

        public static string FormatRect(this Rect rc)
        {
            string str = string.Empty;
            str += String.Format("{0:0.00}, ", rc.X);
            str += String.Format("{0:0.00}, ", rc.Y);
            str += String.Format("{0:0.00}, ", rc.Width);
            str += String.Format("{0:0.00}", rc.Height);
            return str;
        }
        public static string FormatToString(this Rect rc, string format = "0.0000")
        {
            string str = string.Empty;
            str += String.Format("{0: " + format + "}, ", rc.X);
            str += String.Format("{0: " + format + "}, ", rc.Y);
            str += String.Format("{0: " + format + "}, ", rc.Width);
            str += String.Format("{0:" + format + "} ", rc.Height);
            return str;
        }
    }
}