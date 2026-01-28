using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Resources;
using IGNetworkNode.Custom;
using Infragistics.Samples.Shared.DataProviders;

namespace IGNetworkNode.Samples.Data
{
    public partial class GmlReader : Infragistics.Samples.Framework.SampleContainer
    {
        public GmlReader()
        {
            InitializeComponent();
            this.SampleLoaded += new EventHandler(GmlReader_SampleLoaded);
        }

        void GmlReader_SampleLoaded(object sender, EventArgs e)
        {
            var items = new List<int> { 1,2,3,4 };

            this.graphsComboBox.ItemsSource = items;
            this.graphsComboBox.SelectedIndex = 1;
        }

        protected GmlDataProvider GmlDataProvider;
        private void graphsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
            string gmlFileName = String.Format("Graph{0}.gml", e.AddedItems[0]);

            // create data provider for reading data from a GML file
            this.GmlDataProvider = new GmlDataProvider();
            this.GmlDataProvider.GetGmlDataCompleted += OnGetGmlDataCompleted;
            this.GmlDataProvider.GetGmlDataAsync(gmlFileName);

        }

        void OnGetGmlDataCompleted(object sender, GetGmlDataCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != null)
            {
                // read and parse GML file content
                var stringReader = new StringReader(e.Result);
                var nodes = new GmlParser().Parse(stringReader);
                // bind the Network Node control to the GML nodes 
                xnn.ItemsSource = nodes;
            }
        }
       

        private void Redraw_Button_Click(object sender, RoutedEventArgs e)
        {
            xnn.UpdateNodeArrangement();
        }
    }
}
