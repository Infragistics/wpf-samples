using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Shared.Extensions;

namespace Infragistics.Samples.Shared.DataProviders
{
    public class GeoImageryKeyProvider
    {
        public GeoImageryKeyProvider()
        {
            XmlDataProvider = new XmlDataProvider();
            XmlDataProvider.GetXmlDataCompleted += OnGetXmlDataCompleted;
        }
        public event EventHandler<GetMapKeyCompletedEventArgs> GetMapKeyCompleted;
        protected XmlDataProvider XmlDataProvider;
        
        public void GetMapKeys()
        {
            XmlDataProvider.GetXmlDataAsync("MapKeys.xml");
        }
        private void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnGetMapKeyCompleted(new GetMapKeyCompletedEventArgs("Map Keys Not Found."));
            }
            else
            {
                List<GeoImageryMapKey> mapKeys = ParseMapKeys(e.Result);
                OnGetMapKeyCompleted(new GetMapKeyCompletedEventArgs(mapKeys));
            }
        }
        private List<GeoImageryMapKey> ParseMapKeys(XDocument xmlDocument)
        {
            IEnumerable<GeoImageryMapKey> results =
           (from d in xmlDocument.Element("MapKeys").Elements("MapKey")
            select new GeoImageryMapKey
            {   // get values using XElementExtension
                Name = d.Element("Name").GetString(),
                Key = d.Element("MapKey").GetString()
            });

            List<GeoImageryMapKey> newWorldData = results.Select(dp => new GeoImageryMapKey
            {
                Name = dp.Name,
                Key = dp.Key,
            }).ToList();

            return newWorldData;
        }
        private void OnGetMapKeyCompleted(GetMapKeyCompletedEventArgs eventArgs)
        {
            if (this.GetMapKeyCompleted != null)
            {
                this.GetMapKeyCompleted(this, eventArgs);
            }
        }

    }
    public class GeoImageryMapKey
    {
        public string Name { get; set; }
        public string Key { get; set; }
    }

    public class GetMapKeyCompletedEventArgs : EventArgs
    {
        public GetMapKeyCompletedEventArgs()
            : this(new List<GeoImageryMapKey>(), null)
        {
        }
        public GetMapKeyCompletedEventArgs(List<GeoImageryMapKey> result)
            : this(result, null)
        {
        }
        public GetMapKeyCompletedEventArgs(string error)
            : this(new List<GeoImageryMapKey>(), error)
        {
        }
        public GetMapKeyCompletedEventArgs(List<GeoImageryMapKey> result, string error)
        {
            this.Result = result;
            this.Error = error;
        }

        /// <summary>
        /// List of GeoImageryMapKey
        /// </summary>
        public List<GeoImageryMapKey> Result { get; private set; }
        public string Error { get; private set; }

       
    }

}