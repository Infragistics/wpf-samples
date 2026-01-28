using System;
using System.Windows;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Resources;
using System.Windows.Navigation;

namespace Infragistics.Samples.Framework.Navigation
{
    public class InfragisticsUriMapper : UriMapperBase
    {
        XElement elem = null;
        string _urlSearchPattern;

        public InfragisticsUriMapper(Uri localStorage, string urlSearchPattern)
        {
            _urlSearchPattern = urlSearchPattern;
            StreamResourceInfo xaml = Application.GetResourceStream(localStorage);
            string streamobject = new StreamReader(xaml.Stream).ReadToEnd();
            elem = XElement.Parse(streamobject);
        }

        public override Uri MapUri(Uri uri)
        {
            if (!uri.OriginalString.Contains("Home"))
            {
                string physicalPath = LoadUriMappingFile(uri.OriginalString);
                return new Uri(physicalPath, UriKind.Relative);
            }
            else
            {
                return uri;
            }
        }

        private string LoadUriMappingFile(string requestedUri)
        {
            var list = elem.Descendants("Url").ToList();

            var subList = list.Where(tt => tt != null && tt.Value.ToLower().Equals(_urlSearchPattern.ToLower() + requestedUri.ToLower().TrimStart('/')));

            if (subList.Count() == 0)
                return String.Empty;

            XElement sampleElement = (subList).FirstOrDefault().Parent.Parent.Parent;

            XElement componentElement = sampleElement.Parent.Parent.Parent.Parent;

            XElement productFamilyElement = componentElement.Parent.Parent;

            Guid sampleId = Guid.Empty;
            int componentId = -1;
            int productFamilyId = -1;

            if (Guid.TryParse(sampleElement.Attribute("SampleId").Value, out sampleId))
                ((InfragisticsApplication)Application.Current).CurrentSampleId = sampleId;

            if (int.TryParse(componentElement.Attribute("ComponentId").Value, out componentId))
                ((InfragisticsApplication)Application.Current).CurrentComponentId = componentId;

            if (int.TryParse(productFamilyElement.Attribute("ProductFamilyId").Value, out productFamilyId))
                ((InfragisticsApplication)Application.Current).CurrentProductFamilyId = productFamilyId;
            
            XElement felem = sampleElement.Descendants("FileReference").Where(tt => tt.Attribute("Name").Value.EndsWith("xaml")).FirstOrDefault();

            return felem.Attribute("Url").Value;
        }
    }
}
