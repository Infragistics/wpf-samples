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
