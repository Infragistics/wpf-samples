using System;
using System.Windows;
using IGTreemap.Resources;
using Infragistics.Controls.Charts;

namespace IGTreemap.Samples
{
    public partial class TreemapEvents : Infragistics.Samples.Framework.SampleContainer
    {
        public TreemapEvents()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
        }

        #region Treemap Events
        private void Treemap_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateOutput(TreemapStrings.XWT_TreemapLoaded);
        }

        private void Treemap_NodeMouseLeftButtonDown(object sender, TreemapNodeClickEventArgs e)
        {
            UpdateOutput("NodeMouseLeftButtonDown: " + e.Node.Text);
        }

        private void Treemap_NodeMouseLeftButtonUp(object sender, TreemapNodeClickEventArgs e)
        {
            UpdateOutput("NodeMouseLeftButtonUp: " + e.Node.Text);
        }

        private void Treemap_NodeMouseRightButtonUp(object sender, TreemapNodeClickEventArgs e)
        {
            UpdateOutput("NodeMouseRightButtonUp: " + e.Node.Text);
        }

        private void Treemap_NodeMouseRightButtonDown(object sender, TreemapNodeClickEventArgs e)
        {
            UpdateOutput("NodeMouseRightButtonDown: " + e.Node.Text);
        }

        private void Treemap_NodeMouseMove(object sender, TreemapNodeMouseEventArgs e)
        {
            UpdateOutput("NodeMouseMove: " + e.Node.Text);
        }

        private void Treemap_NodeMouseWheel(object sender, TreemapNodeMouseWheelEventArgs e)
        {
            UpdateOutput(string.Format("NodeMouseWheel (Delta {0}): {1}", e.MouseEventArgs.Delta, e.Node.Text));
        }

        private void Treemap_LayoutTypeChanged(object sender, LayoutTypeChangedEventArgs e)
        {
            UpdateOutput("LayoutTypeChanged: " + e.NewLayoutType);
        }

        private void Treemap_LayoutOrientationChanged(object sender, LayoutOrientationChangedEventArgs e)
        {
            UpdateOutput("LayoutOrientationChanged: " + e.NewLayoutOrientation);
        }
        #endregion

        #region AuxiliaryMethods
        private void UpdateOutput(string text)
        {
            TextBlockOutput.Text += text + Environment.NewLine;
            ScrollViewerOutput.UpdateLayout();
            ScrollViewerOutput.ScrollToVerticalOffset(Double.MaxValue);
        }

        private void ClearOutput()
        {
            TextBlockOutput.Text = string.Empty;
        }
        #endregion
    }
}
