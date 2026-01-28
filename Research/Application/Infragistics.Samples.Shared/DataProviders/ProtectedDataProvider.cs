using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace Infragistics.Samples.Shared.DataProviders
{
    public static class ProtectedDataProvider
    {
        static ProtectedDataProvider()
        {
            MapKeys = new MapKeys();
            
            string filePath = StorageDataProvider.GetStorageLocalPath() + "/ProtectedData/ProtectedMapKeys.xml";
                 
            MapKeyProvider = new WebClient();
            //MapKeyProvider.
            MapKeyProvider.DownloadStringAsync(new Uri(filePath, UriKind.RelativeOrAbsolute));
            MapKeyProvider.DownloadStringCompleted += OnMapKeyProviderCompleted;
  
          }
        /// <summary>
        /// Occurs when finished downloading a xml file.
        /// </summary>
        //public event EventHandler<GetMapKeysCompletedEventArgs> GetMapKeysCompleted;
        public static MapKeys MapKeys { get; private set; }

        private static readonly WebClient MapKeyProvider;
        static void OnMapKeyProviderCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //if (e.Error != null)
            //{
            //    Exception error = new Exception("Failed to download: " + _xmlFilePath + " xml file due to error: ", e.Error);
            //    OnGetXmlDataCompleted(new GetXmlDataCompletedEventArgs(error));
            //}
            if (e.Error == null)
            {
                XDocument xmlDoc = XDocument.Parse(e.Result);

                List<MapKey> entries = (from entry in xmlDoc.Descendants("MapKey")
                              select new MapKey
                              {
                                  Name = entry.Attribute("Name").GetString(),
                                  Key = entry.Attribute("Key").GetString(),
                              }).ToList();

                foreach (MapKey entry in entries)
                {
                    if (entry.Name == "BingMaps") MapKeys.BingMapsKey = entry.Key;
                    if (entry.Name == "CloudMade") MapKeys.CloudMadeKey = entry.Key;
                }
               
            }
        }


        //private void OnGetMapKeysCompleted(GetMapKeysCompletedEventArgs eventArgs)
        //{
        //    if (this.GetMapKeysCompleted != null)
        //    {
        //        this.GetMapKeysCompleted(this, eventArgs);
        //    }
        //}
         
    }
    public class GetMapKeysCompletedEventArgs : EventArgs
    {

        public GetMapKeysCompletedEventArgs(MapKeys mapKeys)
        {
            this.Result = mapKeys;
            this.Error = null;
            this.HasError = false;
        }

        public GetMapKeysCompletedEventArgs(Exception error)
        {
            this.Result = null;
            this.Error = error;
            this.HasError = true;
        }

        /// <summary>
        /// Gets the protected Map Keys
        /// </summary>
        public MapKeys Result { get; private set; }
        /// <summary>
        /// Gets an Exception assosicated with loading the protected Map Keys
        /// </summary>
        public Exception Error { get; private set; }
        /// <summary>
        /// Gets if there were error when loading the protected Map Keys
        /// </summary>
        public bool HasError { get; private set; }

    }
    public class MapKeys
    {
        public MapKeys()
        {
            BingMapsKey = string.Empty;
            CloudMadeKey = string.Empty;
     
        }
        public string CloudMadeKey { get; set; }
        public string BingMapsKey { get; set; }
    }

    public class MapKey
    {
        public string Name { get; set; }
        public string Key { get; set; }
    }
}
