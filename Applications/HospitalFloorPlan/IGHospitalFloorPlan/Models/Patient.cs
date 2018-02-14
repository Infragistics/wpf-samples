using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace IGShowcase.HospitalFloorPlan.Models
{
    /// <summary>
    /// 
    /// </summary>
	public class Patient
	{
        /// <summary>
        /// Gets or sets patient's name.
        /// </summary>
        /// <value>The name.</value>
		public string Name { get; set; }

        /// <summary>
        /// Gets or sets patient's gender.
        /// </summary>
        /// <value>The gender.</value>
		public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets patient's age.
        /// </summary>
        /// <value>The age.</value>
		public int Age { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>The birth date.</value>
        [TypeConverter(typeof(CustomDateTimeConverter))]
        public DateTime BirthDate {get; set;}

        /// <summary>
        /// Gets or sets the type of the blood.
        /// </summary>
        /// <value>The type of the blood.</value>
        public string BloodType { get; set; }

        /// <summary>
        /// Gets or sets patient's phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the name of the ec.
        /// </summary>
        /// <value>The name of the ec.</value>
        public string EcName { get; set; }

        /// <summary>
        /// Gets or sets the ec address.
        /// </summary>
        /// <value>The ec address.</value>
        public string EcAddress { get; set; }

        /// <summary>
        /// Gets or sets the ec phone.
        /// </summary>
        /// <value>The ec phone.</value>
        public string EcPhone { get; set; }

        /// <summary>
        /// Gets or sets the blood pressure.
        /// </summary>
        /// <value>The blood pressure.</value>
        public string BloodPreasure { get; set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>The temperature.</value>
        public double Temperature { get; set; }

        /// <summary>
        /// Gets or sets the index of the pain.
        /// </summary>
        /// <value>The index of the pain.</value>
        public int PainIndex { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public string Height { get; set; }

        /// <summary>
        /// Gets or sets the medication.
        /// </summary>
        /// <value>The medication.</value>
        public string Medication { get; set; }    

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
		public string ImageUrl { get; set; }

		//NOTE: Needed for Xaml parsingof datetime strings
        /// <summary>
        /// Gets or sets the admission date.
        /// </summary>
        /// <value>The admission date.</value>
        [TypeConverter(typeof(CustomDateTimeConverter))]
		public DateTime AdmissionDate { get; set; }

        /// <summary>
        /// Gets or sets patient's heart rate.
        /// </summary>
        /// <value>The heart rate.</value>
		public double HeartRate { get; set; }

        /// <summary>
        /// Gets or sets the ecg data.
        /// </summary>
        /// <value>The ecg data.</value>
		public EcgData[] EcgData { get; set; }
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
