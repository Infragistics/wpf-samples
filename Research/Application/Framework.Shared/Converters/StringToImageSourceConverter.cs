using System;
using System.Globalization;
using System.Reflection;

#if Xamarin
using Xamarin.Forms;
#else
using System.Windows;
#endif

namespace Infragistics.Framework
{
    public class StringToImageResource : IValueConverter
    { 
        /// <summary>
        /// Get resource id for specified image file name
        /// </summary>  
        public static string GetImageResourceId(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            name = name.Replace(@"\", ".");
            name = name.Replace(@"//", ".");
            name = name.Replace(@"/", ".");
            name = name.Replace(@"..", ".");

            if (!name.EndsWith(".png"))
                 name = name + ".png";

            // check for resources in the main app assembly
            var localAssembly = typeof(StringToImageResource).GetTypeInfo().Assembly;
            var localResources = localAssembly.GetManifestResourceNames();
            foreach (var resource in localResources)
            {
                if (resource.EndsWith(name, StringComparison.OrdinalIgnoreCase))
                {
                    //Logs.Trace(assembly.FullName + ": " + resource);
                    return resource;
                }
            }
            // check for resources in the IG Framework assembly
            var frameworkAssembly = typeof(Infragistics.Framework.Components).GetTypeInfo().Assembly;
            var frameworkResources = frameworkAssembly.GetManifestResourceNames();
            foreach (var resource in frameworkResources)
            {
                if (resource.EndsWith(name, StringComparison.OrdinalIgnoreCase))
                {
                    //Logs.Trace(assembly.FullName + ": " + resource);
                    return resource;
                }
            }
            Logs.Error("Cannot find image resource: " + name);
            return null;
        }
        /// <summary>
        /// Get ImageSource for specified resource file name
        /// </summary>  
        public static ImageSource GetImageSource(string name)
        {
            var resource = GetImageResourceId(name);
            if (resource != null)
            {
                return ImageSource.FromResource(resource);
            }
            return null;
        }

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = string.Empty;
            if (parameter != null)
            {
                name = parameter.ToString();
            }
            else if (value != null)
            {
                name = value.ToString();
            }

            if (string.IsNullOrEmpty(name))
            {
                var msg = this.GetType() + " Cannot convert ";
                msg += value == null ? "null value" : value.ToString() + " value";
                msg += parameter == null ? "null parameter" : parameter.ToString() + " parameter";
                msg += typeof(ImageSource) + " type";
                System.Diagnostics.Debug.WriteLine(msg);
            }
            else
            { 
                return GetImageSource(name);
            }
            return null;
        }
        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            if (value is FileImageSource)
            {
                var image = (FileImageSource)value;
                return image.File;
            }
            return "";
        }
    }

    public class StringToImageSourceConverter : TypeConverter, IValueConverter
    {
        public override bool CanConvertFrom(Type sourceType)
        {
            return sourceType == typeof(string);
        } 
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            return GetImageSource(value);
        }
        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            if (value is FileImageSource)
            {
                var image = (FileImageSource)value;
                return image.File;
            }
            return "";
        }

        public static ImageSource GetImageSource(object value)
        { 
            var path = ImageProvider.GetPath(value);
            Uri uri;
            if (!Uri.TryCreate(path, UriKind.Absolute, out uri) || !(uri.Scheme != "file"))
            {
                path = path.Replace(@"//", "");
                return ImageSource.FromFile(path);
            }
            return ImageSource.FromUri(uri);
        }
    }
    public static class ImageProvider
    {
        public static string GetPath(object value)
        {
            if (value == null)
            {
                return null;
            }
            var path = value as string;
            if (path == null)
            {
                var msg = string.Format("Cannot convert \"{0}\" into {1}", new[]
                    {
                        value, typeof(ImageSource)
                    });
                throw new InvalidOperationException(msg);
            }

            if (!path.StartsWith("Images/"))
            {
                //Device.OnPlatform(WinPhone: () => path = "Images/" + path);
                if (Device.RuntimePlatform == Device.UWP)
                    path = "Images/" + path; 
            }

            return path;
        }
    }
}