using IGExtensions.Common.Assets.Resources; 

namespace IGShowcase.EarthQuake.Resources
{
    /// <summary>
    /// Represents an assets localizer that provides access to the application <see cref="AppStrings"/> 
    /// as well as to the <see cref="CommonStrings"/> 
    /// </summary>
    public class AppLocalizer : CommonLocalizer
	{
        
		public AppLocalizer()
		{
		  
		}

        private static readonly AppStrings AppStringsAsset = new AppStrings();
        public AppStrings AppStrings
		{
            get { return AppStringsAsset; }
		}

	}
}
