//-------------------------------------------------------------------------
// <copyright file="CustomerSatisfactionData.cs" company="Infragistics">
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
using System.ComponentModel;
using System.Windows.Controls;

namespace IGShowcase.AirplaneSeatingChart.Models
{
	/// <summary>
	/// This class represents a single data point in the customer satisfaction chart for the seat class.
	/// </summary>
	public class CustomerSatisfactionData
	{
		#region Public Properties
		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value>
        [TypeConverter(typeof(CustomDateTimeConverter))]
		public DateTime Date { get; set; }

		/// <summary>
		/// The customer satisfaction in percents.
		/// </summary>
		public double Value { get; set; }
		#endregion Public Properties
	}

    /// <summary>
    /// This class is a custom TypeConverter used to easily parse the Date string from the PlaneData.xaml file
    /// using the XamlReader.Load() method regardless of the CurrentCulture.
    /// </summary>
    public class CustomDateTimeConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string date = value as string;
            return DateTime.Parse(date, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
