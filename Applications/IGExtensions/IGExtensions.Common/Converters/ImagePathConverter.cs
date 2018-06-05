using System;

namespace IGExtensions.Common.Converters
{
    /// <summary>
    /// Represents a converter for paths to images 
    /// </summary>
    public class ImagePathConverter : AssetsPathConverter
    {
        public ImagePathConverter()
        {
            ComponentName = "IGExtensions.Common";
            ComponentAssetsPath = "/assets/images/";
            IsCultureSpecific = false;
        }
        /// <summary>
        /// 
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string path = GetImageLocalPath(value.ToString());
#if WPF
                path = @"pack://application:,,," + path;
#endif
                return path;  
            }
            catch
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns localized path to an image resource.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetImageLocalPath(string fileName)
        {
            string path = GetComponentFullPath();
            path = LocalizePath(path, this.IsCultureSpecific) + fileName;
            return path;
        }

    }

}