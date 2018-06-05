using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Infragistics.Controls.Charts;

namespace IGExtensions.Common.Maps.StyleSelectors 
{
    public class ShapeControlStyleSelector : StyleSelector, INotifyPropertyChanged
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

            Style result = new Style(typeof(ShapeControl));
            result.Setters.Add(new Setter(ShapeControl.BackgroundProperty, new SolidColorBrush(color) { Opacity = 0.05 }));
            result.Setters.Add(new Setter(ShapeControl.BorderBrushProperty, new SolidColorBrush(color) { Opacity = 0.75 }));
            //result.Setters.Add(new Setter(ShapeControl.StrokeThicknessProperty, 1.5));
            return result;
        }

        #region RangeMinValue
        private const string ColorScalePropertyName = "ColorScale";
        private ColorScale _colorScale;
        public ColorScale ColorScale
        {
            get
            {
                return _colorScale;
            }
            set
            {
                bool isChanged = _colorScale != value;
                if (isChanged)
                {
                    ColorScale oldValue = _colorScale;
                    _colorScale = value;
                    this.RaisePropertyChanged(ColorScalePropertyName, oldValue, _colorScale);
                }
            }
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises PropertyChanged event if any listeners have been registered.
        /// </summary>
        /// <param name="propertyName">Name of property whos value changed.</param>
        /// <param name="oldValue">Property value before change.</param>
        /// <param name="newValue">Property value after change.</param>
        protected void RaisePropertyChanged(string propertyName, object oldValue, object newValue)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
 
        }
    }
    
    
}