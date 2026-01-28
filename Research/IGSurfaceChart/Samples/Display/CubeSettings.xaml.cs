using Infragistics.Samples.Framework;

namespace IGSurfaceChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for CubeSettings.xaml
    /// </summary>
    public partial class CubeSettings : SampleContainer
    {
        public CubeSettings()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
}
