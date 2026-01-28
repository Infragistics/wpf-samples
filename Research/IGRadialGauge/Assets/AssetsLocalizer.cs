using Infragistics.Samples.Shared.Resources;
using IGRadialGauge.Assets.Resources;

namespace IGRadialGauge.Assets
{
    /// <summary>
    /// Represents an assets localizer that provides access to the <see cref="GaugeStrings"/> 
    /// as well as to the <see cref="CommonStrings"/> 
    /// </summary>
    public class AssetsLocalizer : CommonLocalizer
    {
        private static readonly GaugeStrings StringAssets = new GaugeStrings();

        public GaugeStrings GaugeStrings
        {
            get { return StringAssets; }
        }

    }
}