using System;
using System.Windows;
using IGGeographicMap.Models;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

// FormatRect() method

namespace IGGeographicMap.Samples
{
    public partial class MapEvents : Infragistics.Samples.Framework.SampleContainer
    {
        public MapEvents()
        {
            InitializeComponent();
        }

        #region Event Handlers - ShapefileConverter Events
        private void ShapefileConverter_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            DisplayOutput("ShapeConverter.CollectionChanged {" + e.Action + " " + e.NewItems.Count + "}");
        }

        private void ShapefileConverter_ImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var converter = (ShapefileConverter)sender;
            DisplayOutput("ShapeConverter.ImportCompleted  {" + converter.Count + " }");

            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        }

        private void ShapefileConverter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DisplayOutput("ShapeConverter.PropertyChanged: " + e.PropertyName);
        }
        #endregion

        #region Event Handlers - GeoMap Mouse Events

        private void GeoMap_SeriesMouseLeftButtonDown(object sender, Infragistics.Controls.Charts.DataChartMouseButtonEventArgs e)
        {
            var item = (ShapefileRecord)e.Item;
            if (item == null) return;
            DisplayOutput("GeoSeries.MouseLeftButtonDown: " + item.Fields["NAME"]);
        }

        private void GeoMap_SeriesMouseLeftButtonUp(object sender, Infragistics.Controls.Charts.DataChartMouseButtonEventArgs e)
        {
            DisplayOutput("GeoSeries.MouseLeftButtonUp");
        }
        private void GeoMap_SeriesMouseRightButtonDown(object sender, Infragistics.Controls.Charts.DataChartMouseButtonEventArgs e)
        {
            DisplayOutput("GeoSeries.MouseRightButtonDown");
            e.Handled = true;
        }
        private void GeoMap_SeriesMouseRightButtonUp(object sender, Infragistics.Controls.Charts.DataChartMouseButtonEventArgs e)
        {
            DisplayOutput("GeoSeries.MouseRightButtonUp");
            e.Handled = true;
        }

        private void GeoMap_SeriesMouseEnter(object sender, Infragistics.Controls.Charts.ChartMouseEventArgs e)
        {
            DisplayOutput("GeoSeries.MouseEnter");
        }
        private void GeoMap_SeriesMouseLeave(object sender, Infragistics.Controls.Charts.ChartMouseEventArgs e)
        {
            DisplayOutput("GeoSeries.MouseLeave");
        }
        private void GeoMap_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DisplayOutput("GeoMap.MouseEnter");
        }
        private void GeoMap_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DisplayOutput("GeoMap.MouseLeave");
        }

        private string _lastHoverElement;
        private void GeoMap_SeriesMouseMove(object sender, Infragistics.Controls.Charts.ChartMouseEventArgs e)
        {
            var item = (ShapefileRecord)e.Item;
            if (item == null) return;
            string elementName = item.Fields["NAME"].ToString();
            if (elementName != _lastHoverElement)
            {
                _lastHoverElement = elementName;
                DisplayOutput("GeoMap.MouseMove: " + elementName);
            }
          }
      
        #endregion

        #region Event Handlers - GeoMap Core Events
        private void GeoMap_WindowRectChanged(object sender, Infragistics.RectChangedEventArgs e)
        {
            if (e.NewRect.IsEmpty) return;
            DisplayOutput("GeoMap.WindowRectChanged " + e.NewRect.FormatRect());
        }
        private void GeoMap_GridAreaRectChanged(object sender, Infragistics.RectChangedEventArgs e)
        {
            if (e.NewRect.IsEmpty) return;
            DisplayOutput("GeoMap.GridAreaRectChanged " + e.NewRect.FormatRect());
        }
        private void GeoMap_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayOutput("GeoMap.Loaded");
        }
   
        #endregion

        #region Methods
    

        private void DisplayOutput(string content)
        {
            if (this.EventsHistoryTextBlock != null && base.IsSampleLoaded)
            {
                this.EventsHistoryTextBlock.Text += content + Environment.NewLine;
                this.EventsHistoryTextBlock.UpdateLayout();
                this.EventsHistoryViewer.ScrollToVerticalOffset(double.MaxValue);
            }
        }
        private void ClearOutput()
        {
            this.EventsHistoryTextBlock.Text = string.Empty;
        }

        #endregion

        private void ClearEventsHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
        }


      
      
    }
}
