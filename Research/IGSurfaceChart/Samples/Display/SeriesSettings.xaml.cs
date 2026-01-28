using Infragistics.Samples.Framework;

namespace IGSurfaceChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for SeriesSettings.xaml
    /// </summary>
    public partial class SeriesSettings : SampleContainer
    {
        public SeriesSettings()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
}
