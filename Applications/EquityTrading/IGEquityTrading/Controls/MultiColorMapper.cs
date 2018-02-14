//-------------------------------------------------------------------------
// <copyright file="StringComparisonToBoolValueConverter.cs" company="Infragistics">
//
// Copyright (c) 2010 Infragistics, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
//-------------------------------------------------------------------------

using System.Reflection;
using System.Windows.Media;
using IGTrading.ViewModels;
using Infragistics.Controls.Charts;

namespace IGTrading.Controls
{
    class MultiColorMapper : RangeMapper
    {
        public Color LowColor { get; set; }
        public Color MiddleColor { get; set; }
        public Color HighColor { get; set; }

        public override void MapValue(TreemapNode node)
        {
            if (node.DataContext == null) return;

            if (node.DataContext is IndustryViewModel)
            {
                double value = GetMappedValue(node.DataContext);

                double relativeValue = value > 0
                                           ? GetRelativeValue(value, 0, this.DataMaximum)
                                           : GetRelativeValue(value, this.DataMinimum, 0);

                Color color = value > 0
                                  ? GetColor(relativeValue, this.MiddleColor, this.HighColor)
                                  : GetColor(relativeValue, this.LowColor, this.MiddleColor);

                node.Fill = new SolidColorBrush(color);
            }
        }

        public override void ResetValue(TreemapNode node)
        { }

        private double GetMappedValue(object dataContext)
        {
            object value = null;

            if (string.IsNullOrEmpty(this.ValuePath) == false)
            {
                PropertyInfo propInfo = typeof(IndustryViewModel).GetProperty(this.ValuePath);

                if (propInfo != null)
                {
                    value = propInfo.GetValue(dataContext, null);
                }
            }

            return (double)value;
        }

        private double GetRelativeValue(double value, double dataMinimum, double dataMaximum)
        {
            double relativeValue = 0;

            double min = double.IsNaN(dataMinimum) ? this.ActualDataMinimum : dataMinimum;
            double max = double.IsNaN(dataMaximum) ? this.ActualDataMaximum : dataMaximum;

            if (max > min)
            {
                relativeValue = (value - min) / (max - min);
            }
            else if (min > max)
            {
                relativeValue = (value - max) / (min - max);
            }
            else
            {
                relativeValue = 0;
            }


            if (relativeValue < 0)
            {
                relativeValue = 0;
            }
            if (relativeValue > 1)
            {
                relativeValue = 1;
            }

            return relativeValue;
        }

        private Color GetColor(double relativeValue, Color fromColor, Color toColor)
        {
            return Color.FromArgb(
                (byte)(fromColor.A + relativeValue * (toColor.A - fromColor.A)),
                (byte)(fromColor.R + relativeValue * (toColor.R - fromColor.R)),
                (byte)(fromColor.G + relativeValue * (toColor.G - fromColor.G)),
                (byte)(fromColor.B + relativeValue * (toColor.B - fromColor.B)));
        }
    }
}
