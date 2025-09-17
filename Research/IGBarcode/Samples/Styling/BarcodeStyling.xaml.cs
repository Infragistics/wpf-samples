using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IGBarcode.Samples.Styling
{
    public partial class BarcodeStyling : Infragistics.Samples.Framework.SampleContainer
    {
        private Path oceanScene; //The Path which represents the ocean scene
        private BezierSegment bezierSegment; //The wave

        private double height; //The distance of the wave from the top
        private double degree; //The degree for the sin con functions


        public BarcodeStyling()
        {
            InitializeComponent();
            this.SampleLoaded += new System.EventHandler(BarcodeStyling_SampleLoaded);
        }

        void BarcodeStyling_SampleLoaded(object sender, System.EventArgs e)
        {
            //Initialize the wave animation
            InitializeWavesAnimation();
        }

        private void InitializeWavesAnimation()
        {
            Grid grid = VisualTreeHelper.GetChild(xamCode39Barcode, 0) as Grid;
            //Get the LayoutRoot grid from the barcode's template
            oceanScene = grid.FindName("OceanScene") as Path; //Get the path which represents the ocean scene
            bezierSegment = grid.FindName("BezierSegment") as BezierSegment; //Get the bezier curve which is the wave

            height = oceanScene.Height - 15;
            degree = 0;

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, System.EventArgs e)
        {
            //Move the anchor points of the bezier segment.
            bezierSegment.Point1 = new Point(oceanScene.Width / 4, height + 20 * System.Math.Cos(degree * System.Math.PI / 180));
            bezierSegment.Point2 = new Point(3 * oceanScene.Width / 4, height + 20 * System.Math.Sin(degree * System.Math.PI / 180));

            degree = degree >= 360 ? 2 : degree + 2;
        }
    }
}