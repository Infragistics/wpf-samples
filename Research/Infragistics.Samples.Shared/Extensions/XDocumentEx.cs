using System.Xml;
using System.Xml.Linq;

namespace Infragistics.Samples.Shared.Extensions
{
    public static class XDocumentEx
    {
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            return XDocument.Parse(xmlDocument.OuterXml);
        }

        //public static XDocument ToXDocumentNavigator(this XmlDocument doc)
        //{
        //    return XDocument.Load(doc.CreateNavigator().ReadSubtree());
        //}

        //public static XDocument ToXDocumentReader(this XmlDocument doc)
        //{
        //    return XDocument.Load(new XmlNodeReader(doc));
        //}
    }
}