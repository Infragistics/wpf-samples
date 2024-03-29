using System.IO;
using System.Xml;

namespace System.Windows.Markup
{
    public static class XamlReaderEx
    {
        public static object Load(string xaml)
        {
            var strReader = new StringReader(xaml);
            var xmlReader = XmlReader.Create(strReader);
            return XamlReader.Load(xmlReader); 
        }
    }
}