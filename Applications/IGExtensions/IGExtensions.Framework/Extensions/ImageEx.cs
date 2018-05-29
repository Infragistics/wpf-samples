using System.Windows.Media.Imaging;

namespace System.Windows.Controls
{
    public static class ImageEx
    {
        /// <summary>
        /// Converts Image control to BitmapSource
        /// </summary>
        public static BitmapSource ToBitmapSource(this Image image)
        {
            BitmapSource bmp = null;

#if SILVERLIGHT
            if (image != null) bmp = new WriteableBitmap(image, null);
            
#else // WPF
            if (image != null) bmp = image.Source as BitmapSource;
#endif
            return bmp;
        }
        /// <summary>
        /// Checks if the Image control is empty (all pixels are blank)
        /// </summary>
        public static bool IsEmpty(this Image image)
        {
            var bitmap = image.ToBitmapSource();
            if (bitmap == null )return true;
            return bitmap.IsEmpty();
        }
        /// <summary>
        /// Gets array of Pixels that builds this Image
        /// </summary>
        public static int[] GetPixels(this Image image)
        {
            var bitmap = image.ToBitmapSource();
            return bitmap.GetPixels();
        }
    }
}