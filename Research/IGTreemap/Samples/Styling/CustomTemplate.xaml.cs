using System;
using System.Windows.Data;
using System.Windows.Navigation;

namespace IGTreemap.Samples
{
    public partial class CustomTemplate : Infragistics.Samples.Framework.SampleContainer
    {
        public CustomTemplate()
        {
            InitializeComponent();
        }
    }

    public class NameConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string text = value.ToString();
            return text.Length > 15 ? text.Substring(0, 15) + "..." : text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }

        #endregion
    }
}
