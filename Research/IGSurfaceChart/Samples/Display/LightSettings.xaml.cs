using Infragistics.Samples.Framework;

namespace IGSurfaceChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for LightSettings.xaml
    /// </summary>
    public partial class LightSettings : SampleContainer
    {
        public LightSettings()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
}
