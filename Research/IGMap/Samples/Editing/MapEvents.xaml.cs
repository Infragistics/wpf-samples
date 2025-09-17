using System;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Maps;

namespace IGMap.Samples.Editing
{
    public partial class MapEvents : Infragistics.Samples.Framework.SampleContainer
    {
        public MapEvents()
        {
            InitializeComponent();
        }
     
        private void MapLayer_Imported(object sender, MapLayerImportEventArgs e)
        {
            DisplayOutput("MapLayer.ImportProgress: " + e.Action + "  " + e.Progress);
        }

        private void statesLayer_ElementPrerender(object sender, MapElementRenderEventArgs e)
        {
            DisplayOutput("MapLayer.ElementPrerender: " + e.Element.Name);
        }

        private void statesLayer_ElementDerender(object sender, MapElementRenderEventArgs e)
        {
            DisplayOutput("MapLayer.ElementDerender: " + e.Element.Name);
        }

        private void statesLayer_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayOutput("MapLayer.Loaded");
        }

        private void theMap_ActualWorldRectChanged(object sender, MapWorldRectChangedEventArgs e)
        {
            // This event fires before any visual elements have been rendered.
            DisplayOutput("ActualWorldRectChanged: " + e.NewWorldRect);
        }

        private void theMap_DragRectChanged(object sender, MapRectChangedEventArgs e)
        {
            DisplayOutput("DragRectChanged: " + e.Rect);
        }

        private void theMap_LayoutUpdated(object sender, EventArgs e)
        {
            DisplayOutput("LayoutUpdated");
        }

        private void theMap_SelectionChanged(object sender, MapSelectionChangedEventArgs e)
        {
            DisplayOutput("SelectionChanged");
        }

        private void theMap_WindowRectChanged(object sender, MapWindowRectChangedEventArgs e)
        {
            DisplayOutput("WindowRectChanged: " + e.WindowRect);
        }

        private void theMap_ZoomRectChanged(object sender, MapRectChangedEventArgs e)
        {
            DisplayOutput("ZoomRectChanged: " + e.Rect);
        }

        private void theMap_ElementClick(object sender, MapElementClickEventArgs e)
        {
            DisplayOutput("ElementClick: " + e.Element.Name);
        }

        private void theMap_ElementHover(object sender, MapElementHoverEventArgs e)
        {
            DisplayOutput("ElementHover: " + e.Element.Name);
        }

        private void theMap_MapMouseMove(object sender, MapMouseEventArgs e)
        {
            DisplayOutput("MapMouseMove: " + e.Position);
        }

        private void theMap_MapMouseLeftButtonDown(object sender, MapMouseButtonEventArgs e)
        {
            DisplayOutput("MapMouseLeftButtonDown");
        }

        private void theMap_MapMouseLeftButtonUp(object sender, MapMouseButtonEventArgs e)
        {
            DisplayOutput("MapMouseLeftButtonUp");
        }

        private void DisplayOutput(string content)
        {
            if (evtOutput != null)
            {
                evtOutput.Text += content + Environment.NewLine;
                svOutput.UpdateLayout();
                svOutput.ScrollToVerticalOffset(double.MaxValue);
            }
        }

        private void EraseOutputWindow()
        {
            evtOutput.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EraseOutputWindow();
        }
    }
}