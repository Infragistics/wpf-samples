using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using System.Windows;
using System.Windows.Controls;

namespace IGSurfaceChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for RotationUsingMouse.xaml
    /// </summary>
    public partial class RotationUsingMouse : SampleContainer
    {
        public RotationUsingMouse()
        {
            InitializeComponent();
        }

        private void RotationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //CheckBox checkbox = sender as CheckBox;

            //if (this.Chb_RotationX == null || this.Chb_RotationY == null || this.Chb_RotationZ == null || this.Chb_NoRotation == null) return;

            //this.SurfaceChart.RotationAxes = AxisFlags3D.None;

            //if (this.Chb_RotationX.IsChecked == true)
            //{
            //    if (this.Chb_NoRotation.IsChecked == true && !checkbox.Name.Equals("Chb_NoRotation"))
            //        this.Chb_NoRotation.IsChecked = false;

            //    this.SurfaceChart.RotationAxes |= AxisFlags3D.X;
            //}

            //if (this.Chb_RotationY.IsChecked == true)
            //{
            //    if (this.Chb_NoRotation.IsChecked == true && !checkbox.Name.Equals("Chb_NoRotation"))
            //        this.Chb_NoRotation.IsChecked = false;

            //    this.SurfaceChart.RotationAxes |= AxisFlags3D.Y;
            //}

            //if (this.Chb_RotationZ.IsChecked == true)
            //{
            //    if (this.Chb_NoRotation.IsChecked == true && !checkbox.Name.Equals("Chb_NoRotation"))
            //        this.Chb_NoRotation.IsChecked = false;

            //    this.SurfaceChart.RotationAxes |= AxisFlags3D.Z;
            //}

            //if (this.Chb_NoRotation.IsChecked == true && checkbox.Name.Equals("Chb_NoRotation"))
            //{
            //    this.Chb_RotationX.IsChecked = Chb_RotationY.IsChecked = Chb_RotationZ.IsChecked = false;
            //    this.SurfaceChart.RotationAxes = AxisFlags3D.None;
            //    this.Chb_NoRotation.IsChecked = true;
            //}
        }
    }
}
