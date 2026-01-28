using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using IGBarcodeReader.Resources;

namespace IGBarcodeReader.Controls
{
    public class ReaderSettingsToDescriptionConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int propertyValue = System.Convert.ToInt32(value);
            string stringDescription = propertyValue.ToString();

            if (propertyValue == -1)
            {
                string propertyName = parameter.ToString();

                switch (propertyName)
                {
                    case "MaxNumberOfSymbolsToRead":
                        stringDescription += " " + BarcodeReaderStrings.BarcodeReader_AllSymbologies;
                        break;
                    case "MinSymbolSize":
                        stringDescription += " " + BarcodeReaderStrings.BarcodeReader_NotSpecified;
                        break;
                    default:
                        stringDescription = string.Empty;
                        break;
                }
            }

            return stringDescription;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
