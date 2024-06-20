using Infragistics.Samples.Shared.Resources;

namespace IGDataChart.Resources
{
    /// <summary>
    /// Represents an assets localizer that provides access to the <see cref="DataChartStrings"/> as well as to the <see cref="CommonStrings"/> 
    /// </summary>
    public class AssetsLocalizer : CommonLocalizer
    {
        private static readonly DataChartStrings StringAssets = new DataChartStrings();

        public DataChartStrings ChartStrings
        {
            get { return StringAssets; }
        }

    }
}