using Infragistics.Samples.Framework;

namespace IGSurfaceChart.Samples.Navigation
{
    /// <summary>
    /// Interaction logic for AspectPerspective.xaml
    /// </summary>
    public partial class AspectPerspective : SampleContainer
    {
        public AspectPerspective()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
}
