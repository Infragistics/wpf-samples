using System;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IGSurfaceChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for FirstLookSample.xaml
    /// </summary>
    public partial class FirstLookSample : SampleContainer
    {
        public FirstLookSample()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
         

        private void Chb_Crosshairs_Checked(object sender, RoutedEventArgs e)
        {
            if (this.Chb_Crosshairs.IsChecked == true)
            {
                this.SurfaceChart.CrosshairAxes = AxisFlags3D.XYZ;
                this.SurfaceChart.CrosshairThickness = 2;
                this.SurfaceChart.CrosshairBrush = this.Resources["CrosshairsBrush"] as SolidColorBrush;
            }
            else
            {
                this.SurfaceChart.CrosshairAxes = AxisFlags3D.None;
            }
        }
    }
}
