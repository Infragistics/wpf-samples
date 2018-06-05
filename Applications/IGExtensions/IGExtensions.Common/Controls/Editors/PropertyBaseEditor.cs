using System.ComponentModel;
using System.Windows;

namespace IGExtensions.Common.Controls
{
    public class PropertyBaseEditor : ObservableControl
    {

        public PropertyBaseEditor()
        {
            DefaultStyleKey = typeof(PropertyBaseEditor);

        }
        #region Properties
        public const string PropertyNamePropertyName = "PropertyName";
        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
            PropertyNamePropertyName, typeof(string), typeof(PropertyBaseEditor), new PropertyMetadata("Property Name:", (sender, e) =>
            {
                (sender as PropertyBaseEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyNamePropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyName property 
        /// </summary>
        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        public const string PropertyToolTipPropertyName = "PropertyToolTip";
        public static readonly DependencyProperty PropertyToolTipProperty = DependencyProperty.Register(
            PropertyToolTipPropertyName, typeof(string), typeof(PropertyBaseEditor), new PropertyMetadata(string.Empty, (sender, e) =>
            {
                (sender as PropertyBaseEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyToolTipPropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyToolTip property 
        /// </summary>
        public string PropertyToolTip
        {
            get { return (string)GetValue(PropertyToolTipProperty); }
            set { SetValue(PropertyToolTipProperty, value); }
        }
        public const string PropertyNameVisibilityPropertyName = "PropertyNameVisibility";
        public static readonly DependencyProperty PropertyNameVisibilityProperty = DependencyProperty.Register(
            PropertyNameVisibilityPropertyName, typeof(Visibility), typeof(PropertyBaseEditor), new PropertyMetadata(Visibility.Collapsed, (sender, e) =>
            {
                (sender as PropertyBaseEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyNameVisibilityPropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyNameVisibility property 
        /// </summary>
        public Visibility PropertyNameVisibility
        {
            get { return (Visibility)GetValue(PropertyNameVisibilityProperty); }
            set { SetValue(PropertyNameVisibilityProperty, value); }
        }

        public const string PropertyToolTipVisibilityPropertyName = "PropertyToolTipVisibility";
        public static readonly DependencyProperty PropertyToolTipVisibilityProperty = DependencyProperty.Register(
            PropertyToolTipVisibilityPropertyName, typeof(Visibility), typeof(PropertyBaseEditor), new PropertyMetadata(Visibility.Collapsed, (sender, e) =>
            {
                (sender as PropertyBaseEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyToolTipVisibilityPropertyName));
            }));
        /// <summary>
        /// Gets or sets the PropertyToolTipVisibility property 
        /// </summary>
        public Visibility PropertyToolTipVisibility
        {
            get { return (Visibility)GetValue(PropertyToolTipVisibilityProperty); }
            set { SetValue(PropertyToolTipVisibilityProperty, value); }
        }

        #endregion
        public new void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            base.OnPropertyChanged(ea);
            switch (ea.PropertyName)
            {
                case "PropertyToolTip":
                    {
                        PropertyToolTipVisibility = this.PropertyToolTip == string.Empty ? Visibility.Collapsed : Visibility.Visible;
                        break;
                    }
                case "PropertyName":
                    {
                        PropertyNameVisibility = this.PropertyName == string.Empty ? Visibility.Collapsed : Visibility.Visible;
                        break;
                    }
            }
        }
    }
}