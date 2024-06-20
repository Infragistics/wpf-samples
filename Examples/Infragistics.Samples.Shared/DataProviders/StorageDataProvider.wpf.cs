using Infragistics.Samples.Assets.Providers;

namespace Infragistics.Samples.Shared.DataProviders
{
    //TODO: clean up when all samples are functional
    public class StorageDataProvider : StorageProvider
    {
        ///// <summary>
        ///// Returns localized base path for all storage assets: xml, mdb, images files
        ///// </summary>
        //public static string GetStorageLocalPath()
        //{
        //    return StorageProvider.GetStorageLocalPath();
        //}
        ///// <summary>
        ///// Returns Stream to a resource located at the specified path:
        ///// <para>For example: [StorageAssemblyName].Storage.[local].xml.filename.xml</para>
        ///// <para>where: [local] can be "ja-jp" or "en-us"</para>
        ///// </summary>
        //public static Stream GetStorageResourceStream(string resourcePath)
        //{
        //    return StorageProvider.GetStorageResourceStream(resourcePath);
        //}

         
        //public static string StorageAssemblyName = "Infragistics.Samples.Assets";

        //public static string GetStorageLocalPathBase()
        //{
        //    //string path = Application.Current.Host.Source.OriginalString.Substring(0, Application.Current.Host.Source.OriginalString.LastIndexOf("/"));
        //    //string path = "/Infragistics.Samples.Assets;component";
        //    string path = StorageAssemblyName; // + ".";

        //    if (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
        //    {
        //        path += ".DataSources.ja-jp"; //ja-jp
        //    }
        //    else
        //    {
        //        path += ".DataSources.en-us"; // en-us
        //    }
        //    return path;
        //}
        //public static string GetStorageLocalPath(string fileName)
        //{
        //    string path = StorageDataPathProvider.GetStorageLocalPathBase();
        //    return path + "." + fileName;
        //}
        //public static Uri GetStorageLocalUri(string fileName)
        //{
        //    string path = StorageDataPathProvider.GetStorageLocalPathBase();
        //    return new Uri(path + "." + fileName);
        //}
        //internal static Stream GetStorageLocalStream(string streamName)
        //{
        //   // Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(streamName);

        //    Assembly assembly = StorageProvider.GetStorageAssembly();
            
        //    Stream stream = assembly.GetManifestResourceStream(streamName);
        //    if (stream != null)
        //    {
        //        //StreamReader sr = new StreamReader(stream);
        //        XmlTextReader reader = new XmlTextReader(stream);
        //        XmlDocument doc = new XmlDocument();
        //        doc.Load(reader);
        //    }

        //    return stream;
        //}

        //public static string GetDataSourcePath(string dataSourceName)
        //{
        //    //string path = AssemblyProvider.GetAssemblyPack(StorageAssemblyName);
        //    Stream stream = GetStorageLocalStream(GetStorageLocalPath("xml.StockMarketData.xml"));

        //    //Assembly assembly = Assembly..LoadAssembly(assemblyPath, true);
        //    string path = StorageAssemblyName + ".";

        //    //Stream stream = this.GetType().Assembly.GetManifestResourceStream(StorageAssemblyName + "");

        //    path = Assembly.GetExecutingAssembly().CodeBase;
        //    path = path.Substring(8);
        //    path = Path.GetDirectoryName(path);
        //    DirectoryInfo dir = new DirectoryInfo(path);
        //    path = dir.FullName;
        //    path = path + Path.DirectorySeparatorChar + "DataSources" + Path.DirectorySeparatorChar;
        //    if (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
        //    {
        //        path += "ja" + Path.DirectorySeparatorChar;
        //    }
        //    path += dataSourceName;
        //    return path;
        //}
        /// <summary>
        /// Reads information from an embedded resource.
        /// In VS, set the type of the file in solution explorer to "Embedded Resource"
        /// <example>
        /// bytes = ReadBytesFromStream("MyTestProgram.SomeDataFile.xml")
        /// </example>
        /// </summary>
        /// <param name="streamName"></param>
        /// <returns></returns>
        private byte[] ReadBytesFromStream(string streamName)
        {
            using (System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(streamName))
            {
                byte[] result = new byte[stream.Length];
                stream.Read(result, 0, (int)stream.Length);
                return result;
            }
        }
    }
}