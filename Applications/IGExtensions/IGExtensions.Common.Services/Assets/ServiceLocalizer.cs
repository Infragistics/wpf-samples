using IGExtensions.Common.Assets;
using IGExtensions.Common.Assets.Resources;
using IGExtensions.Common.Services.Assets.Resources;

namespace IGExtensions.Common.Services.Assets
{
    /// <summary>
    /// Represents service assets localizer that provides access to the <see cref="ServiceStrings"/> as well as to the <see cref="CommonStrings"/> 
    /// </summary>
    public class ServiceLocalizer : CommonLocalizer // provides access to CommonStrings
	{
        private static readonly ServiceStrings ServiceAssetStrings = new ServiceStrings();
        /// <summary>
        /// </summary>
        public ServiceLocalizer()
		{
		  
		}
        /// <summary>
        /// Gets service strings resource 
        /// </summary>
        public ServiceStrings ServiceStrings
		{
            get { return ServiceAssetStrings; }
		}

	}
}
