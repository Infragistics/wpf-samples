using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;

namespace IGExtensions.Common.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AssetsPathConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets component name used to build path to an asset: /[ComponentName];component/[ComponentAssetsPath]  
        /// <example> component = "IGExtensions.Common" </example>
        /// </summary>
        public string ComponentName { get; set; }
        /// <summary>
        /// Gets or sets component path to an asset: /[ComponentName];component/[ComponentAssetsPath]  
        /// <example> path = "/assets/images/" </example>
        /// </summary>
        public string ComponentAssetsPath { get; set; }
        /// <summary>
        /// Gets or sets whether or component path is culture specific, and it requires "en", "ja" in the component path 
        /// </summary>
        public bool IsCultureSpecific { get; set; }

        protected AssetsPathConverter()
        {
            ComponentName = "IGExtensions.Common";
            ComponentAssetsPath = "/assets/";
            IsCultureSpecific = false;
        }

        public string GetComponentFullPath()
        {
            // /IGExtensions.AirplaneSeatingMap;component/Assets/Images/ 
            string path = "/" + ComponentName + ";component" + ComponentAssetsPath;
            return path;
        }
        /// <summary>
        /// Returns localized storage path for all assets such as xml, mdb, and images files
        /// </summary>
        public static string LocalizePath(string path, bool cultureSpecific)
        {
            if (cultureSpecific &&
                (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp" ||
                 Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja"))
            {
                path += "ja/"; //ja-jp
            }
            //else
            //{
            //    path += "en/"; // en-us
            //}
            return path;
        }

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        /// <param name="value">The source data being passed to the target.</param><param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param><param name="parameter">An optional parameter to be used in the converter logic.</param><param name="culture">The culture of the conversion.</param>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        /// <param name="value">The target data being passed to the source.</param><param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param><param name="parameter">An optional parameter to be used in the converter logic.</param><param name="culture">The culture of the conversion.</param>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }

}