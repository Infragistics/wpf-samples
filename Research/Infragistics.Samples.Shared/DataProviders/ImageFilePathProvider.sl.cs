using System;
using System.Windows.Media.Imaging;

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a data provider for image files.
    /// </summary>
    public class ImageFilePathProvider
    {
        /// <summary>
        /// Return Uri to an image in "/ClientBin/storage/[Local]/images/[fileName]" location for a given image.
        /// <para>where [Local] is "en-us" or "ja-jp" depending on Thread.CurrentThread.CurrentCulture. </para>
        /// <para>where [fileName] is file name of an image.</para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Uri GetImageLocalUri(string fileName)
        {
            return new Uri(GetImageLocalPath(fileName));
        }
        
        public static string GetImageLocalPath(string fileName)
        {
            fileName = fileName.TrimStart(@" \/".ToCharArray()).Replace(@"\", "/");

            return StorageDataProvider.GetStorageImagePath(fileName);
        }

        /// <summary>
        /// Return path to an image in "ClientBin/storage/[Local]/images" location.
        /// <para>where [Local] is "en-us" or "ja-jp" depending on Thread.CurrentThread.CurrentCulture. </para>
        /// </summary>
        /// <returns></returns>
        public static string GetImageLocalPath()
        {
            return StorageDataProvider.GetStorageImagePath();
        }

        public static Uri GetLocalizedImageUri(string fileName)
        {
            fileName = fileName.TrimStart(@" \/".ToCharArray()).Replace(@"\", "/");

            return new Uri(StorageDataProvider.GetStorageImagePath(fileName));
        }

        public string ImageFileRelativePath { get; set; }
        public string ImageFileAbsolutePath
        {
            get
            {
                string path = GetImageLocalPath(this.ImageFileRelativePath);
                return path;
            }
        }
        public Uri ShapeFileAbsoluteUri
        {
            get
            {
                return GetImageLocalUri(this.ImageFileRelativePath);
            }
        }

        public static BitmapImage CreateBitmapImage(string imagePath)
        {
            BitmapImage bmpImage = new BitmapImage();
            //bmpImage.BeginInit();
            bmpImage.UriSource = new Uri(imagePath, UriKind.Absolute);
            //bmpImage.EndInit();
            return bmpImage;
        }
    }
}