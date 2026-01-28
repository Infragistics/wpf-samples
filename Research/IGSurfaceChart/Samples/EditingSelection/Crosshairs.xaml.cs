using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using System.Windows;
using System.Windows.Controls;

namespace IGSurfaceChart.Samples.EditingSelection
{
    /// <summary>
    /// Interaction logic for Crosshairs.xaml
    /// </summary>
    public partial class Crosshairs : SampleContainer
    {
        public Crosshairs()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }

        private void Chb_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;

            if (this.Chb_CrosshairX == null || this.Chb_CrosshairY == null || this.Chb_CrosshairZ == null || this.Chb_NonCrosshairs == null) return;

            this.SurfaceChart.CrosshairAxes = AxisFlags3D.None;

            if (this.Chb_CrosshairX.IsChecked == true)
            {
                if (this.Chb_NonCrosshairs.IsChecked == true && !checkbox.Name.Equals("Chb_NonCrosshairs"))
                    this.Chb_NonCrosshairs.IsChecked = false;

                this.SurfaceChart.CrosshairAxes |= AxisFlags3D.X;
            }

            if (this.Chb_CrosshairY.IsChecked == true)
            {
                if (this.Chb_NonCrosshairs.IsChecked == true && !checkbox.Name.Equals("Chb_NonCrosshairs"))
                    this.Chb_NonCrosshairs.IsChecked = false;

                this.SurfaceChart.CrosshairAxes |= AxisFlags3D.Y;
            }

            if (this.Chb_CrosshairZ.IsChecked == true)
            {
                if (this.Chb_NonCrosshairs.IsChecked == true && !checkbox.Name.Equals("Chb_NonCrosshairs"))
                    this.Chb_NonCrosshairs.IsChecked = false;

                this.SurfaceChart.CrosshairAxes |= AxisFlags3D.Z;
            }

            if (this.Chb_NonCrosshairs.IsChecked == true && checkbox.Name.Equals("Chb_NonCrosshairs"))
            {
                this.Chb_CrosshairX.IsChecked = Chb_CrosshairY.IsChecked = Chb_CrosshairZ.IsChecked = false;
                this.SurfaceChart.CrosshairAxes = AxisFlags3D.None;
                this.Chb_NonCrosshairs.IsChecked = true;
            }
        }
    }
}
