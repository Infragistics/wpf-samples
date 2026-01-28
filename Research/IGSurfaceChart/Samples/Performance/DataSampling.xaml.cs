using Infragistics.Samples.Framework;

namespace IGSurfaceChart.Samples.Performance
{
    /// <summary>
    /// Interaction logic for DataSampling.xaml
    /// </summary>
    public partial class DataSampling : SampleContainer
    {
        public DataSampling()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
}
