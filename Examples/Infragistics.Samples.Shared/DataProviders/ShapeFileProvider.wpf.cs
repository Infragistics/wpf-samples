using System;
using System.Reflection;
using System.IO;

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a class for providing absolute Path/Uri to shape files 
    /// </summary>
    public class ShapeFileProvider
    {

        #region Shape Files
        public string ShapeFileRelativePath { get; set; }
        public string ShapeFileAbsolutePath
        {
            get
            {
                string path = GetShapeFileLocalPath(this.ShapeFileRelativePath);
                return path;
            }
        }
        public Uri ShapeFileAbsoluteUri
        {
            get
            {
                return GetShapeFileLocalUri(this.ShapeFileRelativePath);
            }
        }
        /// <summary>
        /// Return Uri to an shape file in the Assets assembly for a given name of shape file.
        /// <para>where [Local] is "en-us" or "ja-jp" depending on Thread.CurrentThread.CurrentCulture. </para>
        /// <para>where [fileName] is file name of a shape file.</para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Uri GetShapeFileLocalUri(string fileName)
        {
            return new Uri(GetShapeFileLocalPath(fileName));
        }
        /// <summary>
        /// Return path to a shape file in the Assets assembly for a given name of shape file.
        /// <para>where [Local] is "en-us" or "ja-jp" depending on Thread.CurrentThread.CurrentCulture. </para>
        /// <para>where [fileName] is file name of a shape file.</para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetShapeFileLocalPath(string fileName)
        {
            fileName = fileName.TrimStart(@" \/".ToCharArray()).Replace(@"\", "/");

            return StorageDataProvider.GetStorageShapeFilePath(fileName);
        }

        /// <summary>
        /// Return path to a shape file in the Assets assembly.
        /// <para>where [Local] is "en-us" or "ja-jp" depending on Thread.CurrentThread.CurrentCulture. </para>
        /// </summary>
        /// <returns></returns>
        public static string GetShapeFileLocalPath()
        {
            return StorageDataProvider.GetStorageShapeFilePath();
        }
        
        #endregion
   
        #region Shape Database Files
        public string ShapeDatabaseRelativePath { get; set; }
        public string ShapeDatabaseAbsolutePath
        {
            get
            {
                string path = GetShapeFileLocalPath(this.ShapeDatabaseRelativePath);
                return path;
            }
        }
        public Uri ShapeDatabaseAbsoluteUri
        {
            get
            {
                return GetShapeFileLocalUri(this.ShapeDatabaseRelativePath);
            }
        }
        /// <summary>
        /// Return Uri to an shape database file in the Assets assembly for a given name of shape database file.
        /// <para>where [Local] is "en-us" or "ja-jp" depending on Thread.CurrentThread.CurrentCulture. </para>
        /// <para>where [fileName] is file name of a shape file.</para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Uri GetShapeDatabaseLocalUri(string fileName)
        {
            return new Uri(GetShapeDatabaseLocalPath(fileName));
        }
        /// <summary>
        /// Return path to a shape database file in the Assets assembly for a given name of shape database file.
        /// <para>where [Local] is "en-us" or "ja-jp" depending on Thread.CurrentThread.CurrentCulture. </para>
        /// <para>where [fileName] is file name of a shape file.</para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetShapeDatabaseLocalPath(string fileName)
        {
            fileName = fileName.TrimStart(@" \/".ToCharArray()).Replace(@"\", "/");

            return StorageDataProvider.GetStorageShapeDatabasePath(fileName);
        }

        /// <summary>
        /// Return path to a shape database file in the Assets assembly.
        /// <para>where [Local] is "en-us" or "ja-jp" depending on Thread.CurrentThread.CurrentCulture. </para>
        /// </summary>
        /// <returns></returns>
        public static string GetShapeDatabaseLocalPath()
        {
            return StorageDataProvider.GetStorageShapeDatabasePath();
        }

        #endregion

       ///// <summary>
       ///// Returns absolute path for relative path of a file used by the current executing assembly
       ///// </summary>
       //public static string GetAbsolutePath(string relativePath)
       //{
       //    relativePath = relativePath.Replace("@", "");
       //     if (relativePath.StartsWith("/"))
       //         relativePath = relativePath.Substring(1);

       //    relativePath = relativePath.Replace("/", "\\");
       //    string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
       //    return Path.Combine(appDir, relativePath);
       //}

       ///// <summary>
       ///// Returns absolute uri for relative path of a file used by the current executing assembly
       ///// </summary>
       //public static Uri GetAbsoluteUri(string relativePath)
       //{
       //    return new Uri(GetAbsolutePath(relativePath), UriKind.RelativeOrAbsolute);
       //}

       //public string RelativePath { get; set; }
       //public string AbsolutePath 
       //{   get
       //    {
       //        return GetAbsolutePath(RelativePath);
       //    }
       //}
       //public Uri AbsoluteUri
       //{
       //    get
       //    {
       //        return GetAbsoluteUri(RelativePath);
       //    }
       //}
    }
}
