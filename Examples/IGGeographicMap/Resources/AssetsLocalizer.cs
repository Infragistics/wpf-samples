using Infragistics.Samples.Shared.Resources;

namespace IGGeographicMap.Resources
{
    /// <summary>
    /// Represents an assets localizer that provides access to the <see cref="MapStrings"/> as well as to the <see cref="CommonStrings"/> 
    /// </summary>
    public class AssetsLocalizer : CommonLocalizer
    {
        private static readonly MapStrings StringAssets = new MapStrings();

        public MapStrings MapStrings
        {
            get { return StringAssets; }
        }

    }
}