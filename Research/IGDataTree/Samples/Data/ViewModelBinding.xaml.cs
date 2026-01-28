using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGDataTree.Samples.Data
{
    public partial class ViewModelBinding : SampleContainer
    {
        public ViewModelBinding()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void dataTree_Loaded(object sender, RoutedEventArgs e)
        {
            Infragistics.Controls.Menus.XamDataTree tree = (Infragistics.Controls.Menus.XamDataTree)sender;

            if (tree.Nodes.Count() > 0)
            {
                tree.Nodes[0].IsExpanded = true;
            }
        }

        private BookDataSource viewModel;
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = new BookDataSource();
            this.DataContext = this.viewModel;

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

                this.viewModel.Books = dataSource.ToList();
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

        public class BookDataSource : ObservableModel
        {
            public BookDataSource()
            {
            }

            private IEnumerable<Book> books;
            public IEnumerable<Book> Books
            {
                get
                {
                    return this.books;
                }
                set
                {
                    if (this.books != value)
                    {
                        this.books = value;
                        this.OnPropertyChanged("Books");
                    }
                }
            }
        }
    }
}