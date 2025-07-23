using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGOutlookBar.Samples.Data
{
    public partial class OutlookBarDataBindingViewModel : SampleContainer
    {
        private readonly XmlDataProvider dataProvider = new XmlDataProvider();

        public OutlookBarDataBindingViewModel()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("Books.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("book")
                              where (d.Descendants("chapter") != null && d.Descendants("chapter").Count() > 0)
                              select new Book
                              {
                                  Author = d.Attribute("Author").GetString(),
                                  Title = d.Attribute("Title").GetString(),
                                  UnitPrice = d.Attribute("UnitPrice").GetDecimal(),
                                  Url = d.Attribute("Url").GetString(),
                                  ReleaseDate = d.Attribute("ReleaseDate").GetString(),
                                  Chapters = this.GetChapters(d)
                              });

            this.XamOutlookBar.GroupsSource = dataSource.ToList();
            if (this.XamOutlookBar.Groups.Count > 0)
            {
                this.XamOutlookBar.Groups[0].IsSelected = true;
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

    }
}
