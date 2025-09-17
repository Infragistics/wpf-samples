using Infragistics.Controls.Charts;
using System; 
using System.Globalization; 
using System.Windows.Data;
using System.Windows.Media; 

namespace Infragistics.Framework 
{
    public class ColorToCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colecion = new ColorCollection();
            if (value is Color)
            {
                var color = (Color)value;
                 
                colecion.Add(color.GetLightened(-1.0));
                colecion.Add(color);
                colecion.Add(color.GetLightened(1.0));

                //colecion.Add(Color.FromArgb(150, color.R, color.G, color.B));
                //colecion.Add(Color.FromArgb(100, color.R, color.G, color.B));
            }

            return colecion;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
