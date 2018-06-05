using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Infragistics.Controls.Charts;

namespace IGExtensions.Common.Maps.StyleSelectors 
{
    public class TemperatureStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            var record = ((Infragistics.Controls.Maps.ShapefileRecord)(item));
            var field = Convert.ToInt32(record.Fields["Contour"]);

            Color color;

            if (field < 6)
            {
                color = Colors.Blue;
            }
            else if (field < 12)
            {
                color = Colors.Green;
            }
            else if (field < 20)
            {
                color = Colors.Yellow;
            }
            else if (field < 26)
            {
                color = Colors.Orange;
            }
            else
            {
                color = Colors.Red;
            }

            var result = new Style(typeof(Path));
            result.Setters.Add(new Setter(Path.FillProperty, new SolidColorBrush(color) { Opacity = 0.05 }));
            result.Setters.Add(new Setter(Path.StrokeProperty, new SolidColorBrush(color) { Opacity = 0.75 }));
            //result.Setters.Add(new Setter(Path.StrokeThicknessProperty, 1.5));
            return result;
        }
    }

    public class TemperatureStyleSelector2 : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            var record = ((Infragistics.Controls.Maps.ShapefileRecord)(item));
            var field = Convert.ToInt32(record.Fields["Contour"]);

            Color color;

            if (field < 6)
            {
                color = Colors.Blue;
            }
            else if (field < 12)
            {
                color = Colors.Green;
            }
            else if (field < 20)
            {
                color = Colors.Yellow;
            }
            else if (field < 26)
            {
                color = Colors.Orange;
            }
            else
            {
                color = Colors.Red;
            }

            ToolTipService.SetToolTip(container, "Temprature " + field);

            var result = new Style(typeof(ShapeControl));
            result.Setters.Add(new Setter(ShapeControl.BackgroundProperty, new SolidColorBrush(color) { Opacity = 0.05 }));
            result.Setters.Add(new Setter(ShapeControl.BorderBrushProperty, new SolidColorBrush(color) { Opacity = 0.75 }));
            //result.Setters.Add(new Setter(ShapeControl.StrokeThicknessProperty, 1.5));
            return result;
        }
    }

}