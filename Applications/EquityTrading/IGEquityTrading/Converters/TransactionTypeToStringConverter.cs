using System;
using System.Globalization;
using System.Windows.Data;
using IGTrading.ViewModels;

namespace IGTrading.Converters
{
    class TransactionTypeToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            TransactionType transactionType = (TransactionType) value;

            return transactionType == TransactionType.Buy
                                  ? IGTrading.Assets.LocalizedResources.Strings.Buy
                                  : IGTrading.Assets.LocalizedResources.Strings.Sell;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
