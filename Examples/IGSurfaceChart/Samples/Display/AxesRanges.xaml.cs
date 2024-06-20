using Infragistics.Samples.Framework;

namespace IGSurfaceChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for AxesRanges.xaml
    /// </summary>
    public partial class AxesRanges : SampleContainer
    {
        public AxesRanges()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
}
