using Infragistics.Samples.Framework;
using System.Windows;

namespace IGSurfaceChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for AxesTitles.xaml
    /// </summary>
    public partial class AxesTitles : SampleContainer
    {
        public AxesTitles()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }

        private void Templating_Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            var scaleX = this.SurfaceChart.XAxis;
            var scaleY = this.SurfaceChart.YAxis;
            var scaleZ = this.SurfaceChart.ZAxis;

            scaleX.TitleTemplate = this.Resources["TitleDataTemplate"] as DataTemplate;
            scaleY.TitleTemplate = this.Resources["TitleDataTemplate"] as DataTemplate;
            scaleZ.TitleTemplate = this.Resources["TitleDataTemplate"] as DataTemplate;
        }

        private void Templating_Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            var scaleX = this.SurfaceChart.XAxis;
            var scaleY = this.SurfaceChart.YAxis;
            var scaleZ = this.SurfaceChart.ZAxis;

            scaleX.TitleTemplate = null;
            scaleY.TitleTemplate = null;
            scaleZ.TitleTemplate = null;
        }
    }
}
