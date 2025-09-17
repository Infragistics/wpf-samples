using Xamarin.Forms;

namespace Infragistics.Core.Graphics
{
    //TODO append .XF suffix to file name
    public static class BrushEx
    {
        public static bool IsDefault(this Brush brush, BindableProperty prop)
        {
            var brushDefault = prop.DefaultValue as Brush;

            return brush.IsEqual(brushDefault);
        }

        public static bool IsEqual(this Brush brush, Brush brushDefault)
        {
            if (brush == null && brushDefault == null) return true;
             
            var solidDefault = brushDefault as SolidColorBrush;
            var solidBrush = brush as SolidColorBrush;
            if (solidBrush != null && solidDefault != null)
            {
                return solidBrush.IsEqual(solidDefault); 
            }

            var linearDefault = brushDefault as LinearGradientBrush;
            var linearBrush = brush as LinearGradientBrush;
            if (linearBrush != null && linearDefault != null)
            {
                return linearBrush.IsEqual(linearDefault);
            }

            return false;
        }

        /// <summary>
        /// Checks if this brush is equal to other <see cref="LinearGradientBrush"/>
        /// </summary>  
        public static bool IsEqual(this SolidColorBrush brush, SolidColorBrush other)
        {
            if (other == null) return false;
            return brush.Color == other.Color;
        }
        
        /// <summary>
        /// Checks if this brush is equal to other <see cref="LinearGradientBrush"/>
        /// </summary>  
        public static bool IsEqual(this LinearGradientBrush brush, LinearGradientBrush other)
        {
            if (other == null) return false;

            //var isAbsolute = brush.IsAbsolute == other.IsAbsolute;
            var sameStops = brush.GradientStops == other.GradientStops;
            var sameEndX = brush.EndX == other.EndX;
            var sameEndY = brush.EndY == other.EndY;
            var sameStartX = brush.StartX == other.StartX;
            var sameStartY = brush.StartY == other.StartY;

            return sameStops &&
                   sameEndX && sameStartX && 
                   sameEndY && sameStartY;
        }
    }
}