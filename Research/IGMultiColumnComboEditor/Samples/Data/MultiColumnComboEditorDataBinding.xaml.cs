using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGMultiColumnComboEditor.Samples.Data
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class MultiColumnComboEditorDataBinding : SampleContainer
    {
        private string isoLangName;
        private XmlDataProvider xmlDataProvider;

        public MultiColumnComboEditorDataBinding()
        {
            InitializeComponent();
            isoLangName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }

        void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // create xml data provider that will load data from xml file
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            xmlDataProvider.GetXmlDataAsync("Customers.xml"); // xml file name
        }

        /// <summary>
        /// This method create a collection of the DataItems objects by parsing the XML content and set it as ItemsSource.
        /// </summary>
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  Company = d.Element("CompanyName").Value,
                                  ContactName = d.Element("ContactName").Value,
                                  ImageResourcePath = d.Element("ImageResourcePath").Value
                              });

            xamMultiColumnComboEditor.ItemsSource = dataSource.ToList();

            if (isoLangName.ToLower().Equals("ja"))
            {
                xamMultiColumnComboEditor.CustomValueEnteredAction = CustomValueEnteredActions.Allow;
            }
        }
    }
}
