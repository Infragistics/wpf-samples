using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using IGTrading.ViewModels;

namespace IGTrading.Converters
{
    class TransactionTypeToChartDataConverter : IMultiValueConverter
    {

        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            TransactionType transactionType = values[0] == DependencyProperty.UnsetValue
                                                      ? TransactionType.Sell
                                                      : (TransactionType)values[0];

            StockTickerDetailsViewModel viewModel = values[1] as StockTickerDetailsViewModel;

            if (viewModel != null)
            {
                if (transactionType == TransactionType.Buy)
                {
                    return viewModel.BuyTickerData;
                }
                else
                {
                    return viewModel.SellTickerData;
                }
            }

            return Enumerable.Empty<StockTickerDataViewModel>();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
