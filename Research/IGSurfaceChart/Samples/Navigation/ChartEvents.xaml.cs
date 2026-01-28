using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using System;
using System.Windows;
using System.Windows.Controls;

namespace IGSurfaceChart.Samples.Navigation
{
    /// <summary>
    /// Interaction logic for ChartEvents.xaml
    /// </summary>
    public partial class ChartEvents : SampleContainer
    {
        public ChartEvents()
        {
            InitializeComponent();
            this.SynchronizeScale(this.SurfaceChart);
            
        }

        

        private void SurfaceChart_SeriesMouseDown(object sender, Chart3DMouseButtonEventArgs e)
        {
          
            if (SeriesMouseDownCheckBox.IsChecked != true)
                return;
            
            PrintLog(string.Format("SeriesMouseDown X: {0:N2} Y: {1:N2} Z: {2:N2}", e.X, e.Y, e.Value));
        }

        private void SurfaceChart_SeriesMouseMove(object sender, Chart3DMouseEventArgs e)
        {
            if (SeriesMouseMoveCheckBox.IsChecked != true)
                return;
            
            PrintLog(string.Format("SeriesMouseMove X: {0:N2} Y: {1:N2} Z {2:N2}", e.X, e.Y, e.Value));
        }

        private void SurfaceChart_SeriesMouseUp(object sender, Chart3DMouseButtonEventArgs e)
        {
            if (SeriesMouseUpCheckBox.IsChecked != true)
                return;
            
            PrintLog(string.Format("SeriesMouseUp X: {0:N2} Y: {1:N2} Z {2:N2}", e.X, e.Y, e.Value));
        }

        private void SurfaceChart_PointMarkerMouseDown(object sender, Chart3DMouseButtonEventArgs e)
        {
            if (PointMarkerMouseDownCheckBox.IsChecked != true)
                return;
            
            PrintLog(string.Format("PointMarkerMouseDown X: {0:N2} Y: {1:N2} Z {2:N2}", e.X, e.Y, e.Value));
        }

        private void SurfaceChart_PointMarkerMouseMove(object sender, Chart3DMouseEventArgs e)
        {
            if (PointMarkerMouseMoveCheckBox.IsChecked != true)
                return;
            
            PrintLog(string.Format("PointMarkerMouseMove X: {0:N2} Y: {1:N2} Z {2:N2}", e.X, e.Y, e.Value));
        }

        private void SurfaceChart_PointMarkerMouseUp(object sender, Chart3DMouseButtonEventArgs e)
        {
            if (PointMarkerMouseUpCheckBox.IsChecked != true)
                return;
            
            PrintLog(string.Format("PointMarkerMouseUp X: {0:N2} Y: {1:N2} Z {2:N2}", e.X, e.Y, e.Value));
        }

        private void PrintLog(string msg)
        {
            var logMsg = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + msg + "\n";

            var lstBoxItem = new ListBoxItem
            {
                Content = logMsg,
                FontSize = 10,
                Height = 20,
                FontWeight = FontWeights.Bold
            };

            OutputListBox.Items.Insert(0, lstBoxItem);
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            if (OutputListBox.Items.Count > 0)
            {
                OutputListBox.Items.Clear();
            }
        }

        private void OnShowPointMarkers(object sender, RoutedEventArgs e)
        {
            SurfaceChart.ShowPointMarkers = true;
        }
    }
}
