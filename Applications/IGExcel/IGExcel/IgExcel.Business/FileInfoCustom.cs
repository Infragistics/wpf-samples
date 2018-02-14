using System;
using System.IO;
using System.Xml.Serialization;

namespace IgExcel.Business
{
    [Serializable()]
    public class SystemInfoCustom
    {
        public string FullName { get; set; }
        public DateTime DateOpened;


        [XmlIgnoreAttribute]
        public string Name
        {
            get { return Path.GetFileName(FullName); }
        }


        public SystemInfoCustom()
        {

        }

        public SystemInfoCustom(string filePath)
        {
            FullName = filePath;
            DateOpened = DateTime.Now;
        }
    }
}
