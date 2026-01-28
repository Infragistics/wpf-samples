using Infragistics.Samples.Framework;

namespace IGSurfaceChart.Samples.Navigation
{
    /// <summary>
    /// Interaction logic for ChartRotation.xaml
    /// </summary>
    public partial class ChartRotation : SampleContainer
    {
        public ChartRotation()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
}
