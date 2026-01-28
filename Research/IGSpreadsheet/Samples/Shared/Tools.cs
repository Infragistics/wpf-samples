using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Resources;

namespace IGSpreadsheet.Samples.Shared
{
    public static class Tools
    {
        private static Graphics _gfx = null;

        private static Graphics Gfx
        {
            get
            {
                if (_gfx == null)
                {
                    foreach (Window w in Application.Current.Windows)
                    {
                        if (w.IsActive)
                        {
                            WindowInteropHelper helper = new WindowInteropHelper(w);
                            _gfx = Graphics.FromHwnd(helper.Handle);
                            break;
                        }
                    }
                }
                return _gfx;
            }
        }

        public static double PixelsToTwips(int pixels)
        {
            // (72 is a constant (points per inch), 96 depends on the resolution/monitor)
            // pixel = point * 96 / 72 
            // point = pixel * 72 / 96

            // convert pixels to points
            double po = pixels * 72 / Tools.Gfx.DpiX;
            // convert points to twips
            return po * 20;
        }

        // returns a file depending on the current thread's culture
        public static Stream GetLocalizedFileAsStream(string fileName)
        {
            string thePath = "/IGSpreadsheet;component/Files/";
            if (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
            {
                thePath += "ja/";
            }
            else
            {
                thePath += "en/";
            }
            thePath += fileName;
            Uri uri = new Uri(thePath, UriKind.Relative);
            StreamResourceInfo sri = Application.GetResourceStream(uri);
            return sri.Stream;
        }
    }
}
