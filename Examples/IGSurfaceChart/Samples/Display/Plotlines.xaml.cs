using Infragistics.Samples.Framework;

namespace IGSurfaceChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for Plotlines.xaml
    /// </summary>
    public partial class Plotlines : SampleContainer
    {
        public Plotlines()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
}
