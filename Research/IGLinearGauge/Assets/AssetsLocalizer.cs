using Infragistics.Samples.Shared.Resources;
using IGLinearGauge.Assets.Resources;

namespace IGLinearGauge.Assets
{
    /// <summary>
    /// Represents an assets localizer that provides access to the <see cref="AppStrings"/> 
    /// as well as to the <see cref="CommonStrings"/> 
    /// </summary>
    public class AssetsLocalizer : CommonLocalizer
    {
        private static readonly AppStrings StringAssets = new AppStrings();

        public AppStrings AppStrings
        {
            get { return StringAssets; }
        }

    }
}