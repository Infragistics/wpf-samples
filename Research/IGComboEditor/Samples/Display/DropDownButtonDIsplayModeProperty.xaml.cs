using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using Infragistics.Controls.Editors;

namespace IGComboEditor.Samples.Display
{
    public partial class DropDownButtonDIsplayModeProperty : SampleContainer
    {
        public DropDownButtonDIsplayModeProperty()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();

            this.comboDropDownButtonDisplayMode.ItemsSource = Enum.GetValues(typeof(DropDownButtonDisplayMode));

            JPCustomValueEnteredActionCheck();
        }

        private void DownloadDataSource()
        {
            var xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("Customers.xml");
        }

        private void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            if (gxdcea.Error != null)
            {
                return;
            }

            XDocument doc = gxdcea.Result;

            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  ContactName = d.Element("ContactName").Value,
                                  City = d.Element("City").Value,
                                  ImageResourcePath = ImageFilePathProvider.GetImageLocalPath("People/" + d.Element("ImageResourcePath").Value)
                              }).ToList();

            ComboEditor.ItemsSource = dataSource;
            ComboEditor.DisplayMemberPath = "ContactName";
            ComboEditor.SelectedIndex = 0;
        }

        private void JPCustomValueEnteredActionCheck()
        {
            string isoLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            // customization
            if (isoLanguage.ToLower().Equals("ja"))
            {
                ComboEditor.CustomValueEnteredAction = CustomValueEnteredActions.Allow;
            }
        }
    }
}
