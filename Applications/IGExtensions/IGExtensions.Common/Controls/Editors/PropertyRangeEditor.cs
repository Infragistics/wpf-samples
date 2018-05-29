using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace IGExtensions.Common.Controls
{
    public class PropertyRangeEditor : PropertyBaseEditor  
    {
        public PropertyRangeEditor()
        {
            DefaultStyleKey = typeof(PropertyRangeEditor);
        }
        
        #region Properties
        public const string PropertyValueRangeMinStringPropertyName = "PropertyValueRangeMinString";
        public static readonly DependencyProperty PropertyValueRangeMinStringProperty = DependencyProperty.Register(
            PropertyValueRangeMinStringPropertyName, typeof(string), typeof(PropertyRangeEditor), new PropertyMetadata("0", (sender, e) =>
        {
            (sender as PropertyRangeEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValueRangeMinStringPropertyName));
        })); 
        /// <summary>
        /// Gets or sets the PropertyValueRangeMinString property 
        /// </summary>
        public string PropertyValueRangeMinString
        {
            get { return (string)GetValue(PropertyValueRangeMinStringProperty); }
            protected set { SetValue(PropertyValueRangeMinStringProperty, value); }
        }
        public const string PropertyValueRangeMaxStringPropertyName = "PropertyValueRangeMaxString";
        public static readonly DependencyProperty PropertyValueRangeMaxStringProperty = DependencyProperty.Register(
            PropertyValueRangeMaxStringPropertyName, typeof(string), typeof(PropertyRangeEditor), new PropertyMetadata("0", (sender, e) =>
            {
                (sender as PropertyRangeEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValueRangeMaxStringPropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyValueRangeMaxString property 
        /// </summary>
        public string PropertyValueRangeMaxString
        {
            get { return (string)GetValue(PropertyValueRangeMaxStringProperty); }
            protected set { SetValue(PropertyValueRangeMaxStringProperty, value); }
        }


        //public const string PropertyNamePropertyName = "PropertyName";
        //public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
        //    PropertyNamePropertyName, typeof(string), typeof(PropertyRangeEditor), new PropertyMetadata("Property Name:", (sender, e) =>
        //    {
        //        (sender as PropertyRangeEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyNamePropertyName));
        //    }));
        /// <summary>
        /// Gets or sets the PropertyName property 
        /// </summary>
        //public string PropertyName
        //{
        //    get { return (string)GetValue(PropertyNameProperty); }
        //    set { SetValue(PropertyNameProperty, value); }
        //}

        public const string PropertyValueFormatPropertyName = "PropertyValueFormat";
        public static readonly DependencyProperty PropertyValueFormatProperty = DependencyProperty.Register(
            PropertyValueFormatPropertyName, typeof(string), typeof(PropertyRangeEditor), new PropertyMetadata("0", (sender, e) =>
            {
                (sender as PropertyRangeEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValueFormatPropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyValueFormat property 
        /// </summary>
        public string PropertyValueFormat
        {
            get { return (string)GetValue(PropertyValueFormatProperty); }
            set { SetValue(PropertyValueFormatProperty, value); }
        }

        //public const string PropertyValuePropertyName = "PropertyValue";
        //public static readonly DependencyProperty PropertyValueProperty = DependencyProperty.Register(
        //    PropertyValuePropertyName, typeof(double), typeof(PropertyRangeEditor), new PropertyMetadata(10.0, (sender, e) =>
        //    {
        //        (sender as PropertyRangeEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValuePropertyName));
        //    }));
        ///// <summary>
        ///// Gets or sets the PropertyValue property 
        ///// </summary>
        //public double PropertyValue
        //{
        //    get { return (double)GetValue(PropertyValueProperty); }
        //    set { SetValue(PropertyValueProperty, value); }
        //}

        public const string PropertyValueAbsoluteMinPropertyName = "PropertyValueAbsoluteMin";
        public static readonly DependencyProperty PropertyValueAbsoluteMinProperty = DependencyProperty.Register(
            PropertyValueAbsoluteMinPropertyName, typeof(double), typeof(PropertyRangeEditor), 
            new PropertyMetadata(0.0, (sender, e) =>
            {
                (sender as PropertyRangeEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValueAbsoluteMinPropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyValueAbsoluteMin property Absolute
        /// </summary>
        public double PropertyValueAbsoluteMin
        {
            get { return (double)GetValue(PropertyValueAbsoluteMinProperty); }
            set { SetValue(PropertyValueAbsoluteMinProperty, value); }
        }
        public const string PropertyValueAbsoluteMaxPropertyName = "PropertyValueAbsoluteMax";
        public static readonly DependencyProperty PropertyValueAbsoluteMaxProperty = DependencyProperty.Register(
            PropertyValueAbsoluteMaxPropertyName, typeof(double), typeof(PropertyRangeEditor), 
            new PropertyMetadata(10.0, (sender, e) =>
            {
                (sender as PropertyRangeEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValueAbsoluteMaxPropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyValueAbsoluteMax property 
        /// </summary>
        public double PropertyValueAbsoluteMax
        {
            get { return (double)GetValue(PropertyValueAbsoluteMaxProperty); }
            set { SetValue(PropertyValueAbsoluteMaxProperty, value); }
        }


        public const string PropertyValueRangeMinPropertyName = "PropertyValueRangeMin";
        public static readonly DependencyProperty PropertyValueRangeMinProperty = DependencyProperty.Register(
            PropertyValueRangeMinPropertyName, typeof(double), typeof(PropertyRangeEditor), 
            new PropertyMetadata(0.0, (sender, e) =>
            {
                (sender as PropertyRangeEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValueRangeMinPropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyValueRangeMin property 
        /// </summary>
        public double PropertyValueRangeMin
        {
            get { return (double)GetValue(PropertyValueRangeMinProperty); }
            set { SetValue(PropertyValueRangeMinProperty, value); }
        }
        public const string PropertyValueRangeMaxPropertyName = "PropertyValueRangeMax";
        public static readonly DependencyProperty PropertyValueRangeMaxProperty = DependencyProperty.Register(
            PropertyValueRangeMaxPropertyName, typeof(double), typeof(PropertyRangeEditor), 
            new PropertyMetadata(10.0, (sender, e) =>
            {
                (sender as PropertyRangeEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyValueRangeMaxPropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyValueRangeMax property 
        /// </summary>
        public double PropertyValueRangeMax
        {
            get { return (double)GetValue(PropertyValueRangeMaxProperty); }
            set { SetValue(PropertyValueRangeMaxProperty, value); }
        }


        private new void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case PropertyValueRangeMaxPropertyName:
                    {
                        this.PropertyValueRangeMaxString = string.Format(string.Format("{{0:{0}}}", this.PropertyValueFormat), this.PropertyValueRangeMax);
                        break;
                    }
                case PropertyValueRangeMinPropertyName:
                    {
                        this.PropertyValueRangeMinString = string.Format(string.Format("{{0:{0}}}", this.PropertyValueFormat), this.PropertyValueRangeMin);
                        break;
                    }
                case PropertyValueFormatPropertyName:
                    {
                        this.PropertyValueRangeMinString = string.Format(string.Format("{{0:{0}}}", this.PropertyValueFormat), this.PropertyValueRangeMin);
                        this.PropertyValueRangeMaxString = string.Format(string.Format("{{0:{0}}}", this.PropertyValueFormat), this.PropertyValueRangeMax);
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