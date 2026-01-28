using Infragistics.Samples.Framework;

namespace IGSurfaceChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for FloorSettings.xaml
    /// </summary>
    public partial class FloorSettings : SampleContainer
    {
        public FloorSettings()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
}
