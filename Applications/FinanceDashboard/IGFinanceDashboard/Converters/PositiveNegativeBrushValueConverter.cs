using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace IGShowcase.FinanceDashboard.Converters
{
    public class ValueSignConverter : IValueConverter
    {
        #region IValueConverter
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return "NUL";
           
            double data;
            if (double.TryParse(value.ToString(), out data))
            {
                string result;
                if (parameter is string)
                {
                    var format = parameter as string;
                    result = string.Format(format, data);
                }
                else
                {
                    result = data.ToString();
                }
                result = data >= 0 ? "+" + result : result;
                return result;
            }
            return value;
        }
        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion IValueConverter
   
    }
    public class PositiveNegativeBrushValueConverter : IValueConverter
    {
        public PositiveNegativeBrushValueConverter()
        {
            this.StockPositiveChangeColor = new SolidColorBrush(Colors.Green);
            this.StockNegativeChangeColor = new SolidColorBrush(Colors.Red);
        }
        public Brush StockPositiveChangeColor { get; set; }
        public Brush StockNegativeChangeColor { get; set; }

        #region IValueConverter
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return StockPositiveChangeColor;

            double data;

            if (double.TryParse(value.ToString(), out data))
            {
                return data >= 0 ? StockPositiveChangeColor : StockNegativeChangeColor;
            }
            return StockPositiveChangeColor;
        }
        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion IValueConverter
    }
    public class BrushValueConverter : DependencyObject, IValueConverter
    {
        public BrushValueConverter()
        {
            this.PositiveValueBrush = new SolidColorBrush(Colors.Green);
            this.NegativeValueBrush = new SolidColorBrush(Colors.Red);
        }
        //public Brush PositiveValueBrush { get; set; }
        //public Brush NegativeValueBrush { get; set; }
        public Brush PositiveValueBrush
        {
            get { return (Brush)GetValue(PositiveValueBrushProperty); }
            set { SetValue(PositiveValueBrushProperty, value); }
        }

        public static readonly DependencyProperty PositiveValueBrushProperty =
            DependencyProperty.Register("PositiveValueBrush",
                typeof(Brush), typeof(BrushValueConverter), new PropertyMetadata(OnValueBrushChanged));


        public Brush NegativeValueBrush
        {
            get { return (Brush)GetValue(NegativeValueBrushProperty); }
            set { SetValue(NegativeValueBrushProperty, value); }
        }

        public static readonly DependencyProperty NegativeValueBrushProperty =
            DependencyProperty.Register("NegativeValueBrush",
                typeof(Brush), typeof(BrushValueConverter), new PropertyMetadata(OnValueBrushChanged));

        private static void OnValueBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #region IValueConverter
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return PositiveValueBrush;

            double data;

            if (double.TryParse(value.ToString(), out data))
            {
                return data >= 0 ? PositiveValueBrush : NegativeValueBrush;
            }
            return PositiveValueBrush;
        }
        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion IValueConverter
    }
  }
