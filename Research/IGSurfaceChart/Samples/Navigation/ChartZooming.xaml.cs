using Infragistics.Samples.Framework;

namespace IGSurfaceChart.Samples.Navigation
{
    /// <summary>
    /// Interaction logic for ChartZooming.xaml
    /// </summary>
    public partial class ChartZooming : SampleContainer
    {
        public ChartZooming()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
}
