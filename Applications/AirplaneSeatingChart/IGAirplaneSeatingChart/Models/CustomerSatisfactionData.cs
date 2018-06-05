using System;
using System.ComponentModel;

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
