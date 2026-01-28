using System.Collections.Generic;
using System.Windows.Media;
using Infragistics;

namespace System.Collections.Generic
{
    public static class BrushCollectionEx
    { 
        public static BrushCollection ToBrushes(this IEnumerable<Color> colors)
        {
            var bc = new BrushCollection();
            foreach (var color in colors)
            {
                bc.Add(new SolidColorBrush(color));
            }
            return bc;
        }
        public static BrushCollection ToBrushes(params Color[] colors)
        {
            var bc = new BrushCollection();
            foreach (var color in colors)
            {
                bc.Add(new SolidColorBrush(color));
            }
            return bc;
        } 
    }
}