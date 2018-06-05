using System;
using System.Windows.Data;
using IGShowcase.HospitalFloorPlan.Models;

namespace IGShowcase.HospitalFloorPlan.Converters
{
	public class GenderToImageConverter : IValueConverter
	{
		public const string MaleImg = "/IGShowcase.HospitalFloorPlan;component/Assets/Images/IconMale.png";
        public const string FemaleImg = "/IGShowcase.HospitalFloorPlan;component/Assets/Images/IconFemale.png";
        public const string UnknownImg = "/IGShowcase.HospitalFloorPlan;component/Assets/Images/IconUnknown.png";

		#region IValueConverter Members

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
			if (value == null) return null;
			var gender = (Gender)value;

			switch (gender)
			{
				case Gender.Male:
					return MaleImg;
				case Gender.Female:
					return FemaleImg;
				case Gender.Unknown:
					return UnknownImg;
				default:
					throw new ArgumentOutOfRangeException();
			}
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

		#endregion
	}
}