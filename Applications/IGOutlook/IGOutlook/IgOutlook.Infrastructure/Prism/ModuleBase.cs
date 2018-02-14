using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace IgOutlook.Infrastructure.Prism
{
    public abstract class ModuleBase : IModule
    {
        protected IUnityContainer Container { get; private set; }
        protected IRegionManager RegionManager { get; private set; }

        public ModuleBase(IUnityContainer container, IRegionManager regionManager)
        {
            Container = container;
            RegionManager = regionManager;            
        }

        public void Initialize()
        {
            RegisterTypes();
            ResolveOutlookGroup();
        }

        protected abstract void RegisterTypes();
        protected abstract void ResolveOutlookGroup();
    }
}
