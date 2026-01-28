using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace System.Windows.Media.Imaging // IGExtensions.Framework.System
{
    // TODO: http://writeablebitmapex.codeplex.com/

    public static class BitmapEx
    {
        /// <summary>
        /// Checks if the BitmapSource is empty (all pixels are blank)
        /// </summary>
        public static bool IsEmpty(this BitmapSource bitmap)
        {
            var pixels = bitmap.GetPixels();
            foreach (var pixel in pixels)
            {
                if (pixel != 0) return false;
            }
            return true;
        }
        /// <summary>
        /// Gets Color at pixel location of BitmapSource 
        /// <remarks>This is an extension method provided by <seealso cref="BitmapEx"/></remarks>
        /// </summary>
        public static Color GetPixelColorAt(this BitmapSource bitmap, int x, int y)
        {
            int index = ((y - 1) * bitmap.PixelWidth) + x;
            var pixels = bitmap.GetPixels();
            var color = GetPixelColor(index, pixels);
            return color;
        }
        /// <summary>
        /// Gets Color at pixel location of BitmapSource 
        /// </summary>
        /// <remarks>This is an extension method provided by <seealso cref="BitmapEx"/></remarks>
        public static Color GetPixelColorAt(this BitmapSource bitmap, Point location)
        {
            return bitmap.GetPixelColorAt((int)location.X, (int)location.Y);
        }
       
        /// <summary>
        /// Gets Color at index of pixel array 
        /// </summary>
        /// <remarks>This is an extension method provided by <seealso cref="BitmapEx"/></remarks>
        public static Color GetPixelColor(int index, int[] pixels)
        {
            var color = Colors.Transparent;
            if (index >= 0 && index < pixels.Length)
            {
                int pixel = pixels[index];
                color = pixel.GetPixelColor();
                //var B = (byte)(pixel & 0xFF); pixel >>= 8;
                //var G = (byte)(pixel & 0xFF); pixel >>= 8;
                //var R = (byte)(pixel & 0xFF); pixel >>= 8;
                //var A = (byte)pixel; // alpha
                //color = Color.FromArgb(A, R, G, B);
            }
            return color;
        }

        /// <summary>
        /// Converts pixel to a Color  
        /// </summary>
        public static Color GetPixelColor(this int pixel)
        {
            var B = (byte)(pixel & 0xFF); pixel >>= 8;
            var G = (byte)(pixel & 0xFF); pixel >>= 8;
            var R = (byte)(pixel & 0xFF); pixel >>= 8;
            var A = (byte)pixel; // alpha
            var color = Color.FromArgb(A, R, G, B); 
            return color;
        }
  

#if SILVERLIGHT
        //public static Color GetPixelColor(this BitmapSource bitmap, int x, int y)
        //{
        //    int index = ((y - 1) * bitmap.PixelWidth) + x;
        //    var pixels = bitmap.GetPixels();
        //    var color = GetPixelColor(index, pixels);
        //    //if (index >= 0 && index < bitmap.Pixels.Length)
        //    //{
        //    //    int pixel = bitmap.Pixels[index];

        //    //    var B = (byte)(pixel & 0xFF); pixel >>= 8;
        //    //    var G = (byte)(pixel & 0xFF); pixel >>= 8;
        //    //    var R = (byte)(pixel & 0xFF); pixel >>= 8;
        //    //    var A = (byte)pixel; // alpha

        //    //    color = Color.FromArgb(A, R, G, B);
        //    //}
        //    return color;
        //}
        //public static int[] GetPixels(this BitmapSource bitmap)
        //{
        //    return bitmap.Pixels;
        //}
        /// <summary>
        /// Gets array of Pixels that builds this BitmapSource
        /// </summary>
        public static int[] GetPixels(this BitmapSource bitmap)
        {
            var wbm = new WriteableBitmap(bitmap);
            return wbm.Pixels;
        }

#else // WPF
        /// <summary>
        /// Gets array of Pixels that builds this BitmapSource
        /// </summary>
        public static int[] GetPixels(this BitmapSource bitmap)
        {
            if (bitmap.Format != PixelFormats.Bgra32)
                bitmap = new FormatConvertedBitmap(bitmap, PixelFormats.Bgra32, null, 0);

            var width = bitmap.PixelWidth;
            var height = bitmap.PixelHeight;
            int stride = (bitmap.PixelWidth * bitmap.Format.BitsPerPixel + 7) / 8;

            var pixels = new int[height * stride];
            bitmap.CopyPixels(pixels, stride, 0);

            return pixels;
        }
        //public static Color GetPixelColor(this WriteableBitmap bitmap, int x, int y)
        //{
        //    int index = ((y - 1) * bitmap.PixelWidth) + x;
        //    var pixels = bitmap.GetPixels();
        //    var color = GetPixelColor(index, GetPixels(bitmap));
        //    //var color = Colors.Transparent;
        //    // //if (index >= 0 && index < pixels.Length)
        //    //{
        //    //    int pixel = pixels[index];

        //    //    var B = (byte)(pixel & 0xFF); pixel >>= 8;
        //    //    var G = (byte)(pixel & 0xFF); pixel >>= 8;
        //    //    var R = (byte)(pixel & 0xFF); pixel >>= 8;
        //    //    var A = (byte)pixel; // alpha

        //    //    color = Color.FromArgb(A, R, G, B);
        //    //}
        //    return color;

        //    //var pixel = bitmap.Pixels()[x, y];
        //    //var color = Color.FromArgb(pixel.A, pixel.R, pixel.G, pixel.B);
        //    //return color;
        //}

#endif
    }

}