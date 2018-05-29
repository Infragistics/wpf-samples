using IGExtensions.Common.Assets.Resources;

namespace IGExtensions.Common.Maps.Assets.Resources
{
    /// <summary>
    /// Represents an assets localizer that provides access to the <see cref="MapStrings"/> as well as to the <see cref="CommonStrings"/> 
    /// </summary>
    public class MapAssetsLocalizer : CommonLocalizer
    {
        private static readonly MapStrings StringAssets = new MapStrings();

        public MapStrings MapStrings
        {
            get { return StringAssets; }
        }

    }
}