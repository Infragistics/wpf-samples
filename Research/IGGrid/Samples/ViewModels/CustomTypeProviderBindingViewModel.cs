using System;
using System.Collections.Generic;
using System.ComponentModel;
using IGGrid.Resources;

namespace IGGrid.Samples.ViewModels
{
    public class CustomTypeProviderBindingViewModel  : INotifyPropertyChanged
    {
        private string propertyName;
        private PropertyTypeInfo propertyTypeInfo;
        private List<PropertyTypeInfo> propertyTypes;

        public CustomTypeProviderBindingViewModel()
        {
            PropertyTypes = new List<PropertyTypeInfo>();

            PropertyTypes.Add(new PropertyTypeInfo(typeof(int), 0));
            PropertyTypes.Add(new PropertyTypeInfo(typeof(string), GridStrings.ictp_SampleStringData)); // "sample string data"
            PropertyTypes.Add(new PropertyTypeInfo(typeof(DateTime), DateTime.Now));
            PropertyTypes.Add(new PropertyTypeInfo(typeof(Uri), new Uri("http://www.infragistics.com", UriKind.Absolute)));

            propertyName = null;
            SelectedPropertyTypeInfo = PropertyTypes[0];
        }

        #region Public properties

        public string PropertyName
        {
            get { return propertyName; }
            set
            {
                propertyName = value;
                OnPropertyChanged("PropertyName");
            }
        }

        public PropertyTypeInfo SelectedPropertyTypeInfo
        {
            get { return propertyTypeInfo; }
            set
            {
                propertyTypeInfo = value;
                OnPropertyChanged("SelectedPropertyTypeInfo");
            }
        }

        public List<PropertyTypeInfo> PropertyTypes
        {
            get { return propertyTypes; }
            set
            {
                if (value != propertyTypes)
                {
                    propertyTypes = value;
                    OnPropertyChanged("PropertyTypes");
                }
            }
        }

        #endregion // public properties

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion // INotifyPropertyChanged
    }

    public class PropertyTypeInfo
    {
        public PropertyTypeInfo(Type type, object defaultValue)
        {
            PropertyType = type;
            DefaultValue = defaultValue;
        }

        public Type PropertyType { get; set; }
        public object DefaultValue { get; set; }
    }
}