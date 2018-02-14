using IGExtensions.Common.Assets.Resources;
using IGShowcase.GeographicMapBrowser.Assets.Resources;

namespace IGShowcase.GeographicMapBrowser
{
    /// <summary>
    /// Represents common assets localizer that provides access to the <see cref="CommonStrings"/> 
    /// </summary>
    public class AssetsLocalizer : CommonLocalizer
    {
        private static readonly AppStrings AppStringsAssets = new AppStrings();
        private static readonly PropertyStrings PropertyStringsAssets = new PropertyStrings();

        public AssetsLocalizer()
        {
          
        }
        /// <summary>
        /// Gets common strings resource 
        /// </summary>
        public AppStrings AppStrings
		{
            get { return AppStringsAssets; }
		}
        /// <summary>
        /// Gets property strings resource 
        /// </summary>
        public PropertyStrings PropertyStrings
        {
            get { return PropertyStringsAssets; }
        }
         
    }
}
