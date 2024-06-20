using Infragistics.Samples.Shared.Resources;

namespace IGShapeChart.Resources
{
    /// <summary>
    /// Represents an assets localizer that provides access to the <see cref="ShapeChartStrings"/> as well as to the <see cref="CommonStrings"/> 
    /// </summary>
    public class AssetsLocalizer : CommonLocalizer
    {
        private static readonly ShapeChartStrings StringAssets = new ShapeChartStrings();

        public ShapeChartStrings MapStrings
        {
            get { return StringAssets; }
        }

    }
}