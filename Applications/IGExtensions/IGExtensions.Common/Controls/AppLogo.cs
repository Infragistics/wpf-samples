using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IGExtensions.Common.Controls
{
    public class AppLogo : Control
    {
        public AppLogo()
        {
            this.DefaultStyleKey = typeof(AppLogo);
        }
       
        internal const string AppImagePropertyName = "AppImage";

        public static readonly DependencyProperty AppImageProperty = DependencyProperty.Register(
            AppImagePropertyName, typeof (ImageSource), typeof (AppLogo), null);

        /// <summary>
        /// Gets or sets images source for an application
        /// </summary>
        public ImageSource AppImage
        {
            get { return (ImageSource) GetValue(AppImageProperty); }
            set { SetValue(AppImageProperty, value); }
        }

        internal const string AppNamePropertyName = "AppName";
        internal static string AppNameDefault = "App Name";

        public static readonly DependencyProperty AppNameProperty =
            DependencyProperty.RegisterAttached(AppNamePropertyName,
            typeof(string), typeof(AppLogo), new PropertyMetadata(AppNameDefault, 
            (o, e) => OnAppNameChanged(o, e.OldValue as string, e.NewValue as string)));

        public string AppName
        {
            get { return (string)GetValue(AppNameProperty); }
            set { SetValue(AppNameProperty, value); }
        }


        private static void OnAppNameChanged(DependencyObject target, string oldValue, string newValue)
        {
        }

        internal const string AppSubTitlePropertyName = "AppSubTitle";
        internal static string AppSubTitleDefault = "App Title";

        public static readonly DependencyProperty AppSubTitleProperty =
            DependencyProperty.RegisterAttached(AppSubTitlePropertyName,
            typeof(string), typeof(AppLogo), new PropertyMetadata(AppSubTitleDefault, 
            (o, e) => OnAppSubTitleChanged(o, e.OldValue as string, e.NewValue as string)));

	 
        public string AppSubTitle
        {
            get { return (string)GetValue(AppSubTitleProperty); }
            set { SetValue(AppSubTitleProperty, value); }
        }

        private static void OnAppSubTitleChanged(DependencyObject target, string oldValue, string newValue)
        {
        }

         
 
    }


    //public class StringToBitmapImage : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return new BitmapImage(new Uri((string)value, UriKind.RelativeOrAbsolute));
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}