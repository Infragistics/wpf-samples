using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace IGExtensions.Common.Controls
{
    public class PropertyValueEditor : PropertyBaseEditor //Control
    {
        public PropertyValueEditor()
        {
            // add the generic style to the control's resources
            //const string stylePath = "/IGExtensions.Common;component/Controls/Editors/PropertyValueEditor.xaml";
            //this.ApplyStyle(stylePath);
            DefaultStyleKey = typeof(PropertyValueEditor);
        }
        
        #region Properties
        public const string PropertyValueStringPropertyName = "PropertyValueString";
        public static readonly DependencyProperty PropertyValueStringProperty = DependencyProperty.Register(
            PropertyValueStringPropertyName, typeof(string), typeof(PropertyValueEditor), new PropertyMetadata("0", (sender, e) =>
        {
            (sender as PropertyValueEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValueStringPropertyName));
        })); 
        /// <summary>
        /// Gets or sets the PropertyValueString property 
        /// </summary>
        public string PropertyValueString
        {
            get { return (string)GetValue(PropertyValueStringProperty); }
            protected set { SetValue(PropertyValueStringProperty, value); }
        }


        //public const string PropertyNamePropertyName = "PropertyName";
        //public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
        //    PropertyNamePropertyName, typeof(string), typeof(PropertyValueEditor), new PropertyMetadata("Property Name:", (sender, e) =>
        //    {
        //        (sender as PropertyValueEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyNamePropertyName));
        //    }));
        ///// <summary>
        ///// Gets or sets the PropertyName property 
        ///// </summary>
        //public string PropertyName
        //{
        //    get { return (string)GetValue(PropertyNameProperty); }
        //    set { SetValue(PropertyNameProperty, value); }
        //}

        public const string PropertyValueFormatPropertyName = "PropertyValueFormat";
        public static readonly DependencyProperty PropertyValueFormatProperty = DependencyProperty.Register(
            PropertyValueFormatPropertyName, typeof(string), typeof(PropertyValueEditor), new PropertyMetadata("0", (sender, e) =>
            {
                (sender as PropertyValueEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValueFormatPropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyValueFormat property 
        /// </summary>
        public string PropertyValueFormat
        {
            get { return (string)GetValue(PropertyValueFormatProperty); }
            set { SetValue(PropertyValueFormatProperty, value); }
        }

        public const string PropertyValuePropertyName = "PropertyValue";
        public static readonly DependencyProperty PropertyValueProperty = DependencyProperty.Register(
            PropertyValuePropertyName, typeof(double), typeof(PropertyValueEditor), new PropertyMetadata(10.0, (sender, e) =>
            {
                (sender as PropertyValueEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValuePropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyValue property 
        /// </summary>
        public double PropertyValue
        {
            get { return (double)GetValue(PropertyValueProperty); }
            set { SetValue(PropertyValueProperty, value); }
        }

        public const string PropertyValueMinPropertyName = "PropertyValueMin";
        public static readonly DependencyProperty PropertyValueMinProperty = DependencyProperty.Register(
            PropertyValueMinPropertyName, typeof(double), typeof(PropertyValueEditor), new PropertyMetadata(0.0, (sender, e) =>
            {
                (sender as PropertyValueEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValueMinPropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyValueMin property 
        /// </summary>
        public double PropertyValueMin
        {
            get { return (double)GetValue(PropertyValueMinProperty); }
            set { SetValue(PropertyValueMinProperty, value); }
        }
        public const string PropertyValueMaxPropertyName = "PropertyValueMax";
        public static readonly DependencyProperty PropertyValueMaxProperty = DependencyProperty.Register(
            PropertyValueMaxPropertyName, typeof(double), typeof(PropertyValueEditor), new PropertyMetadata(10.0, (sender, e) =>
            {
                (sender as PropertyValueEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValueMaxPropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyValueMax property 
        /// </summary>
        public double PropertyValueMax
        {
            get { return (double)GetValue(PropertyValueMaxProperty); }
            set { SetValue(PropertyValueMaxProperty, value); }
        }

        protected new void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case PropertyValuePropertyName:
                case PropertyValueFormatPropertyName:
                    {
                        this.PropertyValueString = string.Format(string.Format("{{0:{0}}}", this.PropertyValueFormat), this.PropertyValue);
                        break;
                    }
                    //{
                    //    this.PropertyValueString = string.Format(string.Format("{{0:{0}}}", this.PropertyValueFormat), this.PropertyValue);
                    //    break;
                    //}
              
            }
            base.OnPropertyChanged(ea);
        }
        //private void OnPropertyChanged(PropertyChangedEventArgs ea)
        //{
        //}
        #endregion
    }
}