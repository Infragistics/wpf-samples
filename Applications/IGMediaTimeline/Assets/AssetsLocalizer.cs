using MediaTimeline.Assets.Resources;
using IGExtensions.Common.Assets;
using IGExtensions.Common.Assets.Resources;

namespace MediaTimeline.Assets
{
    /// <summary>
    /// Represents an assets localizer that provides access to the application <see cref="Strings"/> 
    /// as well as to the <see cref="CommonStrings"/> 
    /// </summary>
    public class AssetsLocalizer : CommonLocalizer // provides access to CommonStrings
    {
        private static readonly AppStrings StringAssets = new AppStrings();
        private static readonly Images ImageAssets = new Images();

        //public AssetsLocalizer()
        //{
        //}

        public AppStrings AppStrings
		{
            get { return StringAssets; }
		}
        public Images Images
        {
            get { return ImageAssets; }
        }
      
    }
}