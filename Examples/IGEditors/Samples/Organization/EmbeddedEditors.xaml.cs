using IGEditors.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Editors;
using System;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;

namespace IGEditors.Samples.Organization
{
    /// <summary>
    /// Interaction logic for EmbeddedEditors.xaml
    /// </summary>
    public partial class EmbeddedEditors : SampleContainer
    {
        public EmbeddedEditors()
        {
            InitializeComponent();

            ComboBoxItemsProvider packagingProvider = this.xamDG.TryFindResource("PackagingItemsProvider") as ComboBoxItemsProvider;
            if (packagingProvider != null)
            {
                packagingProvider.ItemsSource = new ComboBoxDataItem[]
                {
                    new ComboBoxDataItem(Packaging.Box, EditorsStringRes.Embedded_Box),
                    new ComboBoxDataItem(Packaging.Bottle, EditorsStringRes.Embedded_Bottle),
                    new ComboBoxDataItem(Packaging.Can, EditorsStringRes.Embedded_Can),
                    new ComboBoxDataItem(Packaging.Other, EditorsStringRes.Embedded_Other)
                };
            }
            var prioritiesXml = this.xamDG.TryFindResource("PrioritiesXml") as XmlDataProvider;

            if (prioritiesXml != null)
            {
                var priorities = new XElement("priorities",
                    new XElement("priority",
                        new XAttribute("text", EditorsStringRes.Embedded_High),
                        new XAttribute("id",0)),
                    new XElement("priority",
                        new XAttribute("text", EditorsStringRes.Embedded_Normal),
                        new XAttribute("id",2)),
                    new XElement("priority",
                        new XAttribute("text", EditorsStringRes.Embedded_Low),
                        new XAttribute("id",1))
                    );

                var xmlDocument = new XmlDocument();
                using (var xmlReader = priorities.CreateReader())
                {
                    xmlDocument.Load(xmlReader); 
                }
                prioritiesXml.Document = xmlDocument;
            }

            base.DataContext = new ProductDeliveryTask[]
            {
                new ProductDeliveryTask
                {
                    Name=EditorsStringRes.Embedded_MineralWater,
                    Package = Packaging.Bottle,
                    PriorityLevel = 0,
                    Price = 10,
                    DateOrdered = DateTime.Now.AddDays(-4),
                    IsActive=true
                },
                new ProductDeliveryTask
                {
                    Name=EditorsStringRes.Embedded_BlueCheese,
                    Package = Packaging.Other,
                    PriorityLevel = 2,
                    Price = 22,
                    DateOrdered = DateTime.Now.AddDays(-2),
                    IsActive=true
                },
                new ProductDeliveryTask
                {
                    Name=EditorsStringRes.Embedded_CherryTomatoes,
                    Package = Packaging.Box,
                    PriorityLevel = 2,
                    Price = 34,
                    IsActive=true
                },
                new ProductDeliveryTask
                {
                    Name=EditorsStringRes.Embedded_LightBeer,
                    Package = Packaging.Can,
                    PriorityLevel = 1,
                    Price = 7,
                    DateOrdered = DateTime.Now,
                    IsActive=false
                }
            };
        }

        public class ProductDeliveryTask
        {
            public string Name { get; set; }
            public int PriorityLevel { get; set; }
            public Packaging Package { get; set; }
            public decimal Price { get; set; }
            public DateTime? DateOrdered { get; set; }
            public bool IsActive { get; set; }
        }

        public enum Packaging
        {
            Box,
            Bottle,
            Can,
            Other
        }
    }
}
