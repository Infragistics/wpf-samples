using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Infragistics.Controls.Charts;

namespace IGExtensions.Common.Maps.StyleSelectors 
{
    public class DrillDownStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            var result = new Style(typeof(Path));

            var mapElement = ((MapShapeElement)(item));
            if (mapElement.Visibility == Visibility.Visible)
            {
                //result.Setters.Add(new Setter(Path.StrokeProperty, new SolidColorBrush(Colors.Yellow)));
            }
            else
            {
                result.Setters.Add(new Setter(Path.StrokeProperty, new SolidColorBrush(Colors.Transparent)));
                result.Setters.Add(new Setter(Path.FillProperty, new SolidColorBrush(Colors.Transparent)));
            }
            //result.Setters.Add(new Setter(Path.VisibilityProperty, record.ShapeVisibility));
            //result.Setters.Add(new Setter(Path.FillProperty, new SolidColorBrush(Colors.Gray)));
            //result.Setters.Add(new Setter(Path.StrokeProperty, new SolidColorBrush(Colors.Yellow)));
            return result;
        }
    }
    public class DrillDownShapePathStyleSelector : StyleSelector
    {
        public DrillDownShapePathStyleSelector()
        {
            this.DrillDownStyle = new Style(typeof(Path));
        }
        public Style DrillDownStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            var result = new Style(typeof(Path));
            var mapElement = ((MapShapeElement)(item));
            if (mapElement.Visibility == Visibility.Visible)
            {
                result = this.DrillDownStyle;
            }
            else
            {
                result.Setters.Add(new Setter(Path.IsHitTestVisibleProperty, false));
                result.Setters.Add(new Setter(Path.StrokeProperty, new SolidColorBrush(Colors.Transparent)));
                result.Setters.Add(new Setter(Path.FillProperty, new SolidColorBrush(Colors.Transparent)));
            }
            return result;
        }
    }
    public class DrillDownShapeControlStyleSelector : StyleSelector
    {
        public DrillDownShapeControlStyleSelector()
        {
            this.DrillDownStyle = new Style(typeof(ShapeControl));
        }
        public Style DrillDownStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            var result = new Style(typeof(ShapeControl));
            var mapElement = ((MapShapeElement)(item));
            if (mapElement.Visibility == Visibility.Visible)
            {
                result = this.DrillDownStyle;
                // result.Setters.Add(new Setter(ShapeControl.BorderBrushProperty, new SolidColorBrush(Colors.Red)));
            }
            else
            {
                result.Setters.Add(new Setter(ShapeControl.BorderBrushProperty, new SolidColorBrush(Colors.Transparent)));
                // result.Setters.Add(new Setter(ShapeControl.VisibilityProperty, Visibility.Collapsed));
                //result.Setters.Add(new Setter(ShapeControl.BorderBrushProperty, new SolidColorBrush(Colors.Transparent)));
                result.Setters.Add(new Setter(ShapeControl.BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
                // result.Setters.Add(new Setter(ShapeControl.VisibilityProperty, Visibility.Collapsed));
            }
            return result;
        }
    }
   
}