using System;
using System.Reflection;
using System.Windows.Markup;

namespace Infragistics.Samples.Shared.Tools
{
    public class ObjectValueExtension : MarkupExtension
    {
        public object DefaultValue { get; set; }
        public object DataObject { get; set; }
        public string PropertyName { get; set; }
        public int Index { get; set; }

        public ObjectValueExtension()
        {
            this.Index = -1;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                if (this.DataObject != null && !string.IsNullOrEmpty(this.PropertyName))
                {
                    PropertyInfo propertyInfo = this.DataObject.GetType().GetProperty(this.PropertyName);
                    if (propertyInfo != null)
                    {
                        object[] index = null;
                        if (this.Index > -1)
                        {
                            index = new object[] { this.Index };
                        }

                        object value = propertyInfo.GetValue(this.DataObject, index);

                        return value;
                    }
                }
            }
            catch (Exception)
            {
            }

            return this.DefaultValue;
        }
    }
}
