using IGExtensions.Common.Assets.Resources;
using IGShowcase.HospitalFloorPlan.Assets.Resources;

namespace IGShowcase.HospitalFloorPlan.Assets
{
    /// <summary>
    /// Represents an assets localizer that provides access to the application <see cref="Strings"/> 
    /// as well as to the <see cref="CommonStrings"/> 
    /// </summary>
    public class AppLocalizer : CommonLocalizer
	{
        private static readonly Strings AssetStrings = new Strings();

		public AppLocalizer()
		{
		  
		}

		public Strings Strings
		{
            get { return AssetStrings; }
		}

	}
}
