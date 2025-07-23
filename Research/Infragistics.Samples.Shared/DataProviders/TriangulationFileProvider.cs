using System;

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a class for providing absolute Path/Uri to Triangulation files 
    /// </summary>
    public class TriangulationFileProvider
    {
        #region Triangulation Files
        public string TriangulationFileRelativePath { get; set; }
        public string TriangulationFileAbsolutePath
        {
            get
            {
                string path = GetTriangulationFileLocalPath(this.TriangulationFileRelativePath);
                return path;
            }
        }
        public Uri TriangulationFileAbsoluteUri
        {
            get
            {
                return GetTriangulationFileLocalUri(this.TriangulationFileRelativePath);
            }
        }
        /// <summary>
        /// Return Uri to an Triangulation file in the Assets assembly for a given name of Triangulation file.
        /// <para>where [Local] is "en-us" or "ja-jp" depending on Thread.CurrentThread.CurrentCulture. </para>
        /// <para>where [fileName] is file name of a Triangulation file.</para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Uri GetTriangulationFileLocalUri(string fileName)
        {
            return new Uri(GetTriangulationFileLocalPath(fileName));
        }
        /// <summary>
        /// Return path to a Triangulation file in the Assets assembly for a given name of Triangulation file.
        /// <para>where [Local] is "en-us" or "ja-jp" depending on Thread.CurrentThread.CurrentCulture. </para>
        /// <para>where [fileName] is file name of a Triangulation file.</para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetTriangulationFileLocalPath(string fileName)
        {
            fileName = fileName.TrimStart(@" \/".ToCharArray()).Replace(@"\", "/");

            return StorageDataProvider.GetStorageShapeFilePath(fileName);
        }

        /// <summary>
        /// Return path to a Triangulation file in the Assets assembly.
        /// <para>where [Local] is "en-us" or "ja-jp" depending on Thread.CurrentThread.CurrentCulture. </para>
        /// </summary>
        /// <returns></returns>
        public static string GetTriangulationFileLocalPath()
        {
            return StorageDataProvider.GetStorageShapeFilePath();
        }

        #endregion

    }
}