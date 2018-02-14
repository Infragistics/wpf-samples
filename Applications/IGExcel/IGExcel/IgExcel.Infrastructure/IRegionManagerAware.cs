using Prism.Regions;

namespace IgExcel.Infrastructure
{
    public interface IRegionManagerAware
    {
        IRegionManager RegionManager { get; set; }
    }
}
