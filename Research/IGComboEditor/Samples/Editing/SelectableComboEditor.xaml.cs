using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGComboEditor.Samples.Editing
{
    public partial class SelectableComboEditor : SampleContainer
    {
        public SelectableComboEditor()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("Books.xml");
        }

        private void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            var dataSource = (from d in doc.Descendants("book")
                              select new Book
                              {
                                  Author = d.Attribute("Author").Value,
                                  Title = d.Attribute("Title").Value
                              });

            LayoutRoot.DataContext = dataSource.ToList();

            // DisplayMemberPath specifies path to data property that will be displayed 
            multiselectCombo.DisplayMemberPath = "Author";
            multiselectComboEditable.DisplayMemberPath = "Author";
            multiChbCombo.DisplayMemberPath = "Title";
        }
    }
}