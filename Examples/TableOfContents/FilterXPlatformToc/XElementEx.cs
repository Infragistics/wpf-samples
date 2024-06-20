
namespace System.Xml.Linq
{
    public static class Element
    {
        public static string ElementValue(this XElement current, string name)
        {
            var element = current.Element(name);
            if (element != null)
                return element.Value;
            
            LOG.Warn("XML under " + current.FirstNode + " is missing in '" + name + "' element");
            return "";
        }
        public static string AttributeValue(this XElement current, string name)
        {
            var att = current.Attribute(name);
            if (att != null)
                return att.Value;
            
            LOG.Warn("XML " + current.FirstNode + " is missing in '" + name + "' attribute");
            return "";
        }
    }
}
