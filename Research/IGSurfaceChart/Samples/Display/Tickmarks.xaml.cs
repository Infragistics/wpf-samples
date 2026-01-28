using Infragistics.Samples.Framework;

namespace IGSurfaceChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for Tickmarks.xaml
    /// </summary>
    public partial class Tickmarks : SampleContainer
    {
        public Tickmarks()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
}
