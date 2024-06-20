using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace IGMultiColumnComboEditor.Samples.Display
{
    /// <summary>
    /// Interaction logic for DropDownButtonDisplayModeProperty.xaml
    /// </summary>
    public partial class DropDownButtonDisplayModeProperty : SampleContainer
    {
        public DropDownButtonDisplayModeProperty()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DownloadDataSource();
            comboDropDownButtonDisplayMode.ItemsSource = Enum.GetValues(typeof(DropDownButtonDisplayMode));            
        }

        private void DownloadDataSource()
        {
            var xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("Customers.xml");
        }

        private void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            XDocument doc = e.Result;

            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  ContactName = d.Element("ContactName").Value,
                                  City = d.Element("City").Value,
                                  Country = d.Element("Country").Value,
                                  ImageResourcePath = ImageFilePathProvider.GetImageLocalPath("People/" + d.Element("ImageResourcePath").Value)
                              }).ToList();

            ComboEditor.ItemsSource = dataSource;

            ComboEditor.DisplayMemberPath = "ContactName";
            ComboEditor.SelectedIndex = 0;
        }
    }
}
