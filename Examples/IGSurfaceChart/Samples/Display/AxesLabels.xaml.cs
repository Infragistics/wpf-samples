using Infragistics.Samples.Framework;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace IGSurfaceChart.Samples.Display
{ 
    public partial class AxesLabels : SampleContainer
    {
        public AxesLabels()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }

        private void Templating_Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            var scaleX = this.SurfaceChart.XAxis;
            var scaleY = this.SurfaceChart.YAxis;
            var scaleZ = this.SurfaceChart.ZAxis;

            scaleX.LabelTemplate = this.Resources["LabelDataTemplate"] as DataTemplate;        
            scaleY.LabelTemplate = this.Resources["LabelDataTemplate"] as DataTemplate;
            scaleZ.LabelTemplate = this.Resources["LabelDataTemplate"] as DataTemplate;
        }

        private void Templating_Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            var scaleX = this.SurfaceChart.XAxis;
            var scaleY = this.SurfaceChart.YAxis;
            var scaleZ = this.SurfaceChart.ZAxis;

            scaleX.LabelTemplate = null;
            scaleY.LabelTemplate = null;
            scaleZ.LabelTemplate = null;
        }
    }
}
