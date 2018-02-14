using Infragistics.Documents.RichText.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IgWord.Converters
{
    class RichTextAdapterRefreshTriggerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var triger = (RichTextAdapterRefreshTrigger)value;

            switch (triger)
            {
                case RichTextAdapterRefreshTrigger.ContentChanged: return RichTextRefreshTrigger.ContentChanged;
                case RichTextAdapterRefreshTrigger.Delayed: return RichTextRefreshTrigger.Delayed;
                default: return RichTextRefreshTrigger.Explicit;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
