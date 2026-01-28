using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using IGMenu.Models;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Microsoft.Win32;

namespace IGMenu.Samples.Editing
{
    public partial class Events : SampleContainer
    {


        public Events()
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
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("FileMenu.xml");
        }

        /// <summary>
        /// Ths methods' XLinq Query uses special extention methods for converting data. 
        /// The extention methods can be found in the Infragistics.Samples.Shared.Extensions\XElementExtension class. 
        /// </summary>
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = GetItems(doc.Root);

            this.MainMenu.ItemsSource = dataSource;
        }

        private DataItemCollection GetItems(XElement parent)
        {
            return new DataItemCollection((from d in parent.Elements("Menu")
                                           select new DataItem
                                           {
                                               Id = d.Attribute("ID").Value,
                                               Name = d.Attribute("Value").GetString(),
                                               Children = this.GetItems(d)
                                           }).ToList<DataItem>());
        }

        private void MainMenu_ItemClicked(object sender, ItemClickedEventArgs e)
        {
            DataItem selectedmenuItem = e.Item.Header as DataItem;

            switch (selectedmenuItem.Id)
            {
                case "Open":
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "Txt Files|*.txt";
                    e.Item.IsSubmenuOpen = false;
                    e.Item.ParentXamMenuItem.IsSubmenuOpen = false;
                    bool? showDialog = dialog.ShowDialog();
                    break;
                case "Save":
                    SaveFileDialog saveDialog = new SaveFileDialog { Filter = "Document files|*.doc", DefaultExt = "doc" };
                    e.Item.IsSubmenuOpen = false;
                    e.Item.ParentXamMenuItem.IsSubmenuOpen = false;
                    bool? showDialogResult = saveDialog.ShowDialog();
                    break;
                default:
                    // This sample only focuses on showing dialogs. A typical application would have additonal features.
                    break;
            }
        }
    }
}
