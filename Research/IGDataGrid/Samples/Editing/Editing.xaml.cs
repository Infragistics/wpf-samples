using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using System.Data;
using System.IO;

namespace IGDataGrid.Samples.Editing
{
    public partial class Editing : SampleContainer
    {
        private XmlDataProvider xmlDataProvider;

        public Editing()
        {
            InitializeComponent();
            DownloadDataSource();
        }

        /// <summary>
        /// This method is used for downloading the sample's data source. To support localization we created a custom helper class LocalizationHelper. 
        /// To use the sample code outside of the sample browser remove LocalizationHelper.GetLocalizeDataSource and create a URI directly.
        /// </summary>
        private void DownloadDataSource()
        {
            // create xml data provider that will load data from xml file
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            xmlDataProvider.GetXmlDataAsync("Movies.xml"); // xml file name
        }

        /// <summary>
        /// This method creates a collection of the DataItems objects by parsing the XML content and set it as ItemsSource.
        /// </summary>
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            DataSet ds = new DataSet();

            string fileContent = e.Result.ToString();
            using (TextReader reader = new StringReader(fileContent))
            {
                ds.ReadXml(reader);
                this.xamDataGrid1.DataSource = ds.Tables[0].DefaultView;
            }
        }
    }
}
