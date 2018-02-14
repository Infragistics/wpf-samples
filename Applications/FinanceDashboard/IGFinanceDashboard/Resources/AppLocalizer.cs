using IGExtensions.Common.Assets.Resources;

namespace IGShowcase.FinanceDashboard.Resources
{
    /// <summary>
    /// Represents an assets localizer that provides access to the application <see cref="AppStrings"/> 
    /// as well as to the <see cref="CommonStrings"/> 
    /// </summary>
    public class AppLocalizer : CommonLocalizer // provides access to CommonStrings
    {
        public AppLocalizer()
        {

        }

        private static readonly AppStrings AppStringsAssets = new AppStrings();

        public AppStrings AppStrings
        {
            get { return AppStringsAssets; }
        }

      
    }
}

namespace IGShowcase.FinanceDashboard.Assets
{
}