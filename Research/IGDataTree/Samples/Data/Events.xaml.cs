using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGDataTree.Resources;
using Infragistics.DragDrop;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models.General;

namespace IGDataTree.Samples.Data
{
    public partial class Events : SampleContainer
    {
        public Events()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
            optPanel.Focus();
        }

        private XmlDataProvider xmlDataProvider;

        /// <summary>
        /// This method is used for downloading the sample's data source. To support localization we created a custom helper class. 
        /// </summary>
        void DownloadDataSource()
        {
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("Contacts.xml"); // xml file name
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;
            if (doc != null)
            {
                var dataSource = (from d in doc.Descendants("Contacts")
                                  select new Contact
                                  {
                                      Name = d.Element("Name").Value,
                                      ContactDetails = this.GetContactDetails(d)
                                  });

                this.MyTree.ItemsSource = dataSource.ToList();
            }
        }

        private IEnumerable<ContactDetail> GetContactDetails(XElement parent)
        {
            return (from d in parent.Descendants("ContactDetails")
                    select new ContactDetail
                    {
                        Name = d.Element("Name").Value,
                    }).ToList<ContactDetail>();
        }


        private void BtnClearLog_Click(object sender, RoutedEventArgs e)
        {
            txtBox.Text = String.Empty;
        }

        private static string PrintLog(string msg)
        {
            return "[" + DateTime.Now.ToString("hh:mm") + "] " + msg + "\n";
        }

        private void MyTree_ActiveNodeChanged(object sender, Infragistics.Controls.Menus.ActiveNodeChangedEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_ActiveNodeChanged);
        }

        private void MyTree_DragEnter(object sender, DragEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_DragEnter);
        }

        private void MyTree_DragLeave(object sender, DragEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_DragLeave);
        }

        private void MyTree_DragOver(object sender, DragEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_DragOver);
        }

        private void MyTree_Drop(object sender, DragEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_Drop);
        }

        private void MyTree_NodeCheckedChanged(object sender, Infragistics.Controls.Menus.NodeEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_NodeCheckedChanged);
        }

        private void MyTree_NodeDragCancel(object sender, DragDropEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_NodeDragCancel);
        }

        private void MyTree_NodeDragEnd(object sender, DragDropEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_NodeDragEnd);
        }

        private void MyTree_NodeDraggingStart(object sender, DragDropStartEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_NodeDraggingStart);
        }

        private void MyTree_NodeEnteredEditMode(object sender, Infragistics.Controls.Menus.TreeEditingNodeEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_NodeEnteredEditMode);
        }

        private void MyTree_NodeEnteringEditMode(object sender, Infragistics.Controls.Menus.BeginEditingNodeEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_NodeEnteringEditMode);
        }

        private void MyTree_NodeExitedEditMode(object sender, Infragistics.Controls.Menus.NodeEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_NodeExitedEditMode);
        }

        private void MyTree_NodeExitingEditMode(object sender, Infragistics.Controls.Menus.TreeExitEditingEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_NodeExitingEditMode);
        }

        private void MyTree_NodeExpansionChanged(object sender, Infragistics.Controls.Menus.NodeExpansionChangedEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_NodeExpansionChanged);
        }

        private void MyTree_NodeExpansionChanging(object sender, Infragistics.Controls.Menus.CancellableNodeExpansionChangedEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_NodeExpansionChanging);
        }

        private void MyTree_NodeLayoutAssigned(object sender, Infragistics.Controls.Menus.NodeLayoutAssignedEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_NodeLayoutAssigned);
        }

        private void MyTree_NodeDragDrop(object sender, Infragistics.Controls.Menus.TreeDropEventArgs e)
        {
            txtBox.Text += PrintLog(DataTreeStrings.XDT_NodeDragDrop);
        }

    }
}
