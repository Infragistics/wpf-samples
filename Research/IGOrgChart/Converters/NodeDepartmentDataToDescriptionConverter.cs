using System;
using System.Globalization;
using System.Windows.Data;
using Infragistics.Samples.Shared.Models;

namespace IGOrgChart.Converters
{
    public class NodeDepartmentDataToDescriptionConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DepartmentGroup)
            {
                return ((DepartmentGroup)value).GroupName;
            }
            if (value is Department)
            {
                return ((Department)value).DepartmentName;
            }
            if (value is EmployeePosition)
            {
                return ((EmployeePosition)value).PositionName;
            }
            if (value is Employee)
            {
                return ((Employee)value).FullName;
            }

            return string.Empty;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Not used.
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
