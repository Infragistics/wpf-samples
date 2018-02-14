using IgOutlook.Infrastructure.Dialogs;
using Prism.Regions;

namespace IgOutlook.Infrastructure.Prism
{
    public static class NavigationExtensions
    {
        /// <summary>
        /// Navigates the request.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="target">The target.</param>
        /// <param name="overrideNavigationSourceUri">The override navigation source URI.</param>
        /// <param name="enableOpenInNewWindow">The enable open in new window.</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, string source, bool openInWindow = false)
        {
            if (openInWindow)
            {
                NavigateToNewWindow(regionManager, regionName, source);
            }
            else
            {
                regionManager.Regions[regionName].RequestNavigate(source);
            }
        }

        static void NavigateToNewWindow(IRegionManager regionManager, string regionName, string source)
        {
            RibbonDialog dialog = new RibbonDialog();
            var newRegionManager = regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(dialog, newRegionManager);
            newRegionManager.RequestNavigate(RegionNames.ContentRegion, source);

            dialog.Owner = System.Windows.Application.Current.MainWindow;
            dialog.ShowInTaskbar = true;
            dialog.ShowActivated = true;            
            dialog.Show();
        }

    }
}
