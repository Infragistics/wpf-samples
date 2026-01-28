using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGDataTree.Samples.Editing
{
    public partial class Editing : SampleContainer
    {
        public Editing()
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
            xmlDataProvider.GetXmlDataAsync("Books.xml"); // xml file name
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            if (doc != null)
            {
                var dataSource = (from d in doc.Descendants("book")
                                  where (d.Descendants("chapter") != null && d.Descendants("chapter").Count() > 0)
                                  select new Book
                                  {
                                      Author = d.Attribute("Author").Value,
                                      Title = d.Attribute("Title").Value,
                                      UnitPrice = d.Attribute("UnitPrice").GetDecimal(),
                                      Url = d.Attribute("Url").Value,
                                      ReleaseDate = d.Attribute("ReleaseDate").Value,
                                      Chapters = this.GetChapters(d)
                                  });

                this.MyTree.ItemsSource = dataSource.ToList();
            }
        }

        private IEnumerable<Chapter> GetChapters(XElement parent)
        {
            return (from d in parent.Descendants("chapter")
                    select new Chapter
                    {
                        Title = d.Attribute("Title").Value
                    }).ToList<Chapter>();
        }

        private void cbIsF2EditingEnabled_Click(object sender, RoutedEventArgs e)
        {
            MyTree.EditingSettings.IsF2EditingEnabled = (bool)this.cbIsF2EditingEnabled.IsChecked;
        }

        private void cbIsEnterKeyEditingEnabled_Click(object sender, RoutedEventArgs e)
        {
            MyTree.EditingSettings.IsEnterKeyEditingEnabled = (bool)this.cbIsEnterKeyEditingEnabled.IsChecked;
        }

        private void cbIsOnNodeActiveEditingEnabled_Click(object sender, RoutedEventArgs e)
        {
            MyTree.EditingSettings.IsOnNodeActiveEditingEnabled = (bool)this.cbIsOnNodeActiveEditingEnabled.IsChecked;
        }

        private void cbEnableEditing_Click(object sender, RoutedEventArgs e)
        {
            MyTree.EditingSettings.AllowEditing = (bool)this.cbEnableEditing.IsChecked;
        }

        private void cbEnableDeleting_Click(object sender, RoutedEventArgs e)
        {
            MyTree.EditingSettings.AllowDeletion = (bool)this.cbEnableDeleting.IsChecked;
        }
    }
}