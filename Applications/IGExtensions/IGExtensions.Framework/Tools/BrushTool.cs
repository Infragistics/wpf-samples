using System;
using System.Windows;
using System.Windows.Media;

namespace IGExtensions.Framework.Tools
{
    public static class BrushTool
    {
        public static Brush FromInterpolation(Brush min, double p, Brush max)
        {
            if (min is SolidColorBrush)
            {
                if (max is SolidColorBrush)
                {
                    return fromInterpolation(min as SolidColorBrush, p, max as SolidColorBrush);
                }

                if (max is LinearGradientBrush)
                {
                    return fromInterpolation(min as SolidColorBrush, p, max as LinearGradientBrush);
                }

                if (max is RadialGradientBrush)
                {
                    return fromInterpolation(min as SolidColorBrush, p, max as RadialGradientBrush);
                }
            }

            if (min is LinearGradientBrush)
            {
                if (max is SolidColorBrush)
                {
                    return fromInterpolation(max as SolidColorBrush, 1.0 - p, min as LinearGradientBrush);
                }

                if (max is LinearGradientBrush)
                {
                    return fromInterpolation(min as LinearGradientBrush, p, max as LinearGradientBrush);
                }

                if (max is RadialGradientBrush)
                {
                    return fromInterpolation(min as LinearGradientBrush, p, max as RadialGradientBrush);
                }
            }

            if (min is RadialGradientBrush)
            {
                if (max is SolidColorBrush)
                {
                    return fromInterpolation(max as SolidColorBrush, 1.0 - p, min as RadialGradientBrush);
                }

                if (max is LinearGradientBrush)
                {
                    return fromInterpolation(max as LinearGradientBrush, 1.0-p, min as RadialGradientBrush);
                }

                if (max is RadialGradientBrush)
                {
                    return fromInterpolation(min as RadialGradientBrush, p, max as RadialGradientBrush);
                }
            }

            throw new NotSupportedException();
        }

        private static Brush fromInterpolation(SolidColorBrush min, double p, SolidColorBrush max)
        {
            return new SolidColorBrush(ColorTool.FromARGBInterpolation(min.Color, p, max.Color)) { Opacity = min.Opacity * (1.0 - p) + max.Opacity * p };
        }

        private static Brush fromInterpolation(SolidColorBrush min, double p, RadialGradientBrush max)
        {
            double q=1.0-p;

            return new RadialGradientBrush()
            {
                Center = max.Center,
                ColorInterpolationMode = max.ColorInterpolationMode,
                GradientOrigin = max.GradientOrigin,
                GradientStops=fromInterpolation(min.Color, p, max.GradientStops),
                MappingMode = max.MappingMode,
                Opacity = min.Opacity * q + max.Opacity * p,
                RadiusX = max.RadiusX,
                RadiusY = max.RadiusY,
                RelativeTransform = max.RelativeTransform,
                SpreadMethod = max.SpreadMethod,
                Transform = max.Transform
            };
        }

        private static Brush fromInterpolation(SolidColorBrush min, double p, LinearGradientBrush max)
        {
            double q=1.0-p;

            return new LinearGradientBrush()
            {
                ColorInterpolationMode = max.ColorInterpolationMode,
                EndPoint = max.EndPoint,
                GradientStops = fromInterpolation(min.Color, p, max.GradientStops),
                MappingMode = max.MappingMode,
                Opacity = min.Opacity * q + max.Opacity * p,
                RelativeTransform = max.RelativeTransform,
                SpreadMethod = max.SpreadMethod,
                StartPoint = max.StartPoint,
                Transform = max.Transform

            };
        }

        private static Brush fromInterpolation(LinearGradientBrush min, double p, RadialGradientBrush max)
        {
            return null;
        }

        private static Brush fromInterpolation(LinearGradientBrush min, double p, LinearGradientBrush max)
        {
            double q = 1.0 - p;

            return new LinearGradientBrush()
            {
                ColorInterpolationMode = max.ColorInterpolationMode,
                EndPoint = new Point(min.EndPoint.X*q+max.EndPoint.X*p, min.EndPoint.Y*q+max.EndPoint.Y*p),
                GradientStops = fromInterpolation(min.GradientStops, p, max.GradientStops),
                MappingMode = max.MappingMode,
                Opacity = min.Opacity * q + max.Opacity * p,
                RelativeTransform = max.RelativeTransform,
                SpreadMethod = max.SpreadMethod,
                StartPoint = new Point(min.StartPoint.X * q + max.StartPoint.X * p, min.StartPoint.Y * q + max.StartPoint.Y * p),
                Transform = max.Transform
            };
        }

        private static Brush fromInterpolation(RadialGradientBrush min, double p, RadialGradientBrush max)
        {
            double q = 1.0 - p;

            return new RadialGradientBrush()
            {
                Center = max.Center,
                ColorInterpolationMode = max.ColorInterpolationMode,
                GradientOrigin = new Point(min.GradientOrigin.X * q + max.GradientOrigin.X * p, min.GradientOrigin.Y * q + max.GradientOrigin.Y * p),
                GradientStops = fromInterpolation(min.GradientStops, p, max.GradientStops),
                MappingMode = max.MappingMode,
                Opacity = min.Opacity * q + max.Opacity * p,
                RadiusX = min.RadiusX*q+max.RadiusX*p,
                RadiusY = min.RadiusY * q + max.RadiusY * p,
                RelativeTransform = max.RelativeTransform,
                SpreadMethod = max.SpreadMethod,
                Transform = max.Transform
            };
        }

        private static GradientStopCollection fromInterpolation(Color min, double p, GradientStopCollection max)
        {
            GradientStopCollection ret = new GradientStopCollection();
            double[] ahsvMin=ColorTool.AHSV(min);

            for (int i = 0; i < max.Count; ++i)
            {
                double[] ahsv=ColorTool.FromAHSVInterpolation(ahsvMin, p, ColorTool.AHSV(max[i].Color));
                Color color=ColorTool.FromAHSV(ahsv[0], ahsv[1], ahsv[2], ahsv[3]);

                color = ColorTool.FromARGBInterpolation(min, p, max[i].Color);
                ret.Add(new GradientStop() { Color = color, Offset = max[i].Offset });
            }

            return ret;
        }

        private static GradientStopCollection fromInterpolation(GradientStopCollection min, double p, GradientStopCollection max)
        {
            if (min.Count < max.Count)
            {
                GradientStopCollection t = min;
                min = max;
                max = t;

                p = 1 - p;
            }

            GradientStopCollection ret = new GradientStopCollection();

            for (int i = 0; i < min.Count; ++i)
            {
                ret.Add(new GradientStop()
                { 
                    Color = ColorTool.FromARGBInterpolation(min[i].Color, p, max[i].Color),
                    Offset = min[i].Offset * (1.0 - p) + max[i].Offset * p
                });
            }

            for (int i = min.Count; i < max.Count; ++i)
            {
                ret.Add(new GradientStop()
                {
                    Color = ColorTool.FromARGBInterpolation(min[min.Count-1].Color, p, max[i].Color),
                    Offset = min[min.Count - 1].Offset * (1.0 - p) + max[i].Offset * p
                });
            }

            return ret;
        }
    }
}
