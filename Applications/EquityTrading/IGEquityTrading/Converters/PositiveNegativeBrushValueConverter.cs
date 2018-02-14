//-------------------------------------------------------------------------
// <copyright file="PositiveNegativeBrushValueConverter.cs" company="Infragistics">
//
// Copyright (c) 2010 Infragistics, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace IGTrading.Converters
{
    public class PositiveNegativeBrushValueConverter : FrameworkElement, IValueConverter
    {
        #region Private Memeber Variables
        private Brush _stockPositiveChangeColor;
        private Brush _stockNegativeChangeColor;
        #endregion Private Memeber Variables

        #region Dependency Properties
        public static DependencyProperty StockPositiveChangeColorProperty = DependencyProperty.Register("StockPositiveChangeColor",
                                                                                            typeof(Brush),
                                                                                            typeof(PositiveNegativeBrushValueConverter),
                                                                                            new PropertyMetadata(new SolidColorBrush(Colors.Green), OnStockPositiveChangeColorChanged));

        public static DependencyProperty StockNegativeChangeColorProperty = DependencyProperty.Register("StockNegtiveChangeColor",
                                                                                            typeof(Brush),
                                                                                            typeof(PositiveNegativeBrushValueConverter),
                                                                                            new PropertyMetadata(new SolidColorBrush(Colors.Red), OnStockNegativeChangeColorChanged));
        #endregion Dependency Properties

        #region Properties
		/// <summary>
		/// Gets or sets the color of the stock positive change.
		/// </summary>
		/// <value>The color of the stock positive change.</value>
        public Brush StockPositiveChangeColor
        {
            get { return _stockPositiveChangeColor; }
            set { if (value == _stockPositiveChangeColor) return; _stockPositiveChangeColor = value; }
        }

		/// <summary>
		/// Gets or sets the color of the stock negative change.
		/// </summary>
		/// <value>The color of the stock negative change.</value>
        public Brush StockNegativeChangeColor
        {
            get { return _stockNegativeChangeColor; }
            set { if (value == _stockNegativeChangeColor) return; _stockNegativeChangeColor = value; }
        }
        #endregion Properties

        #region Dependency Property Change EventHandlers
		/// <summary>
		/// Called when [stock positive change color changed].
		/// </summary>
		/// <param name="d">The d.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnStockPositiveChangeColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(StockPositiveChangeColorProperty, e.NewValue);
        }

		/// <summary>
		/// Called when [stock negative change color changed].
		/// </summary>
		/// <param name="d">The d.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnStockNegativeChangeColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(StockNegativeChangeColorProperty, e.NewValue);
        }
        #endregion Dependency Property Change EventHandlers

        #region IValueConverter
		/// <summary>
		/// Modifies the source data before passing it to the target for display in the UI.
		/// </summary>
		/// <param name="value">The source data being passed to the target.</param>
		/// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
		/// <param name="parameter">An optional parameter to be used in the converter logic.</param>
		/// <param name="culture">The culture of the conversion.</param>
		/// <returns>
		/// The value to be passed to the target dependency property.
		/// </returns>
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
		/// <param name="value">The target data being passed to the source.</param>
		/// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
		/// <param name="parameter">An optional parameter to be used in the converter logic.</param>
		/// <param name="culture">The culture of the conversion.</param>
		/// <returns>
		/// The value to be passed to the source object.
		/// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion IValueConverter
    }
}
