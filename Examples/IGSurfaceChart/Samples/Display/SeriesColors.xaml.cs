using Infragistics.Samples.Framework;

namespace IGSurfaceChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for SeriesColors.xaml
    /// </summary>
    public partial class SeriesColors : SampleContainer
    {
        public SeriesColors()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
}
