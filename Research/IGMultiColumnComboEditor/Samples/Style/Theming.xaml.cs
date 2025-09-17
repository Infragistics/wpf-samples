using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Themes;

namespace IGMultiColumnComboEditor.Samples.Style
{
    /// <summary>
    /// Interaction logic for Theming.xaml
    /// </summary>
    public partial class MultiColumnComboEditorTheming : SampleContainer
    {
        private string _isoLangName;
        private XmlDataProvider xmlDataProvider;

        public MultiColumnComboEditorTheming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent();
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _isoLangName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
 
            DownloadDataSource();
        }
        
        private void OnSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme  
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }


        private void DownloadDataSource()
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
            if (e.Error != null)
            {
                return;
            }

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  Company = d.Element("CompanyName").Value,
                                  ContactName = d.Element("ContactName").Value,
                                  ImageResourcePath = d.Element("ImageResourcePath").Value
                              });

            xamMultiColumnComboEditor.ItemsSource = dataSource.ToList();

            if (_isoLangName.ToLower().Equals("ja"))
            {
                xamMultiColumnComboEditor.CustomValueEnteredAction = CustomValueEnteredActions.Allow;
            }
        }
    }
}
