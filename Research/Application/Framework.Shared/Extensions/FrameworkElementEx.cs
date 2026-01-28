//using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace System.Windows //IGExtensions.Framework.Controls
{
    /// <summary>
    /// Provides extension methods for <seealso cref="FrameworkElement"/>
    /// </summary>
    public static class FrameworkElementEx
    {
        /// <summary>
        /// Applies style at location of resources to the current <seealso cref="FrameworkElement"/>
        /// </summary>
        /// <remarks>This is an extension method provided by <seealso cref="FrameworkElementEx"/></remarks>
        public static void ApplyStyle(this FrameworkElement element, string stylePath)
        {
            var style = new ResourceDictionary();
            style.Source = new Uri(stylePath, UriKind.Relative);
            element.Resources.MergedDictionaries.Add(style);
        }
        /// <summary>
        /// Converts the current <seealso cref="FrameworkElement"/> to a <seealso cref="WriteableBitmap"/>
        /// </summary>
        /// <remarks>This is an extension method provided by <seealso cref="FrameworkElementEx"/></remarks>
        public static WriteableBitmap ToBitmap(this FrameworkElement element)
        {
            return CreateBitmap(element);
        }

        /// <summary>
        /// Creates <seealso cref="WriteableBitmap"/> from a <seealso cref="FrameworkElement"/> to 
        /// </summary>
        /// <remarks>This is an extension method provided by <seealso cref="FrameworkElementEx"/></remarks>
        public static WriteableBitmap CreateBitmap(FrameworkElement element, bool isElementInVisualTree = false)
        {
            WriteableBitmap bitmap = null;

            if (!isElementInVisualTree)
            {
                element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                element.Arrange(new Rect(new Point(0, 0), element.DesiredSize));
            }

            var width = (int)Math.Ceiling(element.ActualWidth);
            var height = (int)Math.Ceiling(element.ActualHeight);

            width = width == 0 ? 1 : width;
            height = height == 0 ? 1 : height;

#if SILVERLIGHT
            bitmap = new WriteableBitmap(element, null);
#else // WPF
           var rtbmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Default);
           rtbmp.Render(element);
           //var bitmapSource = rtbmp;
           bitmap = new WriteableBitmap(rtbmp);
#endif
            return bitmap;
        }
        //public static IEnumerable<DependencyObject> Descendents(this DependencyObject root, int depth)
        //{
          
        //    int count = VisualTreeHelper.GetChildrenCount(root);
        //    for (int i = 0; i < count; i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(root, i);
        //        yield return child;
        //        if (depth > 0)
        //        {
        //            foreach (var descendent in Descendents(child, --depth))
        //                yield return descendent;
        //        }
        //    }
        //}

        //public static IEnumerable<DependencyObject> Descendents(this DependencyObject root)
        //{
        //    return Descendents(root, Int32.MaxValue);
        //}

        //public static IEnumerable<DependencyObject> Descendents(this DependencyObject root, Type descendentsType)
        //{
        //    return root.Descendents().Where(i => i.GetType() == descendentsType);
        //}

        //public static IEnumerable<DependencyObject> Ancestors(this DependencyObject root)
        //{
        //    var grid = new Grid();
        //    var image2 = grid.Descendents().Where(i => i is Image).FirstOrDefault();

        //    DependencyObject current = VisualTreeHelper.GetParent(root);
        //    while (current != null)
        //    {
        //        yield return current;
        //        current = VisualTreeHelper.GetParent(current);
        //    }
        //}
    }


}