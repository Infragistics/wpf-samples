using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;

namespace Infragistics.Samples.Browser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Samples should call this method to extract an assembly resource.
        /// NOTE: Be sure to dispose of the Stream that gets returned.
        /// </summary>
        /// <param name="resourceFile">A pack URI pointing to a resource in the EXE.</param>
        public static Stream GetResourceStream(string resourceFile)
        {
            Uri uri = new Uri(resourceFile, UriKind.RelativeOrAbsolute);

            var info = Application.GetResourceStream(uri);
            if (info == null || info.Stream == null)
                throw new ApplicationException("Missing resource file: " + resourceFile);

            return info.Stream;
        }
    }
}
