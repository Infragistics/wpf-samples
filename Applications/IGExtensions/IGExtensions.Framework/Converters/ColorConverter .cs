using System;
using System.Windows.Data;
using System.Windows.Media;

namespace IGExtensions.Framework.Converters
{
   public class ColorConverter : IValueConverter
   {
     
        #region IValueConverter Members
    
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           string val = value.ToString();
           val = val.Replace("#", "");
    
           byte a = System.Convert.ToByte("ff", 16);
           byte pos = 0;
           if (val.Length == 8)
           {
               a = System.Convert.ToByte(val.Substring(pos, 2), 16);
               pos = 2;
           }
    
           byte r = System.Convert.ToByte(val.Substring(pos, 2), 16);
           pos += 2;
    
           byte g = System.Convert.ToByte(val.Substring(pos, 2), 16);
           pos += 2;
   
           byte b = System.Convert.ToByte(val.Substring(pos, 2), 16);
    

           Color col = Color.FromArgb(a, r, g, b);
    
           return new SolidColorBrush(col);
       }
       public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
       {
           var val = value as SolidColorBrush;
           if (val != null)
               return "#" + val.Color.A.ToString() + val.Color.R.ToString() + val.Color.G.ToString() + val.Color.B.ToString();

           return "#00000000";
       }

       #endregion
   }
}