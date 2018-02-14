using System;
using System.Windows.Data;
using IGShowcase.FinanceDashboard.ViewModels;

namespace IGShowcase.FinanceDashboard.Converters
{
    public class StockChartTypeToBoolConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value == null || parameter == null || !(value is StockChartType) || !(parameter is string))
            {
                return false;
            }

            var chartType = (StockChartType)value;
            var chartName = parameter.ToString();

            if (chartType == StockChartType.Area && chartName == "Area")
                return true;
            if (chartType == StockChartType.StepArea && chartName == "StepArea")
                return true;
            if (chartType == StockChartType.RangeArea && chartName == "RangeArea")
                return true;

            if (chartType == StockChartType.Line && chartName == "Line")
                return true;

            if (chartType == StockChartType.CandleStick && chartName == "CS")
                return true;

            if (chartType == StockChartType.OpenHighLowClose && chartName == "OHLC")
                return true;

            return false;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null || !(value is bool) || !(parameter is string))
            {
                return StockChartType.None;
            }

            var isSelected = (bool)value;
            var chartName = parameter.ToString();

            if (isSelected && chartName == "Area")
                return StockChartType.Area;
            if (isSelected && chartName == "StepArea")
                return StockChartType.StepArea;
            if (isSelected && chartName == "RangeArea")
                return StockChartType.RangeArea;

            if (isSelected && chartName == "Line")
                return StockChartType.Line;

            if (isSelected && chartName == "CS")
                return StockChartType.CandleStick;

            if (isSelected && chartName == "OHLC")
                return StockChartType.OpenHighLowClose;

            return StockChartType.None;

            //throw new NotImplementedException();
        }

        #endregion
    }
}