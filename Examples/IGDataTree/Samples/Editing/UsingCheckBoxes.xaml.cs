using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGDataTree.Samples.Editing
{
    public partial class UsingCheckBoxes : SampleContainer
    {
        public UsingCheckBoxes()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DownloadDataSource();

            lbCheckBoxMode.ValueChanged += lbCheckBoxMode_ValueChanged;
            cbEnableCheckBox.ValueChanged += cbEnableCheckBox_ValueChanged;
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

        private void lbCheckBoxMode_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            EnableCheckBoxTriState.IsEnabled = MyTree.CheckBoxSettings.CheckBoxMode == TreeCheckBoxMode.Manual;
        }

        void cbEnableCheckBox_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.lbCheckBoxMode.IsEnabled = MyTree.CheckBoxSettings.CheckBoxVisibility == Visibility.Visible;

            if (lbCheckBoxMode.Value == null) return;

            if (!lbCheckBoxMode.IsEnabled)
                EnableCheckBoxTriState.IsEnabled = false;
            else if ((TreeCheckBoxMode)lbCheckBoxMode.Value == TreeCheckBoxMode.Manual)
                EnableCheckBoxTriState.IsEnabled = true;
            else 
                EnableCheckBoxTriState.IsEnabled = false;
        }
    }
}