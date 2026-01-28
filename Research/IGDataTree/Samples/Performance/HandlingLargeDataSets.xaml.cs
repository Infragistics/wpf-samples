using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models.General;

namespace IGDataTree.Samples.Performance
{
    public partial class HandlingLargeDataSets : SampleContainer
    {
        public HandlingLargeDataSets()
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
                var dataSource = (from d in Enumerable.Range(0, 5000)
                                  select new BookViewModel
                                  {
                                      Name = string.Format("{0:#}", d),
                                      Chapters = this.GetChapters()
                                  });

                this.MyTree.ItemsSource = dataSource.ToList();
            }
        }

        private System.Collections.Generic.IEnumerable<ChapterViewModel> GetChapters()
        {
            var result = (from d in Enumerable.Range(0, 5)
                          select new ChapterViewModel
                          {
                              Name = string.Format("{0:#}", d),
                          }).ToList<ChapterViewModel>();

            return result;
        }
    }
}