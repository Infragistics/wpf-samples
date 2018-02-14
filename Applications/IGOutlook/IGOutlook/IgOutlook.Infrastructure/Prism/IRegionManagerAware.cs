using Prism.Regions;

namespace IgOutlook.Infrastructure.Prism
{
    public interface IRegionManagerAware
    {
        IRegionManager RegionManager { get; set; } 
    }
}
