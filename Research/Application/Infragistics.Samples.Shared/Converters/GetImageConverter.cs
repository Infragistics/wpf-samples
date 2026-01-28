using System;
using System.Windows.Data;
using Infragistics.Samples.Shared.DataProviders;

namespace Infragistics.Samples.Shared.Converters
{
    public class GetImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                Uri link = ImageFilePathProvider.GetImageLocalUri(value.ToString());
                return link; // ImageFilePathProvider.GetImageLocalUri(value.ToString());
            }
            catch
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class GetImagePathConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string path = ImageFilePathProvider.GetImageLocalPath(value.ToString());
                return path; // ImageFilePathProvider.GetImageLocalUri(value.ToString());
            }
            catch
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class GetLocalizedImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                Uri link = ImageFilePathProvider.GetLocalizedImageUri(value.ToString());
                return link;
            }
            catch
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}