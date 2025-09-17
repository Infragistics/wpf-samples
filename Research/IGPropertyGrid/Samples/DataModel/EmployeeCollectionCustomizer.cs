using IGPropertyGrid.Resources;
using System;
using System.ComponentModel;
using System.Globalization;

namespace IGPropertyGrid.Samples.DataModel
{
    public class EmployeeCollectionCustomizer : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
        {
            return PropertyGridStrings.lblEmployeeCollectionDescription;
        }
    }
}
