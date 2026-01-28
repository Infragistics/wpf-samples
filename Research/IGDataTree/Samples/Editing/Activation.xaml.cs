using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGDataTree.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models.General;

namespace IGDataTree.Samples.Editing
{
    public partial class Activation : SampleContainer
    {
        public Activation()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DownloadDataSource();
        }

        private XmlDataProvider xmlDataProvider;
        /// <summary>
        /// This method is used for downloading the sample's data source. To support localization we created a custom helper class. 
        /// </summary>
        void DownloadDataSource()
        {
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("TreeEmployees.xml"); // xml file name
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            if (doc != null)
            {
                var result = this.GetEmployees(doc.Root);
                this.MyTree.ItemsSource = result;
            }
        }

        private IList<TreeEmployee> GetEmployees(XElement parent)
        {
            return (from em in parent.Elements("Employees")
                    select new TreeEmployee
                    {
                        Id = em.Element("EmpID").Value,
                        Name = em.Element("EmpName").Value,
                        JobTitle = em.Element("JobTitle").GetString(),
                        EmployeeSince = em.Element("EmployeeSince").Value,
                        ManagerName = parent.Element("EmpName").GetString(),
                        DirectReports = this.GetEmployees(em)
                    }).ToList<TreeEmployee>();
        }

        private void MyTree_ActiveNodeChanged(object sender, Infragistics.Controls.Menus.ActiveNodeChangedEventArgs e)
        {
            if (e.NewActiveTreeNode != null)
            {
                TreeEmployee myemployee = e.NewActiveTreeNode.Data as TreeEmployee;
                this.ActiveNodeValue.Text = myemployee.Name.ToString();
            }
            else
            {
                ActiveNodeValue.Text = DataTreeStrings.XDT_None;
            }
        }
    }
}