using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGDataGrid.Utils
{
    class Utils
    {
        public static string GetTempFileName(string extension)
        {
            for (int i = 1; i < 5000; i++)
            {
                string fileName = System.IO.Path.GetTempPath() + "XamSamplesBrowserTempFile" + i.ToString() + extension;
                if (false == System.IO.File.Exists(fileName))
                    return fileName;
            }

            return string.Empty;
        }
    }
}
